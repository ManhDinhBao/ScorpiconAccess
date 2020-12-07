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
    }
}
