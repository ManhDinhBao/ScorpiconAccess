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
    /// Interaction logic for wdDeviceDetail.xaml
    /// </summary>
    public partial class wdDeviceDetail : Window
    {
        #region Variables
        public BUS_Device bUS_Device;
        public DTO_Device myDevice, newDevice;
        public EType.WindowMode mode;
        public BackgroundWorker myDeviceWorker, getDeviceWorker, deleteDeviceWorker;
        public SQLResult result;
        int hwNum;
        private List<int> lstHWNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public int funcClick { get; set; }
        #endregion

        #region Control events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();
                    myDevice = new DTO_Device();
                    break;
                default:
                    BindData();
                    break;
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 1;

            btSave.IsEnabled = false;

            try
            {
                myDevice.Name = tbDeviceName.Text;
                myDevice.MAC = tbDeviceMAC.Text;
                myDevice.IP = tbDeviceIP.Text;
                myDevice.Subnet = tbDeviceSubnet.Text;
                myDevice.GateWay = tbDeviceGateway.Text;
                myDevice.HostIp = tbDeviceHost.Text;
                myDevice.Description = General.StringFromRichTextBox(rtbDescription).Trim();

                if (cbHWNumber.SelectedItem != null)
                {
                    hwNum = (int)cbHWNumber.SelectedValue;
                }

                if (cbHWNumber.SelectedItem != null)
                {
                    EType.ControlMode ctrlMode = (EType.ControlMode)cbMode.SelectedItem;

                    myDevice.CtrMode = ctrlMode;
                }

                if (rbNormal.IsChecked == true)
                {
                    myDevice.FAMode = new FAMode(false, 0);
                }

                if (rbFAMode.IsChecked == true)
                {
                    myDevice.FAMode = new FAMode(true, hwNum);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (myDeviceWorker.IsBusy)
            {
                myDeviceWorker.CancelAsync();
            }
            else
            {
                myDeviceWorker.RunWorkerAsync();
            }
        }
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 2;
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (deleteDeviceWorker.IsBusy)
                {
                    deleteDeviceWorker.CancelAsync();
                }
                else
                {
                    deleteDeviceWorker.RunWorkerAsync();
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

        #region Contructor
        public wdDeviceDetail(EType.WindowMode mode, DTO_Device device)
        {
            InitializeComponent();
            this.myDevice = device;
            this.mode = mode;
        }
        #endregion

        #region Background worker
        private void GetDeviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newDevice == null || newDevice.Id == "")
            {

            }

            Repository.lstAllDevices.Add(newDevice);
            this.DialogResult = true;
            this.Hide();
        }
        private void GetDeviceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newDevice = bUS_Device.GetDeviceByKey(result.ExtraData);
        }
        private void MyDeviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getDeviceWorker.IsBusy)
                    {
                        getDeviceWorker.CancelAsync();
                    }
                    else
                    {
                        getDeviceWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_Device oldDevice = Repository.lstAllDevices.FirstOrDefault(c => c.Id == myDevice.Id);

                    if (oldDevice != null)
                    {
                        oldDevice = myDevice;
                    }

                    this.DialogResult = true;
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void MyDeviceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Device.AddNewDevice(myDevice);
            }
            else
            {
                result = bUS_Device.UpdateDevice(myDevice);
            }
        }
        private void DeleteDeviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_Device oldDevice = Repository.lstAllDevices.FirstOrDefault(c => c.Id == myDevice.Id);
                if (oldDevice != null)
                {
                    Repository.lstAllDevices.Remove(oldDevice);
                }

                this.DialogResult = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DeleteDeviceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Device.DeleteDevice(myDevice.Id);
        }
        #endregion

        #region Custom events
        private void InitData()
        {
            bUS_Device = new BUS_Device();

            myDeviceWorker = new BackgroundWorker();
            myDeviceWorker.WorkerReportsProgress = true;
            myDeviceWorker.WorkerSupportsCancellation = true;
            myDeviceWorker.DoWork += MyDeviceWorker_DoWork;
            myDeviceWorker.RunWorkerCompleted += MyDeviceWorker_RunWorkerCompleted;

            getDeviceWorker = new BackgroundWorker();
            getDeviceWorker.WorkerReportsProgress = true;
            getDeviceWorker.WorkerSupportsCancellation = true;
            getDeviceWorker.DoWork += GetDeviceWorker_DoWork;
            getDeviceWorker.RunWorkerCompleted += GetDeviceWorker_RunWorkerCompleted;

            deleteDeviceWorker = new BackgroundWorker();
            deleteDeviceWorker.WorkerReportsProgress = true;
            deleteDeviceWorker.WorkerSupportsCancellation = true;
            deleteDeviceWorker.DoWork += DeleteDeviceWorker_DoWork;
            deleteDeviceWorker.RunWorkerCompleted += DeleteDeviceWorker_RunWorkerCompleted;

            cbHWNumber.ItemsSource = lstHWNumber;
            cbMode.ItemsSource = Enum.GetValues(typeof(EType.ControlMode));
        }
        private void ResetControl()
        {
            tbDeviceName.Text = "";
            tbDeviceMAC.Text = "";
            tbDeviceIP.Text = "";
            tbDeviceSubnet.Text = "";
            tbDeviceGateway.Text = "";
            tbDeviceHost.Text = "";
        }
        private void BindData()
        {
            if (myDevice != null)
            {
                //Basic information
                tbDeviceId.Text = myDevice.Id;
                tbDeviceName.Text = myDevice.Name;
                rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(myDevice.Description)));

                //Connection
                tbDeviceIP.Text         = myDevice.IP;
                tbDeviceSubnet.Text     = myDevice.Subnet;
                tbDeviceGateway.Text    = myDevice.GateWay;
                tbDeviceHost.Text       = myDevice.HostIp;
                tbDeviceMAC.Text        = myDevice.MAC;

                cbHWNumber.SelectedValue = myDevice.FAMode.FAHWNumber;
                cbMode.SelectedItem      = myDevice.CtrMode;

                if (myDevice.FAMode.IsUse)
                {
                    rbFAMode.IsChecked = true;
                }
                else
                {
                    rbNormal.IsChecked = true;
                }

                lbListReader.ItemsSource = myDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.READER);
                lbListInput.ItemsSource  = myDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.INPUT);
                lbListOutput.ItemsSource = myDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.OUTPUT);
            }
        }
        #endregion
    }
}
