using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class FAMode
    {
        public bool IsUse { get; set; }
        public int FAHWNumber { get; set; }

        public FAMode()
        {
        }

        public FAMode(bool isUse, int fAHWNumber)
        {
            IsUse = isUse;
            if (isUse)
            {
                FAHWNumber = fAHWNumber;
            }
            else
                fAHWNumber = 0;
            
        }
    }
}
