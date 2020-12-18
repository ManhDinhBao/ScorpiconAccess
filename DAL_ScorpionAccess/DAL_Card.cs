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
    public class DAL_Card:DBConnect
    {
        /// <summary>
        /// Get all card from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCard()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("CardSerial", DBNull.Value);
                command.Parameters.AddWithValue("CardNumber", DBNull.Value);
                command.Parameters.AddWithValue("Holder"    , DBNull.Value);
                command.Parameters.AddWithValue("Type"      , DBNull.Value);

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
        /// Get card by key (number) from database
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public DataTable GetCardByNumber(string number)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardQry";
                command.Parameters.AddWithValue("workType", "Q");
                command.Parameters.AddWithValue("CardSerial", DBNull.Value);
                command.Parameters.AddWithValue("CardNumber", number);
                command.Parameters.AddWithValue("Holder", DBNull.Value);
                command.Parameters.AddWithValue("Type", DBNull.Value);

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
        /// Get card by number or holder from databse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SearchCard(string value)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spCard_Qry";
                command.Parameters.AddWithValue("workType", "Q");
                command.Parameters.AddWithValue("number", value);
                command.Parameters.AddWithValue("holder", value);

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
        /// Insert new card to database
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public SQLResult AddNewCard(DTO_Card card)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("CardNumber", "");
                command.Parameters.AddWithValue("CardSerial", card.CardSerial);
                command.Parameters.AddWithValue("Pin", card.Pin);
                if (string.IsNullOrEmpty(card.Holder))
                {
                    command.Parameters.AddWithValue("Holder", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Holder", card.Holder);
                }
                command.Parameters.AddWithValue("Type", (int)card.Type);
                command.Parameters.AddWithValue("STime", card.STime);
                command.Parameters.AddWithValue("ETime", card.ETime);

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
        /// Update card information in database
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public SQLResult UpdateCard(DTO_Card card)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("CardNumber", card.CardNumber);
                command.Parameters.AddWithValue("CardSerial", card.CardSerial);
                command.Parameters.AddWithValue("Pin", card.Pin);
                if (string.IsNullOrEmpty(card.Holder))
                {
                    command.Parameters.AddWithValue("Holder", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Holder", card.Holder);
                }
                
                command.Parameters.AddWithValue("Type", (int)card.Type);
                command.Parameters.AddWithValue("STime", card.STime);
                command.Parameters.AddWithValue("ETime", card.ETime);

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
        /// Delete card by number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SQLResult DeleteCard(string number)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLCardSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("CardNumber", number);
                command.Parameters.AddWithValue("CardSerial", DBNull.Value);
                command.Parameters.AddWithValue("Pin", DBNull.Value);
                command.Parameters.AddWithValue("Holder", DBNull.Value);
                command.Parameters.AddWithValue("Type",  DBNull.Value);
                command.Parameters.AddWithValue("STime", DBNull.Value);
                command.Parameters.AddWithValue("ETime", DBNull.Value);

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
