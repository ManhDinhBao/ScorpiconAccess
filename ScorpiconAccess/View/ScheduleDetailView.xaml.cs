using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
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
        BUS_Period bus_Period = new BUS_Period();
        BUS_General bUS_General = new BUS_General();

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        #endregion

        public ScheduleDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        #region Tab Monday Period
        private void tbTabMondaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabMondayCalHours.Text = CalculateDate(tbTabMondaySTime.Text, tbTabMondayETime.Text).ToString("hh");
            tbTabMondayCalMins.Text = CalculateDate(tbTabMondaySTime.Text, tbTabMondayETime.Text).ToString("mm");
        }

        private void tbTabMondayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabMondayCalHours.Text = CalculateDate(tbTabMondaySTime.Text, tbTabMondayETime.Text).ToString("hh");
            tbTabMondayCalMins.Text = CalculateDate(tbTabMondaySTime.Text, tbTabMondayETime.Text).ToString("mm");
        }
        private void btTabMondayAddPeriod_Click(object sender, RoutedEventArgs e)
        {
            SQLResult result = bus_Period.AddNewPeriod(new DTO_Period("", EType.WeekDay.MONDAY, tbScheduleId.Text, tbTabMondaySTime.Text, tbTabMondayETime.Text));
            if (!result.Result)
            {
                MessageBox.Show(result.Detail,"" , MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Repository.selectedSchedule.Periods = bus_Period.GetAllPeriodOfSchedule(Repository.selectedSchedule.Id);
            
            ReloadPeriod();
            gridTabMondayPeriod.SelectedIndex = gridTabMondayPeriod.Items.Count - 1;
        }

        private void btTabMondayUptPeriod_Click(object sender, RoutedEventArgs e)
        {
            DTO_Period period = (DTO_Period)gridTabMondayPeriod.SelectedItem;
            period.StartTime = tbTabMondaySTime.Text;
            period.EndTime = tbTabMondayETime.Text;
            SQLResult result = bus_Period.UpdatePeriod(period);

            ReloadPeriod();
        }

        private void btTabMondayDelPeriod_Click(object sender, RoutedEventArgs e)
        {
            DTO_Period period = (DTO_Period)gridTabMondayPeriod.SelectedItem;
            SQLResult result = bus_Period.DeletePeriod(period.Id);
            if (result.Result)
            {
                Repository.selectedSchedule.Periods.Remove(period);
            }


            ReloadPeriod();
            gridTabMondayPeriod.SelectedIndex = gridTabMondayPeriod.Items.Count - 1;
        }


        #endregion

        #region Tab Tuesday Period
        private void tbTabTuesdaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabTuesdayCalHours.Text = CalculateDate(tbTabTuesdaySTime.Text, tbTabTuesdayETime.Text).ToString("hh");
            tbTabTuesdayCalMins.Text = CalculateDate(tbTabTuesdaySTime.Text, tbTabTuesdayETime.Text).ToString("mm");
        }

        private void tbTabTuesdayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabTuesdayCalHours.Text = CalculateDate(tbTabTuesdaySTime.Text, tbTabTuesdayETime.Text).ToString("hh");
            tbTabTuesdayCalMins.Text = CalculateDate(tbTabTuesdaySTime.Text, tbTabTuesdayETime.Text).ToString("mm");
        }
        #endregion

        #region Tab Wednesday
        private void tbTabWednesdaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabWednesdayCalHours.Text = CalculateDate(tbTabWednesdaySTime.Text, tbTabWednesdayETime.Text).ToString("hh");
            tbTabWednesdayCalMins.Text = CalculateDate(tbTabWednesdaySTime.Text, tbTabWednesdayETime.Text).ToString("mm");
        }

        private void tbTabWednesdayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabWednesdayCalHours.Text = CalculateDate(tbTabWednesdaySTime.Text, tbTabWednesdayETime.Text).ToString("hh");
            tbTabWednesdayCalMins.Text = CalculateDate(tbTabWednesdaySTime.Text, tbTabWednesdayETime.Text).ToString("mm");
        }
        #endregion

        #region Tab Thurday Period
        private void tbTabThursdaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabThursdayCalHours.Text = CalculateDate(tbTabThursdaySTime.Text, tbTabThursdayETime.Text).ToString("hh");
            tbTabThursdayCalMins.Text = CalculateDate(tbTabThursdaySTime.Text, tbTabThursdayETime.Text).ToString("mm");
        }

        private void tbTabThursdayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabThursdayCalHours.Text = CalculateDate(tbTabThursdaySTime.Text, tbTabThursdayETime.Text).ToString("hh");
            tbTabThursdayCalMins.Text = CalculateDate(tbTabThursdaySTime.Text, tbTabThursdayETime.Text).ToString("mm");
        }
        #endregion

        #region Tab Friday Period
        private void tbTabFridaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabFridayCalHours.Text = CalculateDate(tbTabFridaySTime.Text, tbTabFridayETime.Text).ToString("hh");
            tbTabFridayCalMins.Text = CalculateDate(tbTabFridaySTime.Text, tbTabFridayETime.Text).ToString("mm");
        }

        private void tbTabFridayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabFridayCalHours.Text = CalculateDate(tbTabFridaySTime.Text, tbTabFridayETime.Text).ToString("hh");
            tbTabFridayCalMins.Text = CalculateDate(tbTabFridaySTime.Text, tbTabFridayETime.Text).ToString("mm");
        }

        #endregion

        #region Tab Saturday Period
        private void tbTabSaturdaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabSaturdayCalHours.Text = CalculateDate(tbTabSaturdaySTime.Text, tbTabSaturdayETime.Text).ToString("hh");
            tbTabSaturdayCalMins.Text = CalculateDate(tbTabSaturdaySTime.Text, tbTabSaturdayETime.Text).ToString("mm");
        }

        private void tbTabSaturdayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabSaturdayCalHours.Text = CalculateDate(tbTabSaturdaySTime.Text, tbTabSaturdayETime.Text).ToString("hh");
            tbTabSaturdayCalMins.Text = CalculateDate(tbTabSaturdaySTime.Text, tbTabSaturdayETime.Text).ToString("mm");
        }
        #endregion

        #region Tab Sunday Period
        private void tbTabSundaySTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabSundayCalHours.Text = CalculateDate(tbTabSundaySTime.Text, tbTabSundayETime.Text).ToString("hh");
            tbTabSundayCalMins.Text = CalculateDate(tbTabSundaySTime.Text, tbTabSundayETime.Text).ToString("mm");
        }

        private void tbTabSundayETime_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbTabSundayCalHours.Text = CalculateDate(tbTabSundaySTime.Text, tbTabSundayETime.Text).ToString("hh");
            tbTabSundayCalMins.Text = CalculateDate(tbTabSundaySTime.Text, tbTabSundayETime.Text).ToString("mm");
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbTabMondayCalHours.Text = "00";
            tbTabMondayCalMins.Text = "00";

            tbTabTuesdayCalHours.Text = "00";
            tbTabTuesdayCalMins.Text = "00";

            tbTabWednesdayCalHours.Text = "00";
            tbTabWednesdayCalMins.Text = "00";

            tbTabThursdayCalHours.Text = "00";
            tbTabThursdayCalMins.Text = "00";

            tbTabFridayCalHours.Text = "00";
            tbTabFridayCalMins.Text = "00";

            tbTabSaturdayCalHours.Text = "00";
            tbTabSaturdayCalMins.Text = "00";

            tbTabSundayCalHours.Text = "00";
            tbTabSundayCalMins.Text = "00";

            tbScheduleId.IsReadOnly = true;

            if (mode == ADD_MODE)
            {
                Repository.newSchedule = new DTO_Schedule();

                tbScheduleId.Text = bUS_General.GenerateId("SCHEDULE");                
            }
            else
            {
                tbScheduleId.Text = Repository.selectedSchedule.Id;

                tbScheduleName.Text = Repository.selectedSchedule.Name;
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedSchedule.Description)));

                if (Repository.selectedSchedule.Periods != null)
                {
                    gridTabMondayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.MONDAY).ToList();
                    if (gridTabMondayPeriod.Items.Count > 0)
                    {
                        gridTabMondayPeriod.SelectedIndex = 0;
                    }

                    gridTabTuesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.TUESDAY).ToList();
                    if (gridTabTuesdayPeriod.Items.Count > 0)
                    {
                        gridTabTuesdayPeriod.SelectedIndex = 0;
                    }

                    gridTabWednesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.WEDNESDAY).ToList();
                    if (gridTabWednesdayPeriod.Items.Count > 0)
                    {
                        gridTabWednesdayPeriod.SelectedIndex = 0;
                    }

                    gridTabThursdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.THURSDAY).ToList();
                    if (gridTabThursdayPeriod.Items.Count > 0)
                    {
                        gridTabThursdayPeriod.SelectedIndex = 0;
                    }

                    gridTabFridayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.FRIDAY).ToList();
                    if (gridTabFridayPeriod.Items.Count > 0)
                    {
                        gridTabFridayPeriod.SelectedIndex = 0;
                    }

                    gridTabSaturdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SATURDAY).ToList();
                    if (gridTabSaturdayPeriod.Items.Count > 0)
                    {
                        gridTabSaturdayPeriod.SelectedIndex = 0;
                    }

                    gridTabSundayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SUNDAY).ToList();
                    if (gridTabSundayPeriod.Items.Count > 0)
                    {
                        gridTabSundayPeriod.SelectedIndex = 0;
                    }
                }

                // GenerateListItemCard();
            }
        }

        private TimeSpan CalculateDate(string strStartDate, string strEndDate)
        {
            TimeSpan timeSpan;
            try
            {
                string[] arrStartDate = strStartDate.Split(':');
                string[] arrEndDate = strEndDate.Split(':');
                if (arrStartDate.Count() < 3 || arrEndDate.Count() < 3)
                {
                    return new TimeSpan(00, 00, 00);
                }

                DateTime sDate = new DateTime(2020, 01, 01, Convert.ToInt16(arrStartDate[0]), Convert.ToInt16(arrStartDate[1]), Convert.ToInt16(arrStartDate[2]));
                DateTime eDate = new DateTime(2020, 01, 01, Convert.ToInt16(arrEndDate[0]), Convert.ToInt16(arrEndDate[1]), Convert.ToInt16(arrEndDate[2]));

                timeSpan = eDate.Subtract(sDate);
            }
            catch
            {
                return new TimeSpan(00, 00, 00);
            }

            return timeSpan;
        }

        private void ReloadPeriod()
        {
            TabItem selectedTab = (TabItem)tabPeriod.SelectedItem;
            DTO_Schedule schedule = new DTO_Schedule();      

            switch (selectedTab.Header)
            {
                case "Monday":
                    gridTabMondayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.MONDAY).ToList();
                    break;
                case "Tuesday":
                    gridTabTuesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.TUESDAY).ToList();                    
                    break;
                case "Wednesday":
                    gridTabWednesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.WEDNESDAY).ToList();
                    break;
                case "Thursday":
                    gridTabThursdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.THURSDAY).ToList();
                    break;
                case "Fri":
                    gridTabFridayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.FRIDAY).ToList();
                    break;
                case "Saturday":
                    gridTabSaturdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SATURDAY).ToList();
                    break;
                case "Sunday":
                    gridTabSundayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SUNDAY).ToList();
                    break;
            }
        }

        private void tbScheduleName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedSchedule != null)
                    Repository.selectedSchedule.Name = tbScheduleName.Text;
            }
            else
            {
                if (Repository.newSchedule != null)
                    Repository.newSchedule.Name = tbScheduleName.Text;
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedSchedule != null)
                    Repository.selectedSchedule.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
            else
            {
                if (Repository.newSchedule != null)
                    Repository.newSchedule.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
        }

        public string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }

        private void tbScheduleId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedSchedule != null)
                    Repository.selectedSchedule.Id = tbScheduleId.Text;
            }
            else
            {
                if (Repository.newSchedule != null)
                    Repository.newSchedule.Id = tbScheduleId.Text;
            }
        }
    }
}
