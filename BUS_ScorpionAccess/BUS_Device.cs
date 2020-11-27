using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace BUS_ScorpionAccess
{
    public class BUS_Device
    {
        DAL_Device dal = new DAL_Device();

        /// <summary>
        /// Get all device
        /// </summary>
        /// <returns>Return list device or null if error</returns>
        public List<DTO_Device> GetAllDevice()
        {
            List<DTO_Device> lstDevices = new List<DTO_Device>();
            DataTable dt = dal.GetAllDevice();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Device device = new DTO_Device();

                    device.Id           = row["Id"].ToString();
                    device.Name         = row["Name"].ToString();
                    device.MAC          = row["MAC"].ToString();
                    device.IP           = row["IP"].ToString();
                    device.Subnet       = row["Subnet"].ToString();
                    device.GateWay      = row["GateWay"].ToString();
                    device.HostIp       = row["HostIp"].ToString();
                    device.FAMode       = (FAMode)Convert.ToInt16(row["FAMode"].ToString());
                    device.FAHW         = Convert.ToInt16(row["FAHW"].ToString());
                    device.CtrMode      = (ControlMode)Convert.ToInt16(row["CtrlMode"].ToString());
                    device.ReaderQty    = Convert.ToInt16(row["ReaderQty"].ToString());
                    device.InputQty     = Convert.ToInt16(row["InputQty"].ToString());
                    device.OutputQty    = Convert.ToInt16(row["OutputQty"].ToString());
                    device.Description  = row["Description"].ToString();

                    lstDevices.Add(device);
                }

                return lstDevices;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get card by Id (number)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return card if founed or null if error</returns>
        public DTO_Device GetDeviceByKey(string Id)
        {
            DataTable dt = dal.GetDeviceById(Id);
            DTO_Device device = new DTO_Device();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {                   
                    device.Id = row["Id"].ToString();
                    device.Name = row["Name"].ToString();
                    device.MAC = row["MAC"].ToString();
                    device.IP = row["IP"].ToString();
                    device.Subnet = row["Subnet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.HostIp = row["HostIp"].ToString();
                    device.FAMode = (FAMode)Convert.ToInt16(row["FAMode"].ToString());
                    device.FAHW = Convert.ToInt16(row["FAHW"].ToString());
                    device.CtrMode = (ControlMode)Convert.ToInt16(row["CtrlMode"].ToString());
                    device.ReaderQty = Convert.ToInt16(row["ReaderQty"].ToString());
                    device.InputQty = Convert.ToInt16(row["InputQty"].ToString());
                    device.OutputQty = Convert.ToInt16(row["OutputQty"].ToString());
                    device.Description = row["Description"].ToString();
                }

                return device;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Search card
        /// </summary>
        /// <param name="searchValue">Name, MAC address or IP address</param>
        /// <returns>Return list device if founded or error if null</returns>
        public List<DTO_Device> SearchDevice(string searchValue)
        {
            List<DTO_Device> lstDevices = new List<DTO_Device>();
            DataTable dt = dal.SearchDevice(searchValue);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Device device = new DTO_Device();

                    device.Id = row["Id"].ToString();
                    device.Name = row["Name"].ToString();
                    device.MAC = row["MAC"].ToString();
                    device.IP = row["IP"].ToString();
                    device.Subnet = row["Subnet"].ToString();
                    device.GateWay = row["GateWay"].ToString();
                    device.HostIp = row["HostIp"].ToString();
                    device.FAMode = (FAMode)Convert.ToInt16(row["FAMode"].ToString());
                    device.FAHW = Convert.ToInt16(row["FAHW"].ToString());
                    device.CtrMode = (ControlMode)Convert.ToInt16(row["CtrlMode"].ToString());
                    device.ReaderQty = Convert.ToInt16(row["ReaderQty"].ToString());
                    device.InputQty = Convert.ToInt16(row["InputQty"].ToString());
                    device.OutputQty = Convert.ToInt16(row["OutputQty"].ToString());
                    device.Description = row["Description"].ToString();

                    lstDevices.Add(device);
                }

                return lstDevices;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new device
        /// </summary>
        /// <param name="device">The device object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewDevice(DTO_Device device)
        {
            if (device == null)
            {
                return new SQLResult(false, "");
            }

            return dal.AddNewDevice(device);
        }

        /// <summary>
        /// Update device information
        /// </summary>
        /// <param name="device">The device object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateDevice(DTO_Device device)
        {
            if (device == null)
            {
                return new SQLResult(false, "");
            }

            return dal.UpdateDevice(device);
        }

        /// <summary>
        /// Delete device
        /// </summary>
        /// <param name="deviceId">Id of device want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteCard(string deviceId)
        {
            if (deviceId == null)
            {
                return new SQLResult(false, "");
            }

            return dal.DeleteDevice(deviceId);
        }
    }
}
