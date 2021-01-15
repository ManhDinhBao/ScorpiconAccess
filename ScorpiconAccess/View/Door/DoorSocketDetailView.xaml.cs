using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for DoorSocketDetailView.xaml
    /// </summary>
    public partial class DoorSocketDetailView : Window
    {
        #region Variables
        private EType.WindowMode mode;
        private DTO_DoorSocket socket;
        private string doorId;

        BUS_DoorSocket bUS_DoorSocket;
        #endregion

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            socket.Name = tbSocketName.Text;
            if(cbConnectSocket.SelectedItem!=null)
            socket.ConnectedDeviceSocketId = cbConnectSocket.SelectedValue.ToString();

            switch (socket.Type)
            {
                case EType.DoorSocketType.CONTACT:
                    DTO_Contact contact = new DTO_Contact();
                    contact.Type = (EType.ContactType)cbSocketDetailType.SelectedItem;
                    contact.Mode = (EType.ContactMode)cbSocketDetailMode.SelectedItem;

                    socket.Property = contact;
                    break;
                case EType.DoorSocketType.LOCK:
                    DTO_Lock llock = new DTO_Lock();
                    llock.Type = (EType.LockType)cbSocketDetailType.SelectedItem;

                    socket.Property = llock;

                    break;
                case EType.DoorSocketType.READER:
                    DTO_Reader reader = new DTO_Reader();
                    reader.Type = (EType.ReaderType)cbSocketDetailType.SelectedItem;

                    socket.Property = reader;

                    break;
                case EType.DoorSocketType.REX:
                    DTO_Rex rex = new DTO_Rex();
                    rex.Type = (EType.RexType)cbSocketDetailType.SelectedItem;

                    socket.Property = rex;
                    break;
            }

            SQLResult result;
            if (mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_DoorSocket.AddNewSocket(socket);
                if (result.Result)
                {
                    socket = bUS_DoorSocket.GetDoorSocketById(result.ExtraData);

                    DTO_Door selectedDoor = Repository.lstAllDoor.FirstOrDefault(d => d.Id == doorId);
                    if (selectedDoor != null)
                    {
                        selectedDoor.Sockets.Add(socket);
                    }

                    this.DialogResult = true;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(result.Detail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                result = bUS_DoorSocket.UpdateSocket(socket);
                if (result.Result)
                {
                    DTO_Door selectedDoor = Repository.lstAllDoor.FirstOrDefault(d => d.Id == doorId);
                    if (selectedDoor != null)
                    {
                        DTO_DoorSocket oldSocket = (DTO_DoorSocket)selectedDoor.Sockets.FirstOrDefault(s => s.Id == socket.Id);
                        oldSocket = socket;
                    }

                    this.DialogResult = true;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(result.Detail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
        private void btPullOut_Click(object sender, RoutedEventArgs e)
        {
            // socket.ConnectedDeviceSocketId = null;
            socket.Id = null;
            SQLResult result = bUS_DoorSocket.UpdateSocket(socket);

            if (result.Result)
            {
                socket.ConnectedDevice = null;
                socket.ConnectedDeviceId = null;
                socket.ConnectedDeviceSocketId = null;
                socket.ConnectedDeviceSocketOrder = 0;

                foreach (DTO_Device device in Repository.lstAllDevices)
                {
                    DTO_DeviceSocket devSocket = device.Sockets.FirstOrDefault(s => s.DoorSocket == socket.Id);
                    if (devSocket != null)
                    {
                        devSocket.DoorSocket = null;
                    }
                }

                this.DialogResult = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show(result.Detail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        public DoorSocketDetailView(EType.WindowMode mode, DTO_DoorSocket socket, string doorId)
        {
            InitializeComponent();
            this.mode = mode;
            this.socket = socket;
            this.doorId = doorId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }

        private void cbConnectDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(mode == EType.WindowMode.ADD_MODE)
            {
                DTO_Device selectedDevice = (DTO_Device)cbConnectDevice.SelectedItem;

                cbConnectSocket.ItemsSource = selectedDevice.Sockets.Where(s => s.IsUse == false);
                cbConnectSocket.DisplayMemberPath = "Name";
                cbConnectSocket.SelectedValuePath = "Id";
                
                cbConnectSocket.SelectedIndex = 0;
            }
            else
            {
                DTO_Device selectedDevice = (DTO_Device)cbConnectDevice.SelectedItem;

                cbConnectSocket.ItemsSource = selectedDevice.Sockets;
                cbConnectSocket.DisplayMemberPath = "Name";
                cbConnectSocket.SelectedValuePath = "Id";

                if (socket.ConnectedDeviceId == null)
                {
                    cbConnectSocket.SelectedIndex = 0;
                }
                else
                    cbConnectSocket.SelectedValue = socket.ConnectedDeviceSocketId;
            }
           
        }

        private void cbSocketType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EType.DoorSocketType socketType = (EType.DoorSocketType)cbSocketType.SelectedItem;

            if(cbSocketType.SelectedValue!=null) socket.Type = socketType;

            if (mode == EType.WindowMode.ADD_MODE)
            {

                switch (socketType)
                {
                    case EType.DoorSocketType.CONTACT:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.ContactType));
                        cbSocketDetailMode.ItemsSource = Enum.GetValues(typeof(EType.ContactMode));
                        cbSocketDetailMode.IsEnabled = true;

                        cbSocketDetailMode.SelectedIndex = 0;
                        break;
                    case EType.DoorSocketType.LOCK:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.LockType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;

                        break;
                    case EType.DoorSocketType.READER:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.ReaderType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;

                        break;
                    case EType.DoorSocketType.REX:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.RexType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;
                        break;
                }

                cbSocketDetailType.SelectedIndex = 0;
            }
        }
      
        private void InitData()
        {
            bUS_DoorSocket = new BUS_DoorSocket();

            cbSocketType.ItemsSource = Enum.GetValues(typeof(EType.DoorSocketType));
            cbDoorOfSocket.ItemsSource = Repository.lstAllDoor;
            cbDoorOfSocket.DisplayMemberPath = "Name";
            cbDoorOfSocket.SelectedValuePath = "Id";
            cbDoorOfSocket.SelectedValue = doorId;
            cbDoorOfSocket.IsEnabled = false;

            cbConnectDevice.ItemsSource = Repository.lstAllDevices;
            cbConnectDevice.DisplayMemberPath = "Name";
            cbConnectDevice.SelectedValuePath = "Id";

            tbSocketId.IsReadOnly = true;

            if (mode == EType.WindowMode.ADD_MODE)
            {
                socket = new DTO_DoorSocket();
                socket.Door = doorId;

                tbSocketId.Text = "";
                tbSocketName.Text = "";

                cbSocketType.SelectedIndex = 0;
                //cbConnectDevice.SelectedIndex = 0;

                btPullOut.IsEnabled = false;
                btSave.IsEnabled = true;

            }
            else
            {
                tbSocketId.Text = socket.Id;
                tbSocketName.Text = socket.Name;
                cbSocketType.SelectedValue = socket.Type;
                cbSocketType.IsEnabled = false;
                cbConnectDevice.SelectedValue = socket.ConnectedDeviceId;

                if (socket.ConnectedDeviceId == null)
                {
                    btPullOut.IsEnabled = false;
                    btSave.IsEnabled = true;
                }
                else
                {
                    btPullOut.IsEnabled = true;
                    btSave.IsEnabled = true;

                }

                switch (socket.Type)
                {
                    case EType.DoorSocketType.CONTACT:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.ContactType));
                        cbSocketDetailMode.ItemsSource = Enum.GetValues(typeof(EType.ContactMode));
                        cbSocketDetailMode.IsEnabled = true;
                        DTO_Contact contact = (DTO_Contact)socket.Property;
                        cbSocketDetailType.SelectedValue = contact.Type;
                        cbSocketDetailMode.SelectedValue = contact.Mode;
                        break;
                    case EType.DoorSocketType.LOCK:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.LockType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;
                        DTO_Lock llock = (DTO_Lock)socket.Property;
                        cbSocketDetailType.SelectedItem = llock.Type;
                        break;
                    case EType.DoorSocketType.READER:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.ReaderType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;
                        DTO_Reader reader = (DTO_Reader)socket.Property;
                        cbSocketDetailType.SelectedItem = reader.Type;
                        break;
                    case EType.DoorSocketType.REX:
                        cbSocketDetailType.ItemsSource = Enum.GetValues(typeof(EType.RexType));
                        cbSocketDetailMode.ItemsSource = null;
                        cbSocketDetailMode.IsEnabled = false;
                        DTO_Rex rex = (DTO_Rex)socket.Property;
                        cbSocketDetailType.SelectedItem = rex.Type;
                        break;
                }
            }
        }
    }
}
