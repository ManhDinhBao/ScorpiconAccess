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
    /// Interaction logic for ucEvent.xaml
    /// </summary>
    public partial class ucEvent : UserControl
    {
        public List<DTO_Event> lstEvent;
        public BUS_Event bUS_Event;
        public BackgroundWorker myWorker;
        public string myDepartmentId;

        public string holderName, doorName, cardNumber;
        public DateTime startTime, endTime;

        public ucEvent(string departmentId)
        {
            InitializeComponent();
            this.myDepartmentId = departmentId;
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            btSearch.IsEnabled = false;

            if (myWorker.IsBusy)
            {
                myWorker.CancelAsync();
            }
            else
            {
                myWorker.RunWorkerAsync();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();
            dpStartTime.SelectedDate = DateTime.Today;
            dpEndTime.SelectedDate = DateTime.Today.AddDays(1);
            gridEvents.ItemsSource = lstEvent;
        }

        private void dpStartTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.startTime = (DateTime)dpStartTime.SelectedDate;
        }

        private void dpEndTime_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.endTime = (DateTime)dpEndTime.SelectedDate;
        }

        private void tbCardNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.cardNumber = tbCardNumber.Text;
        }

        private void tbHolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.holderName = tbHolder.Text;
        }

        private void tbDoor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.doorName = tbDoor.Text;
        }

        private void btExportTimesheets_Click(object sender, RoutedEventArgs e)
        {
            DTO_Department dept = (DTO_Department)cbDepartment.SelectedItem;
            if (dept != null)
            {
                ExportFileAttendance view = new ExportFileAttendance(int.Parse(dept.Id));
                view.ShowDialog();
            }          
        }

        public void InitData()
        {
            bUS_Event = new BUS_Event();

            lstEvent = new List<DTO_Event>();

            cbDepartment.ItemsSource = Repository.lstAllDepartment;
            cbDepartment.DisplayMemberPath = "Name";
            cbDepartment.SelectedValuePath = "Id";

            if (!MainWindow.isAdmin)
            {
                cbDepartment.IsEnabled = false;
                cbDepartment.SelectedValue = myDepartmentId;
            }
            else
            {
                cbDepartment.SelectedIndex = 0;
            }

            myWorker = new BackgroundWorker();
            myWorker.WorkerReportsProgress = true;
            myWorker.WorkerSupportsCancellation = true;
            myWorker.DoWork += MyWorker_DoWork;
            myWorker.RunWorkerCompleted += MyWorker_RunWorkerCompleted;
        }

        private void MyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSearch.IsEnabled = true;

            if (lstEvent != null)
            {
                gridEvents.ItemsSource = lstEvent;
            }
        }

        private void MyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (cbDepartment.SelectedItem != null)
                {
                    DTO_Department dept = (DTO_Department)cbDepartment.SelectedItem;
                    lstEvent = bUS_Event.GetEvent(cardNumber, holderName, doorName, -1, int.Parse(dept.Id), startTime, endTime);
                }
            });
            
          
        }
    }
}
