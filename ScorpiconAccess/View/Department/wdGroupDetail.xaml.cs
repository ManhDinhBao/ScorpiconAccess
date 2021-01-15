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
    /// Interaction logic for wdGroupDetail.xaml
    /// </summary>
    public partial class wdGroupDetail : Window
    {
        #region Variables
        public BUS_Department bUS_Department;
        public DTO_Department newGroup;
        public SQLResult result;
        public BackgroundWorker myDeptWorker;
        public DTO_Department parent;
        #endregion

        #region Constructor
        public wdGroupDetail(DTO_Department parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        #endregion

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            newGroup = new DTO_Department();
            newGroup.Name = tbGroupName.Text;
            newGroup.ParentId = parent.Id;

            if (myDeptWorker.IsBusy)
            {
                myDeptWorker.CancelAsync();
            }
            else
            {
                myDeptWorker.RunWorkerAsync();
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
            bUS_Department = new BUS_Department();

            myDeptWorker = new BackgroundWorker();
            myDeptWorker.WorkerSupportsCancellation = true;
            myDeptWorker.DoWork += MyDoorWorker_DoWork;
            myDeptWorker.RunWorkerCompleted += MyDoorWorker_RunWorkerCompleted;
        }

        #region Background worker
        private void MyDoorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Department.AddNewGroup(newGroup);
        }
        private void MyDoorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            parent.Groups.Add(newGroup);

            DTO_Department oldDepartment = Repository.lstAllDepartment.FirstOrDefault(dept => dept.Id == parent.Id);
            if (oldDepartment != null)
            {
                oldDepartment = parent;
            }

            this.DialogResult = true;
            this.Hide();
        }        
        #endregion
    }
}
