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
    public class BUS_General
    {
        DAL_General dal = new DAL_General();

        /// <summary>
        /// Auto generate Id by format: XXX000000000
        /// </summary>
        /// <param name="type">Object type</param>
        /// Card holder = HOLDER
        /// Device = DEVICE
        /// Door = DOOR
        /// Schedule = SCHEDULE
        /// User right = RIGHT
        /// <returns></returns>
        public string GenerateId(string type)
        {
            return dal.GenerateId(type);
        }


        public List<CardConfig> GetCardConfigs(string listDoorString)
        {
            List<CardConfig> lstConfigs = new List<CardConfig>();
            DataTable dt = dal.GetCardFileConfig(listDoorString);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    CardConfig config = new CardConfig();
                    config.CardSerial = row["CardHexCode"] == DBNull.Value ? null : row["CardHexCode"].ToString();
                    config.CardUniqueId = Convert.ToInt32(row["CardNumber"]);
                    config.StartDate = Convert.ToDateTime(row["STime"]);
                    config.EndDate = Convert.ToDateTime(row["ETime"]);

                    int a0 = Convert.ToInt32(row["a0"]);
                    int a1 = Convert.ToInt32(row["a1"]);
                    int a2 = Convert.ToInt32(row["a2"]);
                    int a3 = Convert.ToInt32(row["a3"]);
                    int a4 = Convert.ToInt32(row["a4"]);
                    int a5 = Convert.ToInt32(row["a5"]);
                    int a6 = Convert.ToInt32(row["a6"]);
                    int a7 = Convert.ToInt32(row["a7"]);

                    config.ListGroupAccess = new List<int> { a0, a1, a2, a3, a4, a5, a6, a7 };
                    config.Pin = row["PinCode"] == DBNull.Value ? 0 : Convert.ToInt32(row["PinCode"]);

                    lstConfigs.Add(config);
                }

                return lstConfigs;
            }
            catch
            {
                return null;
            }
        }

        public List<DoorConfig> GetDoorGroupCongfigs(string listRight)
        {
            List<DoorConfig> lstConfigs = new List<DoorConfig>();
            DataTable dt = dal.GetDoorGroupFileConfig(listRight);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DoorConfig config = new DoorConfig();
                    config.DoorGroupId = row["doorGrId"] == DBNull.Value ? null : row["doorGrId"].ToString();
                    config.DoorCount = Convert.ToInt32(row["doorCount"]);
                    config.ListDoor = row["listDoor"] == DBNull.Value ? null : row["listDoor"].ToString();

                    lstConfigs.Add(config);
                }

                return lstConfigs;
            }
            catch
            {
                return null;
            }
        }

        public List<ScheduleConfig> GetScheduleCongfigs(string listSchedule)
        {
            List<ScheduleConfig> lstConfigs = new List<ScheduleConfig>();
            DataTable dt = dal.GetScheduleFileConfig(listSchedule);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    ScheduleConfig config = new ScheduleConfig();
                    config.IntervalDefinition = row["intervalDef"].ToString();
                    config.ScheduleId = Convert.ToInt32(row["scheduleId"]);
                    config.DayCode = Convert.ToInt32(row["dayCode"]);
                    config.Interval = Convert.ToInt32(row["interval"]);
                    config.H1 = Convert.ToInt32(row["H1"]);
                    config.M1 = Convert.ToInt32(row["M1"]);
                    config.S1 = Convert.ToInt32(row["S1"]);
                    config.H2 = Convert.ToInt32(row["H2"]);
                    config.M2 = Convert.ToInt32(row["M2"]);
                    config.S2 = Convert.ToInt32(row["S2"]);

                    lstConfigs.Add(config);
                }

                return lstConfigs;
            }
            catch
            {
                return null;
            }
        }

        public List<AccessGroupConfig> GetAccessGroupCongfigs(string listRight)
        {
            List<AccessGroupConfig> lstConfigs = new List<AccessGroupConfig>();
            DataTable dt = dal.GetAccessGRFileConfig(listRight);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    AccessGroupConfig config = new AccessGroupConfig();
                    config.DoorGroupId = row["doorGrId"] == DBNull.Value ? null : row["doorGrId"].ToString();
                    config.Detail = row["Detail"] == DBNull.Value ? null : row["Detail"].ToString();

                    lstConfigs.Add(config);
                }

                return lstConfigs;
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetAttendance(DateTime month, int deptId)
        {
            return dal.GetAttendance(month, deptId);
        }

        public void SetConnect(string server, string port)
        {
            string path = string.Format("Data Source = {0},{1};Initial Catalog=LefaAccess;User ID=ibs;Password=ibs;Connection Timeout=10", server, port);

            DBConnect dBConnect = new DBConnect();
            dBConnect.SetConnect(path);
        }
    }
}
