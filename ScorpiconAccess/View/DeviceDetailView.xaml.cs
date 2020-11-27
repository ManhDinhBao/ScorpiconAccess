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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for DeviceDetailView.xaml
    /// </summary>
    public partial class DeviceDetailView : UserControl
    {
        #region Variables
        BUS_Device bus_device = new BUS_Device();

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        private List<int> lstHWNumber = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        #endregion

        #region Control events
        public DeviceDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        #endregion

        #region Extra Methods
        
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbHWNum.ItemsSource = lstHWNumber;
            cbCtrlMode.ItemsSource = Enum.GetValues(typeof(EType.ControlMode));

            if (mode == ADD_MODE)
            {
                Repository.newDevice = new DTO_Device();

            }
            else
            {
                //Basic information
                tbDeviceId.Text = Repository.selectedDevice.Id;
                tbDeviceName.Text = Repository.selectedDevice.Name;
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedDevice.Description)));

                //Connection
                tbIp.Text = Repository.selectedDevice.IP;
                tbSubnet.Text = Repository.selectedDevice.Subnet;
                tbGateway.Text = Repository.selectedDevice.GateWay;
                tbHostIp.Text = Repository.selectedDevice.HostIp;

                chkUseFAMode.IsChecked = Repository.selectedDevice.FAMode.IsUse;
                cbHWNum.SelectedValue = Repository.selectedDevice.FAMode.FAHWNumber;
                cbCtrlMode.SelectedItem = Repository.selectedDevice.CtrMode;

            }
        }
    }
}
