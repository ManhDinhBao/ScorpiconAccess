using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for LaunchWindow.xaml
    /// </summary>
    public partial class LaunchWindow : Window
    {
        private BUS_Card bus_Card;
        private BUS_CardHolder bus_CardHolder;
        private BUS_Device bus_Device;
        private BUS_Schedule bus_Schedule;
        private BUS_Door bus_Door;
        private BUS_DoorMode bus_DoorMode;
        private BUS_Right bUS_Right;
        private BUS_Department bUS_Department;

        private BackgroundWorker wokerLoadAllData;
        private List<string> lstDataType;
        public DTO_CardHolder loginUser;

        public LaunchWindow(DTO_CardHolder loginUser)
        {

            InitializeComponent();
            this.loginUser = loginUser;

            bus_Card = new BUS_Card();
            bus_CardHolder = new BUS_CardHolder();
            bus_Device = new BUS_Device();
            bus_Schedule = new BUS_Schedule();
            bus_Door = new BUS_Door();
            bus_DoorMode = new BUS_DoorMode();
            bUS_Right = new BUS_Right();
            bUS_Department = new BUS_Department();

            lstDataType = new List<string>() { "CARD", "HOLDER", "DEVICE","SCHEDULE", "DOOR","DOOR_MODE","RIGHT","DEPARTMENT","LOADHOLDERDEPT"};

            wokerLoadAllData = new BackgroundWorker();
            wokerLoadAllData.WorkerReportsProgress = true;
            wokerLoadAllData.DoWork += WokerLoadAllData_DoWork;
            wokerLoadAllData.ProgressChanged += WokerLoadAllData_ProgressChanged;
            wokerLoadAllData.RunWorkerCompleted += WokerLoadAllData_RunWorkerCompleted;

            wokerLoadAllData.RunWorkerAsync();

        }

        private void WokerLoadAllData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(1000);
            MainWindow main = new MainWindow();
            main.LoginUser = loginUser;
            main.Show();
            this.Hide();
        }

        private void WokerLoadAllData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prBarLoadData.Value = e.ProgressPercentage;
        }

        private void WokerLoadAllData_DoWork(object sender, DoWorkEventArgs e)
        {
            for(int i = 0; i < lstDataType.Count; i++)
            {
                GetSingleData(lstDataType[i]);
                int percentage = (i + 1) * 100 / lstDataType.Count;
                wokerLoadAllData.ReportProgress(percentage);

                this.Dispatcher.Invoke(() =>
                {
                    tbLoadData.Text = string.Format("Đang tải dữ liệu {0}7... {1}%", lstDataType[i], percentage);
                });
            }

            if (this.loginUser.DepartmentId != null)
            {
                this.loginUser.Department = bUS_Department.GetDepartmentById(this.loginUser.DepartmentId);
            }
            

        }

        private void GetSingleData(string dataType)
        {
            switch(dataType)
            {
                case "CARD": 
                    Repository.lstAllCards = bus_Card.GetAllCard();
                    if(Repository.lstAllCards == null)
                    {
                        Repository.lstAllCards = new List<DTO_Card>();
                    }
                    break;
                case "HOLDER":
                    Repository.lstAllCardHolders = bus_CardHolder.GetAllCardHolder();
                    if (Repository.lstAllCardHolders == null)
                    {
                        Repository.lstAllCardHolders = new List<DTO_CardHolder>();
                    }
                    break;
                case "DEVICE":
                    Repository.lstAllDevices = bus_Device.GetAllDevice();
                    if (Repository.lstAllDevices == null)
                    {
                        Repository.lstAllDevices = new List<DTO_Device>();
                    }
                    break;
                case "SCHEDULE":
                    Repository.lstAllSchedules = bus_Schedule.GetAllSchedule();
                    if (Repository.lstAllSchedules == null)
                    {
                        Repository.lstAllSchedules = new List<DTO_Schedule>();
                    }
                    break;
                case "DOOR":
                    Repository.lstAllDoor = bus_Door.GetAllDoor();
                    if (Repository.lstAllDoor == null)
                    {
                        Repository.lstAllDoor = new List<DTO_Door>();
                    }
                    break;
                case "DOOR_MODE":
                    Repository.lstAllDoorMode = bus_DoorMode.GetAllDoorMode();
                    if (Repository.lstAllDoorMode == null)
                    {
                        Repository.lstAllDoorMode = new List<DTO_DoorMode>();
                    }
                    break;
                case "RIGHT":
                    Repository.lstAllRIght = bUS_Right.GetAllUserRight();
                    if (Repository.lstAllRIght == null)
                    {
                        Repository.lstAllRIght = new List<DTO_UserRight>();
                    }
                    break;
                case "DEPARTMENT":
                    Repository.lstAllDepartment = bUS_Department.GetAllDepartment();
                    if (Repository.lstAllDepartment == null)
                    {
                        Repository.lstAllDepartment = new List<DTO_Department>();
                    }
                    break;
                case "LOADHOLDERDEPT":
                    if(Repository.lstAllCardHolders==null)
                    {
                        return;
                    }
                    foreach(DTO_CardHolder holder in Repository.lstAllCardHolders)
                    {
                        holder.Department = bUS_Department.GetDepartmentById(holder.DepartmentId);
                    }
                    break;
                default:
                    break;

            }
        }
    }
}
