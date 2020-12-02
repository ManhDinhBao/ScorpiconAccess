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

        /// <summary>
        /// Insert new period to database
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public SQLResult AddNewPeriod(DTO_Period period)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLPeriodSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("@Id", period.Id);
                command.Parameters.AddWithValue("@DayInWeek", period.WeekDay);
                command.Parameters.AddWithValue("@Schedule", period.Schedule);
                command.Parameters.AddWithValue("@StartTime", period.StartTime);
                command.Parameters.AddWithValue("@EndTime", period.EndTime);

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
        /// Update period information in database
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        public SQLResult UpdatePeriod(DTO_Period period)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLPeriodSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("@Id", period.Id);
                command.Parameters.AddWithValue("@DayInWeek", period.WeekDay);
                command.Parameters.AddWithValue("@Schedule", period.Schedule);
                command.Parameters.AddWithValue("@StartTime", period.StartTime);
                command.Parameters.AddWithValue("@EndTime", period.EndTime);

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
        /// Delete period by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SQLResult DeletePeriod(string Id)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLPeriodSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@DayInWeek", DBNull.Value);
                command.Parameters.AddWithValue("@Schedule", DBNull.Value);
                command.Parameters.AddWithValue("@StartTime", DBNull.Value);
                command.Parameters.AddWithValue("@EndTime", DBNull.Value);

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
