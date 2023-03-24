using Npgsql;

namespace Pracrice_7_8
{
    class DBServerUtils
    {
        public static NpgsqlConnection GetDBConnection(string username, string password)
        {
            string connString = $"Host=localhost;Username={username};Password={password};Database=mirea;Port=5432";

            NpgsqlConnection conn = new NpgsqlConnection(connString);

            return conn;
        }
    }
}
