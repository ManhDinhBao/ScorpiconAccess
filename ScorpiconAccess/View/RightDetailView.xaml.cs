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

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for RightDetailView.xaml
    /// </summary>
    public partial class RightDetailView : Window
    {
        private int DOOR_MODE = 0;
        private int SCHEDULE_MODE = 1;
        private int PERSON_MODE = 2;

        private int mode;
        private string rightId;
        private DTO_UserRight right;

        BUS_Right bUS_Right = new BUS_Right();

        List<ListDetailItem> listRightDetails = new List<ListDetailItem>();
        public RightDetailView(int mode, string RightId)
        {
            InitializeComponent();
            this.mode = mode;
            this.rightId = RightId;

            right = Repository.lstAllRIght.FirstOrDefault(r => r.Id == rightId);
        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (mode == DOOR_MODE)
            {
               
                List<DTO_Door> oldDoors = right.Doors;
                foreach(DTO_Door door in oldDoors)
                {
                    bUS_Right.DeleteDoorInRight(rightId, door.Id);
                   
                }
                right.Doors.Clear();
                List<DTO_Door> newDoors = new List<DTO_Door>();

                foreach (ListDetailItem item in listRightDetails)
                {
                    if (item.IsChecked)
                    {
                        DTO_Door door = Repository.lstAllDoor.FirstOrDefault(d => d.Id == item.Key);
                        newDoors.Add(door);
                        right.Doors.Add(door);
                    }
                    
                }

                foreach (DTO_Door door in newDoors)
                {
                    bUS_Right.AddDoorToRight(rightId, door.Id);
                }

                Repository.lstAllRIght.FirstOrDefault(r => r.Id == rightId).Doors = right.Doors;
                Repository.selectedRight.Doors = right.Doors;
                this.DialogResult = true;
                this.Hide();
                return;
            }

            if (mode == SCHEDULE_MODE)
            {
                
                List<DTO_Schedule> oldSchedules = right.Schedules;
                foreach (DTO_Schedule schedule in oldSchedules)
                {
                    bUS_Right.DeleteScheduleInRight(rightId, schedule.Id);
                }
                right.Schedules.Clear();

                List<DTO_Schedule> newSchedules = new List<DTO_Schedule>();

                foreach (ListDetailItem item in listRightDetails)
                {
                    if (item.IsChecked)
                    {
                        DTO_Schedule schedule = Repository.lstAllSchedules.FirstOrDefault(d => d.Id == item.Key);
                        newSchedules.Add(schedule);
                    }
                    
                }

                right.Schedules = newSchedules;

                foreach (DTO_Schedule schedule in newSchedules)
                {
                    bUS_Right.AddScheduleToRight(rightId, schedule.Id);
                }
                Repository.lstAllRIght.FirstOrDefault(r => r.Id == rightId).Schedules = right.Schedules;
                Repository.selectedRight.Schedules = right.Schedules;
                this.DialogResult = true;
                this.Hide();
                return;
            }

            if (mode == PERSON_MODE)
            {
               
                List<DTO_CardHolder> oldPersons = right.CardHolders;
                foreach (DTO_CardHolder person in oldPersons)
                {
                    bUS_Right.DeletePersonInRight(rightId, person.Id);
                }
                right.CardHolders.Clear();
                List<DTO_CardHolder> newPersons = new List<DTO_CardHolder>();

                foreach (ListDetailItem item in listRightDetails)
                {
                    if (item.IsChecked)
                    {
                        DTO_CardHolder person = Repository.lstAllCardHolders.FirstOrDefault(d => d.Id == item.Key);
                        newPersons.Add(person);
                        right.CardHolders.Add(person);
                    }
                                        
                }

                foreach (DTO_CardHolder person in newPersons)
                {
                    bUS_Right.AddPersonToRight(rightId, person.Id);
                }
                Repository.lstAllRIght.FirstOrDefault(r => r.Id == rightId).CardHolders = right.CardHolders;
                Repository.selectedRight.CardHolders = right.CardHolders;
                this.DialogResult = true;
                this.Hide();

                return;
            }

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listRightDetails.Clear();
            if (mode == DOOR_MODE)
            {
                foreach(DTO_Door door in Repository.lstAllDoor)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = door.Name;
                    item.Key = door.Id;

                    DTO_Door d = right.Doors.FirstOrDefault(o => o.Id == door.Id);
                    if (d == null)
                    {
                        item.IsChecked = false;
                    }
                    else
                    {
                        item.IsChecked = true;
                        
                    }

                    listRightDetails.Add(item);
                }

                lbRightDetail.ItemsSource = listRightDetails;

                return;
            }

            if (mode == SCHEDULE_MODE)
            {
                foreach (DTO_Schedule schedule in Repository.lstAllSchedules)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = schedule.Name;
                    item.Key = schedule.Id;

                    DTO_Schedule d = right.Schedules.FirstOrDefault(o => o.Id == schedule.Id);
                    if (d == null)
                    {
                        item.IsChecked = false;
                    }
                    else
                        item.IsChecked = true;
                    listRightDetails.Add(item);
                }

                lbRightDetail.ItemsSource = listRightDetails;

                return;
            }

            if (mode == PERSON_MODE)
            {
                foreach (DTO_CardHolder hodler in Repository.lstAllCardHolders)
                {
                    ListDetailItem item = new ListDetailItem();
                    item.TextBinding = hodler.Name;
                    item.Key = hodler.Id;

                    DTO_CardHolder d = right.CardHolders.FirstOrDefault(o => o.Id == hodler.Id);
                    if (d == null)
                    {
                        item.IsChecked = false;
                    }
                    else
                        item.IsChecked = true;
                    listRightDetails.Add(item);
                }

                lbRightDetail.ItemsSource = listRightDetails;

                return;
            }
        }
    }
}
