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
        public string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbHWNum.ItemsSource = lstHWNumber;
            cbCtrlMode.ItemsSource = Enum.GetValues(typeof(EType.ControlMode));

            if (mode == ADD_MODE)
            {
                Repository.newDevice = new DTO_Device();
                Repository.newDevice.FAMode = new FAMode();

                cbCtrlMode.SelectedIndex = 0;
                chkUseFAMode.IsChecked = true;
                cbHWNum.SelectedIndex = 0;
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
                tbMAC.Text = Repository.selectedDevice.MAC;

                chkUseFAMode.IsChecked = Repository.selectedDevice.FAMode.IsUse;
                cbHWNum.SelectedValue = Repository.selectedDevice.FAMode.FAHWNumber;
                cbCtrlMode.SelectedItem = Repository.selectedDevice.CtrMode;

                lbReaders.ItemsSource = Repository.selectedDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.READER);
                lbInputs.ItemsSource = Repository.selectedDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.INPUT);
                lbOutputs.ItemsSource = Repository.selectedDevice.Sockets.Where(r => r.Type == EType.DeviceSocketType.OUTPUT);
            }
        }

        private void tbDeviceId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.Id = tbDeviceId.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.Id = tbDeviceId.Text;
            }
        }

        private void tbDeviceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.Name = tbDeviceName.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.Name = tbDeviceName.Text;
            }

        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
        }

        private void tbIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.IP = tbIp.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.IP = tbIp.Text;
            }
        }

        private void tbSubnet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.Subnet = tbSubnet.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.Subnet = tbSubnet.Text;
            }
        }

        private void tbGateway_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.GateWay = tbGateway.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.GateWay = tbGateway.Text;
            }
        }

        private void tbHostIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.HostIp = tbHostIp.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.HostIp = tbHostIp.Text;
            }
        }

        private void chkUseFAMode_Checked(object sender, RoutedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.FAMode.IsUse = (bool)chkUseFAMode.IsChecked;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.FAMode.IsUse = (bool)chkUseFAMode.IsChecked;
            }

        }

        private void cbHWNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbHWNum.SelectedValue != null)
            {
                int hwNum = (int)cbHWNum.SelectedValue;

                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedDevice != null)
                        Repository.selectedDevice.FAMode.FAHWNumber = hwNum;
                }
                else
                {
                    if (Repository.newDevice != null)
                        Repository.newDevice.FAMode.FAHWNumber = hwNum;
                }
            }
            
        }

        private void cbCtrlMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EType.ControlMode ctrlMode = (EType.ControlMode)cbCtrlMode.SelectedItem;

            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.CtrMode = ctrlMode;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.CtrMode = ctrlMode;
            }
        }

        private void tbMAC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDevice != null)
                    Repository.selectedDevice.MAC = tbMAC.Text;
            }
            else
            {
                if (Repository.newDevice != null)
                    Repository.newDevice.MAC = tbMAC.Text;
            }
        }
    }
}
