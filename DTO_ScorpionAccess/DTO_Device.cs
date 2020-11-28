using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DTO_ScorpionAccess
{
    public class DTO_Device
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MAC { get; set; }
        public string IP { get; set; }
        public string Subnet { get; set; }
        public string GateWay { get; set; }
        public string HostIp { get; set; }
        public FAMode FAMode { get; set; }
        public ControlMode CtrMode { get; set; }
        public List<DTO_DeviceSocket> Sockets { get; set; }
        public string Description { get; set; }

        public DTO_Device()
        {
        }

        public DTO_Device(string id, string name, string mAC, string iP, string subnet, string gateWay, string hostIp, FAMode fAMode, ControlMode ctrMode, List<DTO_DeviceSocket> sockets, string description)
        {
            Id = id;
            Name = name;
            MAC = mAC;
            IP = iP;
            Subnet = subnet;
            GateWay = gateWay;
            HostIp = hostIp;
            FAMode = fAMode;
            CtrMode = ctrMode;
            Sockets = sockets;
            Description = description;
        }
    
        public SQLResult Validate()
        {
            SQLResult result = new SQLResult(true, "Validate success");

            //Validate Id
            if(Id == null || Id.Count()<=0)
            {
                result.Result = false;
                result.Detail = "Device id can't empty";
                return result;
            }

            //Validate Name
            if (Name == null || Name.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "Device name can't empty";
                return result;
            }

            //Validate IP
            if (IP == null || IP.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "IP address can't empty";
                return result;
            }

            if (!ValidateIp(IP))
            {
                result.Result = false;
                result.Detail = "IP address invalid";
                return result;
            }

            //Valid Gateway
            if (GateWay == null || GateWay.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "Gateway can't empty";
                return result;
            }

            if (!ValidateIp(GateWay))
            {
                result.Result = false;
                result.Detail = "Gateway invalid";
                return result;
            }

            //Valid Subnet 
            if (Subnet == null || Subnet.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "Subnet can't empty";
                return result;
            }

            if (!ValidateIp(Subnet))
            {
                result.Result = false;
                result.Detail = "Subnet invalid";
                return result;
            }

            //Valid HostIP
            if (HostIp == null || HostIp.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "Host IP address can't empty";
                return result;
            }

            if (!ValidateIp(HostIp))
            {
                result.Result = false;
                result.Detail = "Host IP address invalid";
                return result;
            }

            //Validate MAC
            if (MAC == null || MAC.Count() <= 0)
            {
                result.Result = false;
                result.Detail = "MAC address can't empty";
                return result;
            }
            else
            {
                string inputMAC = MAC.Replace(" ", "").Replace(":", "").Replace("-", "");
                Regex r = new Regex("^[a-fA-F0-9]{12}$");
                if (!r.IsMatch(inputMAC))
                {
                    result.Result = false;
                    result.Detail = "MAC address invalid";
                    return result;
                }
            }
            

            return result;
        }

        private bool ValidateIp(string ip)
        {
            string[] arrIP = ip.Split('.');
            if(arrIP.Length != 4)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < arrIP.Length; i++)
                {
                    try
                    {
                        int ipNum = Convert.ToInt16(arrIP[i]);
                        if (ipNum < 0 || ipNum > 255)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
