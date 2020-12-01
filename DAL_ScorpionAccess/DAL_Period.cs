using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DAL_Period:DBConnect
    {
        /// <summary>
        /// Get period inside a schedule from database
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public DataTable GetPeriodBySchedule(string scheduleId)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLPeriodQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Schedule", scheduleId);

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
