using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Persistance.Connection
{
    public class Connection
    {
        string conString = "Data Source=.;Initial Catalog=Municipality_Db;Integrated Security=True";

        private static Connection instance;
        public static Connection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Connection();
                }
                return instance;
            }
        }
        public SqlConnection connection()
        {
            SqlConnection sqlcon = new SqlConnection(conString);
            sqlcon.Open();
            return sqlcon;
        }
    }
}
