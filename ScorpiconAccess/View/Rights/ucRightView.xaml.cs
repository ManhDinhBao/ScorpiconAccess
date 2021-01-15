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
    /// Interaction logic for ucRightView.xaml
    /// </summary>
    public partial class ucRightView : UserControl
    {
        #region Constructor
        public string RightName { get; set; }
        public string Detail { get; set; }

        public DTO_UserRight MyRight { get; set; }

        public string PersonDetail { get; set; }
        public string DoorDetail { get; set; }
        public string ScheduleDetail { get; set; }
        public ucRightView()
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
            UpdateDoorDetail();
            UpdatePersonDetail();
            UpdateScheduleDetail();
        }

        #region Control click
        private void btShowPerson_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(2, MyRight.Id);
            if (view.ShowDialog() == true)
            {
                UpdatePersonDetail();
            }
        }

        private void btShowSchedule_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(1, MyRight.Id);
            if (view.ShowDialog() == true)
            {
                UpdateScheduleDetail();
            }
        }

        private void btShowDoor_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(0, MyRight.Id);
            if (view.ShowDialog() == true)
            {
                UpdateDoorDetail();
            }
        }

        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            wdRightDetail view = new wdRightDetail(EType.WindowMode.EDIT_MODE, MyRight);
            if (view.ShowDialog() == true)
            {
                if (view.funcClick == 1)
                {
                    DTO_UserRight updatedRight = Repository.lstAllRIght.FirstOrDefault(r => r.Id == MyRight.Id);
                    if (updatedRight != null)
                    {
                        MyRight = updatedRight;
                        RightName = updatedRight.Name;
                        Detail = updatedRight.Description;
                    }

                    BindingExpression binding = tbRightName.GetBindingExpression(TextBlock.TextProperty);
                    binding.UpdateTarget();

                    BindingExpression binding1 = tbRightDetail.GetBindingExpression(TextBlock.TextProperty);
                    binding1.UpdateTarget();
                }
                else
                {
                    this.DeleteClick(sender, e);
                }
            }
        }
        #endregion

        #region Custom methods
        private void UpdatePersonDetail()
        {
            switch (MyRight.CardHolders.Count)
            {
                case 0:
                    PersonDetail = "Chưa có người nào.";
                    break;
                case 1:
                    PersonDetail = MyRight.CardHolders[0].Name;
                    break;
                case 2:
                    PersonDetail = string.Format("Bao gồm {0} và {1}.", MyRight.CardHolders[0].Name, MyRight.CardHolders[1].Name);
                    break;
                default:
                    PersonDetail = string.Format("Bao gồm {0} \r\n{1} và {2} người khác.", MyRight.CardHolders[0].Name, MyRight.CardHolders[1].Name, MyRight.CardHolders.Count-2);
                    break;

            }

            BindingExpression binding = tbPersonDetail.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();
        }

        private void UpdateScheduleDetail()
        {
            switch (MyRight.Schedules.Count)
            {
                case 0:
                    ScheduleDetail = "Chưa có lịch nào.";
                    break;
                case 1:
                    ScheduleDetail = MyRight.Schedules[0].Name;
                    break;
                case 2:
                    ScheduleDetail = string.Format("Bao gồm {0} và {1}.", MyRight.Schedules[0].Name, MyRight.Schedules[1].Name);
                    break;
                default:
                    ScheduleDetail = string.Format("Bao gồm {0} \r\n{1} và {2} lịch khác.", MyRight.Schedules[0].Name, MyRight.Schedules[1].Name, MyRight.Schedules.Count-2);
                    break;

            }

            BindingExpression binding = tbScheduleDetail.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();
        }

        private void UpdateDoorDetail()
        {
            switch (MyRight.Doors.Count)
            {
                case 0:
                    DoorDetail = "Chưa có cửa nào.";
                    break;
                case 1:
                    DoorDetail = MyRight.Doors[0].Name;
                    break;
                case 2:
                    DoorDetail = string.Format("Bao gồm {0} và {1}.", MyRight.Doors[0].Name, MyRight.Doors[1].Name);
                    break;
                default:
                    DoorDetail = string.Format("Bao gồm {0} \r\n{1} và {2} cửa khác.", MyRight.Doors[0].Name, MyRight.Doors[1].Name, MyRight.Doors.Count -2);
                    break;

            }

            BindingExpression binding = tbDoorDetail.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();
        }
        #endregion
    }
}
