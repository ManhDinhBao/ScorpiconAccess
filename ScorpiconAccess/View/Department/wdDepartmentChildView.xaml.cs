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
    /// Interaction logic for wdDepartmentChildView.xaml
    /// </summary>
    public partial class wdDepartmentChildView : Window
    {
        #region Variables
        public BackgroundWorker workerDelDept,workerDelEmp, workerReloadDept;

        public BUS_CardHolder bUS_CardHolder;
        public BUS_Department bUS_Department;

        public DTO_CardHolder selectedEmp;
        public DTO_Department selectedDept;

        public SQLResult result;
        #endregion

        #region Constructor
        public List<DTO_Department> MyDepartment { get; set; }
        public wdDepartmentChildView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion
      
        #region Custom events
        private void InitData()
        {
            bUS_CardHolder = new BUS_CardHolder();
            bUS_Department = new BUS_Department();

            #region Init background worker
            workerDelDept = new BackgroundWorker();
            workerDelDept.WorkerSupportsCancellation = true;
            workerDelDept.DoWork += Worker_DoWork;
            workerDelDept.RunWorkerCompleted += Worker_RunWorkerCompleted;

            workerDelEmp = new BackgroundWorker();
            workerDelEmp.WorkerSupportsCancellation = true;
            workerDelEmp.DoWork += WorkerDelEmp_DoWork;
            workerDelEmp.RunWorkerCompleted += WorkerDelEmp_RunWorkerCompleted;

            workerReloadDept = new BackgroundWorker();
            workerReloadDept.WorkerSupportsCancellation = true;
            workerReloadDept.DoWork += WorkerReloadDept_DoWork; ;
            workerReloadDept.RunWorkerCompleted += WorkerReloadDept_RunWorkerCompleted;
            #endregion
        }
        private void UpdateTreeView()
        {
            tvDepartment.Items.Refresh();
            tvDepartment.UpdateLayout();
        }
        #endregion

        #region Background worker
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Department.DeleteDepartment(selectedDept.Id);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
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
        private void WorkerDelEmp_DoWork(object sender, DoWorkEventArgs e)
        {
            //Remove employee from department
            selectedDept = Repository.lstAllDepartment.FirstOrDefault(d => d.Id == selectedEmp.DepartmentId);
            if (selectedDept != null)
            {
                selectedDept.Employees.Remove(selectedEmp);
            }

            //Reset department of card holder
            selectedEmp = Repository.lstAllCardHolders.FirstOrDefault(d => d.Id == selectedEmp.Id);
            if (selectedEmp != null)
            {
                selectedEmp.Department.Id = null;
            }

            result = bUS_CardHolder.UpdateCardHolder(selectedEmp);
        }
        private void WorkerDelEmp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
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
        private void WorkerReloadDept_DoWork(object sender, DoWorkEventArgs e)
        {
            Repository.lstAllDepartment = bUS_Department.GetAllDepartment();
            
            MyDepartment[0] = Repository.lstAllDepartment.FirstOrDefault(d => d.Id == MyDepartment[0].Id);
        }
        private void WorkerReloadDept_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateTreeView();
        }
        #endregion

        #region Control click events
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Hide();
        }
        private void itemAddEmp_Click(object sender, RoutedEventArgs e)
        {
            DTO_Department dept;
            try
            {
                dept = (DTO_Department)tvDepartment.SelectedItem;

                if (dept == null)
                {
                    MessageBox.Show("Hãy lựa chọn nhóm hoặc bộ phận cần thêm.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            catch
            {
                return;
            }

            #region Show employee detail form
            wdEmployeeDetail view = new wdEmployeeDetail(dept);
            if (view.ShowDialog() == true)
            {
                if (workerReloadDept.IsBusy)
                {
                    workerReloadDept.CancelAsync();
                }
                else
                {
                    workerReloadDept.RunWorkerAsync();
                }
            }            
            #endregion
        }
        private void itemAddGroup_Click(object sender, RoutedEventArgs e)
        {
            DTO_Department dept;
            try
            {
                dept = (DTO_Department)tvDepartment.SelectedItem;

                if (dept == null)
                {
                    MessageBox.Show("Hãy lựa chọn nhóm hoặc bộ phận cần thêm.","",MessageBoxButton.OK,MessageBoxImage.Warning);
                    return;
                }
            }
            catch
            {
                return;
            }

            #region Show department group detail form
            wdGroupDetail view = new wdGroupDetail(dept);
            if (view.ShowDialog()==true)
            {

                if (workerReloadDept.IsBusy)
                {
                    workerReloadDept.CancelAsync();
                }
                else
                {
                    workerReloadDept.RunWorkerAsync();
                }
            }
            #endregion
        }
        private void itemDelete_Click(object sender, RoutedEventArgs e)
        {
            //Part 1: Delete department group
            try
            {
                selectedDept = (DTO_Department)tvDepartment.SelectedItem;

                //User not yet select group
                if (selectedDept == null)
                {
                    MessageBox.Show("Hãy lựa chọn đối tượng cần xóa.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    //Department still have some employee
                    if(selectedDept.Employees!=null && selectedDept.Employees.Count > 0)
                    {
                        MessageBox.Show("Cần xóa nhân viên bên trong phòng ban/bộ phận trước.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Department still have some group
                    if (selectedDept.Groups != null && selectedDept.Groups.Count > 0)
                    {
                        MessageBox.Show("Cần xóa nhóm bên trong phòng ban/bộ phận trước.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Delete group
                    MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng ban/bộ phận không?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (workerDelDept.IsBusy)
                        {
                            workerDelDept.CancelAsync();
                        }
                        else
                        {
                            workerDelDept.RunWorkerAsync();
                        }
                    }
                    return;
                }
            }
            catch
            {

            }


            //PART 2: Delete employee in department
            selectedEmp = (DTO_CardHolder)tvDepartment.SelectedItem;

            MessageBoxResult result1 = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên "+ selectedEmp.Name+ " không?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result1 == MessageBoxResult.Yes)
            {
                if (workerDelEmp.IsBusy)
                {
                    workerDelEmp.CancelAsync();
                }
                else
                {
                    workerDelEmp.RunWorkerAsync();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
        }
    }
}
