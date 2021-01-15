using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InstallSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool is64BitOperatingSystem;
        private bool is64BitProcess;
        private InformationClass _Info;
        private DownloadEnum eDownload;
        private string strUpdateDirectoty;
        private string strPsExecLink;
        private string strLinkDownload;
        private string strSQL2008x64SP2Link;
        private string strSQL2008x86SP2Link;
        private bool isThreadRunning; 
        private string strFileDownloaded;
        private string strAddUser;

        public MainWindow()
        {
            InitializeComponent();
            this.is64BitProcess = IntPtr.Size == 8;
            this.is64BitOperatingSystem = this.is64BitProcess || this.InternalCheckIsWow64();
            this.strPsExecLink = "https://product.misa.vn/HTKT/Tools/MISASQLInstaller/PsExec.exe";
            this.strSQL2008x64SP2Link = "https://product.misa.vn/SoftwareTools/SQLServer/SQL2008R2SP2/SQLEXPR_x64_ENU.exe";
            this.strSQL2008x86SP2Link = "https://product.misa.vn/SoftwareTools/SQLServer/SQL2008R2SP2/SQLEXPR_x86_ENU.exe";
            this.isThreadRunning = false;
            this._Info = new InformationClass();
            //this.prbStatus.Visibility = Visibility.Hidden;
            //this.lblDownload.Visibility = Visibility.Hidden;
            //this.lblRunning.Visibility = Visibility.Hidden;
            this.lblRunning.Text = "";
            this.lblDownload.Text = "";
        }

        private void btSQLConfig_Click(object sender, RoutedEventArgs e)
        {
            wdSQLConfig view = new wdSQLConfig();
            view.ShowDialog();
        }
        public bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major != 5 || Environment.OSVersion.Version.Minor < 1) && Environment.OSVersion.Version.Major < 6)
                return false;
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                bool wow64Process;
                return MainWindow.IsWow64Process(currentProcess.Handle, out wow64Process) && wow64Process;
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.strUpdateDirectoty = Environment.CurrentDirectory + "\\Lefa Installer";
            if (this.is64BitOperatingSystem)
            {
                rbx64.IsChecked = true;
                tbSetupFile.Text = this.strUpdateDirectoty + "\\"+ Repository.strSQL2008SP2x64FileName;
            }
            else
            {
                rbx86.IsChecked = true;
                tbSetupFile.Text = this.strUpdateDirectoty + "\\"+ Repository.strSQL2008SP2x86FileName;
            }

            this.Title = this.Title + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tbInstanceName.Text = "LefaTechnologies";
            
            string path = Environment.CurrentDirectory + "\\Update" + DateAndTime.Now.ToString("yyyyMMdd");
            if (Directory.Exists(path))
                Directory.Delete(path, true);

        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Setup file *.exe|*.exe";
            if (openFileDialog.ShowDialog() != true || string.IsNullOrEmpty(openFileDialog.FileName))
                return;
            this.tbSetupFile.Text = openFileDialog.FileName;
        }
        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ValidateData())
                return;
            this.SetStatusControl(true);
            if (!Directory.Exists(this.strUpdateDirectoty))
                Directory.CreateDirectory(this.strUpdateDirectoty);
            string str = this.strUpdateDirectoty + "\\PsExec.exe";
            if (!System.IO.File.Exists(str))
            {
                this.strLinkDownload = this.strPsExecLink;
                this.DownloadFile(this.strLinkDownload, str);
            }
            switch (this.tabMain.SelectedIndex)
            {
                case 0:
                    this.strFileDownloaded = this.tbSetupFile.Text;

                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tbSetupFile.Text, string.Empty, false) != 0 && !System.IO.File.Exists(this.tbSetupFile.Text))
                    {
                        this.eDownload = DownloadEnum.SQL;
                        this.lblRunning.Text = "Download SQL...";
                        this.isThreadRunning = true;                       
                        this.DownloadAsync(this.strLinkDownload, this.strFileDownloaded);
                        while (this.isThreadRunning)
                            DoEvents();
                    }
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(new FileInfo(this.strFileDownloaded).Extension, ".zip", false) == 0)
                        this.ExtractFile(this.strFileDownloaded);
                    this.InstallSQL();

                    if (CommonFunction.CheckServicesStart("MSSQL$" + this.tbInstanceName.Text))
                    {
                        CommonFunction.ShowInfomation("Cài đặt thành công.\r\nMật khẩu tài khoản sa: 12345678@Abc");
                    }
                    break;
            }
        }
        private void rbx86_Checked(object sender, RoutedEventArgs e)
        {
            tbSetupFile.Text = this.strUpdateDirectoty + "\\" + Repository.strSQL2008SP2x86FileName;
            this.strLinkDownload = this.strSQL2008x86SP2Link;
        }
        private void rbx64_Checked(object sender, RoutedEventArgs e)
        {
            tbSetupFile.Text = this.strUpdateDirectoty + "\\" + Repository.strSQL2008SP2x64FileName;
            this.strLinkDownload = this.strSQL2008x64SP2Link;
        }

        private void InstallSQL()
        {
            string str = string.Empty;

            str = "/QS /ACTION=Install /SkipRules=PerfMonCounterNotCorruptedCheck RebootRequiredCheck FacetPowerShellCheck AclPermissionsFacet /Hideconsole /INDICATEPROGRESS=0 /FEATURES=SQLEngine /INSTANCENAME=" + this.tbInstanceName.Text + " /BROWSERSVCSTARTUPTYPE=Automatic /SQLSVCSTARTUPTYPE=Automatic /SQLSYSADMINACCOUNTS=\"Builtin\\Administrators\" /SQLSVCACCOUNT=\"NT AUTHORITY\\SYSTEM\" /SECURITYMODE=SQL /SAPWD=12345678@Abc /TCPENABLED=1 /ERRORREPORTING=0 /SQLCOLLATION=SQL_Latin1_General_CP1_CI_AS /IACCEPTSQLSERVERLICENSETERMS=1 /ADDCURRENTUSERASSQLADMIN=1 /ENABLERANU=1 /SQMREPORTING=0";
            this.strAddUser = "-S .\\" + this.tbInstanceName.Text + " -Q \"EXEC sp_addlogin 'ibs','ibs';EXEC sp_addsrvrolemember @loginame = N'ibs', @rolename = N'sysadmin';\"";

            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, string.Empty, false) != 0)
            {
                this.lblRunning.Text = "Installing SQL...";
                this.lblRunning.Visibility = Visibility.Visible;

                Process.Start(this.tbSetupFile.Text, str).WaitForExit();

                Process.Start("sc", "start MSSQL$" + this.tbInstanceName.Text);

                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strAddUser, string.Empty, false) != 0)
                {
                    try
                    {
                        Process.Start("sqlcmd", this.strAddUser);
                    }
                    catch (Exception ex)
                    {
                        ProjectData.SetProjectError(ex);
                        ProjectData.ClearProjectError();
                    }
                }
            }
            this.lblRunning.Visibility = Visibility.Hidden;
        }

        public void ExtractFile(string strFile)
        {
            if (Directory.Exists(strFile.Split('.')[0]))
                return;
            CommonFunction.DownloadFile(CommonFunction.strUrlUpdate + "/LiveUpdate/Ionic.Zip.dll", this.strUpdateDirectoty + "\\Ionic.Zip.dll");
            CommonFunction.DownloadFile(CommonFunction.strUrlUpdate + "/LiveUpdate/ExtractFile.exe", this.strUpdateDirectoty + "\\ExtractFile.exe");
            Process.Start(new ProcessStartInfo()
            {
                FileName = this.strUpdateDirectoty + "\\ExtractFile.exe",
                Arguments = strFile.Replace(" ", "_")
            }).WaitForExit();
            Thread.Sleep(1000);
            System.IO.File.Delete(this.strUpdateDirectoty + "\\Ionic.Zip.dll");
            System.IO.File.Delete(this.strUpdateDirectoty + "\\ExtractFile.exe");
        }

        public static void DoEvents()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                                                  new Action(delegate { }));
        }

        private void DownloadAsync(string strLink, string strFileName)
        {
            this.prbStatus.Visibility = Visibility.Visible;
            this.lblRunning.Visibility = Visibility.Visible;
            this.DownloadFileAsync(strLink, strFileName);
        }

        public void DownloadFileAsync(string URL, string targetFolder)
        {
            try
            {
                if (CommonFunction.CheckInternetConnection())
                {
                    WebClient webClient = new WebClient();
                    IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
                    defaultWebProxy.Credentials = (ICredentials)CredentialCache.DefaultNetworkCredentials;
                    webClient.Proxy = defaultWebProxy;
                    webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                    webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    this.lblDownload.Visibility = Visibility.Visible;
                    webClient.DownloadFileAsync(new Uri(URL), targetFolder);
                }
                else
                    CommonFunction.ShowExclamation("Không kết nối được server download, vui lòng kiểm tra lại.");
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                CommonFunction.ShowExclamation(ex.Message);
                ProjectData.ClearProjectError();
            }
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                double num1 = double.Parse(e.BytesReceived.ToString());
                double num2 = double.Parse(e.TotalBytesToReceive.ToString());
                this.prbStatus.Value = int.Parse(Math.Truncate(num1 / num2 * 100.0).ToString());
                this.lblDownload.Text = this.prbStatus.Value.ToString() + "% (" + Math.Truncate(num1 / 1024.0 / 1024.0).ToString() + " MB / " + Math.Truncate(num2 / 1024.0 / 1024.0).ToString() + " MB)";
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                CommonFunction.ShowExclamation(ex.Message);
                ProjectData.ClearProjectError();
            }
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                //this.prbStatus.Visibility = Visibility.Hidden;
                //this.lblRunning.Visibility = Visibility.Hidden;
                //this.lblDownload.Visibility = Visibility.Hidden;

                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strFileDownloaded, string.Empty, false) != 0 & System.IO.File.Exists(this.strFileDownloaded) && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(new FileInfo(this.strFileDownloaded).Extension, ".zip", false) == 0)
                    this.ExtractFile(this.strFileDownloaded);
                this.isThreadRunning = false;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                CommonFunction.ShowExclamation(ex.Message);
                ProjectData.ClearProjectError();
            }
        }


        private void SetStatusControl(bool v)
        {
            rbx64.IsEnabled = !v;
            rbx86.IsEnabled = !v;
            btnBrowse.IsEnabled = !v;
            btSQLConfig.IsEnabled = !v;
            tbInstanceName.IsEnabled = !v;
            tbSetupFile.IsEnabled = !v;
        }

        private bool CheckInstanceNameExist(string strInstanceName)
        {
            RegistryKey registryKey = !this.is64BitOperatingSystem ? Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL", true) : Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Microsoft SQL Server\\Instance Names\\SQL", true);
            if (registryKey != null)
            {
                string[] valueNames = registryKey.GetValueNames();
                int index = 0;
                while (index < valueNames.Length)
                {
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(valueNames[index], strInstanceName, false) == 0)
                        return true;
                    checked { ++index; }
                }
            }
            return CommonFunction.CheckServicesStart("MSSQL$" + this.tbInstanceName.Text);
        }

        
        private bool ValidateData()
        {
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this._Info.OSMachineName.ToUpper(), this._Info.OSUserName.ToUpper(), false) == 0)
            {
                MessageBoxResult result = MessageBox.Show("Hiện tại Tên máy tính là: " + this._Info.OSMachineName.ToUpper() + "\r\n- Trùng với Tên đăng nhập là: " + this._Info.OSUserName.ToUpper() + "\r\n- Bạn cần đổi lại Tên máy tính. Sau đó Khởi động lại máy tính rồi thử lại\r\n- Bấm Yes để đổi lại tên máy tính và khởi động lại máy và thực hiện lại.\r\n- Bấm No để bỏ qua cảnh báo và tiếp tục thực hiện.", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            switch (this.tabMain.SelectedIndex)
            {
                case 0:
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tbInstanceName.Text, string.Empty, false) == 0)
                    {
                        CommonFunction.ShowExclamation("Bạn cần nhập InstanceName.");
                        this.tbInstanceName.Focus();
                        return false;
                    }
                    if (this.tbInstanceName.Text.Contains(" "))
                    {
                        CommonFunction.ShowExclamation("InstanceName không được chứa khoảng trắng.");
                        this.tbInstanceName.Focus();
                        return false;
                    }
                    if (this.CheckInstanceNameExist(this.tbInstanceName.Text))
                    {
                        CommonFunction.ShowExclamation("InstanceName này đã tồn tại.");
                        this.tbInstanceName.Focus();
                        return false;
                    }
                    break;
                case 1:
                   
                    break;                
            }
            return true;
        }

        public void DownloadFile(string URL, string targetFolder)
        {
            try
            {
                if (CommonFunction.CheckInternetConnection())
                {
                    WebClient webClient = new WebClient();
                    IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
                    defaultWebProxy.Credentials = (ICredentials)CredentialCache.DefaultNetworkCredentials;
                    webClient.Proxy = defaultWebProxy;
                    webClient.DownloadFile(new Uri(URL), targetFolder);
                }
                else
                    CommonFunction.ShowExclamation("Không kết nối được Internet, vui lòng kiểm tra lại.");
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                CommonFunction.ShowExclamation(ex.Message);
                ProjectData.ClearProjectError();
            }
        }

        
        #region Tab2


        #endregion
    }
}
