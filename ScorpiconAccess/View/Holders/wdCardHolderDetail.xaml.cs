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
        public BUS_Department bUS_Department;
        public DTO_CardHolder holder, newHolder;
        public SQLResult result;
        public BackgroundWorker myCardHolderWorker, getCardHolderWorker, delCardHolderWorker, workerReloadDept;
        #endregion

        #region Constructor
        public wdCardHolderDetail(EType.WindowMode mode, DTO_CardHolder holder)
        {
            InitializeComponent();
            this.mode = mode;
            this.holder = holder;
        }
        public int funcClick { get; set; }
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
                    cboGender.SelectedIndex = 0;
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
            funcClick = 1;
            
            try
            {
                if (tbHolderName.Text.Count() <= 0)
                {
                    holder.Name = null;
                }
                else
                {
                    holder.Name = tbHolderName.Text;
                }
                
                holder.Email = tbHolderEmail.Text;
                holder.PhoneNumber = tbHolderPhone.Text;
                holder.Address = tbHolderAddress.Text;
                holder.DOB = (DateTime)dpHolderDOB.SelectedDate;
                holder.Description = General.StringFromRichTextBox(rtbDescription).Trim();
                holder.Account = tbAccount.Text;
                holder.Password = tbPassword.Password;

                if (cboDepartment.SelectedItem!=null)
                {
                    holder.Department = new DTO_Department();
                    holder.Department.Id = cboDepartment.SelectedValue.ToString();
                    holder.Department.RootDeptId = cboDepartment.SelectedValue.ToString();
                }
                else
                {
                    holder.Department = null;
                }
                

                if (cboGender.SelectedItem != null)
                {
                    EType.Gender gender = (EType.Gender)cboGender.SelectedItem;

                    holder.Gender = gender;
                }

                if (myCardHolderWorker.IsBusy)
                {
                    myCardHolderWorker.CancelAsync();
                }
                else
                {
                    btSave.IsEnabled = false;
                    myCardHolderWorker.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 2;
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có chắc chắn muốn xóa chủ thẻ này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (delCardHolderWorker.IsBusy)
                {
                    delCardHolderWorker.CancelAsync();
                }
                else
                {
                    delCardHolderWorker.RunWorkerAsync();
                }
            }
        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

        #region Custom methods
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
            bUS_Department = new BUS_Department();

            myCardHolderWorker = new BackgroundWorker();
            myCardHolderWorker.WorkerSupportsCancellation = true;
            myCardHolderWorker.DoWork += MyCardHolderWorker_DoWork;
            myCardHolderWorker.RunWorkerCompleted += MyCardHolderWorker_RunWorkerCompleted;

            getCardHolderWorker = new BackgroundWorker();
            getCardHolderWorker.WorkerSupportsCancellation = true;
            getCardHolderWorker.DoWork += GetCardHolderWorker_DoWork;
            getCardHolderWorker.RunWorkerCompleted += GetCardHolderWorker_RunWorkerCompleted;

            delCardHolderWorker = new BackgroundWorker();
            delCardHolderWorker.WorkerSupportsCancellation = true;
            delCardHolderWorker.DoWork += DelCardHolderWorker_DoWork;
            delCardHolderWorker.RunWorkerCompleted += DelCardHolderWorker_RunWorkerCompleted;

            workerReloadDept = new BackgroundWorker();
            workerReloadDept.WorkerSupportsCancellation = true;
            workerReloadDept.DoWork += WorkerReloadDept_DoWork; ;
            workerReloadDept.RunWorkerCompleted += WorkerReloadDept_RunWorkerCompleted;
           
            cboGender.ItemsSource = Enum.GetValues(typeof(EType.Gender));

            cboDepartment.ItemsSource = Repository.lstAllDepartment;
            cboDepartment.DisplayMemberPath = "Name";
            cboDepartment.SelectedValuePath = "Id";

            if (holder != null)
            {
                List<DTO_Card> listCards = Repository.lstAllCards.Where(card => card.Holder == holder.Id).ToList();
                lbListCard.ItemsSource = listCards;
            }

        }
        private void BindData()
        {
            cboGender.SelectedValue = holder.Gender;
            if(holder.Department!=null)
            cboDepartment.SelectedValue = holder.Department.Id;

            tbId.Text = holder.Id;
            tbHolderName.Text = holder.Name;
            tbHolderAddress.Text = holder.Address;
            tbHolderEmail.Text = holder.Email;
            tbHolderPhone.Text = holder.PhoneNumber;
            tbAccount.Text = holder.Account;
            tbPassword.Password = holder.Password;

            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(holder.Description)));

            dpHolderDOB.SelectedDate = holder.DOB;
        }
        #endregion

        #region Background worker
        private void DelCardHolderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_CardHolder.DeleteCardHolder(holder.Id);
        }
        private void DelCardHolderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_CardHolder oldCardHolder = Repository.lstAllCardHolders.FirstOrDefault(c => c.Id == holder.Id);
                if (oldCardHolder != null)
                {
                    Repository.lstAllCardHolders.Remove(oldCardHolder);
                }

                if (workerReloadDept.IsBusy)
                {
                    workerReloadDept.CancelAsync();
                }
                else
                {
                    workerReloadDept.RunWorkerAsync();
                }

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetCardHolderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newHolder == null || newHolder.Id == "")
            {
                MessageBox.Show("Lỗi");
                return;
            }

            Repository.lstAllCardHolders.Add(newHolder);

            if (workerReloadDept.IsBusy)
            {
                workerReloadDept.CancelAsync();
            }
            else
            {
                workerReloadDept.RunWorkerAsync();
            }
        }
        private void GetCardHolderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newHolder = bUS_CardHolder.GetCardHolderByKey(result.ExtraData);
            if (newHolder.DepartmentId != null)
            {
                newHolder.Department = Repository.lstAllDepartment.FirstOrDefault(dept => dept.Id == newHolder.DepartmentId);
            }
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
                    if (workerReloadDept.IsBusy)
                    {
                        workerReloadDept.CancelAsync();
                    }
                    else
                    {
                        workerReloadDept.RunWorkerAsync();
                    }
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
        private void WorkerReloadDept_DoWork(object sender, DoWorkEventArgs e)
        {
            Repository.lstAllDepartment = bUS_Department.GetAllDepartment();
        }
        private void WorkerReloadDept_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = true;
            this.Hide();
        }
        #endregion
    }
}
