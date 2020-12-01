using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Period
    {
        public EType.WeekDay WeekDay { get; set; }
        public string Schedule { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public DTO_Period()
        {
        }

        public DTO_Period(EType.WeekDay weekDay, string schedule, string startTime, string endTime)
        {
            WeekDay = weekDay;
            Schedule = schedule;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

}
