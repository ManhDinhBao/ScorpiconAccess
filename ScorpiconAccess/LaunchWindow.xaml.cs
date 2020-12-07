using BUS_ScorpionAccess;
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

        private BackgroundWorker wokerLoadAllData;
        private List<string> lstDataType;

        public LaunchWindow()
        {
            InitializeComponent();
            bus_Card = new BUS_Card();
            bus_CardHolder = new BUS_CardHolder();
            bus_Device = new BUS_Device();
            bus_Schedule = new BUS_Schedule();
            bus_Door = new BUS_Door();
            bus_DoorMode = new BUS_DoorMode();

            lstDataType = new List<string>() { "CARD", "HOLDER", "DEVICE","SCHEDULE", "DOOR","DOOR_MODE"};

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
                    tbLoadData.Text = string.Format("Loading {0} data... {1}%", lstDataType[i], percentage);
                });
            }

        }

        private void GetSingleData(string dataType)
        {
            switch(dataType)
            {
                case "CARD": 
                    Repository.lstAllCards = bus_Card.GetAllCard();
                    break;
                case "HOLDER":
                    Repository.lstAllCardHolders = bus_CardHolder.GetAllCardHolder();
                    break;
                case "DEVICE":
                    Repository.lstAllDevices = bus_Device.GetAllDevice();
                    break;
                case "SCHEDULE":
                    Repository.lstAllSchedules = bus_Schedule.GetAllSchedule();
                    break;
                case "DOOR":
                    Repository.lstAllDoor = bus_Door.GetAllDoor();
                    break;
                case "DOOR_MODE":
                    Repository.lstAllDoorMode = bus_DoorMode.GetAllDoorMode();
                    break;
                default:
                    break;

            }
        }
    }
}
