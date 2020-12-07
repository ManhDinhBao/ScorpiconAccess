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
    public class DAL_Door : DBConnect
    {
        /// <summary>
        /// Get all door from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDoor()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);

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
        /// Get door by key (Id) from database
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataTable GetDoorById(string Id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);

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
        /// Get door by serial or holder from databse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SearchDoor(string value)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", value);

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
