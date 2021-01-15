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
    public class BUS_Door
    {
        DAL_Door dal = new DAL_Door();
        BUS_DoorSocket bus_Socket = new BUS_DoorSocket();

        /// <summary>
        /// Get all Door
        /// </summary>
        /// <returns>Return list door or null if error</returns>
        public List<DTO_Door> GetAllDoor()
        {
            List<DTO_Door> lstDoors = new List<DTO_Door>();
            DataTable dt = dal.GetAllDoor();

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    DTO_Door door = new DTO_Door();
                    door.Id             = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                    door.Name           = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                    door.LOTimeOut      = row["LOTimeout"] == DBNull.Value ? 0 : (int)row["LOTimeout"];
                    door.DOTimeOut      = row["DOTimeout"] == DBNull.Value ? 0 : (int)row["DOTimeout"];
                    door.Description    = row["Description"] == DBNull.Value ? null : row["Description"].ToString();

                    DTO_DoorMode mode = new DTO_DoorMode();
                    mode.Id = row["DMode"] == DBNull.Value ? null : row["DMode"].ToString();
                    mode.Name = row["DoorModeName"] == DBNull.Value ? null : row["DoorModeName"].ToString();
                    mode.ReaderUse = row["ReadersUse"] == DBNull.Value ? 0 : (int)row["ReadersUse"];
                    mode.InputUse = row["InputUse"] == DBNull.Value ? 0 : (int)row["InputUse"];
                    mode.OutputUse = row["OutputUse"] == DBNull.Value ? 0 : (int)row["OutputUse"];
                    mode.Description = row["DoorModeDescr"] == DBNull.Value ? null : row["DoorModeDescr"].ToString();

                    door.Mode = mode;

                    door.Sockets = bus_Socket.GetDoorSocketInDoor(door.Id);

                    lstDoors.Add(door);
                }

                return lstDoors;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Get door by key (Id)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return door if founed or null if error</returns>
        public DTO_Door GetDoorByKey(string Id)
        {
            DataTable dt = dal.GetDoorById(Id);

            if (dt.Rows.Count < 0)
            {
                return null;
            }

            try
            {
                DataRow row = dt.Rows[0];

                DTO_Door door = new DTO_Door();
                door.Id = row["Id"] == DBNull.Value ? null : row["Id"].ToString();
                door.Name = row["Name"] == DBNull.Value ? null : row["Name"].ToString();
                door.LOTimeOut = row["LOTimeOut"] == DBNull.Value ? 0 : (int)row["LOTimeOut"];
                door.DOTimeOut = row["DOTimeOut"] == DBNull.Value ? 0 : (int)row["DOTimeOut"];
                door.Description = row["Description"] == DBNull.Value ? null : row["Description"].ToString();

                DTO_DoorMode mode = new DTO_DoorMode();
                mode.Id = row["DMode"] == DBNull.Value ? null : row["DMode"].ToString();
                mode.Name = row["DoorModeName"] == DBNull.Value ? null : row["DoorModeName"].ToString();
                mode.ReaderUse = row["ReadersUse"] == DBNull.Value ? 0 : (int)row["ReadersUse"];
                mode.InputUse = row["InputUse"] == DBNull.Value ? 0 : (int)row["InputUse"];
                mode.OutputUse = row["OutputUse"] == DBNull.Value ? 0 : (int)row["OutputUse"];
                mode.Description = row["DoorModeDescr"] == DBNull.Value ? null : row["DoorModeDescr"].ToString();

                door.Mode = mode;

                door.Sockets = bus_Socket.GetDoorSocketInDoor(door.Id);

                return door;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new door
        /// </summary>
        /// <param name="door">The door object want to add</param>
        /// <returns>Return true if add success, Error if update fail or validate fail</returns>
        public SQLResult AddNewDoor(DTO_Door door)
        {
            if (door == null)
            {
                return new SQLResult(false, "Cửa rỗng.");
            }

            if (!door.Validation().Result)
            {
                return new SQLResult(false, door.Validation().Detail);
            }

            return dal.AddNewDoor(door);
        }

        /// <summary>
        /// Update door information
        /// </summary>
        /// <param name="door">The door object want to update</param>
        /// <returns>Return true if update success, Error if update fail or validate fail</returns>
        public SQLResult UpdateDoor(DTO_Door door)
        {
            if (door == null)
            {
                return new SQLResult(false, "Cửa rỗng.");
            }

            if (!door.Validation().Result)
            {
                return new SQLResult(false, door.Validation().Detail);
            }

            return dal.UpdateDoor(door);
        }

        /// <summary>
        /// Delete door 
        /// </summary>
        /// <param name="doorId">Id of door want to delete</param>
        /// <returns></returns>
        public SQLResult DeleteDoor(string doorId)
        {
            if (doorId == null)
            {
                return new SQLResult(false, "Id của cửa rỗng.");
            }

            return dal.DeleteDoor(doorId);
        }
    }
}
