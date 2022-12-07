using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pracrice_7_8
{
    class DBServerUtils
    {
        public static NpgsqlConnection GetDBConnection(string username, string password)
        {
            string connString = $"Host=localhost;Username={username};Password={password};Database=mirea";

            NpgsqlConnection conn = new NpgsqlConnection(connString);

            return conn;
        }
    }
}
