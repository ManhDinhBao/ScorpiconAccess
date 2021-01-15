using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InstallSQL
{
    public class CommonFunction
    {
        public static string strUrlUpdate = "https://product.misa.vn/HTKT/Tools/MISASQLInstaller";
        public static void ShowExclamation(string content)
        {
            MessageBox.Show(content, "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static void ShowInfomation(string content)
        {
            MessageBox.Show(content, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static bool CheckInternetConnection()
        {
            bool flag;
            try
            {
                Ping ping = new Ping();
                string hostNameOrAddress = "download.misa.com.vn";
                byte[] buffer = new byte[32];
                int timeout = 5000;
                PingOptions options = new PingOptions();
                flag = ping.Send(hostNameOrAddress, timeout, buffer, options).Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public static bool CheckServicesStart(string strServicesName)
        {
            bool flag;
            try
            {
                flag = new ServiceController(strServicesName).Status == ServiceControllerStatus.Running;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }

        public static void DownloadFile(string URL, string targetFolder, bool isShowMessage = true)
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
                else if (isShowMessage)
                    CommonFunction.ShowExclamation("Không kết nối được server download, vui lòng kiểm tra lại.");
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                Exception exception = ex;
                if (isShowMessage)
                    CommonFunction.ShowExclamation(exception.Message);
                ProjectData.ClearProjectError();
            }
        }
    }
}
