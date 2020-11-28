using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DAL_ScorpionAccess
{
    public class DAL_DeviceSocket:DBConnect
    {
        /// <summary>
        /// Get socket in device from database
        /// </summary>
        /// <param name="deviceId">Id of device have socket</param>
        /// <returns></returns>
        public DataTable GetSocketByDevice(string deviceId)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceSocketQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("DoorSocket", DBNull.Value);
                command.Parameters.AddWithValue("Device", deviceId);

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
        /// Get socket connected with Door from database
        /// </summary>
        /// <param name="doorId"></param>
        /// <returns></returns>
        public DataTable GetSocketConnectedDoor(string doorId)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceSocketQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("DoorSocket", doorId);
                command.Parameters.AddWithValue("Device", DBNull.Value);

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
    } 
}
