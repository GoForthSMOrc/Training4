using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient;

namespace ExamTrainP1
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "YEPCock";
            string username = "root";
            string password = "root";
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
