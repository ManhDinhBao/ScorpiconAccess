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
    /// Interaction logic for wdScheduleDetail.xaml
    /// </summary>
    public partial class wdScheduleDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_Schedule bUS_Schedule;
        public DTO_Schedule schedule, newSchedule;
        public SQLResult result;
        public BackgroundWorker myScheduleWorker, getScheduleWorker, delScheduleWorker;
        #endregion

        #region Constructor
        public int funcClick { get; set; }

        public wdScheduleDetail(EType.WindowMode mode, DTO_Schedule schedule)
        {
            InitializeComponent();
            this.mode = mode;
            this.schedule = schedule;
        }
        #endregion

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 2;
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you want to delete this schedule?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (delScheduleWorker.IsBusy)
                {
                    delScheduleWorker.CancelAsync();
                }
                else
                {
                    delScheduleWorker.RunWorkerAsync();
                }
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 1;

            btSave.IsEnabled = false;

            schedule.Name = tbScheduleName.Text;
            schedule.Description = General.StringFromRichTextBox(rtbDescription).Trim();

            if (myScheduleWorker.IsBusy)
            {
                myScheduleWorker.CancelAsync();
            }
            else
            {
                myScheduleWorker.RunWorkerAsync();
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

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    schedule = new DTO_Schedule();
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
            tbScheduleName.Text = "";
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
        }
        private void BindData()
        {

            tbId.Text = schedule.Id;
            tbScheduleName.Text = schedule.Name;

            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(schedule.Description)));
        }
        private void InitData()
        {
            bUS_Schedule = new BUS_Schedule();

            myScheduleWorker = new BackgroundWorker();
            myScheduleWorker.WorkerSupportsCancellation = true;
            myScheduleWorker.DoWork += MyScheduleWorker_DoWork;
            myScheduleWorker.RunWorkerCompleted += MyScheduleWorker_RunWorkerCompleted;

            getScheduleWorker = new BackgroundWorker();
            getScheduleWorker.WorkerSupportsCancellation = true;
            getScheduleWorker.DoWork += GetScheduleWorker_DoWork;
            getScheduleWorker.RunWorkerCompleted += GetScheduleWorker_RunWorkerCompleted;

            delScheduleWorker = new BackgroundWorker();
            delScheduleWorker.WorkerSupportsCancellation = true;
            delScheduleWorker.DoWork += DelScheduleWorker_DoWork;
            delScheduleWorker.RunWorkerCompleted += DelScheduleWorker_RunWorkerCompleted;


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

        #region Background worker
        private void DelScheduleWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Schedule.DeleteSchedule(schedule.Id);
        }
        private void DelScheduleWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_Schedule oldSchedule = Repository.lstAllSchedules.FirstOrDefault(s => s.Id == schedule.Id);
                if (oldSchedule != null)
                {
                    Repository.lstAllSchedules.Remove(oldSchedule);
                }
                this.DialogResult = true;
                this.Hide();

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetScheduleWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newSchedule = bUS_Schedule.GetScheduleByKey(result.ExtraData);
        }
        private void GetScheduleWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newSchedule == null || newSchedule.Id == "")
            {

            }

            Repository.lstAllSchedules.Add(newSchedule);
            this.DialogResult = true;
            this.Hide();
        }
        private void MyScheduleWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Schedule.AddNewSchedule(schedule);
            }
            else
            {
                result = bUS_Schedule.UpdateSchedule(schedule);
            }
        }
        private void MyScheduleWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getScheduleWorker.IsBusy)
                    {
                        getScheduleWorker.CancelAsync();
                    }
                    else
                    {
                        getScheduleWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_Schedule oldSchedule = Repository.lstAllSchedules.FirstOrDefault(c => c.Id == schedule.Id);
                    if (oldSchedule != null)
                    {
                        oldSchedule = schedule;
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
        #endregion
    }
}
