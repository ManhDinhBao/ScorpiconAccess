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

        #region Extra Methods
        private void DeletePeriod(DataGrid dataGrid, EType.WeekDay weekDay)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Confirm delete period?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(messageBoxResult == MessageBoxResult.No)
            {
                return;
            }

            if (dataGrid.SelectedItem == null)
            {
                return;
            }
            DTO_Period period = (DTO_Period)dataGrid.SelectedItem;
            SQLResult result = bus_Period.DeletePeriod(period.Id);
            if (result.Result)
            {
                Repository.selectedSchedule.Periods.Remove(period);
                LoadGridData(weekDay);
            }
        }
        private void LoadGridData(EType.WeekDay weekDay)
        {
            switch (weekDay)
            {
                case EType.WeekDay.MONDAY:
                    gridTabMondayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.MONDAY).ToList();
                    break;
                case EType.WeekDay.TUESDAY:
                    gridTabTuesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.TUESDAY).ToList();
                    break;
                case EType.WeekDay.WEDNESDAY:
                    gridTabWednesdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.WEDNESDAY).ToList();
                    break;
                case EType.WeekDay.THURSDAY:
                    gridTabThursdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.THURSDAY).ToList();
                    break;
                case EType.WeekDay.FRIDAY:
                    gridTabFridayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.FRIDAY).ToList();
                    break;
                case EType.WeekDay.SATURDAY:
                    gridTabSaturdayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SATURDAY).ToList();
                    break;
                case EType.WeekDay.SUNDAY:
                    gridTabSundayPeriod.ItemsSource = Repository.selectedSchedule.Periods.Where(s => s.WeekDay == EType.WeekDay.SUNDAY).ToList();
                    break;

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
        private void ShowPeriodDetail(EType.WeekDay weekDay)
        {
            if (Repository.selectedSchedule != null)
            {
                PeriodDetailView view = new PeriodDetailView(Repository.selectedSchedule.Id, weekDay);
                if (view.ShowDialog() == true)
                {
                    LoadGridData(weekDay);
                }

            }
        }
        #endregion


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
                    LoadGridData(EType.WeekDay.MONDAY);
                    LoadGridData(EType.WeekDay.TUESDAY);
                    LoadGridData(EType.WeekDay.WEDNESDAY);
                    LoadGridData(EType.WeekDay.THURSDAY);
                    LoadGridData(EType.WeekDay.FRIDAY);
                    LoadGridData(EType.WeekDay.SATURDAY);
                    LoadGridData(EType.WeekDay.SUNDAY);
                }

                // GenerateListItemCard();
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

        #region Add Period
        private void itemMondayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.MONDAY);
            }
        }
        private void itemTuesdayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.TUESDAY);
            }
        }
        private void itemWednesdayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.WEDNESDAY);
            }
        }
        private void itemThursdayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.THURSDAY);
            }
        }
        private void itemFridayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.FRIDAY);
            }
        }
        private void itemSaturdayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.SATURDAY);
            }
        }
        private void itemSundayAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.selectedSchedule != null)
            {
                ShowPeriodDetail(EType.WeekDay.SUNDAY);
            }
        }
        #endregion

        #region Delete Period
        private void itemMondayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabMondayPeriod, EType.WeekDay.MONDAY);
        }
        private void itemTuesdayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabTuesdayPeriod, EType.WeekDay.TUESDAY);
        }
        private void itemWednesdayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabWednesdayPeriod, EType.WeekDay.WEDNESDAY);
        }
        private void itemThursdayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabThursdayPeriod, EType.WeekDay.THURSDAY);
        }
        private void itemFridayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabFridayPeriod, EType.WeekDay.FRIDAY);
        }
        private void itemSaturdayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabSaturdayPeriod, EType.WeekDay.SATURDAY);
        }
        private void itemSundayDel_Click(object sender, RoutedEventArgs e)
        {
            DeletePeriod(gridTabSundayPeriod, EType.WeekDay.SUNDAY);
        }
        #endregion

    }
}
