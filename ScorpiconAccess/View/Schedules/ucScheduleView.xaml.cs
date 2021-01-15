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
    /// Interaction logic for ucScheduleView.xaml
    /// </summary>
    public partial class ucScheduleView : UserControl
    {
        #region Constructor
        public DTO_Schedule MySchedule { get; set; }
        public string ScheduleName { get; set; }
        public ucScheduleView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Events
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler DeleteClick;
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPeriod();           
        }
        
        private void View_OnUpdate(object sender, EventArgs e)
        {
            LoadPeriod();
        }

        #region Custom methods
        private void LoadPeriod()
        {
            int i = 1;

            ScheduleName = MySchedule.Name;
            BindingExpression binding = tbScheduleName.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();

            if (MySchedule.Periods.Count <= 0)
            {
                TextBlock tb = new TextBlock();
                tb.Text = "Lịch này chưa có khoảng thời gian nào.";
                tb.Margin = new Thickness(0, 20, 0, 0);
                tb.FontSize = 10;
                tb.Foreground = Brushes.White;
                tb.FontWeight = FontWeights.SemiBold;
                tb.FontFamily = new FontFamily("Arial");

                pnlPeriod.Children.Clear();
                pnlPeriod.Children.Add(tb);
                return;
            }

            if (MySchedule.Periods != null)
            {
                pnlPeriod.Children.Clear();
                foreach (DTO_Period period in MySchedule.Periods)
                {
                    ucPeriodView view = new ucPeriodView();
                    view.PeriodName = "Period  " + i.ToString();
                    view.MyPeriod = period;
                    view.Detail = GenPeriodDetail(period);
                    view.OnUpdate += View_OnUpdate;

                    view.Margin = new Thickness(0, 5, 0, 0);

                    pnlPeriod.Children.Add(view);
                    i++;
                }
            }

        }
        private string GenPeriodDetail(DTO_Period MyPeriod)
        {
            string weekday = "";
            switch (MyPeriod.WeekDay)
            {
                case EType.WeekDay.MONDAY:
                    weekday = "Thứ 2";
                    break;
                case EType.WeekDay.TUESDAY:
                    weekday = "Thứ 3";
                    break;
                case EType.WeekDay.WEDNESDAY:
                    weekday = "Thứ 4";
                    break;
                case EType.WeekDay.THURSDAY:
                    weekday = "Thứ 5";
                    break;
                case EType.WeekDay.FRIDAY:
                    weekday = "Thứ 6";
                    break;
                case EType.WeekDay.SATURDAY:
                    weekday = "Thứ 7";
                    break;
                case EType.WeekDay.SUNDAY:
                    weekday = "CN";
                    break;
            }

            return string.Format("{0}  {1} - {2}", weekday, MyPeriod.StartTime.ToString("HH:mm"), MyPeriod.EndTime.ToString("HH:mm"));
        }
        #endregion

        #region Control click
        private void btAddPeriod_Click(object sender, RoutedEventArgs e)
        {
            DTO_Period period = new DTO_Period();
            period.Schedule = MySchedule.Id;

            wdPeriodDetail view = new wdPeriodDetail(EType.WindowMode.ADD_MODE,period);
            if (view.ShowDialog() == true)
            {
                LoadPeriod();
            }
        }
        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            wdScheduleDetail wdScheduleDetail = new wdScheduleDetail(EType.WindowMode.EDIT_MODE, MySchedule);
            if (wdScheduleDetail.ShowDialog() == true)
            {
                if(wdScheduleDetail.funcClick == 1)
                {
                    LoadPeriod();
                }
                else
                {
                    this.DeleteClick(sender, e);
                }
            }
        }
        #endregion
    }
}
