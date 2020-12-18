using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using Microsoft.Win32;
using ScorpiconAccess.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private BUS_CardHolder bus_CardHolder;
        private BUS_Device bus_Device;
        private BUS_DeviceSocket bUS_DeviceSocket;
        private BUS_Schedule bUS_Schedule;
        private BUS_Door bUS_Door;
        private BUS_Right bus_Right;

        private const int ADD_MODE = 1;
        private const int CHANGE_MODE = 2;

        private int currentMode;
        List<ListMenuItem> listViewItems;

        private bool isMaximize = false;


        public MainWindow()
        {
            InitializeComponent();
            bus_Card = new BUS_Card();
            bus_CardHolder = new BUS_CardHolder();
            bus_Device = new BUS_Device();
            bUS_DeviceSocket = new BUS_DeviceSocket();
            bUS_Schedule = new BUS_Schedule();
            bUS_Door = new BUS_Door();
            bus_Right = new BUS_Right();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btMinnimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (isMaximize)
            {
                this.WindowState = WindowState.Normal;
                this.BorderThickness = new Thickness(0);
                isMaximize = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.BorderThickness = new Thickness(10);
                isMaximize = true;
            }

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listViewItems = new List<ListMenuItem>()
            {
                new ListMenuItem("Card","/Icon/card_white_50px.png","Card"),
                new ListMenuItem("Card Holder","/Icon/holder_white_50px.png","Holder"),
                new ListMenuItem("Device","/Icon/device_white_50px.png","Device"),
                new ListMenuItem("Door","/Icon/door_white_50px.png","Door"),
                new ListMenuItem("Schedule","/Icon/schedule_white_50px.png","Schedule"),
                new ListMenuItem("Right","/Icon/right_white_50px.png","Right"),
                new ListMenuItem("Event","/Icon/event_white_30px.png","Event")
            };

            lbFunctions.ItemsSource = listViewItems;

            //Make Card screen is default
            lbFunctions.SelectedIndex = 0;
        }

        public void LoadCard()
        {
            panelData.Children.Clear();
            foreach (DTO_Card card in Repository.lstAllCards)
            {
                ucCardView view = new ucCardView();
                view.DeleteClick += CardView_DeleteClick;

                view.CardNumber = card.CardNumber;
                view.CardSerial = card.CardSerial;
                view.MyCard = card;

                if (card.Holder != null)
                {
                    string HolderName = Repository.lstAllCardHolders.FirstOrDefault(h => h.Id == card.Holder).Name;

                    if (HolderName != null)
                    {
                        view.Holder = HolderName;
                    }
                    else
                    {
                        view.Holder = card.Holder;
                    }
                }
                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void CardView_DeleteClick(object sender, EventArgs e)
        {
            LoadCard();
        }

        private void LoadHolder()
        {
            panelData.Children.Clear();
            foreach (DTO_CardHolder holder in Repository.lstAllCardHolders)
            {
                ucHolderView view = new ucHolderView();
                view.HolderName = holder.Name;
                view.HolderAddress = holder.Address;
                view.Gender = holder.Gender;
                view.MyHolder = holder;
                view.DeleteClick += HolderView_DeleteClick;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void HolderView_DeleteClick(object sender, EventArgs e)
        {
            LoadHolder();
        }

        private void LoadDevice()
        {
            panelData.Children.Clear();
            foreach (DTO_Device device in Repository.lstAllDevices)
            {
                ucDeviceView view = new ucDeviceView();
                view.DeviceIp = device.IP;
                view.DeviceName = device.Name;
                view.MyDevice = device;
                view.DeleteClick += DeviceView_DeleteClick;
                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void DeviceView_DeleteClick(object sender, EventArgs e)
        {
            LoadDevice();
        }

        private void LoadSchedule()
        {
            panelData.Children.Clear();
            foreach (DTO_Schedule schedule in Repository.lstAllSchedules)
            {
                ucScheduleView view = new ucScheduleView();
                view.ScheduleName = schedule.Name;
                view.MySchedule = schedule;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void LoadDoor()
        {
            panelData.Children.Clear();
            foreach (DTO_Door door in Repository.lstAllDoor)
            {
                ucDoorView view = new ucDoorView();
                view.MyDoor = door;
                view.DoorName = door.Name;
                view.DoorMode = door.Mode.Name;
                view.Detail =   door.Description;
                view.ModeNum = door.Mode.Id;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void LoadRight()
        {
            panelData.Children.Clear();
            foreach (DTO_UserRight right in Repository.lstAllRIght)
            {
                ucRightView view = new ucRightView();
                view.RightName = right.Name;
                view.Detail = right.Description;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void lbFunctions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListMenuItem selectedFunction = (ListMenuItem)lbFunctions.SelectedItem;

            if (selectedFunction != null)
            {
                switch (selectedFunction.Key)
                {
                    case "Card":
                        LoadCard();
                        tbFunctionName.Text = "Cards";
                        currentMode = 1;
                        break;
                    case "Holder":
                        LoadHolder();
                        tbFunctionName.Text = "Holders";
                        currentMode = 2;
                        break;
                    case "Device":
                        LoadDevice();
                        tbFunctionName.Text = "Devices";
                        currentMode = 3;
                        break;
                    case "Door":
                        LoadDoor();
                        tbFunctionName.Text = "Cards";
                        currentMode = 4;
                        break;
                    case "Schedule":
                        LoadSchedule();
                        tbFunctionName.Text = "Schedules";
                        currentMode = 5;
                        break;
                    case "Right":
                        LoadRight();
                        tbFunctionName.Text = "Rights";
                        currentMode = 6;
                        break;
                    case "Event":

                        tbFunctionName.Text = "Events";
                        currentMode = 7;
                        break;
                }
            }
        }

        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            switch (this.currentMode)
            {
                //Card
                case 1:
                    wdCardDetail view = new wdCardDetail(EType.WindowMode.ADD_MODE, null);
                    if(view.ShowDialog() == true)
                    {
                        LoadCard();
                    }
                    break;
                //Card Holder
                case 2:
                    wdCardHolderDetail view1 = new wdCardHolderDetail(EType.WindowMode.ADD_MODE, null);
                    if (view1.ShowDialog() == true)
                    {                       
                        LoadHolder();
                    }
                    break;
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btExport_Click(object sender, RoutedEventArgs e)
        {
            ExportFileWizard uc = new ExportFileWizard();
            uc.ShowDialog();
        }
    }
}
