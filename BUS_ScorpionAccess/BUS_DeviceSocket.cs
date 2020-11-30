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
    public class BUS_DeviceSocket
    {
        DAL_DeviceSocket dal = new DAL_DeviceSocket();

        /// <summary>
        /// Get socket in device
        /// </summary>
        /// /// <param name="deviceId">Id of device have socket</param>
        /// <returns>Return list device socket or null if error</returns>
        public List<DTO_DeviceSocket> GetSocketByDevice(string deviceId)
        {
            List<DTO_DeviceSocket> lstSockets = new List<DTO_DeviceSocket>();
            DataTable dt = dal.GetSocketByDevice(deviceId);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_DeviceSocket socket = new DTO_DeviceSocket();
                    socket.Id           = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    socket.Device       = row["Device"] == DBNull.Value ? null : row["Device"].ToString();
                    socket.Name         = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.OrdNumber    = row["OrdNumber"] == DBNull.Value ? -1 : Convert.ToInt16(row["OrdNumber"].ToString());
                    socket.Type         = row["Type"] == DBNull.Value ? EType.DeviceSocketType.READER : (EType.DeviceSocketType)row["Type"];
                    socket.IsUse        = row["IsUse"] == DBNull.Value ? false : (bool)row["IsUse"];
                    socket.DoorSocket   = row["DoorSocket"] == DBNull.Value ? null : row["DoorSocket"].ToString();

                    lstSockets.Add(socket);
                }

                return lstSockets;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get socket connected with door
        /// </summary>
        /// /// <param name="doorId">Id of door connected with socket</param>
        /// <returns>Return list device socket or null if error</returns>
        public List<DTO_DeviceSocket> GetSocketConnectedDoor(string doorId)
        {
            List<DTO_DeviceSocket> lstSockets = new List<DTO_DeviceSocket>();
            DataTable dt = dal.GetSocketConnectedDoor(doorId);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_DeviceSocket socket = new DTO_DeviceSocket();
                    socket.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    socket.Device = row["Device"] == DBNull.Value ? null : row["Device"].ToString();
                    socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.OrdNumber = row["OrdNumber"] == DBNull.Value ? -1 : Convert.ToInt16(row["OrdNumber"].ToString());
                    socket.Type = row["Type"] == DBNull.Value ? EType.DeviceSocketType.READER : (EType.DeviceSocketType)row["Type"];
                    socket.IsUse = row["IsUse"] == DBNull.Value ? false : (bool)row["IsUse"];
                    socket.DoorSocket = row["DoorSocket"] == DBNull.Value ? null : row["DoorSocket"].ToString();

                    lstSockets.Add(socket);
                }

                return lstSockets;
            }
            catch
            {
                return null;
            }
        }
    }
}
