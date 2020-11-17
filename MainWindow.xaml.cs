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

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BUS_Card bus_Card; 
        private ViewMode viewMode;

        public MainWindow()
        {
            InitializeComponent();
            viewMode = ViewMode.NULL;
            bus_Card = new BUS_Card();

            PrepareData();
        }

        private void btCard_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.CARD_VIEW);
        }

        private void btHolder_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.HOLDER_VIEW);
        }

        private void btDevice_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.DEVICE_VIEW);
        }

        private void btDoor_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.DOOR_VIEW);
        }

        private void btSchedule_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.SCHEDULE_VIEW);
        }

        private void btEvent_Click(object sender, RoutedEventArgs e)
        {
            UpdateView(ViewMode.EVENT_VIEW);
        }

        private void UpdateView(ViewMode viewMode)
        {
            this.viewMode = viewMode;
            switch (viewMode)
            {
                case ViewMode.CARD_VIEW:
                    tbViewName.Text = "Card";
                    trvListItem.Items.Clear();
                    foreach(DTO_Card card in Repository.lstAllCards)
                    {
                        TreeViewItem item = new TreeViewItem();

                        StackPanel stack = new StackPanel();
                        stack.Orientation = Orientation.Horizontal;

                        Image image = new Image();
                        image.Source = new BitmapImage
                            (new Uri("pack://application:,,/Icon/card_gray_50px.png"));
                        image.Width = 16;
                        image.Height = 16;

                        Label lbl = new Label();
                        lbl.Content = card.CardSerial;

                        stack.Children.Add(image);
                        stack.Children.Add(lbl);

                        item.Header = stack;

                        trvListItem.Items.Add(item);
                    }
                    break;
                case ViewMode.HOLDER_VIEW:
                    tbViewName.Text = "Holder";
                    break;
                case ViewMode.DEVICE_VIEW:
                    tbViewName.Text = "Device";
                    break;
                case ViewMode.DOOR_VIEW:
                    tbViewName.Text = "Door";
                    break;
                case ViewMode.SCHEDULE_VIEW:
                    tbViewName.Text = "Schedule";
                    break;
                case ViewMode.RIGHT_VIEW:
                    tbViewName.Text = "Right";
                    break;
                case ViewMode.EVENT_VIEW:
                    tbViewName.Text = "Event";
                    break;
            }
        }

        private void btRight_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrepareData()
        {
            Repository.lstAllCards = bus_Card.GetAllCard();
        }
    }

    public enum ViewMode
    {
        NULL = 0,
        CARD_VIEW = 1,
        HOLDER_VIEW = 2,
        DEVICE_VIEW = 3,
        DOOR_VIEW = 4,
        SCHEDULE_VIEW = 5,
        RIGHT_VIEW = 6,
        EVENT_VIEW = 7
    }
}
