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
    /// Interaction logic for HolderDetailView.xaml
    /// </summary>
    public partial class HolderDetailView : UserControl
    {
        #region Variables
        BUS_CardHolder bus_card = new BUS_CardHolder();

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        #endregion
        public HolderDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        #region Control Events
        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGender.SelectedItem != null)
            {
                EType.Gender gender = (EType.Gender)cbGender.SelectedItem;

                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedHolder != null)
                        Repository.selectedHolder.Gender = gender;
                }
                else
                {
                    if (Repository.newHolder != null)
                        Repository.newHolder.Gender = gender;
                }

            }
        }

        private void dpHolderDOB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.DOB = (DateTime)dpHolderDOB.SelectedDate;
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.DOB = (DateTime)dpHolderDOB.SelectedDate;
            }
        }

        private void tbHolderAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.Address = tbHolderAddress.Text;
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.Address = tbHolderAddress.Text;
            }
        }

        private void tbHolderEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.Email = tbHolderEmail.Text;
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.Email = tbHolderEmail.Text;
            }
        }

        private void tbHolderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == ADD_MODE)
            {
                Repository.newHolder.Id = tbHolderId.Text;
                GenerateBarcode();
            }
        }

        private void tbHolderName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.Name = tbHolderName.Text;
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.Name = tbHolderName.Text;
            }
        }

        private void tbHolderPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.PhoneNumber = tbHolderPhone.Text;
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.PhoneNumber = tbHolderAddress.Text;
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedHolder != null)
                    Repository.selectedHolder.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
            else
            {
                if (Repository.newHolder != null)
                    Repository.newHolder.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbGender.ItemsSource = Enum.GetValues(typeof(EType.Gender));

            if (mode == ADD_MODE)
            {
                Repository.newHolder = new DTO_CardHolder();
                tbHolderId.IsReadOnly = false;
                cbGender.SelectedIndex = 0;
            }
            else
            {
                cbGender.SelectedValue = Repository.selectedHolder.Gender;

                tbHolderId.Text         = Repository.selectedHolder.Id;
                tbHolderId.IsReadOnly = true;
                tbHolderName.Text       = Repository.selectedHolder.Name;
                tbHolderAddress.Text    = Repository.selectedHolder.Address;
                tbHolderEmail.Text      = Repository.selectedHolder.Email;
                tbHolderPhone.Text      = Repository.selectedHolder.PhoneNumber;
                //rtbDescription.Document.Blocks.Clear();
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedHolder.Description)));

                dpHolderDOB.SelectedDate = Repository.selectedHolder.DOB;


                GenerateListItemCard();
                GenerateBarcode();
            }
        }
        #endregion

        #region Extra Methods
        private void GenerateBarcode()
        {
            //Encode data
            Barcodes bb = new Barcodes();
            bb.BarcodeType = Barcodes.BarcodeEnum.Code39;
            if(mode == ADD_MODE)
            {
                bb.Data = Repository.newHolder.Id;
            }
            else
            {
                bb.Data = Repository.selectedHolder.Id;
            }
            
            bb.CheckDigit = Barcodes.YesNoEnum.Yes;
            bb.encode();

            int thinWidth;
            int thickWidth;

            thinWidth = 1;
            thickWidth = 2 * thinWidth;

            string outputString = bb.EncodedData;
            string humanText = bb.HumanText;

            // Draw The Barcode
            int len = outputString.Length;
            int currentPos = 10;
            int currentTop = 10;
            int currentColor = 0;
            for (int i = 0; i < len; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 30;
                if (currentColor == 0)
                {
                    currentColor = 1;
                    rect.Fill = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    currentColor = 0;
                    rect.Fill = new SolidColorBrush(Colors.White);

                }
                Canvas.SetLeft(rect, currentPos);
                Canvas.SetTop(rect, currentTop);

                if (outputString[i] == 't')
                {
                    rect.Width = thinWidth;
                    currentPos += thinWidth;

                }
                else if (outputString[i] == 'w')
                {
                    rect.Width = thickWidth;
                    currentPos += thickWidth;

                }
                barCodeCanvas.Children.Add(rect);
            }
        }

        private void GenerateListItemCard()
        {
            List<DTO_Card>  listCards = Repository.lstAllCards.Where(card => card.Holder == Repository.selectedHolder.Id).ToList();
            lbListCard.ItemsSource = listCards;
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
        #endregion
    }
}
