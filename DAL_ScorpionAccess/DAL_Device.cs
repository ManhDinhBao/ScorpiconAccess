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
    public class DAL_Device : DBConnect
    {
        /// <summary>
        /// Get all device from database
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDevice()
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("MAC", DBNull.Value);
                command.Parameters.AddWithValue("IP", DBNull.Value);

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

        /// <summary>
        /// Get card by key (Id) from database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetDeviceById(string Id)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("MAC", DBNull.Value);
                command.Parameters.AddWithValue("IP", DBNull.Value);

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
        /// Get device by name, mac or IP from databse
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable SearchDevice(string value)
        {
            DataTable dt = new DataTable();

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceQry";
                command.Parameters.AddWithValue("WorkType", "Q");
                command.Parameters.AddWithValue("Id", DBNull.Value);
                command.Parameters.AddWithValue("Name", value);
                command.Parameters.AddWithValue("MAC", value);
                command.Parameters.AddWithValue("IP", value);

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
        /// Insert new device to database
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public SQLResult AddNewDevice(DTO_Device device)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceSave";
                command.Parameters.AddWithValue("WorkType", "A");
                command.Parameters.AddWithValue("Id", "");
                command.Parameters.AddWithValue("Name", device.Name);
                command.Parameters.AddWithValue("MAC", device.MAC);
                if (device.IP == null)
                {
                    command.Parameters.AddWithValue("IP", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("IP", device.IP);
                }

                if (device.Subnet == null)
                {
                    command.Parameters.AddWithValue("Subnet", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Subnet", device.Subnet);
                }

                if (device.GateWay == null)
                {
                    command.Parameters.AddWithValue("Gateway", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Gateway", device.GateWay);
                }

                if (device.HostIp == null)
                {
                    command.Parameters.AddWithValue("HostIP", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("HostIP", device.HostIp);
                }

                command.Parameters.AddWithValue("FAMode", device.FAMode.IsUse ? 1:0);
                command.Parameters.AddWithValue("FAHW", device.FAMode.FAHWNumber);
                command.Parameters.AddWithValue("CtrlMode", (int)device.CtrMode);
                command.Parameters.AddWithValue("Description", device.Description);

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
        /// Update device information in database
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public SQLResult UpdateDevice(DTO_Device device)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceSave";
                command.Parameters.AddWithValue("WorkType", "U");
                command.Parameters.AddWithValue("Id", device.Id);
                command.Parameters.AddWithValue("Name", device.Name);
                command.Parameters.AddWithValue("MAC", device.MAC);
                if (device.IP == null)
                {
                    command.Parameters.AddWithValue("IP", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("IP", device.IP);
                }

                if (device.Subnet == null)
                {
                    command.Parameters.AddWithValue("Subnet", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Subnet", device.Subnet);
                }

                if (device.GateWay == null)
                {
                    command.Parameters.AddWithValue("Gateway", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("Gateway", device.GateWay);
                }

                if (device.HostIp == null)
                {
                    command.Parameters.AddWithValue("HostIP", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("HostIP", device.HostIp);
                }
                command.Parameters.AddWithValue("FAMode", device.FAMode.IsUse ? 1 : 0);
                command.Parameters.AddWithValue("FAHW", device.FAMode.FAHWNumber);
                command.Parameters.AddWithValue("CtrlMode", device.CtrMode);
                command.Parameters.AddWithValue("Description", device.Description);

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
        /// Delete device by Id
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public SQLResult DeleteDevice(string Id)
        {
            DataTable dt = new DataTable();
            SQLResult result = new SQLResult(false, "");

            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLDeviceSave";
                command.Parameters.AddWithValue("WorkType", "D");
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("Name", DBNull.Value);
                command.Parameters.AddWithValue("MAC", DBNull.Value);
                command.Parameters.AddWithValue("IP", DBNull.Value);
                command.Parameters.AddWithValue("Subnet", DBNull.Value);
                command.Parameters.AddWithValue("Gateway", DBNull.Value);
                command.Parameters.AddWithValue("HostIP", DBNull.Value);
                command.Parameters.AddWithValue("FAMode", DBNull.Value);
                command.Parameters.AddWithValue("FAHW", DBNull.Value);
                command.Parameters.AddWithValue("CtrlMode", DBNull.Value);
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
            catch(Exception ex) 
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
