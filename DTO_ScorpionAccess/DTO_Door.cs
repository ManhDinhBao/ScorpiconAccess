using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Door
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DTO_DoorMode Mode { get; set; }
        public string Description { get; set; }
        public int LOTimeOut { get; set; }
        public int DOTimeOut { get; set; }
        public List<DTO_DoorSocket> Sockets { get; set; }

        public DTO_Door()
        {

        }

        public DTO_Door(string id, string name, DTO_DoorMode mode, string description, int lOTimeOut, int dOTimeOut, List<DTO_DoorSocket> sockets)
        {
            Id = id;
            Name = name;
            Mode = mode;
            Description = description;
            LOTimeOut = lOTimeOut;
            DOTimeOut = dOTimeOut;
            Sockets = sockets;
        }


        public SQLResult Validation()
        {
            SQLResult result = new SQLResult(false, "Xác thực không thành công.");

            //Name validate
            if (Name == null)
            {
                result.Detail = "Tên cửa không được để trống.";
                return result;
            }

            //Mode validate
            if (Mode == null || Mode.Id == "")
            {
                result.Detail = "Chưa chọn chế độ hoạt động của cửa.";
                return result;
            }


            result.Result = true;
            result.Detail = "Xác thực thành công.";
            return result;
        }
    }
}
