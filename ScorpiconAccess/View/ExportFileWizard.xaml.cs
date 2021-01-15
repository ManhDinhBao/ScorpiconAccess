using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
using System.Reflection;
using System.Xml;
using MessageBox = System.Windows.MessageBox;
using System.Runtime.InteropServices;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ExportFileWizard.xaml
    /// </summary>
    public partial class ExportFileWizard : Window
    {
        #region Variables
        BUS_General bUS_General = new BUS_General();
        ObservableCollection<ListDetailItem> listItems;
        EType.GenConfigFileType mode = new EType.GenConfigFileType();
        string exportPath = "";
        public static bool x64;
        string strCardFile, strDoorGrFile, strScheduleFile, strAccessGrFile;
        #endregion

        public ExportFileWizard()
        {
            InitializeComponent();
            if (IntPtr.Size == 8)
            {
                ExportFileWizard.x64 = true;
            }
            else
            {
                ExportFileWizard.x64 = false;
            }
        }

        #region PAGE
        #region Page select file type
        private void Page1_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page1_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Page select content to create file
        private void Page2_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            listItems = new ObservableCollection<ListDetailItem>();
            if (rbCardFile.IsChecked==true)
            {
                mode = EType.GenConfigFileType.CARD;
                Page2.Description = "Lựa chọn các cửa mà có thể sử dụng thẻ. Nhấn Next để tiếp tục.";
                foreach (DTO_Door door in Repository.lstAllDoor)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = door.Name;
                    item.Key = door.Id;
                    item.IsChecked = false;

                    listItems.Add(item);
                }

                lbContents.ItemsSource = listItems;
                return;
            }

            if (rbGDoorFile.IsChecked == true)
            {
                mode = EType.GenConfigFileType.DOOR;
                Page2.Description = "Lựa chọn các quyền có chứa các cửa bạn muốn tạo. Nhấn Next để tiếp tục.";
                foreach (DTO_UserRight right in Repository.lstAllRIght)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = right.Name;
                    item.Key = right.Id;
                    item.IsChecked = false;

                    listItems.Add(item);
                }

                lbContents.ItemsSource = listItems;
                return;
            }

            if (rbScheduleFile.IsChecked == true)
            {
                mode = EType.GenConfigFileType.SCHEDULE;
                Page2.Description = "Lựa chọn các quyền có chứa các lịch truy cập bạn muốn tạo. Nhấn Next để tiếp tục.";
                foreach (DTO_Schedule schedule in Repository.lstAllSchedules)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = schedule.Name;
                    item.Key = schedule.Id;
                    item.IsChecked = false;

                    listItems.Add(item);
                }

                lbContents.ItemsSource = listItems;
                return;
            }

            if (rbAccessGrFile.IsChecked == true)
            {
                mode = EType.GenConfigFileType.ACCESSGR;
                Page2.Description = "Lựa chọn các quyền tương ứng với nhóm truy cập. Nhấn Next để tiếp tục.";
                foreach (DTO_UserRight right in Repository.lstAllRIght)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = right.Name;
                    item.Key = right.Id;
                    item.IsChecked = false;

                    listItems.Add(item);
                }

                lbContents.ItemsSource = listItems;
                return;
            }

            if (rbAllFile.IsChecked==true)
            {
                List<string> lstFunction = new List<string>()
                {
                    "Tệp cấu hình thẻ",
                    "Tệp cấu hình nhóm cửa",
                    "Tệp cấu hình lịch",
                    "Tệp cấu hình nhóm truy cập"
                };


                mode = EType.GenConfigFileType.ALL;
                Page2.Description = "Hệ thống sẽ tạo tất cả các tệp cấu hình. Nhấn Next để tiếp tục.";
                foreach (string str in lstFunction)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = str;
                    item.Key = str;
                    item.IsChecked = true;

                    listItems.Add(item);
                }

                lbContents.ItemsSource = listItems;
                
                return;
            }

        }
        #endregion

        #region Last page
        private void LastPage_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void LastPage_Loaded(object sender, RoutedEventArgs e)
        {
            strCardFile = "";
            strDoorGrFile = "";
            strScheduleFile = "";
            strAccessGrFile = "";

            string listItemString = "";
            foreach (ListDetailItem item in listItems)
            {
                if (item.IsChecked)
                {
                    listItemString += item.Key + "_/";
                }
            }

            string fileConfigString = "";

            switch (mode)
            {
                case EType.GenConfigFileType.CARD:
                    List<CardConfig> cardConfigs = bUS_General.GetCardConfigs(listItemString);
                    fileConfigString = GenCardFile(cardConfigs);
                    break;
                case EType.GenConfigFileType.DOOR:
                    List<DoorConfig> doorConfigs = bUS_General.GetDoorGroupCongfigs(listItemString);
                    fileConfigString = GenDoorGroupFile(doorConfigs);
                    break;
                case EType.GenConfigFileType.SCHEDULE:
                    List<ScheduleConfig> scheduileConfigs = bUS_General.GetScheduleCongfigs(listItemString);
                    fileConfigString = GenScheduleFile(scheduileConfigs);
                    break;
                case EType.GenConfigFileType.ACCESSGR:
                    List<AccessGroupConfig> accesConfigs = bUS_General.GetAccessGroupCongfigs(listItemString);
                    fileConfigString = GenAccessGroupFile(accesConfigs);
                    break;
                case EType.GenConfigFileType.ALL:
                    string listAllDoorString = "";
                    foreach (DTO_Door door in Repository.lstAllDoor)
                    {
                        listAllDoorString += door.Id + "_/";
                    }
                    List<CardConfig> cardConfig = bUS_General.GetCardConfigs(listAllDoorString);
                    fileConfigString += "***CARD FILE***\r\n";
                    fileConfigString+= GenCardFile(cardConfig)+"\r\n";
                    strCardFile = GenCardFile(cardConfig);

                    string listAllRightString = "";
                    foreach (DTO_UserRight right in Repository.lstAllRIght)
                    {
                        listAllRightString += right.Id + "_/";
                    }
                    List<DoorConfig> doorConfig = bUS_General.GetDoorGroupCongfigs(listAllRightString);
                    fileConfigString += "***DOOR GROUP FILE***\r\n";
                    fileConfigString += GenDoorGroupFile(doorConfig) + "\r\n";
                    strDoorGrFile = GenDoorGroupFile(doorConfig);

                    string listAllScheduleString = "";
                    foreach (DTO_Schedule schedule in Repository.lstAllSchedules)
                    {
                        listAllScheduleString += schedule.Id + "_/";
                    }
                    List<ScheduleConfig> scheduileConfig = bUS_General.GetScheduleCongfigs(listAllScheduleString);
                    fileConfigString += "***SCHEDULE FILE***\r\n";
                    fileConfigString += GenScheduleFile(scheduileConfig)+"\r\n";
                    strScheduleFile = GenScheduleFile(scheduileConfig);

                    List<AccessGroupConfig> accesConfig = bUS_General.GetAccessGroupCongfigs(listAllRightString);
                    fileConfigString += "***ACCESS GROUP FILE***\r\n";
                    fileConfigString += GenAccessGroupFile(accesConfig) + "\r\n";
                    strAccessGrFile = GenAccessGroupFile(accesConfig);
                    break;
            }
            
            rtbFileDetail.Document.Blocks.Remove(rtbFileDetail.Document.Blocks.FirstBlock);
            rtbFileDetail.Document.Blocks.Add(new Paragraph(new Run(fileConfigString)));
        }
        #endregion
        #endregion

        #region Custom methods
        private string GenCardFile(List<CardConfig> cardConfigs)
        {
            string fileConfigString = "";
            foreach (CardConfig c in cardConfigs)
            {
                fileConfigString += c.ToFileString();
            }

            return fileConfigString;

        }

        private string GenDoorGroupFile(List<DoorConfig> doorConfigs)
        {
            string fileConfigString = "";
            foreach (DoorConfig c in doorConfigs)
            {
                fileConfigString += c.ToFileString();
            }

            return fileConfigString;
        }

        private string GenScheduleFile(List<ScheduleConfig> scheduileConfigs)
        {
            string fileConfigString = "";
            foreach (ScheduleConfig c in scheduileConfigs)
            {
                fileConfigString += c.ToFileString();
            }
            fileConfigString = ScheduleConfig.comment + "\r\n" + fileConfigString;
            return fileConfigString;
        }

        private string GenAccessGroupFile(List<AccessGroupConfig> accesConfigs)
        {
            string fileConfigString = "";
            foreach (AccessGroupConfig c in accesConfigs)
            {
                fileConfigString += c.ToFileString();
            }
            return fileConfigString;
        }
        public string StringFromRichTextBox(System.Windows.Controls.RichTextBox rtb)
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

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Config.xml");

                string cardFileName = doc.SelectSingleNode("/Configuration/File").Attributes["CardFileName"].Value.ToString();
                string doorFileName = doc.SelectSingleNode("/Configuration/File").Attributes["DoorFileName"].Value.ToString();
                string ScheduleFileName = doc.SelectSingleNode("/Configuration/File").Attributes["ScheduleFileName"].Value.ToString();
                string accessgrFileName = doc.SelectSingleNode("/Configuration/File").Attributes["AccessGrFileName"].Value.ToString();
                string downloadFileName = "Download";

                string locationPath = doc.SelectSingleNode("/Configuration/File").Attributes["Location"].Value.ToString();

                switch (mode)
                {
                    case EType.GenConfigFileType.CARD:
                        exportPath = locationPath + "\\" + cardFileName + ".txt";
                        File.WriteAllText(exportPath, StringFromRichTextBox(rtbFileDetail));

                        BuilCardDB();

                        exportPath = locationPath + "\\" + downloadFileName + ".txt";
                        File.WriteAllText(exportPath, "1");
                        break;
                    case EType.GenConfigFileType.DOOR:
                        exportPath = locationPath + "\\" + doorFileName;
                        File.WriteAllText(exportPath, StringFromRichTextBox(rtbFileDetail));
                        break;
                    case EType.GenConfigFileType.SCHEDULE:
                        exportPath = locationPath + "\\" + ScheduleFileName;
                        File.WriteAllText(exportPath, StringFromRichTextBox(rtbFileDetail));
                        break;
                    case EType.GenConfigFileType.ACCESSGR:
                        exportPath = locationPath + "\\" + accessgrFileName;
                        File.WriteAllText(exportPath, StringFromRichTextBox(rtbFileDetail));
                        break;
                    case EType.GenConfigFileType.ALL:
                        exportPath = locationPath + "\\" + cardFileName + ".txt";
                        File.WriteAllText(exportPath, strCardFile);

                        exportPath = locationPath + "\\" + doorFileName;
                        File.WriteAllText(exportPath, strDoorGrFile);

                        exportPath = locationPath + "\\" + ScheduleFileName;
                        File.WriteAllText(exportPath, strScheduleFile);

                        exportPath = locationPath + "\\" + accessgrFileName;
                        File.WriteAllText(exportPath, strAccessGrFile);

                        BuilCardDB();

                        exportPath = locationPath + "\\" + downloadFileName + ".txt";
                        File.WriteAllText(exportPath, "0");
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }

        #region Control click
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
        #endregion

        private void BuilCardDB()
        {
            string path = "C:\\HID";
            string str = "Cards.txt";
            string identfile = "IdentDB";
            string accessfile = "AccessDB";
            string fmtsfile = "Formats";
            string cardsetfile = "CardSets";
            string logfile = "BDLogFile.txt";
            int loglvl = 50;

            if(ExportFileWizard.x64)
            {
                ExportFileWizard.x64BuildDumpCards.BuildCards(path, str, identfile, accessfile, fmtsfile, cardsetfile, logfile, loglvl);
            }
            else
            {
                ExportFileWizard.x32BuildDumpCards.BuildCards(path, str, identfile, accessfile, fmtsfile, cardsetfile, logfile, loglvl);
            }

            //if (ExportFileWizard.x64)
            //{
            //    ExportFileWizard.x64BuildDumpCards.DumpCardFile(path, str, identfile, accessfile, fmtsfile, cardsetfile, logfile, loglvl);

            //}
            //else
            //{
            //    ExportFileWizard.x32BuildDumpCards.DumpCardFile(path, str, identfile, accessfile, fmtsfile, cardsetfile, logfile, loglvl);
            //}
                
        }

        private static class x32BuildDumpCards
        {
            [DllImport("BuildDumpCards.dll")]
            public static extern int ComputeCardBitPattern(
              int cardsetid,
              string cardnumber,
              string path,
              string fmtsfile,
              string cardsetfile,
              string logfile,
              int loglvl,
              StringBuilder cardbitpattern,
              int cardbitpatternsize);

            [DllImport("BuildDumpCards.dll")]
            public static extern int ComputeCardBitPattern2(
              string cardnumber,
              string cardsetdefinition,
              string path,
              string formatfile,
              string logfile,
              int loglvl,
              StringBuilder cardbitpattern,
              int cardbitpatternsize);

            [DllImport("BuildDumpCards.dll")]
            public static extern int BuildCards(
              string path,
              string inputfile,
              string identfile,
              string accessfile,
              string fmtsfile,
              string cardsetfile,
              string logfile,
              int loglvl);

            [DllImport("BuildDumpCards.dll")]
            public static extern int DumpCardFile(
              string path,
              string identfile,
              string accessfile,
              string fmtsfile,
              string cardsetfile,
              string outputfile,
              string logfile,
              int loglvl);

            [DllImport("BuildDumpCards.dll")]
            public static extern void enable_delete_flag();

            [DllImport("BuildDumpCards.dll")]
            public static extern void disable_delete_flag();

            [DllImport("BuildDumpCards.dll")]
            public static extern void enable_roleid_flag();

            [DllImport("BuildDumpCards.dll")]
            public static extern void disable_roleid_flag();
        }

        private static class x64BuildDumpCards
        {
            [DllImport("BuildDumpCards64.dll")]
            public static extern int ComputeCardBitPattern(
              int cardsetid,
              string cardnumber,
              string path,
              string fmtsfile,
              string cardsetfile,
              string logfile,
              int loglvl,
              StringBuilder cardbitpattern,
              int cardbitpatternsize);

            [DllImport("BuildDumpCards64.dll")]
            public static extern int ComputeCardBitPattern2(
              string cardnumber,
              string cardsetdefinition,
              string path,
              string formatfile,
              string logfile,
              int loglvl,
              StringBuilder cardbitpattern,
              int cardbitpatternsize);

            [DllImport("BuildDumpCards64.dll")]
            public static extern int BuildCards(
              string path,
              string inputfile,
              string identfile,
              string accessfile,
              string fmtsfile,
              string cardsetfile,
              string logfile,
              int loglvl);

            [DllImport("BuildDumpCards64.dll")]
            public static extern int DumpCardFile(
              string path,
              string identfile,
              string accessfile,
              string fmtsfile,
              string cardsetfile,
              string outputfile,
              string logfile,
              int loglvl);

            [DllImport("BuildDumpCards64.dll")]
            public static extern void enable_delete_flag();

            [DllImport("BuildDumpCards64.dll")]
            public static extern void disable_delete_flag();

            [DllImport("BuildDumpCards64.dll")]
            public static extern void enable_roleid_flag();

            [DllImport("BuildDumpCards64.dll")]
            public static extern void disable_roleid_flag();
        }
    }

    
}
