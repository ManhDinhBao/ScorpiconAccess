using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class UserLog
    {
        public string LogString { get; set; }
        public EType.UserLogType LogType { get; set; }

        public UserLog()
        {
        }

        public UserLog(string logString, EType.UserLogType logType)
        {
            LogString = logString;
            LogType = logType;
        }
    }

}
