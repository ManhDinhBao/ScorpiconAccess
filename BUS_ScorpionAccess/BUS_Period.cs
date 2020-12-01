using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace BUS_ScorpionAccess
{
    public class BUS_Period
    {
        DAL_Period dal = new DAL_Period();

        /// <summary>
        /// Get all periods inside a schedule
        /// </summary>
        /// <param name="scheduleId">schedule id</param>
        /// <returns>Return list period if founded or error if null</returns>
        public List<DTO_Period> GetAllPeriodOfSchedule(string scheduleId)
        {
            List<DTO_Period> lstPeriods = new List<DTO_Period>();
            DataTable dt = dal.GetPeriodBySchedule(scheduleId);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Period period = new DTO_Period();

                    period.WeekDay      = row["DayInWeek"] == DBNull.Value ? WeekDay.MONDAY : (WeekDay)Convert.ToInt16(row["DayInWeek"]);
                    period.Schedule     = scheduleId;
                    period.StartTime    = row["StartTime"] == DBNull.Value ? null : row["StartTime"].ToString();
                    period.EndTime      = row["EndTime"]   == DBNull.Value ? null : row["EndTime"].ToString();

                    lstPeriods.Add(period);
                }

                return lstPeriods;
            }
            catch
            {
                return null;
            }
        }
    }
}
