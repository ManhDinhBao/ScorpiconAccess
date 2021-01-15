using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class EType
    {
        public enum Gender
        {
            MALE = 0,
            FEMALE = 1,
            OTHER = 2
        }

        public enum CardType
        {
            NOT_USE = 0,
            USED = 1,
            BLOCK = 2,
            LOST = 3
        }

        /// <summary>
        /// Control mode
        /// </summary>
        public enum ControlMode
        {
            CONTROL_DOOR = 1,
            CONTROL_ELEVATOR = 2
        }

        public enum DeviceSocketType
        {
            READER = 0,
            INPUT = 1,
            OUTPUT = 2
        }
        public enum DoorSocketType
        {
            CONTACT = 0,
            LOCK = 1,
            READER = 2,
            REX = 3
        }

        public enum LockType
        {
            NOT_USE = 0,
            NC = 1,
            NO = 2
        }

        public enum ContactType
        {
            DOOR = 1,
            LOCK = 2
        }
        public enum ContactMode
        {
            NOT_USE = 0,
            NC = 1,
            NO = 2
        }

        public enum ReaderType
        {
            IN = 1,
            OUT = 2
        }
        public enum RexType
        {
            IN = 1,
            OUT = 2
        }

        public enum GenConfigFileType
        {
            CARD = 1,
            DOOR = 2,
            SCHEDULE = 3,
            ACCESSGR = 4,
            ALL = 5
        }

        public enum UserLogType
        {
            LOG_TIME = 1,
            LOG_CONTENT = 2,
            LOG_STATUS_SUCCESS = 3,
            LOG_STATUS_ERROR = 4,
            LOG_STATUS_WARNING = 5,
        }

        public enum WindowMode
        {
            ADD_MODE = 1,
            EDIT_MODE = 2
        }

        public enum WeekDay
        {
            MONDAY = 1,
            TUESDAY = 2,
            WEDNESDAY = 3,
            THURSDAY = 4,
            FRIDAY = 5,
            SATURDAY = 6,
            SUNDAY = 0
        }

        public enum Direction
        {
            OTHER = 0,
            IN = 1,
            OUT = 2
           
        }

        public enum EventStatus
        {
            OTHER = 0,            
            GRANDTED = 1,
            DENIED = 2
        }
    }

}
