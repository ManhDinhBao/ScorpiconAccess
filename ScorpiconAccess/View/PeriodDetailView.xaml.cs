using BUS_ScorpionAccess;
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
using System.Windows.Shapes;
using static DTO_ScorpionAccess.EType;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for PeriodDetailView.xaml
    /// </summary>
    public partial class PeriodDetailView : Window
    {
        private WeekDay weekDay;
        private string scheduleId;
        public static bool frmResult;
        BUS_Period bus_Period;

        public PeriodDetailView(string scheduleId, WeekDay weekDay)
        {
            InitializeComponent();
            this.weekDay = weekDay;
            this.scheduleId = scheduleId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bus_Period = new BUS_Period();
            frmResult = true;
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SQLResult result = bus_Period.AddNewPeriod(new DTO_Period("", weekDay, scheduleId, tbStartTime.Text, tbEndTime.Text));
            if (!result.Result)
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
                frmResult = false;
                return;
            }

            Repository.selectedSchedule.Periods = bus_Period.GetAllPeriodOfSchedule(Repository.selectedSchedule.Id);
            this.Hide();
        }
    }
}
