using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InstallSQL
{
    public class InformationClass
    {
        private string _OSMachineName;
        private string _OSUserName;

        private static string GetOSMachineName() => Environment.MachineName;
        private static string GetOSUserName() => Environment.UserName;

        [Browsable(true)]
        [Category("Operating System")]
        [Description("Machine Name")]
        public string OSMachineName
        {
            get
            {
                this._OSMachineName = InformationClass.GetOSMachineName();
                return this._OSMachineName;
            }
        }

        [Category("Operating System")]
        [Description("Logon User Name")]
        [Browsable(true)]
        public string OSUserName
        {
            get
            {
                this._OSUserName = InformationClass.GetOSUserName();
                return this._OSUserName;
            }
        }
    }
}