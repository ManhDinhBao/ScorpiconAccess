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
    /// Interaction logic for CardDetailView.xaml
    /// </summary>
    public partial class CardDetailView : UserControl
    {
        #region Variables
        BUS_CardHolder bus_card = new BUS_CardHolder();

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        #endregion

        #region Control Events
        public CardDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }
        private void cbCardHolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCardHolder.SelectedItem != null)
            {
                DTO_CardHolder holder = (DTO_CardHolder)cbCardHolder.SelectedItem;

                tbHolderName.Text = holder.Name;
                tbHolderDOB.Text = holder.DOB.ToString("dd/MM/yyyy");
                tbHolderPhoneNo.Text = holder.PhoneNumber;
                tbHolderEmail.Text = holder.Email;
                tbHolderAddress.Text = holder.Address;

                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedCard != null)
                        Repository.selectedCard.Holder = holder.Id;
                }
                else
                {
                    if (Repository.newCard != null)
                        Repository.newCard.Holder = holder.Id;
                }

            }
            else
            {
                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedCard != null)
                        Repository.selectedCard.Holder = null;
                }
                else
                {
                    if (Repository.newCard != null)
                        Repository.newCard.Holder = null;
                }
            }
        }
        private void cbCardType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCardType.SelectedItem != null)
            {
                EType.CardType type = (EType.CardType)cbCardType.SelectedItem;

                if (type == EType.CardType.NOT_USE)
                {
                    cbCardHolder.SelectedItem = null;

                    ClearCardHolderInfo();
                }

                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedCard != null)
                        Repository.selectedCard.Type = type;
                }
                else
                {
                    if (Repository.newCard != null)
                        Repository.newCard.Type = type;
                }

            }
        }
        private void dpEndTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedCard != null)
                    Repository.selectedCard.ETime = (DateTime)dpEndTime.SelectedDate;
            }
            else
            {
                if (Repository.newCard != null)
                    Repository.newCard.ETime = (DateTime)dpEndTime.SelectedDate;
            }

        }
        private void dpStartTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedCard != null)
                    Repository.selectedCard.STime = (DateTime)dpStartTime.SelectedDate;
            }
            else
            {
                if (Repository.newCard != null)
                    Repository.newCard.STime = (DateTime)dpStartTime.SelectedDate;
            }

        }
        private void tbCardNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedCard != null)
                    Repository.selectedCard.CardNumber = tbCardNumber.Text;
            }
            else
            {
                if (Repository.newCard != null)
                    Repository.newCard.CardNumber = tbCardNumber.Text;
            }

        }
        private void tbCardSerial_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == ADD_MODE)
            {
                if (Repository.newCard != null)
                    Repository.newCard.CardSerial = tbCardSerial.Text;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearCardHolderInfo();

            cbCardHolder.ItemsSource = Repository.lstAllCardHolders;
            cbCardHolder.DisplayMemberPath = "Name";
            cbCardHolder.SelectedValuePath = "Id";

            //Get all card type
            cbCardType.ItemsSource = Enum.GetValues(typeof(EType.CardType));

            if (mode == ADD_MODE)
            {
                Repository.newCard = new DTO_Card();

                cbCardHolder.SelectedIndex = 0;
                cbCardType.SelectedIndex = 1;
            }
            else
            {
                cbCardHolder.SelectedValue = Repository.selectedCard.Holder;
                cbCardType.SelectedValue = Repository.selectedCard.Type;

                tbCardNumber.Text = Repository.selectedCard.CardNumber;
                tbCardSerial.Text = Repository.selectedCard.CardSerial;

                dpStartTime.SelectedDate = Repository.selectedCard.STime;
                dpEndTime.SelectedDate = Repository.selectedCard.ETime;
            }
        }
        #endregion

        #region Extra Methods
        private void ClearCardHolderInfo()
        {
            tbHolderName.Text = "";
            tbHolderDOB.Text = "";
            tbHolderPhoneNo.Text = "";
            tbHolderEmail.Text = "";
            tbHolderAddress.Text = "";
        }

        #endregion

    }
}
