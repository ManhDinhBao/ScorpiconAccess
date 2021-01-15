using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScorpiconAccess
{
    public class Repository
    {
        public static List<DTO_DoorMode> lstAllDoorMode;
        public static List<DTO_Card> lstAllCards;
        public static List<DTO_CardHolder> lstAllCardHolders;
        public static List<DTO_Device> lstAllDevices;
        public static List<DTO_Schedule> lstAllSchedules;
        public static List<DTO_Door> lstAllDoor;
        public static List<DTO_UserRight> lstAllRIght;
        public static List<DTO_Department> lstAllDepartment;



        public static DTO_Card selectedCard;
        public static DTO_Card newCard;

        public static DTO_CardHolder selectedHolder;
        public static DTO_CardHolder newHolder;

        public static DTO_Device selectedDevice;
        public static DTO_Device newDevice;

        public static DTO_Schedule selectedSchedule;
        public static DTO_Schedule newSchedule;

        public static DTO_Door selectedDoor;
        public static DTO_Door newDoor;

        public static DTO_UserRight selectedRight;
        public static DTO_UserRight newRight;

    }
}
