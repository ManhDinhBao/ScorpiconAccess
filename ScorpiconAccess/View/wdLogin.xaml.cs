using BUS_ScorpionAccess;
using DTO_ScorpionAccess;
using ScorpiconAccess.View;
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
using System.Windows.Threading;
using System.Xml;

namespace ScorpiconAccess
{
    /// <summary>
    /// Interaction logic for wdLogin.xaml
    /// </summary>

    public partial class wdLogin : Window
    {
        #region Variables
        public DTO_CardHolder holder;
        public BUS_CardHolder bUS_CardHolder;
        public BUS_General bUS_General;
        public BackgroundWorker worker;
        XmlDocument doc = new XmlDocument();
        #endregion

        public wdLogin()
        {
            InitializeComponent();
        }
 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bUS_CardHolder = new BUS_CardHolder();
            bUS_General = new BUS_General();

            doc.Load("Config.xml");

            bool isRemember = Convert.ToBoolean(doc.SelectSingleNode("/Configuration/Account").Attributes["Remember"].Value);
            string userName = doc.SelectSingleNode("/Configuration/Account").Attributes["UserName"].Value.ToString();
            string passWord = doc.SelectSingleNode("/Configuration/Account").Attributes["Password"].Value.ToString();

            if (isRemember)
            {
                tbAccount.Text = userName;
                tbPassword.Password = passWord;
                cbRememberAccount.IsChecked = true;
            }

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork; 
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        #region Background worker
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLogin.Content = "Đăng nhập";
            btnLogin.IsEnabled = true;


            if (holder != null)
            {
                if (holder.Id != null)
                {
                    LaunchWindow view = new LaunchWindow(holder);
                    view.Show();
                    this.Hide();
                }
                else
                {
                    tbError.Text = "Sai tên đăng nhập hoặc mật khẩu. Vui lòng kiểm tra lại!";
                }
               
            }
            else
            {
                tbError.Text = "Không kết nối được máy chủ. Vui lòng kiểm tra lại Cài đặt!";
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string userName = "";
            string password = "";

            this.btnLogin.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { btnLogin.Content = "Đang đăng nhập..."; btnLogin.IsEnabled = false; }));
            this.tbError.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { tbError.Text = ""; }));

            this.tbAccount.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { userName= tbAccount.Text; }));
            this.tbPassword.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { password = tbPassword.Password; }));

            holder = bUS_CardHolder.GetCardHolderByAccount(userName, password);
        }
        #endregion

        #region Control click
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void btSetting_Click(object sender, RoutedEventArgs e)
        {
            wdSetting setting = new wdSetting();
            setting.ShowDialog();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            doc.Load("Config.xml");
            if (cbRememberAccount.IsChecked == true)
            {
                doc.SelectSingleNode("/Configuration/Account").Attributes["Remember"].Value = "true";
                doc.SelectSingleNode("/Configuration/Account").Attributes["UserName"].Value = tbAccount.Text;
                doc.SelectSingleNode("/Configuration/Account").Attributes["Password"].Value = tbPassword.Password;

            }
            else
            {
                doc.SelectSingleNode("/Configuration/Account").Attributes["Remember"].Value = "false";
                doc.SelectSingleNode("/Configuration/Account").Attributes["UserName"].Value = "";
                doc.SelectSingleNode("/Configuration/Account").Attributes["Password"].Value = "";
            }

            doc.Save("Config.xml");

            doc.Load("Config.xml");

            string server = doc.SelectSingleNode("/Configuration/Server").Attributes["IP"].Value.ToString();
            string port = doc.SelectSingleNode("/Configuration/Server").Attributes["Port"].Value.ToString();

            bUS_General.SetConnect(server, port);

            tbError.Text = "";
            if (tbAccount.Text.Count() <= 0)
            {
                tbError.Text = "Tên đăng nhập không hợp lệ";
                tbError.Focus();
                return;
            }

            if (tbPassword.Password.Count() <= 0)
            {
                tbError.Text = "Mật khẩu không được để trống";
                tbError.Focus();
                return;
            }


            this.Dispatcher.Invoke(() =>
            {
                //btnLogin.IsEnabled = false;
                btnLogin.Content = "Đang đăng nhập...";
                tbError.Text = "";

            });

            if (worker.IsBusy)
            {
                worker.CancelAsync();
            }
            else
            {
                worker.RunWorkerAsync();
            }
        }
        private void cbRememberAccount_Click(object sender, RoutedEventArgs e)
        {
        }
        private void stackMainTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion
    }
}
