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
using System.Windows.Threading;

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        private BUS_Card bus_Card;
        private BUS_CardHolder bus_CardHolder;
        private BUS_Device bus_Device;
        private BUS_DeviceSocket bUS_DeviceSocket;
        private BUS_Schedule bUS_Schedule;
        private BUS_Door bUS_Door;
        private BUS_Right bus_Right;
        private BackgroundWorker wokerLoadAllData,workerSearch;

        private List<DTO_Card> lstFilterCard;
        private List<DTO_CardHolder> lstFilterCardHolder;
        private List<DTO_Department> lstFilterDepartment;
        private List<DTO_Device>     lstFilterDevice;
        private List<DTO_Door>       lstFilterDoor;
        private List<DTO_Schedule>   lstFilterSchedule;
        private List<DTO_UserRight>  lstFilterRight;

        private int currentMode;
        private 
        List<ListMenuItem> listViewItems;

        public DTO_CardHolder LoginUser { get; set; }

        public string LoginUserName { get; set; }
        public string LoginUserDetail { get; set; }
        public string LoginUserLetter { get; set; }

        private bool isMaximize = false;
        public static bool isAdmin = false;
        #endregion

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

            lstFilterCard = new List<DTO_Card>();
            lstFilterDepartment = new List<DTO_Department>();
            lstFilterDevice = new List<DTO_Device>();
            lstFilterDoor = new List<DTO_Door>();
            lstFilterSchedule = new List<DTO_Schedule>();
            lstFilterRight = new List<DTO_UserRight>();


            this.DataContext = this;
        }

        #region Control Events
        #region Clicks
        private void btClose_Click(object sender, RoutedEventArgs e)
            {
                System.Windows.Application.Current.Shutdown();
            }
        private void btExport_Click(object sender, RoutedEventArgs e)
        {
            ExportFileWizard uc = new ExportFileWizard();
            uc.ShowDialog();
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
        private void btMinnimize_Click(object sender, RoutedEventArgs e)
            {
                this.WindowState = WindowState.Minimized;
            }
        private void btNew_Click(object sender, RoutedEventArgs e)
        {
            switch (this.currentMode)
            {
                //Card
                case 1:
                    wdCardDetail view = new wdCardDetail(EType.WindowMode.ADD_MODE, null);
                    if (view.ShowDialog() == true)
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
                //Device
                case 3:
                    wdDeviceDetail view2 = new wdDeviceDetail(EType.WindowMode.ADD_MODE, null);
                    if (view2.ShowDialog() == true)
                    {
                        LoadDevice();
                    }
                    break;
                //Door
                case 4:
                    wdDoorDetail view3 = new wdDoorDetail(EType.WindowMode.ADD_MODE, null);
                    if (view3.ShowDialog() == true)
                    {
                        LoadDoor();
                    }
                    break;
                //Schedule
                case 5:
                    wdScheduleDetail view4 = new wdScheduleDetail(EType.WindowMode.ADD_MODE, null);
                    if (view4.ShowDialog() == true)
                    {
                        LoadSchedule();
                    }
                    break;
                //Right
                case 6:
                    wdRightDetail view5 = new wdRightDetail(EType.WindowMode.ADD_MODE, null);
                    if (view5.ShowDialog() == true)
                    {
                        LoadRight();
                    }
                    break;
                //Department
                case 8:
                    wdDepartmentDetail view6 = new wdDepartmentDetail(EType.WindowMode.ADD_MODE, null);
                    if (view6.ShowDialog() == true)
                    {
                        LoadDepartment();
                    }
                    break;
            }
        }
        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {
            wokerLoadAllData = new BackgroundWorker();
            wokerLoadAllData.WorkerSupportsCancellation = true;
            wokerLoadAllData.DoWork += WokerLoadAllData_DoWork;
            wokerLoadAllData.RunWorkerCompleted += WokerLoadAllData_RunWorkerCompleted;

            if (wokerLoadAllData.IsBusy)
            {
                wokerLoadAllData.CancelAsync();
            }
            else
            wokerLoadAllData.RunWorkerAsync();

        }
        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            wdAboutView view = new wdAboutView();
            view.ShowDialog();
        }

        private void WokerLoadAllData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            MessageBox.Show("Làm mới dữ liệu thành công.!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void WokerLoadAllData_DoWork(object sender, DoWorkEventArgs e)
        {
                switch (this.currentMode)
                {
                    //Card
                    case 1:
                    Repository.lstAllCards = bus_Card.GetAllCard();
                    lstFilterCard = Repository.lstAllCards;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadCard();
                    });
                    break;
                    //Card Holder
                    case 2:
                    Repository.lstAllCardHolders = bus_CardHolder.GetAllCardHolder();
                    lstFilterCardHolder = Repository.lstAllCardHolders;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadHolder();
                    });
                    break;
                    //Device
                    case 3:
                    Repository.lstAllDevices = bus_Device.GetAllDevice();
                    lstFilterDevice = Repository.lstAllDevices;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadDevice();
                    });
                    break;
                    //Door
                    case 4:
                    Repository.lstAllDoor = bUS_Door.GetAllDoor();
                    lstFilterDoor = Repository.lstAllDoor;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadDoor();
                    });
                    break;
                    //Schedule
                    case 5:
                    Repository.lstAllSchedules = bUS_Schedule.GetAllSchedule();
                    lstFilterSchedule = Repository.lstAllSchedules;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadSchedule();
                    });
                    break;
                    //Schedule
                    case 6:
                    Repository.lstAllRIght = bus_Right.GetAllUserRight();
                    lstFilterRight = Repository.lstAllRIght;
                    this.Dispatcher.Invoke(() =>
                    {
                        LoadRight();
                    });
                    break;
                }
            }
        #endregion

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                workerSearch = new BackgroundWorker();
                workerSearch.WorkerSupportsCancellation = true;
                workerSearch.DoWork += WorkerSearch_DoWork;
                workerSearch.RunWorkerCompleted += WorkerSearch_RunWorkerCompleted;

                if (workerSearch.IsBusy)
                {
                    workerSearch.CancelAsync();
                }
                else
                {
                    workerSearch.RunWorkerAsync();
                }
            }
        }

        private void WorkerSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (this.currentMode)
            {
                case 1:
                    LoadCard();
                    break;
                case 2:
                    LoadHolder();
                    break;
                case 3:
                    LoadDevice();
                    break;
                case 4:
                    LoadDoor();
                    break;
                case 5:
                    LoadSchedule();
                    break;
                case 6:
                    LoadRight();
                    break;
                case 8:
                    LoadDepartment();
                    break;
            }
        }

        private void WorkerSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {

                switch (this.currentMode)
                {
                    case 1:
                        if (tbSearch.Text == "")
                        {
                            lstFilterCard = Repository.lstAllCards;
                        }
                        else
                        {
                            lstFilterCard = Repository.lstAllCards.Where(c => (c.CardNumber == tbSearch.Text) || (c.CardSerial.Contains(tbSearch.Text))).ToList();
                        }
                        break;
                    case 2:
                        if (tbSearch.Text == "")
                        {
                            lstFilterCardHolder = Repository.lstAllCardHolders;
                        }
                        else
                        {
                            lstFilterCardHolder = Repository.lstAllCardHolders.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower())) || (c.PhoneNumber == tbSearch.Text)).ToList();
                        }
                        break;
                    case 3:
                        if (tbSearch.Text == "")
                        {
                            lstFilterDevice = Repository.lstAllDevices;
                        }
                        else
                        {
                            lstFilterDevice = Repository.lstAllDevices.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower())) || (c.IP == tbSearch.Text)).ToList();
                        }
                        break;
                    case 4:
                        if (tbSearch.Text == "")
                        {
                            lstFilterDoor = Repository.lstAllDoor;
                        }
                        else
                        {
                            lstFilterDoor = Repository.lstAllDoor.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
                        }
                        break;
                    case 5:
                        if (tbSearch.Text == "")
                        {
                            lstFilterSchedule = Repository.lstAllSchedules;
                        }
                        else
                        {
                            lstFilterSchedule = Repository.lstAllSchedules.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
                        }
                        break;
                    case 6:
                        if (tbSearch.Text == "")
                        {
                            lstFilterRight = Repository.lstAllRIght;
                        }
                        else
                        {
                            lstFilterRight = Repository.lstAllRIght.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
                        }
                        break;
                    case 8:
                        if (tbSearch.Text == "")
                        {
                            lstFilterDepartment = Repository.lstAllDepartment;
                        }
                        else
                        {
                            lstFilterDepartment = Repository.lstAllDepartment.Where(c => (c.Name.ToLower().Contains(tbSearch.Text.ToLower()))).ToList();
                        }
                        break;
                }
            });
        }
     
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            LoginUserName = LoginUser.Name;
            BindingExpression binding = tbLoginUserName.GetBindingExpression(TextBlock.TextProperty);
            binding.UpdateTarget();

            if (LoginUser.Id == "0")
            {
                isAdmin = true;
            }


            string[] arrLoginUserName = LoginUser.Name.Split(' ');
            if(arrLoginUserName!=null && arrLoginUserName.Length > 0)
            {
                LoginUserLetter = arrLoginUserName[arrLoginUserName.Length-1].Substring(0, 1).ToUpper();
            }
            else
            {
                LoginUserLetter = LoginUser.Name.Substring(0, 1).ToUpper();
            }
           
            BindingExpression binding1 = tbLoginUserLetter.GetBindingExpression(TextBlock.TextProperty);
            binding1.UpdateTarget();

            DTO_Department dept = Repository.lstAllDepartment.FirstOrDefault(d => d.Id == LoginUser.DepartmentId);
            if (dept != null)
            {

                if (dept.Manager == null)
                {
                    LoginUserDetail = string.Format("P. {0} - {1}", dept.Name, "Nhân Viên");
                    BindingExpression binding2 = tbLoginUserDetail.GetBindingExpression(TextBlock.TextProperty);
                    binding2.UpdateTarget();
                }
                else
                {
                    LoginUserDetail = string.Format("P. {0} - {1}", dept.Name, dept.Manager.Id == LoginUser.Id ? "Trưởng phòng" : "Nhân Viên");
                    BindingExpression binding3 = tbLoginUserDetail.GetBindingExpression(TextBlock.TextProperty);
                    binding3.UpdateTarget();
                }
                
            }

            if (isAdmin)
            {
                SetAdminMode();
            }
            else
            {
                SetManagerMode();
            }

            

            lbFunctions.ItemsSource = listViewItems;

            //Make Card screen is default
            lbFunctions.SelectedIndex = 0;      
        }

        #region Load detail data in right panel
        public void LoadCard()
        {
            panelData.Children.Clear();

            foreach (DTO_Card card in lstFilterCard)
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
        private void LoadDoor()
        {
            panelData.Children.Clear();
            foreach (DTO_Door door in lstFilterDoor)
            {
                ucDoorView view = new ucDoorView();
                view.MyDoor = door;
                view.DoorName = door.Name;
                view.DoorMode = door.Mode.Name;
                view.Detail = door.Description;
                view.ModeNum = door.Mode.Id;
                view.OnUpdate += View_OnUpdate;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }
        private void LoadHolder()
        {
            panelData.Children.Clear();
            foreach (DTO_CardHolder holder in lstFilterCardHolder)
            {
                ucHolderView view = new ucHolderView();
                view.HolderName = holder.Name;
                view.HolderAddress = holder.Address;
                view.Gender = holder.Gender;
                view.MyHolder = holder;
                view.DeleteClick += HolderView_DeleteClick;
                view.UpdateCardHolder += View_UpdateCardHolder;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }

        }

        private void View_UpdateCardHolder(object sender, EventArgs e)
        {
            lstFilterDepartment = Repository.lstAllDepartment;
        }

        private void LoadDevice()
        {
            panelData.Children.Clear();
            foreach (DTO_Device device in lstFilterDevice)
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
        public void LoadDepartment()
        {
            panelData.Children.Clear();

            foreach (DTO_Department department in lstFilterDepartment)
            {
                ucDepartmentView view = new ucDepartmentView();
                view.Department = Repository.lstAllDepartment.FirstOrDefault(dept => dept.Id == department.Id);
                view.DeptName = department.Name;
                view.UpdatedDepartment += View_UpdatedDepartment;
                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void View_UpdatedDepartment(object sender, EventArgs e)
        {
            lstFilterDepartment = Repository.lstAllDepartment;
            LoadDepartment();
        }

        private void LoadSchedule()
        {
            panelData.Children.Clear();
            foreach (DTO_Schedule schedule in lstFilterSchedule)
            {
                ucScheduleView view = new ucScheduleView();
                view.ScheduleName = schedule.Name;
                view.MySchedule = schedule;
                view.DeleteClick += View_DeleteClick;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }
        private void LoadRight()
        {
            panelData.Children.Clear();
            foreach (DTO_UserRight right in lstFilterRight)
            {
                ucRightView view = new ucRightView();
                view.RightName = right.Name;
                view.Detail = right.Description;
                view.MyRight = right;
                view.DeleteClick += RightView_DeleteClick;

                view.Margin = new Thickness(5);

                panelData.Children.Add(view);
            }
        }

        private void LoadEvent()
        {
            panelData.Children.Clear();
            ucEvent view = new ucEvent(LoginUser.Department==null?null: LoginUser.Department.Id);
            panelData.Children.Add(view);
        }
        #endregion


        #region Event when user click delete object
        private void CardView_DeleteClick(object sender, EventArgs e)
        {
            LoadCard();
        }
        private void HolderView_DeleteClick(object sender, EventArgs e)
        {
            lstFilterDepartment = Repository.lstAllDepartment;
            LoadHolder();
        }
        private void DeviceView_DeleteClick(object sender, EventArgs e)
        {
            LoadDevice();
        }
        private void View_DeleteClick(object sender, EventArgs e)
        {
            LoadSchedule();
        }
        private void View_OnUpdate(object sender, EventArgs e)
        {
            LoadDoor();
        }
        private void RightView_DeleteClick(object sender, EventArgs e)
        {
            LoadRight();
        }
        #endregion


        private void btSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lbFunctions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbSearch.Text = "";
            ListMenuItem selectedFunction = (ListMenuItem)lbFunctions.SelectedItem;

            if (selectedFunction != null)
            {
                switch (selectedFunction.Key)
                {
                    case "Card":
                        LoadCard();
                        tbFunctionName.Text = "Thẻ vào/ra";
                        currentMode = 1;
                        break;
                    case "Holder":
                        LoadHolder();
                        tbFunctionName.Text = "Chủ thẻ";
                        currentMode = 2;
                        break;
                    case "Device":
                        LoadDevice();
                        tbFunctionName.Text = "Bộ điều khiển";
                        currentMode = 3;
                        break;
                    case "Door":
                        LoadDoor();
                        tbFunctionName.Text = "Cửa";
                        currentMode = 4;
                        break;
                    case "Schedule":
                        LoadSchedule();
                        tbFunctionName.Text = "Lịch";
                        currentMode = 5;
                        break;
                    case "Right":
                        LoadRight();
                        tbFunctionName.Text = "Quyền";
                        currentMode = 6;
                        break;
                    case "Event":
                        LoadEvent();
                        tbFunctionName.Text = "Sự kiện";
                        currentMode = 7;
                        break;
                    case "Department":
                        LoadDepartment();
                        tbFunctionName.Text = "Bộ phận/Phòng ban";
                        currentMode = 8;
                        break;
                }
            }
        }

        private void SetAdminMode()
        {
            lstFilterCard = Repository.lstAllCards;
            lstFilterCardHolder = Repository.lstAllCardHolders;
            lstFilterDepartment = Repository.lstAllDepartment;
            lstFilterDevice = Repository.lstAllDevices;
            lstFilterDoor = Repository.lstAllDoor;
            lstFilterSchedule = Repository.lstAllSchedules;
            lstFilterRight = Repository.lstAllRIght;

            listViewItems = new List<ListMenuItem>()
            {
                new ListMenuItem("Thẻ vào/ra","/Icon/card_white_50px.png","Card"),
                new ListMenuItem("Chủ thẻ","/Icon/holder_white_50px.png","Holder"),
                new ListMenuItem("Bộ phận","/Icon/event_white_30px.png","Department"),
                new ListMenuItem("Bộ điều khiển","/Icon/device_white_50px.png","Device"),
                new ListMenuItem("Cửa","/Icon/door_white_50px.png","Door"),
                new ListMenuItem("Lịch","/Icon/schedule_white_50px.png","Schedule"),
                new ListMenuItem("Quyền","/Icon/right_white_50px.png","Right"),
                new ListMenuItem("Sự kiện","/Icon/event_white_30px.png","Event")
            };

           
        }

        private void SetManagerMode()
        {
            btExport.IsEnabled = false;
            btRefresh.IsEnabled = false;
            btNew.IsEnabled = false;
            tbSearch.IsReadOnly = true;

            listViewItems = new List<ListMenuItem>()
            {
                new ListMenuItem("Sự kiện","/Icon/event_white_30px.png","Event")
            };
        }
    
    }
}
