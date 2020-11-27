using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int FAHW { get; set; }
        public ControlMode CtrMode { get; set; }
        public int ReaderQty { get; set; }
        public int InputQty { get; set; }
        public int OutputQty { get; set; }
        public string Description { get; set; }

        public DTO_Device()
        {
        }

        public DTO_Device(string id, string name, string mAC, string iP, string subnet, string gateWay, string hostIp, FAMode fAMode, int fAHW, ControlMode ctrMode, int readerQty, int inputQty, int outputQty, string description)
        {
            Id = id;
            Name = name;
            MAC = mAC;
            IP = iP;
            Subnet = subnet;
            GateWay = gateWay;
            HostIp = hostIp;
            FAMode = fAMode;
            FAHW = fAHW;
            CtrMode = ctrMode;
            ReaderQty = readerQty;
            InputQty = inputQty;
            OutputQty = outputQty;
            Description = description;
        }
    }
}
