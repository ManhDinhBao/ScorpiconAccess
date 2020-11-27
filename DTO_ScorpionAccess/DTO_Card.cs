using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DTO_ScorpionAccess
{
    public class DTO_Card
    {
        public string CardNumber { get; set; }
        public string CardSerial { get; set; }
        public string Holder { get; set; }
        public CardType Type { get; set; }
        public DateTime STime { get; set; }
        public DateTime ETime { get; set; }

        public DTO_Card()
        {
        }

        public DTO_Card(string cardNumber, string cardSerial, string holder, CardType type, DateTime sTime, DateTime eTime)
        {
            CardNumber = cardNumber;
            CardSerial = cardSerial;
            Holder = holder;
            Type = type;
            STime = sTime;
            ETime = eTime;
        }

        public bool Validation()
        {
            if (CardNumber == null || CardNumber == ""){
                return false;
            }


            if (DateTime.Compare(ETime, DateTime.Today) < 0)
            {
                return false;
            }

            if (DateTime.Compare(ETime, STime) < 0) 
            {
                return false;
            }

            return true;
        }
    }

}
