using Npgsql;

namespace Pracrice_7_8
{
    class DBUtils
    {
        public static NpgsqlConnection GetDBConnection()
        {
            NpgsqlConnection connection = DBServerUtils.GetDBConnection("postgres", ""); // Пароль ввести
            connection.Open();

            return connection;
        }

        public static NpgsqlConnection GetDBConnection(string login)
        {
            NpgsqlConnection connection = DBServerUtils.GetDBConnection(login, "");
            connection.Open();

            return connection;
        }
    }
}
