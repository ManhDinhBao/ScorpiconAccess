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
    /// Interaction logic for ucHolderView.xaml
    /// </summary>
    public partial class ucHolderView : UserControl
    {

        #region Constructor
        public DTO_CardHolder MyHolder { get; set; }
        public string HolderName { get; set; }
        public string HolderAddress { get; set; }
        public EType.Gender Gender { get; set; }
        public BitmapImage ImgSource { get; set; }
        public ucHolderView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Variables
        public BackgroundWorker myCardWorker;
        public SQLResult result;
        public BUS_CardHolder bUS_CardHolder;
        #endregion

        #region Event
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteClick;
        #endregion

        #region Control events
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bUS_CardHolder = new BUS_CardHolder();

            myCardWorker = new BackgroundWorker();
            myCardWorker.WorkerSupportsCancellation = true;
            myCardWorker.DoWork += MyCardWorker_DoWork;
            myCardWorker.RunWorkerCompleted += MyCardWorker_RunWorkerCompleted;

            GenerateImageSource();

            BindingExpression binding = imgAvatar.GetBindingExpression(Image.SourceProperty);
            binding.UpdateTarget();

        }

        private void btHolderInfo_Click(object sender, RoutedEventArgs e)
        {
            wdCardHolderDetail view = new wdCardHolderDetail(EType.WindowMode.EDIT_MODE, MyHolder);
            if (view.ShowDialog() == true)
            {
                MyHolder = Repository.lstAllCardHolders.FirstOrDefault(c => c.Id == MyHolder.Id);
                HolderName = MyHolder.Name;
                HolderAddress = MyHolder.Address;
                Gender = MyHolder.Gender;

                BindingExpression binding = tbHolderAddress.GetBindingExpression(TextBlock.TextProperty);
                binding.UpdateTarget();

                BindingExpression binding1 = tbHolderName.GetBindingExpression(TextBlock.TextProperty);
                binding1.UpdateTarget();

                GenerateImageSource();

                BindingExpression binding2 = imgAvatar.GetBindingExpression(Image.SourceProperty);
                binding2.UpdateTarget();
            }
        }

        private void itemDeleteHolder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you want to delete this holder?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
        #endregion

        #region Background worker
        private void MyCardWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_CardHolder oldCardHolder = Repository.lstAllCardHolders.FirstOrDefault(c => c.Id == MyHolder.Id);
                if (oldCardHolder != null)
                {
                    Repository.lstAllCardHolders.Remove(oldCardHolder);
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
            result = bUS_CardHolder.DeleteCardHolder(MyHolder.Id);
        }
        #endregion

        #region Extra
        private void GenerateImageSource()
        {
            switch (Gender)
            {
                case EType.Gender.MALE:
                    ImgSource = new BitmapImage(new Uri("/ScorpiconAccess;component/Icon/male_avatar_128px.png", UriKind.Relative));
                    break;
                case EType.Gender.FEMALE:
                    ImgSource = new BitmapImage(new Uri("/ScorpiconAccess;component/Icon/female_avatar_128px.png", UriKind.Relative));
                    break;
                default:
                    ImgSource = new BitmapImage(new Uri("/ScorpiconAccess;component/Icon/male_avatar_128px.png", UriKind.Relative));
                    break;
            }
        }
        #endregion
    }
}
