using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Service
{
    public class DataBaseHelper
    {
        private  string Constr = ConfigurationManager.ConnectionStrings["OpenPlatform"].ConnectionString;

        public DataBaseHelper()
        {

        }

        private static readonly string ConnectionSqlserver = ConfigurationManager.ConnectionStrings["OpenPlatform"].ConnectionString;

        //dapper
        public static SqlConnection DepperSqlserver()
        {
            SqlConnection connection = new SqlConnection(ConnectionSqlserver);
            connection.Open();
            return connection;
        }
    }
}
