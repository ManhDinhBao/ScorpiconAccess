using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class ScheduleConfig
    {
        public static string comment = @"# Schedules configuration file
# i=interval definition   i S D I H1 M1 S1 H2 M2 S2
# h=holiday interval definition h S D I H1 M1 S1 H2 M2 S2
# S=schedule id
# D=day code:normal (0=Sun, 1=Mon, 2=Tue, 3=Wed, 4=Thurs, 5=Fri, 6=Sat)
# D=day code:holiday (1=Holiday Group 1, 2=Holiday Group 2, ....)
# I=interval (0 to 5)
# H1 M1 S1 = Start Hour Min Sec (0 0 0 to 23 59 59)
# H2 M2 S2 = Stop Hour Min Sec (0 0 0 to 23 59 59)
# i S D I H1 M1 S1 H2 M2 S2
# (7am-12pm & 12:50-6:30pm Mon-Sat)
# (Holiday Group 1,2,4 - No Access on 1, 8am -12pm & 1pm-5pm on 2 & 4) ";

        public string IntervalDefinition { get; set; }
        public int ScheduleId { get; set; }
        public int DayCode { get; set; }
        public int Interval { get; set; }
        public int H1 { get; set; }
        public int M1 { get; set; }
        public int S1 { get; set; }
        public int H2 { get; set; }
        public int M2 { get; set; }
        public int S2 { get; set; }

        public ScheduleConfig()
        {
        }

        public ScheduleConfig(string intervalDefinition, int scheduleId, int dayCode, int interval, int h1, int m1, int s1, int h2, int m2, int s2)
        {
            IntervalDefinition = intervalDefinition;
            ScheduleId = scheduleId;
            DayCode = dayCode;
            Interval = interval;
            H1 = h1;
            M1 = m1;
            S1 = s1;
            H2 = h2;
            M2 = m2;
            S2 = s2;
        }

        public string ToFileString()
        {
            string result = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", IntervalDefinition, ScheduleId, DayCode,Interval,H1,M1,S1,H2,M2,S2);
            return result + "\r\n";
        }
    }
}
