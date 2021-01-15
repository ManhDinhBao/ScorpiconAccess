using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for wdSetting.xaml
    /// </summary>
    public partial class wdSetting : Window
    {
        XmlDocument doc = new XmlDocument();
        public wdSetting()
        {
            InitializeComponent();
        }

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            doc.SelectSingleNode("/Configuration/Server").Attributes["IP"].Value = tbServer.Text;
            doc.SelectSingleNode("/Configuration/Server").Attributes["Port"].Value = tbPort.Text;
            doc.SelectSingleNode("/Configuration/File").Attributes["CardFileName"].Value = tbCardFileName.Text;
            doc.SelectSingleNode("/Configuration/File").Attributes["DoorFileName"].Value = tbDoorFileName.Text;
            doc.SelectSingleNode("/Configuration/File").Attributes["ScheduleFileName"].Value = tbScheduleFileName.Text;
            doc.SelectSingleNode("/Configuration/File").Attributes["AccessGrFileName"].Value = tbAccessFileName.Text;
            doc.SelectSingleNode("/Configuration/File").Attributes["Location"].Value = tbFileExportLocation.Text;


            doc.Save("Config.xml");

            this.Hide();
        }

        private void btChooseLocation_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                tbFileExportLocation.Text = folderPath;
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            doc.Load("Config.xml");

            string xmlcontents = doc.InnerXml;

            tbServer.Text = doc.SelectSingleNode("/Configuration/Server").Attributes["IP"].Value.ToString();
            tbPort.Text = doc.SelectSingleNode("/Configuration/Server").Attributes["Port"].Value.ToString();

            tbCardFileName.Text = doc.SelectSingleNode("/Configuration/File").Attributes["CardFileName"].Value.ToString();
            tbDoorFileName.Text = doc.SelectSingleNode("/Configuration/File").Attributes["DoorFileName"].Value.ToString();
            tbScheduleFileName.Text = doc.SelectSingleNode("/Configuration/File").Attributes["ScheduleFileName"].Value.ToString();
            tbAccessFileName.Text = doc.SelectSingleNode("/Configuration/File").Attributes["AccessGrFileName"].Value.ToString();

            tbFileExportLocation.Text = doc.SelectSingleNode("/Configuration/File").Attributes["Location"].Value.ToString();
            
        }
    }
}
