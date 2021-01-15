using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Event
    {
        public int Id { get; set; }
        public DateTime EventOccurTime { get; set; }
        public DateTime EventSendTime { get; set; }
        public int CardNumber { get; set; }
        public string CardSerial { get; set; }
        public int HolderId { get; set; }
        public string HolderName { get; set; }
        public int DoorId { get; set; }
        public string DoorName { get; set; }
        public EType.EventStatus Status { get; set; }

        public string DeviceName { get; set; }
        public string Message { get; set; }
        public EType.Direction Direction { get; set; }

        public DTO_Event()
        {
        }

        public DTO_Event(int id, DateTime eventOccurTime, DateTime eventSendTime, int cardNumber, string cardSerial, int holderId, string holderName, int doorId, string doorName, EType.EventStatus status, string deviceName, string message, EType.Direction direction)
        {
            Id = id;
            EventOccurTime = eventOccurTime;
            EventSendTime = eventSendTime;
            CardNumber = cardNumber;
            CardSerial = cardSerial;
            HolderId = holderId;
            HolderName = holderName;
            DoorId = doorId;
            DoorName = doorName;
            Status = status;
            DeviceName = deviceName;
            Message = message;
            Direction = direction;
        }
    }
}
