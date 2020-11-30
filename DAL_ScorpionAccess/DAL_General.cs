using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DAL_General : DBConnect
    {
        /// <summary>
        /// Auto generate Id by format: XXX000000000
        /// </summary>
        /// <param name="type">Object type</param>
        /// Card holder = HOLDER
        /// Device = DEVICE
        /// Door = DOOR
        /// Schedule = SCHEDULE
        /// User right = RIGHT
        /// <returns></returns>
        public string GenerateId(string type)
        {
            DataTable dt = new DataTable();
            string returnId = "";
            try
            {
                _conn.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spLGenId";
                command.Parameters.AddWithValue("@ObjectType", type);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(dt);

                returnId = dt.Rows[0]["ID"]==DBNull.Value? "": dt.Rows[0]["ID"].ToString();
            }
            catch
            {
                return returnId;
            }
            finally
            {
                _conn.Close();
            }

            return returnId;
        }
    }
}
