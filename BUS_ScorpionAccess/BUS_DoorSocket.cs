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
    public class BUS_DoorSocket
    {
        DAL_DoorSocket dal = new DAL_DoorSocket();

        /// <summary>
        /// Get door socket in door
        /// </summary>
        /// <returns>Return list door socket or null if error</returns>
        public List<DTO_DoorSocket> GetDoorSocketInDoor(string DoorId)
        {
            List<DTO_DoorSocket> lstSockets = new List<DTO_DoorSocket>();
            DataSet ds = dal.GetDoorSocketsInDoor(DoorId);

            if (ds.Tables.Count < 0)
            {
                return null;
            }

            try
            {
                #region Contact
                DataTable contactTable = ds.Tables[0];
                foreach (DataRow row in contactTable.Rows)
                {
                    DTO_DoorSocket socket = new DTO_DoorSocket();
                    socket.Id = row["SocketId"] == DBNull.Value ? null : row["SocketId"].ToString();
                    socket.Door = row["Door"] == DBNull.Value ? null : row["Door"].ToString();
                    socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.Type = row["Type"] == DBNull.Value ? EType.DoorSocketType.CONTACT : (DoorSocketType)Convert.ToInt16(row["Type"].ToString());
                    socket.ConnectedDeviceSocketOrder = row["OrdNumber"] == DBNull.Value ? -1 : (int)row["OrdNumber"];
                    socket.ConnectedDevice = row["DeviceName"] == DBNull.Value ? null : row["DeviceName"].ToString();

                    DTO_Contact contact = new DTO_Contact();
                    contact.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    contact.Type = row["Type"] == DBNull.Value ? EType.ContactType.DOOR : (ContactType)Convert.ToInt16(row["Type"].ToString());
                    contact.Mode = row["Mode"] == DBNull.Value ? EType.ContactMode.NOT_USE : (ContactMode)Convert.ToInt16(row["Mode"].ToString());

                    socket.Property = contact;

                    lstSockets.Add(socket);
                }
                #endregion

                #region Lock
                DataTable lockTable = ds.Tables[1];
                foreach (DataRow row in lockTable.Rows)
                {
                    DTO_DoorSocket socket = new DTO_DoorSocket();
                    socket.Id = row["SocketId"] == DBNull.Value ? null : row["SocketId"].ToString();
                    socket.Door = row["Door"] == DBNull.Value ? null : row["Door"].ToString();
                    socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.Type = row["Type"] == DBNull.Value ? EType.DoorSocketType.CONTACT : (DoorSocketType)Convert.ToInt16(row["Type"].ToString());
                    socket.ConnectedDeviceSocketOrder = row["OrdNumber"] == DBNull.Value ? -1 : (int)row["OrdNumber"];
                    socket.ConnectedDevice = row["DeviceName"] == DBNull.Value ? null : row["DeviceName"].ToString();

                    DTO_Lock locks = new DTO_Lock();
                    locks.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    locks.Type = row["Type"] == DBNull.Value ? EType.LockType.NOT_USE : (LockType)Convert.ToInt16(row["Type"].ToString());

                    socket.Property = locks;

                    lstSockets.Add(socket);
                }
                #endregion

                #region Reader
                DataTable readerTable = ds.Tables[2];
                foreach (DataRow row in readerTable.Rows)
                {
                    DTO_DoorSocket socket = new DTO_DoorSocket();
                    socket.Id = row["SocketId"] == DBNull.Value ? null : row["SocketId"].ToString();
                    socket.Door = row["Door"] == DBNull.Value ? null : row["Door"].ToString();
                    socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.Type = row["Type"] == DBNull.Value ? EType.DoorSocketType.CONTACT : (DoorSocketType)Convert.ToInt16(row["Type"].ToString());
                    socket.ConnectedDeviceSocketOrder = row["OrdNumber"] == DBNull.Value ? -1 : (int)row["OrdNumber"];
                    socket.ConnectedDevice = row["DeviceName"] == DBNull.Value ? null : row["DeviceName"].ToString();

                    DTO_Reader reader = new DTO_Reader();
                    reader.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    reader.Type = row["Type"] == DBNull.Value ? EType.ReaderType.IN : (ReaderType)Convert.ToInt16(row["Type"].ToString());
                    reader.IsUse = row["IsUse"] == DBNull.Value ? false : (bool)(row["IsUse"]);

                    socket.Property = reader;

                    lstSockets.Add(socket);
                }
                #endregion

                #region Rex
                DataTable rexTable = ds.Tables[3];
                foreach (DataRow row in rexTable.Rows)
                {
                    DTO_DoorSocket socket = new DTO_DoorSocket();
                    socket.Id = row["SocketId"] == DBNull.Value ? null : row["SocketId"].ToString();
                    socket.Door = row["Door"] == DBNull.Value ? null : row["Door"].ToString();
                    socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    socket.Type = row["Type"] == DBNull.Value ? EType.DoorSocketType.CONTACT : (DoorSocketType)Convert.ToInt16(row["Type"].ToString());
                    socket.ConnectedDeviceSocketOrder = row["OrdNumber"] == DBNull.Value ? -1 : (int)row["OrdNumber"];
                    socket.ConnectedDevice = row["DeviceName"] == DBNull.Value ? null : row["DeviceName"].ToString();

                    DTO_Rex rex = new DTO_Rex();
                    rex.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    rex.Type = row["Type"] == DBNull.Value ? EType.RexType.IN : (RexType)Convert.ToInt16(row["Type"].ToString());


                    socket.Property = rex;

                    lstSockets.Add(socket);
                }
                #endregion

                return lstSockets;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get door socket by key (Id)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return door socket if founed or null if error</returns>
        public DTO_DoorSocket GetDoorSocketById(string Id)
        {
            DataTable dt = dal.GetDoorSocketById(Id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_DoorSocket socket = new DTO_DoorSocket();
                socket.Id = row["SocketId"] == DBNull.Value ? null : row["SocketId"].ToString();
                socket.Door = row["Door"] == DBNull.Value ? null : row["Door"].ToString();
                socket.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                socket.Type = row["Type"] == DBNull.Value ? EType.DoorSocketType.CONTACT : (DoorSocketType)Convert.ToInt16(row["Type"].ToString());
                socket.ConnectedDeviceSocketOrder = row["OrdNumber"] == DBNull.Value ? -1 : (int)row["OrdNumber"];
                socket.ConnectedDevice = row["DeviceName"] == DBNull.Value ? null : row["DeviceName"].ToString();

                switch (socket.Type)
                {
                    case DoorSocketType.CONTACT:
                        DTO_Contact contact = new DTO_Contact();
                        contact.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                        contact.Type = row["Type"] == DBNull.Value ? EType.ContactType.DOOR : (ContactType)Convert.ToInt16(row["Type"].ToString());
                        contact.Mode = row["Mode"] == DBNull.Value ? EType.ContactMode.NOT_USE : (ContactMode)Convert.ToInt16(row["Mode"].ToString());

                        socket.Property = contact;
                        break;
                    case DoorSocketType.LOCK:
                        DTO_Lock locks = new DTO_Lock();
                        locks.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                        locks.Type = row["Type"] == DBNull.Value ? EType.LockType.NOT_USE : (LockType)Convert.ToInt16(row["Type"].ToString());

                        socket.Property = locks;
                        break;
                    case DoorSocketType.READER:

                        DTO_Reader reader = new DTO_Reader();
                        reader.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                        reader.Type = row["Type"] == DBNull.Value ? EType.ReaderType.IN : (ReaderType)Convert.ToInt16(row["Type"].ToString());
                        reader.IsUse = row["IsUse"] == DBNull.Value ? false : (bool)(row["IsUse"]);

                        socket.Property = reader;
                        break;
                    case DoorSocketType.REX:
                        DTO_Rex rex = new DTO_Rex();
                        rex.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                        rex.Type = row["Type"] == DBNull.Value ? EType.RexType.IN : (RexType)Convert.ToInt16(row["Type"].ToString());

                        socket.Property = rex;
                        break;
                }

                return socket;
            }
            catch
            {
                return null;
            }
        }
    }
}
