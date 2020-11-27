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
        protected SqlConnection _conn = new SqlConnection("Data Source=127.0.0.1,1433;Initial Catalog=LefaAccess;User ID=ibs;Password=ibs;Integrated Security=True");
    }
}
