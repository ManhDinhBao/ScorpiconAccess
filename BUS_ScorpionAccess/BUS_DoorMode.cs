using DAL_ScorpionAccess;
using DTO_ScorpionAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_ScorpionAccess
{
    public class BUS_DoorMode
    {
        DAL_DoorMode dal = new DAL_DoorMode();

        /// <summary>
        /// Get all door mode
        /// </summary>
        /// <returns>Return list door mode or null if error</returns>
        public List<DTO_DoorMode> GetAllDoorMode()
        {
            List<DTO_DoorMode> lstModes = new List<DTO_DoorMode>();
            DataTable dt = dal.GetAllDoorMode();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_DoorMode mode = new DTO_DoorMode();
                    mode.Id             = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    mode.Name           = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    mode.Description    = row["Description"]    == DBNull.Value ? null : row["Description"].ToString();
                    mode.ReaderUse      = row["ReadersUse"]      == DBNull.Value ? -1 : (int)row["ReadersUse"];
                    mode.InputUse       = row["InputUse"]       == DBNull.Value ? -1 : (int)row["InputUse"];
                    mode.OutputUse      = row["OutputUse"]      == DBNull.Value ? -1 : (int)row["OutputUse"];

                    lstModes.Add(mode);
                }

                return lstModes;
            }
            catch
            {
                return null;
            }
        }
    }
}
