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

                    period.Id           = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    period.WeekDay      = row["DayInWeek"] == DBNull.Value ? WeekDay.MONDAY : (WeekDay)Convert.ToInt16(row["DayInWeek"]);
                    period.Schedule     = scheduleId;
                    period.StartTime    = row["StartTime"] == DBNull.Value ? new DateTime(1970,1,1) : Convert.ToDateTime(row["StartTime"].ToString());
                    period.EndTime      = row["EndTime"]   == DBNull.Value ? new DateTime(1970, 1, 1) : Convert.ToDateTime(row["EndTime"].ToString());

                    lstPeriods.Add(period);
                }

                return lstPeriods;
            }
            catch
            {
                return null;
            }
        }

        public DTO_Period GetPeriodByKey(string periodId)
        {
            DataTable dt = dal.GetPeriodById(periodId);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_Period period = new DTO_Period();

                period.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                period.WeekDay = row["DayInWeek"] == DBNull.Value ? WeekDay.MONDAY : (WeekDay)Convert.ToInt16(row["DayInWeek"]);
                period.Schedule = row["Schedule"] == DBNull.Value ? null : row["Schedule"].ToString();
                period.StartTime = row["StartTime"] == DBNull.Value ? new DateTime(1970, 1, 1) : Convert.ToDateTime(row["StartTime"].ToString());
                period.EndTime = row["EndTime"] == DBNull.Value ? new DateTime(1970, 1, 1) : Convert.ToDateTime(row["EndTime"].ToString());

                return period;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new period
        /// </summary>
        /// <param name="period">The period object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewPeriod(DTO_Period period)
        {
            if (period == null)
            {
                return new SQLResult(false, "Period null");
            }

            return dal.AddNewPeriod(period);
        }

        /// <summary>
        /// Update period information
        /// </summary>
        /// <param name="period">The card period want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdatePeriod(DTO_Period period)
        {
            if (period == null)
            {
                return new SQLResult(false, "Period null");
            }

            return dal.UpdatePeriod(period);
        }

        /// <summary>
        /// Delete period
        /// </summary>
        /// <param name="periodId">Id of period want to delete</param>
        /// <returns></returns>
        public SQLResult DeletePeriod(string periodId)
        {
            if (periodId == null)
            {
                return new SQLResult(false, "Period Id can't null");
            }

            return dal.DeletePeriod(periodId);
        }
    }
}
