using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_DeviceSocket
    {
        public string Id { get; set; }
        public string Device { get; set; }
        public string DoorSocket { get; set; }
        public int OrdNumber { get; set; }
        public EType.DeviceSocketType Type { get; set; }
        public bool IsUse { get; set; }

        public DTO_DeviceSocket()
        {
        }

        public DTO_DeviceSocket(string id, string device, string doorSocket, int ordNumber, EType.DeviceSocketType type, bool isUse)
        {
            Id = id;
            Device = device;
            DoorSocket = doorSocket;
            OrdNumber = ordNumber;
            Type = type;
            IsUse = isUse;
        }
    }
}
