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
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for wdCardHolderDetail.xaml
    /// </summary>
    public partial class wdCardHolderDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_CardHolder bUS_CardHolder;
        public DTO_CardHolder holder, newHolder;
        public SQLResult result;
        public BackgroundWorker myCardHolderWorker, getCardHolderWorker;
        #endregion

        #region Constructor
        public wdCardHolderDetail(EType.WindowMode mode, DTO_CardHolder holder)
        {
            InitializeComponent();
            this.mode = mode;
            this.holder = holder;
        }
        #endregion

        #region Control events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();
                    dpHolderDOB.SelectedDate = DateTime.Today;
                    holder = new DTO_CardHolder();
                    break;
                default:
                    BindData();
                    break;
            }
        }
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            btSave.IsEnabled = false;

            holder.Name = tbHolderAddress.Text;
            holder.Email = tbHolderEmail.Text;
            holder.PhoneNumber = tbHolderPhone.Text;
            holder.Address = tbHolderAddress.Text;
            holder.DOB = (DateTime)dpHolderDOB.SelectedDate;
            holder.Description = General.StringFromRichTextBox(rtbDescription).Trim();

            if (cboGender.SelectedItem != null)
            {
                EType.Gender gender = (EType.Gender)cboGender.SelectedItem;

                holder.Gender = gender;
            }
            holder.Name = tbHolderName.Text;

            if (myCardHolderWorker.IsBusy)
            {
                myCardHolderWorker.CancelAsync();
            }
            else
            {
                myCardHolderWorker.RunWorkerAsync();
            }
        }

        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        #endregion

        #region Extra
        private void ResetControl()
        {
            tbHolderName.Text = "";
            tbHolderEmail.Text = "";
            tbHolderAddress.Text = "";
            tbHolderPhone.Text = "";
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
        }

        private void InitData()
        {
            bUS_CardHolder = new BUS_CardHolder();
            myCardHolderWorker = new BackgroundWorker();
            myCardHolderWorker.WorkerSupportsCancellation = true;
            myCardHolderWorker.DoWork += MyCardHolderWorker_DoWork;
            myCardHolderWorker.RunWorkerCompleted += MyCardHolderWorker_RunWorkerCompleted;

            getCardHolderWorker = new BackgroundWorker();
            getCardHolderWorker.WorkerSupportsCancellation = true;
            getCardHolderWorker.DoWork += GetCardHolderWorker_DoWork;
            getCardHolderWorker.RunWorkerCompleted += GetCardHolderWorker_RunWorkerCompleted;

            cboGender.ItemsSource = Enum.GetValues(typeof(EType.Gender));

            if (holder != null)
            {
                List<DTO_Card> listCards = Repository.lstAllCards.Where(card => card.Holder == holder.Id).ToList();
                lbListCard.ItemsSource = listCards;
            }

        }

        private void BindData()
        {
            cboGender.SelectedValue = holder.Gender;

            tbId.Text = holder.Id;
            tbHolderName.Text = holder.Name;
            tbHolderAddress.Text = holder.Address;
            tbHolderEmail.Text = holder.Email;
            tbHolderPhone.Text = holder.PhoneNumber;
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(holder.Description)));

            dpHolderDOB.SelectedDate = holder.DOB;
        }
        #endregion

        #region Background worker

        private void GetCardHolderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newHolder == null || newHolder.Id == "")
            {

            }

            Repository.lstAllCardHolders.Add(newHolder);
            this.DialogResult = true;
            this.Hide();
        }

        private void GetCardHolderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newHolder = bUS_CardHolder.GetCardHolderByKey(result.ExtraData);
        }

        private void MyCardHolderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getCardHolderWorker.IsBusy)
                    {
                        getCardHolderWorker.CancelAsync();
                    }
                    else
                    {
                        getCardHolderWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_CardHolder oldHolder = Repository.lstAllCardHolders.FirstOrDefault(c => c.Id == holder.Id);
                    if (oldHolder != null)
                    {
                        oldHolder = holder;
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

        private void MyCardHolderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
           
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_CardHolder.AddNewCardHolder(holder);
            }
            else
            {
                result = bUS_CardHolder.UpdateCardHolder(holder);
            }
        }
        #endregion
    }
}
