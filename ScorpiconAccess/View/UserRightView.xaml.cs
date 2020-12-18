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
    /// Interaction logic for UserRightView.xaml
    /// </summary>
    public partial class UserRightView : UserControl
    {
        #region Variables

        #endregion

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        public UserRightView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void tbRightId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedRight != null)
                    Repository.selectedRight.Id = tbRightId.Text;
            }
            else
            {
                if (Repository.newRight != null)
                    Repository.newRight.Id = tbRightId.Text;
            }
        }

        private void tbRightName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedRight != null)
                    Repository.selectedRight.Name = tbRightName.Text;
            }
            else
            {
                if (Repository.newRight != null)
                    Repository.newRight.Name = tbRightName.Text;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbRightId.IsReadOnly = true;

            if (mode == ADD_MODE)
            {
                Repository.newRight = new DTO_ScorpionAccess.DTO_UserRight();
            }
            else
            {
                tbRightId.Text = Repository.selectedRight.Id;
                tbRightName.Text = Repository.selectedRight.Name;
                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedRight.Description)));

                if (Repository.selectedRight.CardHolders.Count > 0)
                {
                    lbPersons.ItemsSource = Repository.selectedRight.CardHolders;
                    lbPersons.DisplayMemberPath = "Name";
                    lbPersons.SelectedValuePath = "Id";
                }

                if (Repository.selectedRight.Doors.Count > 0)
                {
                    lbDoors.ItemsSource = Repository.selectedRight.Doors;
                    lbDoors.DisplayMemberPath = "Name";
                    lbDoors.SelectedValuePath = "Id";
                }

                if (Repository.selectedRight.Schedules.Count > 0)
                {
                    lbSchedules.ItemsSource = Repository.selectedRight.Schedules;
                    lbSchedules.DisplayMemberPath = "Name";
                    lbSchedules.SelectedValuePath = "Id";
                }
                
            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedRight != null)
                    Repository.selectedRight.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
            else
            {
                if (Repository.newRight != null)
                    Repository.newRight.Description = StringFromRichTextBox(rtbDescription).Trim();
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

        private void itemAddPerson_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(2, Repository.selectedRight.Id);
            if(view.ShowDialog()==true)
            {
                lbPersons.Items.Refresh();
                lbPersons.UpdateLayout();
            }
        }

        private void itemAddDoor_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(0, Repository.selectedRight.Id);
            if (view.ShowDialog() == true)
            {
                lbDoors.Items.Refresh();
                lbDoors.UpdateLayout();
            }
        }

        private void itemAddSchedule_Click(object sender, RoutedEventArgs e)
        {
            RightDetailView view = new RightDetailView(1, Repository.selectedRight.Id);
            if (view.ShowDialog() == true)
            {
                lbSchedules.Items.Refresh();
                lbSchedules.UpdateLayout();
            }
        }
    }
}
