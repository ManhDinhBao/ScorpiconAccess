using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DoorConfig
    {
        public string  DoorGroupId { get; set; }
        public int DoorCount { get; set; }
        public string ListDoor { get; set; }

        public DoorConfig()
        {
        }

        public DoorConfig(string doorGroupId, int doorCount, string listDoor)
        {
            DoorGroupId = doorGroupId;
            DoorCount = doorCount;
            ListDoor = listDoor;
        }

        public string ToFileString()
        {
            string result = string.Format("{0} {1} {2}", DoorGroupId,DoorCount,ListDoor);
            return result + "\r\n";
        }
    }
}
