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
    /// Interaction logic for wdRightDetail.xaml
    /// </summary>
    public partial class wdRightDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_Right bUS_Right;
        public DTO_UserRight userRight, newUserRight;
        public SQLResult result;
        public BackgroundWorker myRightWorker, getRightWorker, delRightWorker;
        #endregion

        #region Constructor
        public int funcClick { get; set; }

        public wdRightDetail(EType.WindowMode mode, DTO_UserRight right)
        {
            InitializeComponent();
            this.mode = mode;
            this.userRight = right;
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    userRight = new DTO_UserRight();
                    ResetControl();
                    break;
                default:
                    BindData();
                    break;
            }
        }

        #region Custom methods
        private void ResetControl()
        {
            tbRightName.Text = "";
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
        }

        private void BindData()
        {

            tbId.Text = userRight.Id;
            tbRightName.Text = userRight.Name;

            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(userRight.Description)));
        }

        private void InitData()
        {
            bUS_Right = new BUS_Right();

            myRightWorker = new BackgroundWorker();
            myRightWorker.WorkerSupportsCancellation = true;
            myRightWorker.DoWork += MyRightWorker_DoWork;
            myRightWorker.RunWorkerCompleted += MyRightWorker_RunWorkerCompleted;

            getRightWorker = new BackgroundWorker();
            getRightWorker.WorkerSupportsCancellation = true;
            getRightWorker.DoWork += GetRightWorker_DoWork;
            getRightWorker.RunWorkerCompleted += GetRightWorker_RunWorkerCompleted;

            delRightWorker = new BackgroundWorker();
            delRightWorker.WorkerSupportsCancellation = true;
            delRightWorker.DoWork += DelRightWorker_DoWork;
            delRightWorker.RunWorkerCompleted += DelRightWorker_RunWorkerCompleted;

            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                btDelete.IsEnabled = false;
            }
            else
            {
                btDelete.IsEnabled = true;
            }

        }
        #endregion

        #region Backround worker
        private void DelRightWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_UserRight oldRight = Repository.lstAllRIght.FirstOrDefault(c => c.Id == userRight.Id);
                if (oldRight != null)
                {
                    Repository.lstAllRIght.Remove(oldRight);
                }

                this.DialogResult = true;
                this.Hide();

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DelRightWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Right.DeleteRight(userRight.Id);
        }
        private void GetRightWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newUserRight == null || newUserRight.Id == "")
            {

            }

            Repository.lstAllRIght.Add(newUserRight);
            this.DialogResult = true;
            this.Hide();
        }
        private void GetRightWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newUserRight = bUS_Right.GetRightByKey(result.ExtraData);
        }
        private void MyRightWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getRightWorker.IsBusy)
                    {
                        getRightWorker.CancelAsync();
                    }
                    else
                    {
                        getRightWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_UserRight oldRight = Repository.lstAllRIght.FirstOrDefault(c => c.Id == userRight.Id);
                    if (oldRight != null)
                    {
                        oldRight = userRight;
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
        private void MyRightWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Right.AddNewRight(userRight);
            }
            else
            {
                result = bUS_Right.UpdateRight(userRight);
            }
        }
        #endregion

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
            this.DialogResult = false;
            this.Hide();
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 1;

            btSave.IsEnabled = false;

            userRight.Name = tbRightName.Text;
            userRight.Description = General.StringFromRichTextBox(rtbDescription).Trim();

            if (myRightWorker.IsBusy)
            {
                myRightWorker.CancelAsync();
            }
            else
            {
                myRightWorker.RunWorkerAsync();
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 2;
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xóa quyền này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (delRightWorker.IsBusy)
                {
                    delRightWorker.CancelAsync();
                }
                else
                {
                    delRightWorker.RunWorkerAsync();
                }
            }
        }
        #endregion
    }
}
