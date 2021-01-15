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
    /// Interaction logic for wdEmployeeDetail.xaml
    /// </summary>
    public partial class wdEmployeeDetail : Window
    {
        #region Variables
        public BUS_CardHolder bus_CardHolder;
        public BUS_Department bUS_Department;
        public BackgroundWorker worker;
        public List<DTO_CardHolder> lstSelectedHolder;
        public DTO_Department parent;
        public List<ListDetailItem> lstNonDeptUsers;
        #endregion

        public wdEmployeeDetail(DTO_Department parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }
        private void InitData()
        {
            bus_CardHolder = new BUS_CardHolder();
            bUS_Department = new BUS_Department();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            tbTitle.Text = "Lựa chọn nhân viên cho " + parent.Name;

            //Get employee not in this department
            lstNonDeptUsers = new List<ListDetailItem>();

            List<DTO_CardHolder> nonDeptUsers = Repository.lstAllCardHolders.Where(u => u.Department == null || u.Department.Id != parent.Id).ToList();
            if (nonDeptUsers != null && nonDeptUsers.Count > 0)
            {
                foreach (DTO_CardHolder holder in nonDeptUsers)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = holder.Name;
                    item.Key = holder.Id;
                    item.IsChecked = false;

                    lstNonDeptUsers.Add(item);
                }

                lbListEmp.ItemsSource = lstNonDeptUsers;
            }
        }

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
            else
            {
                worker.RunWorkerAsync();
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

        #region Background worker
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Get all selected employees
            List<ListDetailItem> items = lstNonDeptUsers.Where(i => i.IsChecked == true).ToList();

            //Update department of employees(card holder)
            if (items != null && items.Count > 0)
            {
                foreach (ListDetailItem item in items)
                {
                    DTO_CardHolder selectedHOlder = Repository.lstAllCardHolders.FirstOrDefault(u => u.Id == item.Key);
                    if (selectedHOlder != null)
                    {
                        selectedHOlder.DepartmentId = parent.Id;
                        selectedHOlder.Department = bUS_Department.GetDepartmentById(selectedHOlder.DepartmentId);
                        bus_CardHolder.UpdateCardHolder(selectedHOlder);
                    }
                }
            }
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = true;
            this.Hide();
        }
        #endregion

    }
}
