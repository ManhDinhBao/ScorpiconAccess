using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class AccessGroupConfig
    {
        public string DoorGroupId { get; set; }
        public string Detail { get; set; }

        public AccessGroupConfig()
        {
        }

        public AccessGroupConfig(string doorGroupId, string detail)
        {
            DoorGroupId = doorGroupId;
            Detail = detail;
        }

        public string ToFileString()
        {
            string result = string.Format("{0} {1}", DoorGroupId, Detail);
            return result + "\r\n";
        }
    }
}
