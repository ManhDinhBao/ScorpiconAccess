using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for wdCardDetail.xaml
    /// </summary>
    public partial class wdCardDetail : Window
    {
        #region Variables
        public BUS_Card bUS_Card;
        public DTO_Card myCard, newCard;
        public EType.WindowMode mode;
        public BackgroundWorker myCardWorker, getCardWorker;
        public SQLResult result;
        #endregion

        #region Constructor
        public wdCardDetail(EType.WindowMode mode, DTO_Card card)
        {
            InitializeComponent();
            this.myCard = card;
            this.mode = mode;
        }
        #endregion

        #region Control Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();

                    //dpStartTime.SelectedDate = DateTime.Today;
                    //dpEndTime.SelectedDate = DateTime.Today.AddDays(1);

                    myCard = new DTO_Card();
                    break;
                default:
                    BindData();
                    break;
            }
        }

        private void tbCardSerial_TextChanged(object sender, TextChangedEventArgs e)
        {
            myCard.CardSerial = tbCardSerial.Text;
        }

        private void tbCardPin_PasswordChanged(object sender, RoutedEventArgs e)
        {
            System.Text.RegularExpressions.Regex myRegex = new System.Text.RegularExpressions.Regex(@"[^\d]");
            if (myRegex.IsMatch(tbCardPin.Password))
                tbCardPin.Password = myRegex.Replace(tbCardPin.Password, "");

            myCard.Pin = int.Parse(tbCardPin.Password);
        }

        private void cbCardHolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCardHolder.SelectedItem != null)
            {
                DTO_CardHolder holder = (DTO_CardHolder)cbCardHolder.SelectedItem;
                myCard.Holder = holder.Id;
            }
        }

        private void dpStartTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myCard != null)
            {
                myCard.STime = (DateTime)dpStartTime.SelectedDate;
            }

        }

        private void dpEndTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myCard != null)
            {
                myCard.ETime = (DateTime)dpEndTime.SelectedDate;
            }

        }

        private void cbCardType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCardType.SelectedItem != null)
            {
                EType.CardType type = (EType.CardType)cbCardType.SelectedItem;
                myCard.Type = type;
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            btSave.IsEnabled = false;

            if (myCardWorker.IsBusy)
            {
                myCardWorker.CancelAsync();
            }
            else
            {
                myCardWorker.RunWorkerAsync();
            }
        }

        private void tbCardPin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Background Worker
        private void GetCardWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newCard == null || newCard.CardNumber == "")
            {

            }

            Repository.lstAllCards.Add(newCard);
            this.DialogResult = true;
            this.Hide();
        }

        private void GetCardWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newCard = bUS_Card.GetCardByKey(result.ExtraData);
        }

        private void MyCardWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getCardWorker.IsBusy)
                    {
                        getCardWorker.CancelAsync();
                    }
                    else
                    {
                        getCardWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardNumber == myCard.CardNumber);
                    if (oldCard != null)
                    {
                        oldCard = myCard;
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

        private void MyCardWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Card.AddNewCard(myCard);
            }
            else
            {
                result = bUS_Card.UpdateCard(myCard);
            }
        }
        #endregion

        #region Extra
        private void InitData()
        {
            bUS_Card = new BUS_Card();

            myCardWorker = new BackgroundWorker();
            myCardWorker.WorkerReportsProgress = true;
            myCardWorker.WorkerSupportsCancellation = true;
            myCardWorker.DoWork += MyCardWorker_DoWork;
            myCardWorker.RunWorkerCompleted += MyCardWorker_RunWorkerCompleted;

            getCardWorker = new BackgroundWorker();
            getCardWorker.WorkerReportsProgress = true;
            getCardWorker.WorkerSupportsCancellation = true;
            getCardWorker.DoWork += GetCardWorker_DoWork;
            getCardWorker.RunWorkerCompleted += GetCardWorker_RunWorkerCompleted;

            cbCardHolder.ItemsSource = Repository.lstAllCardHolders;
            cbCardHolder.SelectedValuePath = "Id";
            cbCardHolder.DisplayMemberPath = "Name";

            cbCardType.ItemsSource = Enum.GetValues(typeof(EType.CardType));


            tbCardNumber.IsReadOnly = true;
            tbCardNumber.Focus();
        }

        private void ResetControl()
        {
            tbCardSerial.Text = "";
            tbCardPin.Password = "";
        }

        private void BindData()
        {
            if (myCard != null)
            {
                tbCardNumber.Text = myCard.CardNumber == null ? "" : myCard.CardNumber;
                tbCardSerial.Text = myCard.CardSerial == null ? "" : myCard.CardSerial;
                
                if (myCard.Pin == 0)
                {
                    tbCardPin.Password = "";
                }
                else
                {
                    tbCardPin.Password = myCard.Pin.ToString();
                }

                if (myCard.Holder == "" || myCard.Holder == null)
                {
                    cbCardHolder.SelectedValue = null;
                }
                else
                {
                    cbCardHolder.SelectedValue = myCard.Holder;
                }

                if(DateTime.Compare(myCard.STime,new DateTime(1970,01,01)) > 0)
                {
                    dpStartTime.SelectedDate = myCard.STime;
                }

                if (DateTime.Compare(myCard.STime, new DateTime(1970, 01, 01)) > 0)
                {
                    dpEndTime.SelectedDate = myCard.ETime;
                }

                cbCardType.SelectedValue = myCard.Type;
            }
        }
        #endregion
    }
}
