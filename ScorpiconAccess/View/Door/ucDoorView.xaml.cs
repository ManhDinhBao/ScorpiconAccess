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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScorpiconAccess.View
{
    /// <summary>
    /// Interaction logic for ucDoorView.xaml
    /// </summary>
    public partial class ucDoorView : UserControl
    {
        #region Constructor
        public DTO_Door MyDoor { get; set; }
        public string DoorName { get; set; }
        public string Detail { get; set; }
        public string DoorMode { get; set; }
        public string ModeNum { get; set; }
        public ucDoorView()
        {
            InitializeComponent();
            DataContext = this;
        }
        #endregion

        #region Event
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks button")]
        public event EventHandler OnUpdate;
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            //int picNum = rnd.Next(1, 9);
            int picNum = int.Parse(ModeNum);
            Console.WriteLine(picNum);

            switch (picNum)
            {
                case 1:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door1.jpg"));
                    break;
                case 2:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door2.jpg"));
                    break;
                case 3:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door3.jpg"));
                    break;
                case 4:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door4.jpg"));
                    break;
                case 5:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door5.jpg"));
                    break;
                case 6:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door6.jpg"));
                    break;
                case 7:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door7.jpg"));
                    break;
                case 8:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door8.jpg"));
                    break;
                case 9:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door9.jpg"));
                    break;
                default:
                    brushBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ScorpiconAccess;component/Image/door10.jpg"));
                    break;
            }
        }

        #region Control click
        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            wdDoorDetail view = new wdDoorDetail(EType.WindowMode.EDIT_MODE, MyDoor);
            if (view.ShowDialog() == true)
            {
                OnUpdate(sender, e);
            }

        }

        private void btSocket_Click(object sender, RoutedEventArgs e)
        {
            wdDoorSocketDetail view = new wdDoorSocketDetail(MyDoor);
            if (view.ShowDialog() == true)
            {

            }
        }
        #endregion
    }
}
