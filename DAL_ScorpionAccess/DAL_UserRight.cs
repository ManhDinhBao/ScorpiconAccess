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
    public class DAL_UserRight:DBConnect
    {
        /// <summary>
        /// Get all right from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRIght()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);

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
        /// Get card by key (id) from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetRightById(string id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", id);

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

        public DataTable GetRightDetailByRight(string rightId, int type)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailQry";
                command.Parameters.AddWithValue("WorkType", "Q");               
                command.Parameters.AddWithValue("RightId", rightId);
                command.Parameters.AddWithValue("Type", type);

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
        /// Insert new right to database
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public SQLResult AddNewRight(DTO_UserRight right)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                command.Parameters.AddWithValue("Name", right.Name);
                command.Parameters.AddWithValue("Description", right.Description);

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
        /// Update right information in database
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        public SQLResult UpdateRight(DTO_UserRight right)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", right.Id);
                command.Parameters.AddWithValue("Name", right.Name);
                command.Parameters.AddWithValue("Description", right.Description);

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
        /// Delete right by id
        /// </summary>
        /// <param name="rightId"></param>
        /// <returns></returns>
        public SQLResult DeleteRight(string rightId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", rightId);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("Description", DBNull.Value);

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
        /// Add door to right
        /// </summary>
        public SQLResult AddDoorToRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 0);

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
        /// Add schedule to right
        /// </summary>
        public SQLResult AddScheduleToRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 1);

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
        /// Add person to right
        /// </summary>
        public SQLResult AddPersonToRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 2);

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
        /// Delete door in right
        /// </summary>
        public SQLResult DeleteDoorInRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 0);

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
        /// Delete schedule in right
        /// </summary>
        public SQLResult DeleteScheduleInRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 1);

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
        /// Delete person in right
        /// </summary>
        public SQLResult DeletePersonInRight(string RightId, string DetailId)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLRightDetailSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("RightId", RightId);
                command.Parameters.AddWithValue("DetailId", DetailId);
                command.Parameters.AddWithValue("Type", 2);

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
