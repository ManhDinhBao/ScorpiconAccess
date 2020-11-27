using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace BUS_ScorpionAccess
{
    public class BUS_Card
    {
        DAL_Card dal = new DAL_Card();

        /// <summary>
        /// Get all card
        /// </summary>
        /// <returns>Return list card or null if error</returns>
        public List<DTO_Card> GetAllCard()
        {
            List<DTO_Card> lstCards = new List<DTO_Card>();
            DataTable dt = dal.GetAllCard();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Card card = new DTO_Card();
                    card.CardNumber = row["CardNumber"] == DBNull.Value ? null : row["CardNumber"].ToString();
                    card.CardSerial = row["CardSerial"] == DBNull.Value ? null :  row["CardSerial"].ToString();
                    card.Holder     = row["Holder"] == DBNull.Value ? null : row["Holder"].ToString();
                    card.Type       = row["Type"] == DBNull.Value ? EType.CardType.BLOCK : (CardType)Convert.ToInt16(row["Type"].ToString());
                    card.STime      = row["STime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["STime"].ToString());
                    card.ETime      = row["ETime"] == DBNull.Value ? new DateTime(1970, 01, 01) :  Convert.ToDateTime(row["ETime"].ToString());

                    lstCards.Add(card);
                }

                return lstCards;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get card by key (serial)
        /// </summary>
        /// <param name="number"></param>
        /// <returns>Return card if founed or null if error</returns>
        public DTO_Card GetCardByKey(string serial)
        {
            DataTable dt = dal.GetCardBySerial(serial);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_Card card = new DTO_Card();
                card.CardNumber = row["CardNumber"] == DBNull.Value ? null : row["CardNumber"].ToString();
                card.CardSerial = row["CardSerial"] == DBNull.Value ? null :  row["CardSerial"].ToString();
                card.Holder     = row["Holder"] == DBNull.Value ? null : row["Holder"].ToString();
                card.Type       = row["Type"] == DBNull.Value ? EType.CardType.BLOCK : (CardType)Convert.ToInt16(row["Type"].ToString());
                card.STime      = row["STime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["STime"].ToString());
                card.ETime      = row["ETime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["ETime"].ToString());

                return card;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Search card
        /// </summary>
        /// <param name="searchValue">Card serial or Card holder id</param>
        /// <returns>Return list card if founded or error if null</returns>
        public List<DTO_Card> SearchCard(string searchValue)
        {
            List<DTO_Card> lstCards = new List<DTO_Card>();
            DataTable dt = dal.SearchCard(searchValue);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Card card = new DTO_Card();
                    card.CardNumber = row["CardNumber"] == DBNull.Value ? null : row["CardNumber"].ToString();
                    card.CardSerial = row["CardSerial"] == DBNull.Value ? null : row["CardSerial"].ToString();
                    card.Holder     = row["Holder"] == DBNull.Value ? null : row["Holder"].ToString();
                    card.Type       = row["Type"] == DBNull.Value ? EType.CardType.BLOCK : (CardType)Convert.ToInt16(row["Type"].ToString());
                    card.STime      = row["STime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["STime"].ToString());
                    card.ETime      = row["ETime"] == DBNull.Value ? new DateTime(1970, 01, 01) : Convert.ToDateTime(row["ETime"].ToString());

                    lstCards.Add(card);
                }

                return lstCards;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new card
        /// </summary>
        /// <param name="card">The card object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewCard(DTO_Card card)
        {
            if (card == null)
            {
                return new SQLResult(false, "Card null");
            }

            if (!card.Validation())
            {
                return new SQLResult(false, "Validation fail");
            }

            return dal.AddNewCard(card);
        }

        /// <summary>
        /// Update card information
        /// </summary>
        /// <param name="card">The card object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateCard(DTO_Card card)
        {
            if (card == null)
            {
                return new SQLResult(false, "Card null");
            }

            if (!card.Validation())
            {
                return new SQLResult(false, "Validation fail");
            }

            return dal.UpdateCard(card);
        }

        /// <summary>
        /// Delete card
        /// </summary>
        /// <param name="cardSerial">Serial of card want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteCard(string cardSerial)
        {
            if (cardSerial == null)
            {
                return new SQLResult(false, "Card serial can't null");
            }

            if (cardSerial == "")
            {
                return new SQLResult(false, "Card serial can't empty");
            }

            return dal.DeleteCard(cardSerial);
        }
    }
}
