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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ucDeviceView.xaml
    /// </summary>
    public partial class ucDeviceView : UserControl
    {
        #region Constructors
        public DTO_Device MyDevice { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIp { get; set; }
        public ucDeviceView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Events
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteClick;
        #endregion

        #region Variables
        public BackgroundWorker myDeviceWorker;
        public SQLResult result;
        public BUS_Device bUS_Device;
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bUS_Device = new BUS_Device();

            myDeviceWorker = new BackgroundWorker();
            myDeviceWorker.WorkerSupportsCancellation = true;
            myDeviceWorker.DoWork += MyDeviceWorker_DoWork;
            myDeviceWorker.RunWorkerCompleted += MyDeviceWorker_RunWorkerCompleted;
        }

        private void MyDeviceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_Device oldDevice = Repository.lstAllDevices.FirstOrDefault(c => c.Id == MyDevice.Id);
                if (oldDevice != null)
                {
                    Repository.lstAllDevices.Remove(oldDevice);
                }

                this.DeleteClick(this, e);

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MyDeviceWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Device.DeleteDevice(MyDevice.Id);
        }

        private void btDeviceInfo_Click(object sender, RoutedEventArgs e)
        {
            wdDeviceDetail view = new wdDeviceDetail(EType.WindowMode.EDIT_MODE, MyDevice);
            if (view.ShowDialog() == true)
            {
                MyDevice = Repository.lstAllDevices.FirstOrDefault(c => c.Id == MyDevice.Id);
                DeviceName = MyDevice.Name;
                DeviceIp = MyDevice.IP;


                BindingExpression binding = tbDeviceName.GetBindingExpression(TextBlock.TextProperty);
                binding.UpdateTarget();

                BindingExpression binding1 = tbDeviceIP.GetBindingExpression(TextBlock.TextProperty);
                binding1.UpdateTarget();

            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you want to delete this device?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (myDeviceWorker.IsBusy)
                {
                    myDeviceWorker.CancelAsync();
                }
                else
                {
                    myDeviceWorker.RunWorkerAsync();
                }
            }
        }
    }
}
