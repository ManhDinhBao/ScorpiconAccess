using BUS_ScorpionAccess;
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
using System.Windows.Shapes;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for wdDoorDetail.xaml
    /// </summary>
    public partial class wdDoorDetail : Window
    {
        #region Variables
        public EType.WindowMode mode;
        public BUS_Door bUS_Door;
        public DTO_Door door, newDoor;
        public SQLResult result;
        public BackgroundWorker myDoorWorker, getDoorWorker, delDoorWorker;
        #endregion

        #region Constructor
        public int funcClick { get; set; }
        public wdDoorDetail(EType.WindowMode mode,DTO_Door door)
        {
            InitializeComponent();
            this.mode = mode;
            this.door = door;
        }
        #endregion

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 2;
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn có muốn xóa cửa này không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (delDoorWorker.IsBusy)
                {
                    delDoorWorker.CancelAsync();
                }
                else
                {
                    delDoorWorker.RunWorkerAsync();
                }
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            funcClick = 1;

            btSave.IsEnabled = false;
            door.Name = tbDoorName.Text;
            door.Mode = (DTO_DoorMode)cbMode.SelectedItem;
            door.LOTimeOut = int.Parse(tbLOTimeOut.Text);
            door.DOTimeOut = int.Parse(tbDOTimeOut.Text);
            door.Description = General.StringFromRichTextBox(rtbDescription).Trim();

            if (myDoorWorker.IsBusy)
            {
                myDoorWorker.CancelAsync();
            }
            else
            {
                myDoorWorker.RunWorkerAsync();
            }
        }       
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitData();

            switch (mode)
            {
                case EType.WindowMode.ADD_MODE:
                    ResetControl();
                    door = new DTO_Door();
                    break;
                default:
                    BindData();
                    break;
            }

        }

        #region Background worker
        private void DelDoorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            result = bUS_Door.DeleteDoor(door.Id);
        }
        private void DelDoorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result.Result)
            {
                DTO_Door oldDoor = Repository.lstAllDoor.FirstOrDefault(d => d.Id == door.Id);
                if (oldDoor != null)
                {
                    Repository.lstAllDoor.Remove(oldDoor);
                }

                this.DialogResult = true;
                this.Hide();

            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetDoorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            newDoor = bUS_Door.GetDoorByKey(result.ExtraData);
        }
        private void GetDoorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (newDoor == null || newDoor.Id == "")
            {
                return;
            }

            Repository.lstAllDoor.Add(newDoor);

            this.DialogResult = true;
            this.Hide();
        }
        private void MyDoorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                result = bUS_Door.AddNewDoor(door);
            }
            else
            {
                result = bUS_Door.UpdateDoor(door);
            }
        }
        private void MyDoorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btSave.IsEnabled = true;

            if (result.Result)
            {
                if (this.mode == EType.WindowMode.ADD_MODE)
                {
                    if (getDoorWorker.IsBusy)
                    {
                        getDoorWorker.CancelAsync();
                    }
                    else
                    {
                        getDoorWorker.RunWorkerAsync();
                    }
                }
                else
                {
                   DTO_Door oldDoor = Repository.lstAllDoor.FirstOrDefault(c => c.Id == door.Id);
                    if (oldDoor != null)
                    {
                        oldDoor = door;
                    }

                    this.DialogResult = true;
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show(result.Detail, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Custom methods
        private void InitData()
        {
            bUS_Door = new BUS_Door();

            cbMode.ItemsSource = Repository.lstAllDoorMode;
            cbMode.DisplayMemberPath = "Name";
            cbMode.SelectedValuePath = "Id";

            myDoorWorker = new BackgroundWorker();
            myDoorWorker.WorkerSupportsCancellation = true;
            myDoorWorker.DoWork += MyDoorWorker_DoWork;
            myDoorWorker.RunWorkerCompleted += MyDoorWorker_RunWorkerCompleted;

            getDoorWorker = new BackgroundWorker();
            getDoorWorker.WorkerSupportsCancellation = true;
            getDoorWorker.DoWork += GetDoorWorker_DoWork;
            getDoorWorker.RunWorkerCompleted += GetDoorWorker_RunWorkerCompleted;

            delDoorWorker = new BackgroundWorker();
            delDoorWorker.WorkerSupportsCancellation = true;
            delDoorWorker.DoWork += DelDoorWorker_DoWork;
            delDoorWorker.RunWorkerCompleted += DelDoorWorker_RunWorkerCompleted;

            if (this.mode == EType.WindowMode.ADD_MODE)
            {
                btDelete.IsEnabled = false;
            }
            else
            {
                btDelete.IsEnabled = true;
            }
        }
        private void BindData()
        {
            tbId.Text = door.Id;
            tbDoorName.Text = door.Name;
            tbLOTimeOut.Text = door.LOTimeOut.ToString();
            tbDOTimeOut.Text = door.DOTimeOut.ToString();
            rtbDescription.Document.Blocks.Remove(rtbDescription.Document.Blocks.FirstBlock);
            rtbDescription.Document.Blocks.Add(new Paragraph(new Run(door.Description)));

            cbMode.SelectedValue = door.Mode.Id;
        }
        private void ResetControl()
        {
            tbDoorName.Text = "";
            tbLOTimeOut.Text = "0";
            tbDOTimeOut.Text = "0";
        }
        #endregion
    }
}
