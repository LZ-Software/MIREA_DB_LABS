using Npgsql;

namespace Pracrice_7_8
{
    class DBUtils
    {
        public static string role = "";
        public static string password = "";

        public static NpgsqlConnection GetDBConnection()
        {
            NpgsqlConnection connection = DBServerUtils.GetDBConnection(role, password);
            connection.Open();

            return connection;
        }
    }
}
