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
    /// Interaction logic for DoorDetailView.xaml
    /// </summary>
    public partial class DoorDetailView : UserControl
    {
        #region Variables
        BUS_Door bus_Door = new BUS_Door();
        BUS_DoorSocket bus_DoorSocket = new BUS_DoorSocket();
        List<DTO_ScorpionAccess.ListViewItem> listViewItems;

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int mode;
        #endregion

        public DoorDetailView(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        private void itemAdd_Click(object sender, RoutedEventArgs e)
        {
            DoorSocketDetailView view = new DoorSocketDetailView(ADD_MODE,null,Repository.selectedDoor.Id);
            if(view.ShowDialog()==true)
            {
                BindSocketToListItemView();
            }
        }

        private void itemDel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Confirm delete socket?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                DTO_ScorpionAccess.ListViewItem item = (DTO_ScorpionAccess.ListViewItem)lbSockets.SelectedItem;

                DTO_DoorSocket socket = Repository.selectedDoor.Sockets.FirstOrDefault(s => s.Id == item.Key);
                SQLResult sQLResult = bus_DoorSocket.DeleteSocket(socket.Id,socket.ConnectedDeviceSocketId);
                if (sQLResult.Result)
                {
                    Repository.selectedDoor.Sockets.Remove(socket);
                    BindSocketToListItemView();
                }
                else
                {
                    MessageBox.Show(sQLResult.Detail, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void itemProperties_Click(object sender, RoutedEventArgs e)
        {
            DTO_ScorpionAccess.ListViewItem item = (DTO_ScorpionAccess.ListViewItem)lbSockets.SelectedItem;
            if (item == null) return;
            DTO_DoorSocket socket = Repository.selectedDoor.Sockets.FirstOrDefault(s => s.Id == item.Key);
            DoorSocketDetailView view = new DoorSocketDetailView(CHANGE_MODE, socket, Repository.selectedDoor.Id);
            if (view.ShowDialog() == true)
            {
                BindSocketToListItemView();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbDoorMode.ItemsSource = Repository.lstAllDoorMode;
            cbDoorMode.DisplayMemberPath = "Name";
            cbDoorMode.SelectedValuePath = "Id";

            if (mode == ADD_MODE)
            {
                Repository.newDoor = new DTO_Door();

                cbDoorMode.SelectedIndex = 0;
                
            }
            else
            {
                cbDoorMode.SelectedValue = Repository.selectedDoor.Mode.Id;

                tbDoorId.Text = Repository.selectedDoor.Id;
                tbDoorName.Text = Repository.selectedDoor.Name;

                tbLOTimeOut.Text = Repository.selectedDoor.LOTimeOut.ToString();
                tbDOTimeOut.Text = Repository.selectedDoor.DOTimeOut.ToString();

                rtbDescription.Document.Blocks.Add(new Paragraph(new Run(Repository.selectedDoor.Description)));

                if (Repository.selectedDoor.Sockets != null)
                {
                    BindSocketToListItemView();
                }
               
            }
        }

        private void BindSocketToListItemView()
        {
            listViewItems = new List<DTO_ScorpionAccess.ListViewItem>();
            foreach (DTO_DoorSocket socket in Repository.selectedDoor.Sockets)
            {
                DTO_ScorpionAccess.ListViewItem listViewItem = new DTO_ScorpionAccess.ListViewItem();
                switch (socket.Type)
                {
                    case EType.DoorSocketType.CONTACT:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/c_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/c_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.LOCK:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/l_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/l_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.READER:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/e_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/e_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.REX:
                        if (socket.ConnectedDevice != null)
                        {
                            listViewItem.ImageSource = "/Icon/r_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/r_character_green_50px.png";
                        }
                        break;
                }
                
                listViewItem.TextBinding = socket.Name;
                listViewItem.Key = socket.Id;

                listViewItems.Add(listViewItem);
            }

            lbSockets.ItemsSource = listViewItems;
        }

        private void tbDoorId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDoor != null)
                    Repository.selectedDoor.Id = tbDoorId.Text;
            }
            else
            {
                if (Repository.newDoor != null)
                    Repository.newDoor.Id = tbDoorId.Text;
            }
        }

        private void tbDoorName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDoor != null)
                    Repository.selectedDoor.Name = tbDoorName.Text;
            }
            else
            {
                if (Repository.newDoor != null)
                    Repository.newDoor.Name = tbDoorName.Text;
            }
        }

        private void tbLOTimeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDoor != null)
                    Repository.selectedDoor.LOTimeOut = Convert.ToInt16(tbLOTimeOut.Text);
            }
            else
            {
                if (Repository.newDoor != null)
                    Repository.newDoor.LOTimeOut = Convert.ToInt16(tbLOTimeOut.Text);
            }
        }

        private void tbDOTimeOut_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDoor != null)
                    Repository.selectedDoor.DOTimeOut = Convert.ToInt16(tbDOTimeOut.Text);
            }
            else
            {
                if (Repository.newDoor != null)
                    Repository.newDoor.DOTimeOut = Convert.ToInt16(tbDOTimeOut.Text);
            }
        }

        private void cbDoorMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDoorMode.SelectedItem != null)
            {
                DTO_DoorMode doorMode = (DTO_DoorMode)cbDoorMode.SelectedItem;

                if (mode == CHANGE_MODE)
                {
                    if (Repository.selectedDoor != null)
                        Repository.selectedDoor.Mode = doorMode;
                }
                else
                {
                    if (Repository.newDoor != null)
                        Repository.newDoor.Mode = doorMode;
                }

            }
        }

        private void rtbDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (mode == CHANGE_MODE)
            {
                if (Repository.selectedDoor != null)
                    Repository.selectedDoor.Description = StringFromRichTextBox(rtbDescription).Trim();
            }
            else
            {
                if (Repository.newDoor != null)
                    Repository.newDoor.Description = StringFromRichTextBox(rtbDescription).Trim();
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
    }
}
