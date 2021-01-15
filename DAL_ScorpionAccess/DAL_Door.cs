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

        /// <summary>
        /// Insert new door to database
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public SQLResult AddNewDoor(DTO_Door door)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                if (door.Name == null)
                {
                    command.Parameters.AddWithValue("Name", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Name", door.Name);

                }
                command.Parameters.AddWithValue("LOTimeOut", door.LOTimeOut);
                command.Parameters.AddWithValue("DOTimeOut", door.DOTimeOut);
                command.Parameters.AddWithValue("Mode", door.Mode.Id);

                if (door.Description == null)
                {
                    command.Parameters.AddWithValue("Description", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Description", door.Description);

                }

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
        /// Update door information in database
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        public SQLResult UpdateDoor(DTO_Door door)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", door.Id);
                if (door.Name == null)
                {
                    command.Parameters.AddWithValue("Name", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Name", door.Name);

                }
                command.Parameters.AddWithValue("LOTimeOut", door.LOTimeOut);
                command.Parameters.AddWithValue("DOTimeOut", door.DOTimeOut);
                command.Parameters.AddWithValue("Mode", door.Mode.Id);

                if (door.Description == null)
                {
                    command.Parameters.AddWithValue("Description", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Description", door.Description);

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
        /// Delete door by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SQLResult DeleteDoor(string id)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDoorSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("LOTimeOut", DBNull.Value);
                command.Parameters.AddWithValue("DOTimeOut", DBNull.Value);
                command.Parameters.AddWithValue("Mode", DBNull.Value);
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

    }
}
