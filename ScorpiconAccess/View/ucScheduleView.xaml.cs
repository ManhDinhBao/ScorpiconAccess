using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
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
        public DTO_Schedule MySchedule { get; set; }
        public string ScheduleName { get; set; }
        public ucScheduleView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            int i = 1;
            if (MySchedule.Periods!=null && MySchedule.Periods.Count>0)
            {
                pnlPeriod.Children.Clear();
                foreach (DTO_Period period in MySchedule.Periods)
                {
                    ucPeriodView view = new ucPeriodView();
                    view.PeriodName = "Period " + i.ToString();
                    view.MyPeriod = period;
                    view.Detail = GenPeriodDetail(period);

                    view.Margin = new Thickness(0, 5, 0, 0);

                    pnlPeriod.Children.Add(view);
                    i++;
                }
            }
            

            ScheduleName = MySchedule.Name;
        }

        private string GenPeriodDetail(DTO_Period MyPeriod)
        {
            string weekday = "";
            switch (MyPeriod.WeekDay)
            {
                case EType.WeekDay.MONDAY:
                    weekday = "Mon";
                    break;
                case EType.WeekDay.TUESDAY:
                    weekday = "Tue";
                    break;
                case EType.WeekDay.WEDNESDAY:
                    weekday = "Wed";
                    break;
                case EType.WeekDay.THURSDAY:
                    weekday = "Thu";
                    break;
                case EType.WeekDay.FRIDAY:
                    weekday = "Fri";
                    break;
                case EType.WeekDay.SATURDAY:
                    weekday = "Sat";
                    break;
                case EType.WeekDay.SUNDAY:
                    weekday = "Sun";
                    break;
            }

            return string.Format("{0} {1} - {2}", weekday, MyPeriod.StartTime.ToString("HH:ss"), MyPeriod.EndTime.ToString("HH:ss"));
        }
    }
}
