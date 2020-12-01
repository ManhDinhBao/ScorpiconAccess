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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ScheduleDetailView.xaml
    /// </summary>
    public partial class ScheduleDetailView : UserControl
    {
        #region Variables
        BUS_Schedule bus_schedule = new BUS_Schedule();

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        #endregion

        public ScheduleDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == ADD_MODE)
            {
                Repository.newSchedule = new DTO_Schedule();
                tbScheduleId.IsReadOnly = false;
            }
            else
            {
                tbScheduleId.Text = Repository.selectedSchedule.Id;
                tbScheduleId.IsReadOnly = true;
                
                tbScheduleName.Text = Repository.selectedSchedule.Name;
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedSchedule.Description)));

                gridTabMondayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.MONDAY).ToList();

               // GenerateListItemCard();
            }
        }
    }
}
