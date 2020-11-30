using DAL_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_ScorpionAccess
{
    public class BUS_General
    {
        DAL_General dal = new DAL_General();

        /// <summary>
        /// Auto generate Id by format: XXX000000000
        /// </summary>
        /// <param name="type">Object type</param>
        /// Card holder = HOLDER
        /// Device = DEVICE
        /// Door = DOOR
        /// Schedule = SCHEDULE
        /// User right = RIGHT
        /// <returns></returns>
        public string GenerateId(string type)
        {
            return dal.GenerateId(type);
        }
    }
}
