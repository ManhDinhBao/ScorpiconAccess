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
    public class DAL_Schedule:DBConnect
    {
        /// <summary>
        /// Get all schedule from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSchedule()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("@Id", DBNull.Value);
                command.Parameters.AddWithValue("@Name", DBNull.Value);

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
        /// Get schedule by key (Id)
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetScheduledById(string Id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Name", DBNull.Value);

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
        /// Get schedule by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetScheduledByName(string name)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("@Id", DBNull.Value);
                command.Parameters.AddWithValue("@Name", name);

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
        /// Insert new schedule to database
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public SQLResult AddNewSchedule(DTO_Schedule schedule)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("@Id", "");
                command.Parameters.AddWithValue("@Name", schedule.Name);
                command.Parameters.AddWithValue("@Description", schedule.Description);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
                result.ExtraData = dt.Rows[0]["ExtraData"].ToString();
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
        /// Update schedule information in database
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public SQLResult UpdateSchedule(DTO_Schedule schedule)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("@Id", schedule.Id);
                command.Parameters.AddWithValue("@Name", schedule.Name);
                command.Parameters.AddWithValue("@Description", schedule.Description);

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
        /// Delete schedule by serial
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public SQLResult DeleteSchedule(string serial)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLScheduleSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("@Id", serial);
                command.Parameters.AddWithValue("@Name", DBNull.Value);
                command.Parameters.AddWithValue("@Description", DBNull.Value);

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
