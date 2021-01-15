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
    public class DAL_CardHolder : DBConnect
    {
        /// <summary>
        /// Get all card holder from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCardHolder()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                command.Parameters.AddWithValue("Email", DBNull.Value);
                command.Parameters.AddWithValue("UserName", DBNull.Value);
                command.Parameters.AddWithValue("Password", DBNull.Value);
                command.Parameters.AddWithValue("Department", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Get card holder by key (Id) from database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetCardHOlderById(string Id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderQry";
                command.Parameters.AddWithValue("WorkType", "I");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                command.Parameters.AddWithValue("Email", DBNull.Value);
                command.Parameters.AddWithValue("UserName", DBNull.Value);
                command.Parameters.AddWithValue("Password", DBNull.Value);
                command.Parameters.AddWithValue("Department", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        public DataTable GetCardHolderByDept(string deptId)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                command.Parameters.AddWithValue("Email", DBNull.Value);
                command.Parameters.AddWithValue("UserName", DBNull.Value);
                command.Parameters.AddWithValue("Password", DBNull.Value);
                command.Parameters.AddWithValue("Department", deptId);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        public DataTable GetCardHolderByAccount(string account, string password)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderQry";
                command.Parameters.AddWithValue("WorkType", "L");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                command.Parameters.AddWithValue("Email", DBNull.Value);
                command.Parameters.AddWithValue("UserName", account);
                command.Parameters.AddWithValue("Password", password);
                command.Parameters.AddWithValue("Department", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Get card holder by Name, Phone number or Email from databse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SearchCardHolder(string value)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", value);
                command.Parameters.AddWithValue("Name", value);
                command.Parameters.AddWithValue("PhoneNumber", value);
                command.Parameters.AddWithValue("Email", value);
                command.Parameters.AddWithValue("UserName", DBNull.Value);
                command.Parameters.AddWithValue("Password", DBNull.Value);
                command.Parameters.AddWithValue("Department", DBNull.Value);

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
        /// Insert new card holder to database
        /// </summary>
        /// <param name="cardHolder"></param>
        /// <returns></returns>
        public SQLResult AddNewCardHolder(DTO_CardHolder cardHolder)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                command.Parameters.AddWithValue("Name", cardHolder.Name);
                command.Parameters.AddWithValue("Gender", (int)cardHolder.Gender);
                command.Parameters.AddWithValue("DOB", cardHolder.DOB);

                if (string.IsNullOrEmpty(cardHolder.Address))
                {
                    command.Parameters.AddWithValue("Address", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Address", cardHolder.Address);
                }

                if (string.IsNullOrEmpty(cardHolder.PhoneNumber))
                {
                    command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("PhoneNumber", cardHolder.PhoneNumber);
                }

                if (string.IsNullOrEmpty(cardHolder.Email))
                {
                    command.Parameters.AddWithValue("Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Email", cardHolder.Email);
                }

                if (string.IsNullOrEmpty(cardHolder.Description))
                {
                    command.Parameters.AddWithValue("Description", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Description", cardHolder.Description);
                }

                if (string.IsNullOrEmpty(cardHolder.Account))
                {
                    command.Parameters.AddWithValue("UserName", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("UserName", cardHolder.Account);
                }

                if (string.IsNullOrEmpty(cardHolder.Password))
                {
                    command.Parameters.AddWithValue("Password", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Password", cardHolder.Password);
                }

                if (cardHolder.Department==null)
                {
                    command.Parameters.AddWithValue("Department", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Department", cardHolder.Department.Id);
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;
                    result.ExtraData = dt.Rows[0]["ExtraData"].ToString();

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
        /// Update card holder information in database
        /// </summary>
        /// <param name="cardHolder"></param>
        /// <returns></returns>
        public SQLResult UpdateCardHolder(DTO_CardHolder cardHolder)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", cardHolder.Id);
                command.Parameters.AddWithValue("Name", cardHolder.Name);
                command.Parameters.AddWithValue("Gender", (int)cardHolder.Gender);
                command.Parameters.AddWithValue("DOB", cardHolder.DOB);
                if (string.IsNullOrEmpty(cardHolder.Address))
                {
                    command.Parameters.AddWithValue("Address", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Address", cardHolder.Address);
                }

                if (string.IsNullOrEmpty(cardHolder.PhoneNumber))
                {
                    command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("PhoneNumber", cardHolder.PhoneNumber);
                }

                if (string.IsNullOrEmpty(cardHolder.PhoneNumber))
                {
                    command.Parameters.AddWithValue("Email", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Email", cardHolder.Email);
                }

                if (string.IsNullOrEmpty(cardHolder.PhoneNumber))
                {
                    command.Parameters.AddWithValue("Description", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Description", cardHolder.Description);
                }

                if (string.IsNullOrEmpty(cardHolder.Account))
                {
                    command.Parameters.AddWithValue("UserName", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("UserName", cardHolder.Account);
                }

                if (string.IsNullOrEmpty(cardHolder.Password))
                {
                    command.Parameters.AddWithValue("Password", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Password", cardHolder.Password);
                }

                if (string.IsNullOrEmpty(cardHolder.Department.Id))
                {
                    command.Parameters.AddWithValue("Department", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Department", cardHolder.Department.Id);
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
        /// Delete card holder by number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SQLResult DeleteCardHolder(string Id)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardHolderSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("Gender", DBNull.Value);
                command.Parameters.AddWithValue("DOB", DBNull.Value);
                command.Parameters.AddWithValue("Address", DBNull.Value);
                command.Parameters.AddWithValue("PhoneNumber", DBNull.Value);
                command.Parameters.AddWithValue("Email", DBNull.Value);
                command.Parameters.AddWithValue("Description", DBNull.Value);
                command.Parameters.AddWithValue("UserName", DBNull.Value);
                command.Parameters.AddWithValue("Password", DBNull.Value);
                command.Parameters.AddWithValue("Department", DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                if (dt.Rows[0]["Result"].ToString() == "OK")
                {
                    result.Result = true;

                }

                result.Detail = dt.Rows[0]["Detail"].ToString();
            }
            catch
            {

            }
            finally
            {
                _conn.Close();
            }

            return result;
        }
    }
}
