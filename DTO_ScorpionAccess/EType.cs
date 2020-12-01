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
