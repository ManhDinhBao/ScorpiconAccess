using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DTO_ScorpionAccess
{
    public class DTO_DeviceSocket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Device { get; set; }
        public int OrdNumber { get; set; }
        public DeviceSocketType Type { get; set; }
        public bool IsUse { get; set; }
        public string DoorSocket { get; set; }

        public DTO_DeviceSocket()
        {
        }

        public DTO_DeviceSocket(string id, string name, string device, int ordNumber, DeviceSocketType type, bool isUse, string doorSocket)
        {
            Id = id;
            Name = name;
            Device = device;
            OrdNumber = ordNumber;
            Type = type;
            IsUse = isUse;
            DoorSocket = doorSocket;
        }
    }
}
