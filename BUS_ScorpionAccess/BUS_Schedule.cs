using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_ScorpionAccess
{
    public class BUS_Schedule
    {
        DAL_Schedule dal = new DAL_Schedule();
        BUS_Period bus_period = new BUS_Period();

        /// <summary>
        /// Get all schedule
        /// </summary>
        /// <returns>Return list schedule or null if error</returns>
        public List<DTO_Schedule> GetAllSchedule()
        {
            List<DTO_Schedule> lstSchedules = new List<DTO_Schedule>();
            DataTable dt = dal.GetAllSchedule();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Schedule  schedule = new DTO_Schedule();
                    schedule.Id             = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    schedule.Name           = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    schedule.Description    = row["Description"] == DBNull.Value ? null : row["Description"].ToString();
                    schedule.Periods        = bus_period.GetAllPeriodOfSchedule(schedule.Id);

                    lstSchedules.Add(schedule);
                }

                return lstSchedules;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get schedule by key (id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return schedule if founed or null if error</returns>
        public DTO_Schedule GetScheduleByKey(string id)
        {
            DataTable dt = dal.GetScheduledById(id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_Schedule schedule = new DTO_Schedule();
                schedule.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                schedule.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                schedule.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();
                schedule.Periods = bus_period.GetAllPeriodOfSchedule(schedule.Id);

                return schedule;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Search schedule by name
        /// </summary>
        /// <param name="scheduleName">Schedule name</param>
        /// <returns>Return list schedule if founded or error if null</returns>
        public List<DTO_Schedule> SearchCard(string scheduleName)
        {

            List<DTO_Schedule> lstSchedules = new List<DTO_Schedule>();
            DataTable dt = dal.GetScheduledByName(scheduleName);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Schedule schedule = new DTO_Schedule();
                    schedule.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    schedule.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    schedule.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();
                    schedule.Periods = bus_period.GetAllPeriodOfSchedule(schedule.Id);

                    lstSchedules.Add(schedule);
                }

                return lstSchedules;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new schedule
        /// </summary>
        /// <param name="card">The schedule object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewSchedule(DTO_Schedule schedule)
        {
            if (schedule == null)
            {
                return new SQLResult(false, "Schedule null");
            }

            return dal.AddNewSchedule(schedule);
        }

        /// <summary>
        /// Update schedule information
        /// </summary>
        /// <param name="schedule">The schedule object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateSchedule(DTO_Schedule schedule)
        {
            if (schedule == null)
            {
                return new SQLResult(false, "Schedule null");
            }


            return dal.UpdateSchedule(schedule);
        }

        /// <summary>
        /// Delete schedule
        /// </summary>
        /// <param name="scheduleId">Id of schedule want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteCard(string scheduleId)
        {
            if (scheduleId == null)
            {
                return new SQLResult(false, "Schedule id can't null");
            }

            return dal.DeleteSchedule(scheduleId);
        }
    }
}
