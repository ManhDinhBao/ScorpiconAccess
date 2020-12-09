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
    public class BUS_Right
    {
        DAL_UserRight dal = new DAL_UserRight();
        BUS_Door bus_Door = new BUS_Door();
        BUS_CardHolder bus_CardHolder = new BUS_CardHolder();
        BUS_Schedule bus_Schedule = new BUS_Schedule();

        /// <summary>
        /// Get all card
        /// </summary>
        /// <returns>Return list card or null if error</returns>
        public List<DTO_UserRight> GetAllUserRight()
        {
            List<DTO_UserRight> lstRighgts = new List<DTO_UserRight>();
            DataTable dt = dal.GetAllRIght();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_UserRight right = new DTO_UserRight();
                    right.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    right.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    right.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();
                    right.Doors = GetDoorInRight(right.Id);
                    right.Schedules = GetScheduleInRight(right.Id);
                    right.CardHolders = GetPersonInRight(right.Id);

                    lstRighgts.Add(right);
                }

                return lstRighgts;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get right by key (id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return right if founed or null if error</returns>
        public DTO_UserRight GetRightByKey(string id)
        {
            DataTable dt = dal.GetRightById(id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_UserRight right = new DTO_UserRight();
                right.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                right.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                right.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();
                right.Doors = GetDoorInRight(right.Id);
                right.Schedules = GetScheduleInRight(right.Id);
                right.CardHolders = GetPersonInRight(right.Id);

                return right;
            }
            catch
            {
                return null;
            }
        }

        public List<DTO_Door> GetDoorInRight(string rightId)
        {
            List<DTO_Door> lstDoor = new List<DTO_Door>();
            DataTable dt = dal.GetRightDetailByRight(rightId,0);
            if (dt.Rows.Count <= 0)
            {
                return lstDoor;
            }

            foreach(DataRow dr in dt.Rows)
            {
                string id = dr["DetailId"].ToString();
                DTO_Door door = bus_Door.GetDoorByKey(id);
                lstDoor.Add(door);
            }

            return lstDoor;
        }

        public List<DTO_CardHolder> GetPersonInRight(string rightId)
        {
            List<DTO_CardHolder> lstHolders= new List<DTO_CardHolder>();
            DataTable dt = dal.GetRightDetailByRight(rightId,2);
            if (dt.Rows.Count <= 0)
            {
                return lstHolders;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["DetailId"].ToString();
                DTO_CardHolder holder = bus_CardHolder.GetCardHolderByKey(id);
                lstHolders.Add(holder);
            }

            return lstHolders;
        }

        public List<DTO_Schedule> GetScheduleInRight(string rightId)
        {
            List<DTO_Schedule> lstSchedules = new List<DTO_Schedule>();
            DataTable dt = dal.GetRightDetailByRight(rightId, 1);
            if (dt.Rows.Count <= 0)
            {
                return lstSchedules;
            }

            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["DetailId"].ToString();
                DTO_Schedule schedule = bus_Schedule.GetScheduleByKey(id);
                lstSchedules.Add(schedule);
            }

            return lstSchedules;
        }

        /// <summary>
        /// Add new right
        /// </summary>
        /// <param name="card">The right object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewRight(DTO_UserRight right)
        {
            if (right == null)
            {
                return new SQLResult(false, "Right null");
            }

            return dal.AddNewRight(right);
        }

        /// <summary>
        /// Update right information
        /// </summary>
        /// <param name="card">The right object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateRight(DTO_UserRight right)
        {
            if (right == null)
            {
                return new SQLResult(false, "Right null");
            }

            return dal.UpdateRight(right);
        }

        /// <summary>
        /// Delete right
        /// </summary>
        /// <param name="rightId">id of right want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteRight(string rightId)
        {
            if (rightId == null)
            {
                return new SQLResult(false, "Right null");
            }

            return dal.DeleteRight(rightId);
        }

        public SQLResult AddDoorToRight(string RightId, string DetailId)
        {
            return dal.AddDoorToRight(RightId, DetailId);
        }

        public SQLResult AddScheduleToRight(string RightId, string DetailId)
        {
            return dal.AddScheduleToRight(RightId, DetailId);
        }

        public SQLResult AddPersonToRight(string RightId, string DetailId)
        {
            return dal.AddPersonToRight(RightId, DetailId);
        }

        public SQLResult DeleteDoorInRight(string RightId, string DetailId)
        {
            return dal.DeleteDoorInRight(RightId, DetailId);
        }

        public SQLResult DeleteScheduleInRight(string RightId, string DetailId)
        {
            return dal.DeleteScheduleInRight(RightId, DetailId);
        }

        public SQLResult DeletePersonInRight(string RightId, string DetailId)
        {
            return dal.DeletePersonInRight(RightId, DetailId);
        }
    }
}
