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

        public DTO_CardHolder()
        {
        }

        public DTO_CardHolder(string id, string name, Gender gender, DateTime dOB, string phoneNumber, string address, string email, string description)
        {
            Id = id;
            Name = name;
            Gender = gender;
            DOB = dOB;
            PhoneNumber = phoneNumber;
            Address = address;
            Email = email;
            Description = description;
        }

        public SQLResult Validation()
        {
            SQLResult result = new SQLResult(false, "Validate fail");

            //Name validate
            if (Name == null)
            {
                result.Detail = "Card holder name can't null";
                return result;
            }
            else
            {
                if (Name.Count() < 1 || Name.Count() > 50)
                {
                    result.Detail = "Name must contain between 1 and 50 characters";
                    return result;
                }
            }

            //DOB validate
            if (DOB == null)
            {
                result.Detail = "Card holder DOB can't null";
                return result;
            }
            else
            {
                if (DateTime.Compare(DateTime.Today, DOB) <= 0)
                {
                    result.Detail = "Date of birth can't greater than today";
                    return result;
                }
            }

            result.Result = true;
            result.Detail = "Validate success";
            return result;
        }
    }
}
