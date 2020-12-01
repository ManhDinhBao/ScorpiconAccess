using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Schedule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DTO_Period> Periods { get; set; }

        public DTO_Schedule()
        {
        }

        public DTO_Schedule(string id, string name, string description, List<DTO_Period> periods)
        {
            Id = id;
            Name = name;
            Description = description;
            Periods = periods;
        }

        public string PeriodsToString()
        {
            string result = null;
            foreach(DTO_Period period in Periods)
            {
                result += string.Format("{0}_/{1}_/{2}_/{3}=/", period.WeekDay, period.Schedule, period.StartTime, period.EndTime);
            }

            for(int i = 0; i < Periods.Count; i++)
            {
                result += string.Format("{0}_/{1}_/{2}_/{3}", Periods[i].WeekDay, Periods[i].Schedule, Periods[i].StartTime, Periods[i].EndTime);
                if (i < Periods.Count-1)
                {
                    result += "=/";
                }
            }

            return result;
        }
    }
}
