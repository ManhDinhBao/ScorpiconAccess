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



        public enum UserLogType
        {
            LOG_TIME = 1,
            LOG_CONTENT = 2,
            LOG_STATUS_SUCCESS = 3,
            LOG_STATUS_ERROR = 4,
            LOG_STATUS_WARNING = 5,
        }

        public enum WeekDay
        {
            MONDAY = 0,
            TUESDAY = 1,
            WEDNESDAY = 2,
            THURSDAY = 3,
            FRIDAY = 4,
            SATURDAY = 5,
            SUNDAY = 6
        }
    }

}
