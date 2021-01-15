using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_ScorpionAccess
{
    public class DBConnect
    {
        public static string sqlPath = "";
        public static SqlConnection _conn;

        public void SetConnect(string sqlPathNew)
        {
            sqlPath = sqlPathNew;
            _conn = new SqlConnection(sqlPath);
        }
    }
}
