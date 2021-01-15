using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DTO_ScorpionAccess
{
    public class DTO_CardHolder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public DTO_Department Department { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public DTO_CardHolder()
        {
        }

        public DTO_CardHolder(string id, string name, Gender gender, DateTime dOB, string phoneNumber, string address, string email, string description, string departmentId, DTO_Department department, string account, string password)
        {
            Id = id;
            Name = name;
            Gender = gender;
            DOB = dOB;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            Description = description;
            DepartmentId = departmentId;
            Department = department;
            Account = account;
            Password = password;
        }

        public SQLResult Validation()
        {
            SQLResult result = new SQLResult(false, "[210] - Xác thực không thành công.");

            //Name validate
            if (Name == null)
            {
                result.Detail = "[211] - Họ và tên chủ thẻ không được để trống.";
                return result;
            }
            else
            {
                if (Name.Count() < 1 || Name.Count() > 50)
                {
                    result.Detail = "[212] - Họ và tên chủ thẻ phải có độ dài từ 1 đến 50 ký tự.";
                    return result;
                }
            }

            //DOB validate
            if (DateTime.Compare(DateTime.Today, DOB) <= 0)
            {
                result.Detail = "[213] - Ngày sinh của chủ thẻ không được nhỏ hơn ngày hiện tại.";
                return result;
            }

            result.Result = true;
            result.Detail = "Xác thực thành công.";
            return result;
        }
    }
}
