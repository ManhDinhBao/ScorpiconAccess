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
    /// Interaction logic for wdPeriodDetail.xaml
    /// </summary>
    public partial class wdPeriodDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_Period bUS_Period;
        public DTO_Period period, newPeriod;
        public SQLResult result;
        public BackgroundWorker myPeriodWorker, getPeriodWorker, delPeriodWorker;
        #endregion

        #region Contructor
        public wdPeriodDetail(EType.WindowMode mode, DTO_Period period)
        {
            InitializeComponent();
            this.period = period;
            this.mode = mode;
        }
        #endregion

        #region Control Events
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
           
            string[] arrSTime = tbStartTime.Text.Split(':');
            string[] arrETime = tbEndTime.Text.Split(':');

            try
            {
                period.WeekDay = (EType.WeekDay)cbWeekDay.SelectedItem;
                period.StartTime = new DateTime(1970, 01, 01, int.Parse(arrSTime[0]), int.Parse(arrSTime[1]), int.Parse(arrSTime[2]));
                period.EndTime = new DateTime(1970, 01, 01, int.Parse(arrETime[0]), int.Parse(arrETime[1]), int.Parse(arrETime[2]));
            }
            catch
            {
                MessageBox.Show("Định dạng thời gian không đúng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DateTime.Compare(period.StartTime, period.EndTime)>=0)
            {
                MessageBox.Show("Thời gian bắt đầu không được lớn hơn thời gian kết thúc.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (myPeriodWorker.IsBusy)
            {
                myPeriodWorker.CancelAsync();
            }
            else
            {
                btSave.IsEnabled = false;
                myPeriodWorker.RunWorkerAsync();
            }

        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you want to delete this period?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (delPeriodWorker.IsBusy)
                {
                    delPeriodWorker.CancelAsync();
                }
                else
                {
                    delPeriodWorker.RunWorkerAsync();
                }
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();
                    cbWeekDay.SelectedIndex = 0;
                    tbId.Text = period.Schedule;
                    break;
                default:
                    BindData();
                    break;
            }
        }
        #endregion

        #region Extra
        private void BindData()
        {
            tbId.Text = period.Schedule;
            cbWeekDay.SelectedValue = period.WeekDay;
            tbStartTime.Text = period.StartTime.ToString("HH:mm:ss");
            tbEndTime.Text = period.EndTime.ToString("HH:mm:ss");
        }

        private void ResetControl()
        {
            tbStartTime.Text = "000000";
            tbEndTime.Text = "000000";
        }

        private void InitData()
        {
            bUS_Period = new BUS_Period();

            myPeriodWorker = new BackgroundWorker();
            myPeriodWorker.WorkerSupportsCancellation = true;
            myPeriodWorker.DoWork += MyPeriodWorker_DoWork;
            myPeriodWorker.RunWorkerCompleted += MyPeriodWorker_RunWorkerCompleted;

            getPeriodWorker = new BackgroundWorker();
            getPeriodWorker.WorkerSupportsCancellation = true;
            getPeriodWorker.DoWork += GetPeriodWorker_DoWork;
            getPeriodWorker.RunWorkerCompleted += GetPeriodWorker_RunWorkerCompleted;

            delPeriodWorker = new BackgroundWorker();
            delPeriodWorker.WorkerSupportsCancellation = true;
            delPeriodWorker.DoWork += DelPeriodWorker_DoWork;
            delPeriodWorker.RunWorkerCompleted += DelPeriodWorker_RunWorkerCompleted;

            cbWeekDay.ItemsSource = Enum.GetValues(typeof(EType.WeekDay));    
            
            if(this.mode == EType.WindowMode.ADD_MODE)
            {
                btDel.IsEnabled = false;
            }
            else
            {
                btDel.IsEnabled = true;
            }
        }
        #endregion

        #region Background worker

        private void DelPeriodWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                foreach(DTO_Schedule schedule in Repository.lstAllSchedules)
                {
                    DTO_Period pr = schedule.Periods.FirstOrDefault(p => p.Id == period.Id);
                    if (pr != null)
                    {
                        schedule.Periods.Remove(pr);
                    }
                }

                this.DialogResult = true;
                this.Hide();

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DelPeriodWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Period.DeletePeriod(period.Id);
        }

        private void GetPeriodWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newPeriod == null || newPeriod.Id == "")
            {

            }

            DTO_Schedule schedule = Repository.lstAllSchedules.FirstOrDefault(s => s.Id == newPeriod.Schedule);
            if (schedule != null)
            {
                schedule.Periods.Add(newPeriod);
            }

            this.DialogResult = true;
            this.Hide();
        }

        private void GetPeriodWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newPeriod = bUS_Period.GetPeriodByKey(result.ExtraData);
        }

        private void MyPeriodWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getPeriodWorker.IsBusy)
                    {
                        getPeriodWorker.CancelAsync();
                    }
                    else
                    {
                        getPeriodWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    DTO_Schedule schedule = Repository.lstAllSchedules.FirstOrDefault(c => c.Id == period.Schedule);
                    if (schedule != null)
                    {
                        DTO_Period oldPeriod = schedule.Periods.FirstOrDefault(p => p.Id == period.Id);
                        if (oldPeriod != null)
                        {
                            oldPeriod = period;
                        }
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

        private void MyPeriodWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Period.AddNewPeriod(period);
            }
            else
            {
                result = bUS_Period.UpdatePeriod(period);
            }
        }
        #endregion
    }
}
