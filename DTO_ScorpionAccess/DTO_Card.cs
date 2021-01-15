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
                result.Detail = "[109] - Mã thẻ không được để trống.";
                return result;
            }

            if (DateTime.Compare(ETime, STime) < 0) 
            {
                result.Result = false;
                result.Detail = "[110] - Ngày thẻ hết hiệu lực phải lớn hơn ngày thẻ bắt đầu có hiệu lực.";
                return result;
            }

            if (Pin>999999 || Pin<0)
            {
                result.Result = false;
                result.Detail = "[111] - Mã pin của thẻ phải có độ dài từ 1 đến 6 ký tự.";
                return result;
            }

            return result;
        }
    }

}
