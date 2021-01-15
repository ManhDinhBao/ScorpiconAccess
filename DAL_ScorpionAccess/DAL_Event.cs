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
    public class DAL_Event:DBConnect
    {
        public DataTable GetEvent(string cardNumber,string holderName, string doorName, int status, int deptId, DateTime startTime, DateTime endTime)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLEventQry";
                command.Parameters.AddWithValue("@id", DBNull.Value);

                if (startTime == null)
                {
                    command.Parameters.AddWithValue("eventSendStartTime", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("eventSendStartTime", startTime.ToString("yyyy-MM-dd"));

                }

                if (endTime == null)
                {
                    command.Parameters.AddWithValue("eventSendEndTime", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("eventSendEndTime", endTime.ToString("yyyy-MM-dd"));

                }

                if (holderName == null)
                {
                    command.Parameters.AddWithValue("holderName", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("holderName", holderName);

                }

                command.Parameters.AddWithValue("holderId", DBNull.Value);

                command.Parameters.AddWithValue("doorId", DBNull.Value);
                command.Parameters.AddWithValue("department", deptId);

                if (doorName == null)
                {
                    command.Parameters.AddWithValue("doorName", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("doorName", doorName);

                }

                if (cardNumber == null)
                {
                    command.Parameters.AddWithValue("cardNumber", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("cardNumber", cardNumber);

                }

                command.Parameters.AddWithValue("status", status);

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

        public SQLResult AddNewEvent(int Id,DateTime eventOccurTime, DateTime eventSendTime,int cardNumber, string cardSerial, int holderId, string holderName, int doorId, string doorName, int status, string deviceName, string message, int direction)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLEventSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("EventOccurTime", eventOccurTime);
                command.Parameters.AddWithValue("EventSendTime", eventSendTime);
                command.Parameters.AddWithValue("CardNumber", cardNumber);
                command.Parameters.AddWithValue("CardSerial", cardSerial);
                command.Parameters.AddWithValue("HolderId", holderId);
                command.Parameters.AddWithValue("HolderName", holderName);
                command.Parameters.AddWithValue("DoorId", doorId);
                command.Parameters.AddWithValue("DoorName", doorName);
                command.Parameters.AddWithValue("Status", status);
                command.Parameters.AddWithValue("DeviceName", deviceName);
                command.Parameters.AddWithValue("Message", message);
                command.Parameters.AddWithValue("Direction", direction);

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
