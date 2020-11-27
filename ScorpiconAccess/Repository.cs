using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScorpiconAccess
{
    public class Repository
    {
        public static List<DTO_Card> lstAllCards;
        public static List<DTO_CardHolder> lstAllCardHolders;
        public static List<DTO_Device> lstAllDevices;




        public static DTO_Card selectedCard;
        public static DTO_Card newCard;

        public static DTO_CardHolder selectedHolder;
        public static DTO_CardHolder newHolder;
    }
}
