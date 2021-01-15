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
    public class BUS_Event
    {
        DAL_Event dal = new DAL_Event();

        public List<DTO_Event> GetEvent(string cardNumber, string holderName, string doorName, int status,int deptId, DateTime startTime, DateTime endTime)
        {
            List<DTO_Event> lstEvents = new List<DTO_Event>();
            DataTable dt = dal.GetEvent(cardNumber,holderName,doorName,status,deptId, startTime, endTime);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Event ev = new DTO_Event();
                    ev.Id                 = row["Id"] == DBNull.Value ? -1 : int.Parse(row["Id"].ToString());
                    ev.EventOccurTime     = row["EventOccurTime"] == DBNull.Value ? new DateTime(1970,01,01) : Convert.ToDateTime(row["EventOccurTime"].ToString());
                    ev.EventSendTime      = row["EventSendTime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["EventSendTime"].ToString());
                    ev.CardNumber         = row["CardNumber"] == DBNull.Value ? -1 : int.Parse(row["CardNumber"].ToString());
                    ev.CardSerial         = row["CardSerial"] == DBNull.Value ? "" : row["CardSerial"].ToString();
                    ev.HolderId           = row["HolderId"] == DBNull.Value ? -1 : int.Parse(row["HolderId"].ToString());
                    ev.HolderName         = row["HolderName"] == DBNull.Value ? "" : row["HolderName"].ToString();
                    ev.DoorId             = row["DoorId"] == DBNull.Value ? -1 : int.Parse(row["DoorId"].ToString());
                    ev.DoorName           = row["DoorName"] == DBNull.Value ? "" : row["DoorName"].ToString();
                    ev.Status             = row["Status"] == DBNull.Value ? 0 : (EType.EventStatus)int.Parse(row["Status"].ToString());
                    ev.DeviceName         = row["DeviceName"] == DBNull.Value ? "" : row["DeviceName"].ToString();
                    ev.Message            = row["Message"] == DBNull.Value ? "" : row["Message"].ToString();
                    ev.Direction          = row["Direction"] == DBNull.Value ? 0 : (EType.Direction)int.Parse(row["Direction"].ToString());

                    lstEvents.Add(ev);
                }

                return lstEvents;
            }
            catch
            {
                return null;
            }
        }
    }
}
