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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ucCardView.xaml
    /// </summary>
    public partial class ucCardView : UserControl
    {
        #region Constructors
        public DTO_Card MyCard { get; set; }
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string CardSerial { get; set; }

        public ucCardView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Events
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteClick;
        #endregion

        #region Variables
        public BackgroundWorker myCardWorker;
        public SQLResult result;
        public BUS_Card bUS_Card;
        #endregion

        #region Control Events
        private void itemEditCard_Click(object sender, RoutedEventArgs e)
        {
            wdCardDetail view = new wdCardDetail(EType.WindowMode.EDIT_MODE, MyCard);
            if (view.ShowDialog() == true)
            {
                MyCard = Repository.lstAllCards.FirstOrDefault(c => c.CardNumber == MyCard.CardNumber);
                CardNumber = MyCard.CardNumber;
                CardSerial = MyCard.CardSerial;

                DTO_CardHolder holder = Repository.lstAllCardHolders.FirstOrDefault(h => h.Id == MyCard.Holder);
                Holder = holder.Name;

                BindingExpression binding = lbCardNumber.GetBindingExpression(Label.ContentProperty);
                binding.UpdateTarget();

                BindingExpression binding1 = lbCardSerial.GetBindingExpression(Label.ContentProperty);
                binding1.UpdateTarget();

                BindingExpression binding2 = lbCardHolderName.GetBindingExpression(Label.ContentProperty);
                binding2.UpdateTarget();

            }
        }

        private void itemDeleteCard_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you want to delete this card?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (myCardWorker.IsBusy)
                {
                    myCardWorker.CancelAsync();
                }
                else
                {
                    myCardWorker.RunWorkerAsync();
                }
            }          
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bUS_Card = new BUS_Card();

            myCardWorker = new BackgroundWorker();
            myCardWorker.WorkerSupportsCancellation = true;
            myCardWorker.DoWork += MyCardWorker_DoWork;
            myCardWorker.RunWorkerCompleted += MyCardWorker_RunWorkerCompleted;
        }
        #endregion

        #region Threads
        private void MyCardWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_Card oldCard = Repository.lstAllCards.FirstOrDefault(c => c.CardNumber == CardNumber);
                if (oldCard != null)
                {
                    Repository.lstAllCards.Remove(oldCard);
                }

                this.DeleteClick(this, e);

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MyCardWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Card.DeleteCard(CardNumber);
        }
        #endregion
    }
}
