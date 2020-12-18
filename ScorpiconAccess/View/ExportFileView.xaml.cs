using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ExportFileView.xaml
    /// </summary>
    public partial class ExportFileView : Window
    {
        BUS_General bUS_General = new BUS_General();
        List<ListDetailItem> listRightDetails = new List<ListDetailItem>();
        string exportPath = "";
        public ExportFileView()
        {
            InitializeComponent();
        }

        private void btGenCardConfigFile_Click(object sender, RoutedEventArgs e)
        {
            string listDoorString = "";
            foreach(ListDetailItem item in listRightDetails)
            {
                if (item.IsChecked)
                {
                    listDoorString += item.Key + "_/";
                }
            }

            string fileConfigString = "";
            List<CardConfig> cardConfigs = bUS_General.GetCardConfigs(listDoorString);
            foreach(CardConfig c in cardConfigs)
            {
                fileConfigString += c.ToFileString();
            }
            rtbCardFileDetail.Document.Blocks.Remove(rtbCardFileDetail.Document.Blocks.FirstBlock);
            rtbCardFileDetail.Document.Blocks.Add(new Paragraph(new Run(fileConfigString)));

        }

        private void TabControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (DTO_Door door in Repository.lstAllDoor)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = door.Name;
                    item.Key = door.Id;
                    item.IsChecked = false;

                    listRightDetails.Add(item);
                }
            });

            lbDoors.ItemsSource = listRightDetails;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, StringFromRichTextBox(rtbCardFileDetail));
        }

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
    }
}
