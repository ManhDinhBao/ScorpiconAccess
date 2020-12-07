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

        }

        private void itemDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void itemProperties_Click(object sender, RoutedEventArgs e)
        {
            DoorSocketDetailView view = new DoorSocketDetailView();
            view.ShowDialog();
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

                BindSocketToListItemView();
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
                        if (socket.ConnectedDevice == null)
                        {
                            listViewItem.ImageSource = "/Icon/c_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/c_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.LOCK:
                        if (socket.ConnectedDevice == null)
                        {
                            listViewItem.ImageSource = "/Icon/l_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/l_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.READER:
                        if (socket.ConnectedDevice == null)
                        {
                            listViewItem.ImageSource = "/Icon/e_character_gray_50px.png";
                        }
                        else
                        {
                            listViewItem.ImageSource = "/Icon/e_character_green_50px.png";
                        }
                        break;
                    case EType.DoorSocketType.REX:
                        if (socket.ConnectedDevice == null)
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
    }
}
