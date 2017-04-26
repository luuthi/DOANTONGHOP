using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConnectDB
    {
        public static SqlConnection GetConnection()
        {
            string str = ConfigurationManager.ConnectionStrings["NSXConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
                cn.Dispose();
            }
            return cn;
        }
    }
}
