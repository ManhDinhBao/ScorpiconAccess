using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class CardConfig
    {
        public string CardSerial { get; set; }
        public int CardUniqueId { get; set; }
        public int Pin { get; set; }
        public List<int> ListGroupAccess { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CardConfig()
        {
        }

        public CardConfig(string cardSerial, int cardUniqueId, int pin, List<int> listGroupAccess, DateTime startDate, DateTime endDate)
        {
            CardSerial = cardSerial;
            CardUniqueId = cardUniqueId;
            Pin = pin;
            ListGroupAccess = listGroupAccess;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string ToFileString()
        {
            string result = string.Format("n 0 {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} 0 0 0 0 {11} {12} {13} {14} 0 0 0 0 0", CardSerial, Pin, CardUniqueId, ListGroupAccess[0], ListGroupAccess[1], ListGroupAccess[2], ListGroupAccess[3], ListGroupAccess[4], ListGroupAccess[5], ListGroupAccess[6], ListGroupAccess[7], StartDate.ToString("yyyy/MM/dd"), StartDate.ToString("hh:mm:ss"), EndDate.ToString("yyyy/MM/dd"), EndDate.ToString("hh:mm:ss"));
            if (Pin == 0)
            {
                return result + "\r\n";
            }
            else
            {
                return result + "\r\n" + string.Format("o {0} {1}", Pin, CardUniqueId)+"\r\n";
            }

        }
    }
}
