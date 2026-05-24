using MySql.Data.MySqlClient;

namespace GoDriveDesktop
{
    public static class DBConnection
    {
        private static string connectionString =
            "server=127.0.0.1;port=3306;database=GoDriveDB;user=root;password=Qq151190;charset=utf8mb4;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}