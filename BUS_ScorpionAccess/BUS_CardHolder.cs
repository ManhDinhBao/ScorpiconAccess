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
    public class BUS_CardHolder
    {
        DAL_CardHolder dal = new DAL_CardHolder();

        /// <summary>
        /// Get all card holder
        /// </summary>
        /// <returns>Return list card holder or null if error</returns>
        public List<DTO_CardHolder> GetAllCardHolder()
        {
            List<DTO_CardHolder> lstCardHolders = new List<DTO_CardHolder>();
            DataTable dt = dal.GetAllCardHolder();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_CardHolder holder = new DTO_CardHolder();
                    holder.Id = row["Id"].ToString();
                    holder.Name = row["Name"].ToString();
                    holder.Gender = (Gender)Convert.ToInt16(row["Gender"].ToString());
                    holder.DOB = Convert.ToDateTime(row["DOB"].ToString());
                    holder.Address = row["Address"].ToString();
                    holder.PhoneNumber = row["PhoneNumber"].ToString();
                    holder.Email = row["Email"].ToString();
                    holder.Description = row["Description"].ToString();

                    lstCardHolders.Add(holder);
                }

                return lstCardHolders;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get card holder by key (Id)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return card holder if founed or null if error</returns>
        public DTO_CardHolder GetCardHolderByKey(string Id)
        {
            DataTable dt = dal.GetCardHOlderById(Id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_CardHolder holder = new DTO_CardHolder();
                holder.Id = row["Id"].ToString();
                holder.Name = row["Name"].ToString();
                holder.Gender = (Gender)Convert.ToInt16(row["Gender"].ToString());
                holder.DOB = Convert.ToDateTime(row["DOB"].ToString());
                holder.Address = row["Address"].ToString();
                holder.PhoneNumber = row["PhoneNumber"].ToString();
                holder.Email = row["Email"].ToString();
                holder.Description = row["Description"].ToString();

                return holder;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Search card holder
        /// </summary>
        /// <param name="searchValue">name, phone number or email</param>
        /// <returns>Return list card holder if founded or error if null</returns>
        public List<DTO_CardHolder> SearchCardHolder(string searchValue)
        {
            List<DTO_CardHolder> lstCardHolders = new List<DTO_CardHolder>();
            DataTable dt = dal.SearchCardHolder(searchValue);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_CardHolder holder = new DTO_CardHolder();
                    holder.Id = row["Id"].ToString();
                    holder.Name = row["Name"].ToString();
                    holder.Gender = (Gender)Convert.ToInt16(row["Gender"].ToString());
                    holder.DOB = Convert.ToDateTime(row["DOB"].ToString());
                    holder.Address = row["Address"].ToString();
                    holder.PhoneNumber = row["PhoneNumber"].ToString();
                    holder.Email = row["Email"].ToString();
                    holder.Description = row["Description"].ToString();

                    lstCardHolders.Add(holder);
                }

                return lstCardHolders;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new card holder
        /// </summary>
        /// <param name="card">The card holder object want to add</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult AddNewCardHolder(DTO_CardHolder holder)
        {
            if (holder == null)
            {
                return new SQLResult(false, "Card holder can't null");
            }

            if(!holder.Validation().Result)
            {
                return new SQLResult(false, holder.Validation().Detail);
            }

            return dal.AddNewCardHolder(holder);
        }

        /// <summary>
        /// Update card holder information
        /// </summary>
        /// <param name="card">The card holder object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateCardHolder(DTO_CardHolder holder)
        {
            if (holder == null)
            {
                return new SQLResult(false, "Card holder can't null");
            }

            if (!holder.Validation().Result)
            {
                return new SQLResult(false, holder.Validation().Detail);
            }

            return dal.UpdateCardHolder(holder);
        }

        /// <summary>
        /// Delete card holder
        /// </summary>
        /// <param name="holderId">Id of card holder want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteCardHolder(string holderId)
        {
            if (holderId == null || holderId == "")
            {
                return new SQLResult(false, "Card holder id can't empty");
            }

            return dal.DeleteCardHolder(holderId);
        }
    }
}
