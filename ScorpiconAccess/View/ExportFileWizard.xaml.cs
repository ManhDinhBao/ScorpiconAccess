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

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ExportFileWizard.xaml
    /// </summary>
    public partial class ExportFileWizard : Window
    {
        BUS_General bUS_General = new BUS_General();
        ObservableCollection<ListDetailItem> listItems;
        EType.GenConfigFileType mode = new EType.GenConfigFileType();
        string exportPath = "";

        public ExportFileWizard()
        {
            InitializeComponent();
            
        }

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
        }
        #endregion

        #region Page choose export folder
        private void Page3_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page3_Loaded(object sender, RoutedEventArgs e)
        {
            tbPath.Text = System.AppDomain.CurrentDomain.BaseDirectory;
        }
        private void btChangePath_Click(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //DialogResult result = folderBrowserDialog.ShowDialog();
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
                
            //}

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                exportPath = saveFileDialog.FileName;
                tbPath.Text = exportPath;
            }

        }


        #endregion

        private void LastPage_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void LastPage_Loaded(object sender, RoutedEventArgs e)
        {
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
                    foreach (CardConfig c in cardConfigs)
                    {
                        fileConfigString += c.ToFileString();
                    }
                    break;
                case EType.GenConfigFileType.DOOR:
                    List<DoorConfig> doorConfigs = bUS_General.GetDoorGroupCongfigs(listItemString);
                    foreach (DoorConfig c in doorConfigs)
                    {
                        fileConfigString += c.ToFileString();
                    }
                    break;
                case EType.GenConfigFileType.SCHEDULE:
                    List<ScheduleConfig> scheduileConfigs = bUS_General.GetScheduleCongfigs(listItemString);
                    foreach (ScheduleConfig c in scheduileConfigs)
                    {
                        fileConfigString += c.ToFileString();
                    }
                    fileConfigString = ScheduleConfig.comment + "\r\n" + fileConfigString;
                    break;
            }
            
            rtbFileDetail.Document.Blocks.Remove(rtbFileDetail.Document.Blocks.FirstBlock);
            rtbFileDetail.Document.Blocks.Add(new Paragraph(new Run(fileConfigString)));
        }

        private void Wizard_Finish(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
        {
            File.WriteAllText(exportPath, StringFromRichTextBox(rtbFileDetail));
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

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btMinnimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btMaximize_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
