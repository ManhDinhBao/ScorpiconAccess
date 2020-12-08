using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DAL_DoorSocket:DBConnect
    {
        /// <summary>
        /// Get door socket by key (Id) from database
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetDoorSocketById(string Id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSocketQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("DoorId", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch
            {

            }
            finally 
            {
                _conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Get all door sockets in a door from database
        /// </summary>
        /// <param name="DoorId"></param>
        /// <returns></returns>
        public DataSet GetDoorSocketsInDoor(string DoorId)
        {
            DataSet dataSet = new DataSet();
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSocketQry";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("DoorId", DoorId);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            catch
            {

            }
            finally
            {
                _conn.Close();
            }

            return dataSet;
        }

        /// <summary>
        /// Insert new door socket to database
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public SQLResult AddNewSocket(DTO_DoorSocket socket)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSocketSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                command.Parameters.AddWithValue("Name", socket.Name);
                command.Parameters.AddWithValue("Door", socket.Door);
                command.Parameters.AddWithValue("Type", (int)socket.Type);
                if (socket.ConnectedDeviceSocketId == null)
                {
                    command.Parameters.AddWithValue("DeviceSocket", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("DeviceSocket", socket.ConnectedDeviceSocketId);
                }


                switch (socket.Type)
                {
                    case EType.DoorSocketType.CONTACT:
                        DTO_Contact contact = (DTO_Contact)socket.Property;
                        command.Parameters.AddWithValue("SocketType", contact.Type);
                        command.Parameters.AddWithValue("SocketMode", contact.Mode);
                        break;
                    case EType.DoorSocketType.LOCK:
                        DTO_Lock llock = (DTO_Lock)socket.Property;
                        command.Parameters.AddWithValue("SocketType", llock.Type);
                        command.Parameters.AddWithValue("SocketMode", DBNull.Value);
                        break;
                    case EType.DoorSocketType.READER:
                        DTO_Reader reader = (DTO_Reader)socket.Property;
                        command.Parameters.AddWithValue("SocketType", reader.Type);
                        command.Parameters.AddWithValue("SocketMode", DBNull.Value);
                        break;
                    case EType.DoorSocketType.REX:
                        DTO_Rex rex = (DTO_Rex)socket.Property;
                        command.Parameters.AddWithValue("SocketType", rex.Type);
                        command.Parameters.AddWithValue("SocketMode", DBNull.Value);
                        break;

                }

               
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Update door socket information in database
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public SQLResult UpdateSocket(DTO_DoorSocket socket)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSocketSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", socket.Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("Door", DBNull.Value);
                command.Parameters.AddWithValue("Type", DBNull.Value);
                if (socket.ConnectedDeviceSocketId == null)
                {
                    command.Parameters.AddWithValue("DeviceSocket", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("DeviceSocket", socket.ConnectedDeviceSocketId);
                }
                command.Parameters.AddWithValue("SocketType", DBNull.Value);
                command.Parameters.AddWithValue("SocketMode", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Delete door socket by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SQLResult DeleteSocket(string id, string connectedDeviceSocketId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSocketSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("Door", DBNull.Value);
                command.Parameters.AddWithValue("Type", DBNull.Value);
                if (connectedDeviceSocketId == null)
                {
                    command.Parameters.AddWithValue("DeviceSocket", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("DeviceSocket", connectedDeviceSocketId);
                }
              
                command.Parameters.AddWithValue("SocketType", DBNull.Value);
                command.Parameters.AddWithValue("SocketMode", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
            }
            catch (Exception ex)
            {
                result.Detail = ex.Message;
            }
            finally
            {
                _conn.Close();
            }

            return result;
        }
    }
}
