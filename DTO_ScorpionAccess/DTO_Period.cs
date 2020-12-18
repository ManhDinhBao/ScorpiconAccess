using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Period
    {
        public string Id { get; set; }
        public EType.WeekDay WeekDay { get; set; }
        public string Schedule { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DTO_Period()
        {
        }

        public DTO_Period(string id, EType.WeekDay weekDay, string schedule, DateTime startTime, DateTime endTime)
        {
            Id = id;
            WeekDay = weekDay;
            Schedule = schedule;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

}
