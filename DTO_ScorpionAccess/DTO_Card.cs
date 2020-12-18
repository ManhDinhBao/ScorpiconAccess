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
        public int Pin { get; set; }
        public DateTime STime { get; set; }
        public DateTime ETime { get; set; }

        public DTO_Card()
        {
        }

        public DTO_Card(string cardNumber, string cardSerial, string holder, CardType type, int pin, DateTime sTime, DateTime eTime)
        {
            CardNumber = cardNumber;
            CardSerial = cardSerial;
            Holder = holder;
            Type = type;
            Pin = pin;
            STime = sTime;
            ETime = eTime;
        }

        public SQLResult Validation()
        {
            SQLResult result = new SQLResult(true, "");

            if (CardSerial == null || CardSerial == ""){
                result.Result = false;
                result.Detail = "Card serial can't be null";
                return result;
            }

            if (DateTime.Compare(ETime, DateTime.Today) < 0)
            {
                result.Result = false;
                result.Detail = "The end date must not be less than the current date";
                return result;
            }

            if (DateTime.Compare(ETime, STime) < 0) 
            {
                result.Result = false;
                result.Detail = "The end date must not be less than the start date";
                return result;
            }

            if (Pin>999999 || Pin<0)
            {
                result.Result = false;
                result.Detail = "Pin must be from 1 to 6 character";
                return result;
            }

            return result;
        }
    }

}
