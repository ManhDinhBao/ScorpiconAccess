using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DAL_DoorMode:DBConnect
    {
        /// <summary>
        /// Get all door mode from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDoorMode()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorModeQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("ReadersUse", DBNull.Value);
                command.Parameters.AddWithValue("InputUse", DBNull.Value);
                command.Parameters.AddWithValue("OutputUse", DBNull.Value);

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
