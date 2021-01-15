using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
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

namespace InstallSQL
{
    /// <summary>
    /// Interaction logic for wdSQLConfig.xaml
    /// </summary>
    public partial class wdSQLConfig : Window
    {
        private string strConnectionString;
        public string txtSQLServiceName1;
        public string txtSQLServiceName2;
        public string txtSQLServiceName3;
        private string strServer;
        public wdSQLConfig()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtSQLServiceName1 = "MSSQL$";
            this.txtSQLServiceName2 = "MSSQLSERVER";
            this.txtSQLServiceName3 = "SQLB";
            this.cmdStart.IsEnabled = false;
            this.cmdStop.IsEnabled = false;
            this.cmdRestart.IsEnabled = false;
            this.ServiceSQL();
        }

        public void ServiceSQL()
        {
            ObservableCollection<SqlServiceClass> SqlServiceCollection = new ObservableCollection<SqlServiceClass>();
            ServiceController[] services = ServiceController.GetServices();
            //this.lvwServices.Items.Clear();
            try
            {
                ServiceController[] serviceControllerArray = services;
                int index = 0;
                while (index < serviceControllerArray.Length)
                {
                    ServiceController serviceController = serviceControllerArray[index];
                    if (serviceController.ServiceName.Contains(this.txtSQLServiceName1.ToUpper()) | serviceController.ServiceName.Contains(this.txtSQLServiceName2.ToUpper()) | serviceController.ServiceName.Contains(this.txtSQLServiceName3.ToUpper()))
                    {
                        SqlServiceClass serviceItem = new SqlServiceClass();
                        serviceItem.DisplayName = serviceController.DisplayName;
                        serviceItem.RealName = serviceController.ServiceName;
                        serviceItem.State = serviceController.Status.ToString();
                        SqlServiceCollection.Add(serviceItem);
                    }
                    checked { ++index; }
                }
                lvwServices.ItemsSource = SqlServiceCollection;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                int num = (int)MessageBox.Show("Không tìm thấy Services SQL.  Hãy kiểm tra lại", "Thông báo: " + ex.Source);
                ProjectData.ClearProjectError();
            }
            finally
            {
            }

            //this.cmdStart.Enabled = false;
            //this.cmdStop.Enabled = false;
        }

        public void StartService(string ServiceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            try
            {
                ServiceController[] serviceControllerArray = services;
                int index = 0;
                while (index < serviceControllerArray.Length)
                {
                    ServiceController serviceController = serviceControllerArray[index];
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.UCase(serviceController.ServiceName), Strings.UCase(ServiceName), false) == 0)
                    {
                        serviceController.Start();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running);
                        break;
                    }
                    checked { ++index; }
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                int num = (int)MessageBox.Show("Không thể Start Services. Thử bấm Refresh và kiểm tra lại", "Thông báo: " + ex.Source);
                ProjectData.ClearProjectError();
            }
            finally
            {
                this.ServiceSQL();
            }
        }

        public void StopService(string ServiceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            try
            {
                ServiceController[] serviceControllerArray = services;
                int index = 0;
                while (index < serviceControllerArray.Length)
                {
                    ServiceController serviceController = serviceControllerArray[index];
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.UCase(serviceController.ServiceName), Strings.UCase(ServiceName), false) == 0)
                    {
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        break;
                    }
                    checked { ++index; }
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                int num = (int)MessageBox.Show("Không thể Stop Services. Thử bấm Refresh và kiểm tra lại", "Thông báo: " + ex.Source);
                ProjectData.ClearProjectError();
            }
            finally
            {
                this.ServiceSQL();
            }
        }

        public void RestartService(string ServiceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            try
            {
                ServiceController[] serviceControllerArray = services;
                int index = 0;
                while (index < serviceControllerArray.Length)
                {
                    ServiceController serviceController = serviceControllerArray[index];
                    if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Strings.UCase(serviceController.ServiceName), Strings.UCase(ServiceName), false) == 0)
                    {
                        serviceController.Stop();
                        serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                        serviceController.Start();
                        serviceController.WaitForStatus(ServiceControllerStatus.Running);
                        break;
                    }
                    checked { ++index; }
                }
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                int num = (int)MessageBox.Show("Không thể Restart Services.  Thử bấm Refresh All và kiểm tra lại", "Thông báo: " + ex.Source);
                ProjectData.ClearProjectError();
            }
            finally
            {
                this.ServiceSQL();
            }
        }

        private void lvwServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SqlServiceClass selectedService = (SqlServiceClass)lvwServices.SelectedItem;
            if (selectedService != null)
            {
                string text = selectedService.State;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "", false) == 0)
                    return;               
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Stopped", false) == 0)
                {
                    this.cmdStop.IsEnabled = false;
                    this.cmdStart.IsEnabled = true;
                    this.cmdRestart.IsEnabled = false;
                }
                else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "Running", false) == 0)
                {
                    this.cmdStop.IsEnabled = true;
                    this.cmdStart.IsEnabled = false;
                    this.cmdRestart.IsEnabled = true;
                }
                else
                {
                    this.cmdStop.IsEnabled = false;
                    this.cmdStart.IsEnabled = false;
                    this.cmdRestart.IsEnabled = false;
                }
            }
            return;
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            SqlServiceClass selectedService = (SqlServiceClass)lvwServices.SelectedItem;
            if (selectedService != null)
            {
                string text = selectedService.State;
                string serviceName = selectedService.RealName;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "", false) != 0)
                    this.StartService(serviceName);
                this.ServiceSQL();
            }
        }

        private void cmdStop_Click(object sender, RoutedEventArgs e)
        {
            SqlServiceClass selectedService = (SqlServiceClass)lvwServices.SelectedItem;
            if (selectedService != null)
            {
                string text = selectedService.State;
                string serviceName = selectedService.RealName;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "", false) != 0)
                    this.StopService(serviceName);
                this.ServiceSQL();
            }
        }

        private void cmdRestart_Click(object sender, RoutedEventArgs e)
        {
            SqlServiceClass selectedService = (SqlServiceClass)lvwServices.SelectedItem;
            if (selectedService != null)
            {
                string text = selectedService.State;
                string serviceName = selectedService.RealName;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(text, "", false) != 0)
                    this.RestartService(serviceName);
                this.ServiceSQL();
            }
        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e) => this.ServiceSQL();

        private void btCheckMixMode_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("DECLARE @AuthenticationMode INT\r\n" + "EXEC master.dbo.xp_instance_regread \r\n" + "N'HKEY_LOCAL_MACHINE',\r\n" + "N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer',\r\n" + "N'LoginMode', @AuthenticationMode OUTPUT\r\n" + "SELECT CASE @AuthenticationMode\r\n" + "WHEN 1 THEN 'Windows Authentication'\r\n" + "WHEN 2 THEN 'Windows and SQL Server Authentication'\r\n" + "ELSE 'Unknown'\r\n" + "END as AuthenMode\r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    string strMessage = string.Empty;
                    while (sqlDataReader.Read())
                        strMessage = "- Authentication Mode: " + sqlDataReader["AuthenMode"].ToString();
                    sqlDataReader.Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation(strMessage);
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btSetMixMode_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("USE master\r\n" + "EXEC master.dbo.xp_instance_regwrite N'HKEY_LOCAL_MACHINE', \r\n" + "N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer', N'LoginMode', REG_DWORD, 2\r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    sqlCommand.ExecuteReader(CommandBehavior.CloseConnection).Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation("Đã Set về chế độ SQL Authentication Mode");
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btnCheckTCP_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("DECLARE @enableValue INT\r\n" + "EXEC   master.dbo.xp_instance_regread\r\n" + "@rootkey = N'HKEY_LOCAL_MACHINE',\r\n" + "@key = N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp',\r\n" + "@value_name = N'Enabled',\r\n" + "@value = @enableValue OUTPUT\r\n" + "SELECT @enableValue AS EnableValue01 \r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    string strMessage = string.Empty;
                    string empty = string.Empty;
                    while (sqlDataReader.Read())
                        strMessage = "- TCP SQL: " + (Convert.ToInt32(RuntimeHelpers.GetObjectValue(sqlDataReader["EnableValue01"])) != 1 ? "Disable" : "Enable");
                    sqlDataReader.Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation(strMessage);
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btnEnableTCPSQL_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("USE master\r\n" + "EXEC master.dbo.xp_instance_regwrite \r\n" + "N'HKEY_LOCAL_MACHINE',\r\n" + "N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp',\r\n" + "N'Enabled',\r\n" + "REG_DWORD,\r\n" + "1\r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    sqlCommand.ExecuteReader(CommandBehavior.CloseConnection).Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation("Đã Enable TCP SQL");
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btnCheckPortStatic_Click(object sender, RoutedEventArgs e)
        {
           
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection2 = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand2 = new SqlCommand("DECLARE  @portNoStatic   NVARCHAR(10)\r\n" + "EXEC   master.dbo.xp_instance_regread\r\n" + "@rootkey = N'HKEY_LOCAL_MACHINE',\r\n" + "@key = N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IpAll',\r\n" + "@value_name = N'TcpPort',\r\n" + "@value = @portNoStatic OUTPUT\r\n" + "SELECT @portNoStatic AS StaticPort \r\n", connection2);
                    if (connection2.State == ConnectionState.Closed)
                        connection2.Open();
                    SqlDataReader sqlDataReader2 = sqlCommand2.ExecuteReader(CommandBehavior.CloseConnection);
                    string str2 = string.Empty;
                    while (sqlDataReader2.Read())
                        str2 = "TCP Static Port: " + sqlDataReader2["StaticPort"].ToString();
                    sqlDataReader2.Close();
                    sqlCommand2.Dispose();
                    connection2.Dispose();
                    CommonFunction.ShowInfomation(str2);
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void CheckPort()
        {
            
        }

        private void btSetPortStatic_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else if (Convert.ToInt32(this.tbPort.Text.Trim()) > 0)
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("DECLARE @portNo   NVARCHAR(10)\r\n" + "SET @portNo = " + Convert.ToInt32(this.tbPort.Text.Trim()).ToString() + "\r\n" + "EXEC master.dbo.xp_instance_regwrite\r\n" + "N'HKEY_LOCAL_MACHINE',\r\n" + "N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IpAll',\r\n" + "N'TcpPort',\r\n" + "REG_SZ,\r\n" + "@portNo\r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    sqlCommand.ExecuteReader(CommandBehavior.CloseConnection).Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation("Đã Set TCP Port Static SQL: " + this.tbPort.Text.Trim());
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btCheckPortDynamic_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection1 = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand1 = new SqlCommand("DECLARE  @portNo   NVARCHAR(10)\r\n" + "EXEC   master.dbo.xp_instance_regread\r\n" + "@rootkey = N'HKEY_LOCAL_MACHINE',\r\n" + "@key = N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IpAll',\r\n" + "@value_name = N'TcpDynamicPorts',\r\n" + "@value = @portNo OUTPUT\r\n" + "SELECT @portNo AS DynamicPort \r\n", connection1);
                    if (connection1.State == ConnectionState.Closed)
                        connection1.Open();
                    SqlDataReader sqlDataReader1 = sqlCommand1.ExecuteReader(CommandBehavior.CloseConnection);
                    string str1 = string.Empty;
                    while (sqlDataReader1.Read())
                        str1 = "TCP Dynamic Ports: " + sqlDataReader1["DynamicPort"].ToString();
                    sqlDataReader1.Close();
                    sqlCommand1.Dispose();
                    connection1.Dispose();
                    CommonFunction.ShowInfomation(str1);
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
        }

        private void btSetPortDynamic_Click(object sender, RoutedEventArgs e)
        {
            this.strServer = this.tbServer.Text.Trim();
            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.strServer, string.Empty, false) == 0)
            {
                CommonFunction.ShowExclamation("Bạn cần nhập tên Server SQL");
                this.tbServer.Focus();
            }
            else if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(this.tbPort.Text.Trim().ToString(), "", false) != 0)
            {
                try
                {
                    this.strConnectionString = "Data Source=" + this.strServer + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=1800";
                    SqlConnection connection = new SqlConnection(this.strConnectionString);
                    SqlCommand sqlCommand = new SqlCommand("DECLARE @portNo   NVARCHAR(10)\r\n" + "SET @portNo = " + Convert.ToInt32(this.tbPort.Text.Trim()).ToString() + "\r\n" + "EXEC master.dbo.xp_instance_regwrite\r\n" + "N'HKEY_LOCAL_MACHINE',\r\n" + "N'Software\\Microsoft\\Microsoft SQL Server\\MSSQLServer\\SuperSocketNetLib\\Tcp\\IpAll',\r\n" + "N'TcpDynamicPorts',\r\n" + "REG_SZ,\r\n" + "@portNo\r\n", connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    sqlCommand.ExecuteReader(CommandBehavior.CloseConnection).Close();
                    sqlCommand.Dispose();
                    connection.Dispose();
                    CommonFunction.ShowInfomation("Đã Set TCP Port Dynamic SQL: " + this.tbPort.Text.Trim());
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    CommonFunction.ShowExclamation(ex.Message);
                    ProjectData.ClearProjectError();
                }
            }
            else
            {
                CommonFunction.ShowInfomation("Bạn chưa nhập Port Dynamic SQL. Hãy kiểm tra lại!");
                this.tbPort.Focus();
            }
        }
    }

    class SqlServiceClass
    {
        public string DisplayName { get; set; }
        public string RealName { get; set; }
        public string State { get; set; }

        public SqlServiceClass()
        {
        }

        public SqlServiceClass(string displayName, string realName, string state)
        {
            DisplayName = displayName;
            RealName = realName;
            State = state;
        }
    }
}
