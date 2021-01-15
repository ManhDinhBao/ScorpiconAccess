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
    /// Interaction logic for wdDepartmentDetail.xaml
    /// </summary>
    public partial class wdDepartmentDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_Department bUS_Department;
        public DTO_Department department, newDepartment;
        public SQLResult result;
        public BackgroundWorker myDeptWorker, getDeptWorker;
        #endregion

        #region Constructor
        public int funcClick { get; set; }
        public wdDepartmentDetail(EType.WindowMode mode, DTO_Department dept)
        {
            InitializeComponent();
            this.mode = mode;
            this.department = dept;
        }
        #endregion

        #region Control click event
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 1;
            btSave.IsEnabled = false;

            try
            {
                if (tbDeptName.Text.Count() <= 0)
                {
                    department.Name = null;
                }
                else
                {
                    department.Name = tbDeptName.Text;
                }

                department.Description = General.StringFromRichTextBox(rtbDescription).Trim();

                if (cbManager.SelectedItem != null)
                {
                    department.Manager = (DTO_CardHolder)cbManager.SelectedItem;
                }
                else
                {
                    department.Manager = null;
                }

                if (myDeptWorker.IsBusy)
                {
                    myDeptWorker.CancelAsync();
                }
                else
                {
                    myDeptWorker.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void MyDeptWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                department.ParentId = "0";
                result = bUS_Department.AddNewDepartment(department);
            }
            else
            {
                result = bUS_Department.UpdateDepartment(department);
            }
        }
        private void MyDeptWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getDeptWorker.IsBusy)
                    {
                        getDeptWorker.CancelAsync();
                    }
                    else
                    {
                        getDeptWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_Department oldDept = Repository.lstAllDepartment.FirstOrDefault(c => c.Id == department.Id);
                    if (oldDept != null)
                    {
                        oldDept.Manager = department.Manager;
                        oldDept.Description = department.Description;
                        oldDept.Name = department.Name;
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
        private void GetDeptWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newDepartment = bUS_Department.GetDepartmentById(result.ExtraData);
        }
        private void GetDeptWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newDepartment == null || newDepartment.Id == "")
            {
                MessageBox.Show("Không lấy được dữ liệu phòng ban/bộ phận vừa thêm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Repository.lstAllDepartment.Add(newDepartment);

            this.DialogResult = true;
            this.Hide();
        }
        #endregion

        #region Custom methods
        private void BindData()
        {
            tbId.Text = department.Id;
            tbDeptName.Text = department.Name;

            if (department.Manager != null)
            {
                cbManager.SelectedValue = department.Manager.Id;
            }

            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(department.Description)));
        }
        private void InitData()
        {
            bUS_Department = new BUS_Department();

            #region Define background worker
            myDeptWorker = new BackgroundWorker();
            myDeptWorker.WorkerSupportsCancellation = true;
            myDeptWorker.DoWork += MyDeptWorker_DoWork;
            myDeptWorker.RunWorkerCompleted += MyDeptWorker_RunWorkerCompleted;

            getDeptWorker = new BackgroundWorker();
            getDeptWorker.WorkerSupportsCancellation = true;
            getDeptWorker.DoWork += GetDeptWorker_DoWork;
            getDeptWorker.RunWorkerCompleted += GetDeptWorker_RunWorkerCompleted;
            #endregion            
        }
        private void ResetControl()
        {
            tbDeptName.Text = "";
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();
                    cbManager.SelectedIndex = 0;
                    cbManager.IsEnabled = false;
                    department = new DTO_Department();

                    
                    break;
                default:
                    //Get all employees in department
                    List<DTO_CardHolder> lstEmpsInDept = Repository.lstAllCardHolders.Where(c => c.Department != null && c.Department.RootDeptId == department.Id).ToList();
                    cbManager.ItemsSource = lstEmpsInDept;
                    cbManager.DisplayMemberPath = "Name";
                    cbManager.SelectedValuePath = "Id";
                    BindData();
                    break;
            }
        }

    }
}
