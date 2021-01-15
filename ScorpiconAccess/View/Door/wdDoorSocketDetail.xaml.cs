using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for wdDoorSocketDetail.xaml
    /// </summary>
    public partial class wdDoorSocketDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_DoorSocket bUS_DoorSocket;
        public DTO_Door door;
        public SQLResult result;
        public BackgroundWorker myPeriodWorker, getPeriodWorker, delPeriodWorker;
        public List<DTO_ScorpionAccess.ListViewItem> listViewItems;
        public List<DTO_DoorSocket> listNewSocket;
        #endregion

        public wdDoorSocketDetail(DTO_Door door)
        {
            InitializeComponent();
            this.door = door;
        }

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void itemAdd_Click(object sender, RoutedEventArgs e)
        {
            DoorSocketDetailView view = new DoorSocketDetailView(EType.WindowMode.ADD_MODE, null, door.Id);
            if (view.ShowDialog() == true)
            {
                BindSocketToListItemView();
            }
        }
        private void itemEdit_Click(object sender, RoutedEventArgs e)
        {
            DTO_ScorpionAccess.ListViewItem item = (DTO_ScorpionAccess.ListViewItem)lbListSockets.SelectedItem;
            if (item == null) return;
            DTO_DoorSocket socket = door.Sockets.FirstOrDefault(s => s.Id == item.Key);

            DoorSocketDetailView view = new DoorSocketDetailView(EType.WindowMode.EDIT_MODE, socket, door.Id);
            if (view.ShowDialog() == true)
            {
                BindSocketToListItemView();
            }
        }
        private void itemDelete_Click(object sender, RoutedEventArgs e)
        {
            DTO_ScorpionAccess.ListViewItem item = (DTO_ScorpionAccess.ListViewItem)lbListSockets.SelectedItem;
            if (item == null) return;

            MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa terminal này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                DTO_DoorSocket socket = door.Sockets.FirstOrDefault(s => s.Id == item.Key);
                SQLResult sQLResult = bUS_DoorSocket.DeleteSocket(socket.Id, socket.ConnectedDeviceSocketId);
                if (sQLResult.Result)
                {
                    DTO_Door selectedDoor = Repository.lstAllDoor.FirstOrDefault(d => d.Id == door.Id);
                    if (selectedDoor != null)
                    {
                        selectedDoor.Sockets.Remove(socket);
                    }
                    BindSocketToListItemView();
                }
                else
                {
                    MessageBox.Show(sQLResult.Detail, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

        private void lbListSockets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTO_ScorpionAccess.ListViewItem selectedItem = (DTO_ScorpionAccess.ListViewItem)lbListSockets.SelectedItem;
            if (selectedItem != null)
            {
                DTO_DoorSocket socket = door.Sockets.FirstOrDefault(s => s.Id == selectedItem.Key);
                if (socket != null)
                {
                    cbDevice.SelectedValue = socket.ConnectedDeviceId;
                    cbDeviceSocket.SelectedValue = socket.ConnectedDeviceSocketId;

                    cbSocketType.SelectedValue = socket.Type;
                }
            }
        }
        private void cbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DTO_Device selectedDevice = (DTO_Device)cbDevice.SelectedItem;
            if (selectedDevice != null)
            {
                cbDeviceSocket.ItemsSource = selectedDevice.Sockets;
                cbDeviceSocket.DisplayMemberPath = "Name";
                cbDeviceSocket.SelectedValuePath = "Id";
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (door.Sockets != null)
            {
                BindSocketToListItemView();
            }

            cbDevice.ItemsSource = Repository.lstAllDevices;
            cbDevice.DisplayMemberPath = "Name";
            cbDevice.SelectedValuePath = "Id";

            cbSocketType.ItemsSource = Enum.GetValues(typeof(EType.DoorSocketType));

            bUS_DoorSocket = new BUS_DoorSocket();
            listNewSocket = new List<DTO_DoorSocket>();
        }

        #region Custom methods
        private void BindSocketToListItemView()
        {
            door = Repository.lstAllDoor.FirstOrDefault(d => d.Id == door.Id);
            listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
            foreach (DTO_DoorSocket socket in door.Sockets)
            {
                DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                switch (socket.Type)
                {
                    case EType.DoorSocketType.CONTACT:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/c_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/c_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.LOCK:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/l_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/l_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.READER:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/e_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/e_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.REX:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/r_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/r_character_green_50px.png";
                        }
                        break;
                }

                listViewItem.TextBinding = socket.Name;
                listViewItem.Key = socket.Id;

                listViewItems.Add(listViewItem);
            }

            lbListSockets.ItemsSource = listViewItems;
        }
        #endregion
    }
}
