using System.Data.SqlClient;

namespace Infrastructure
{
    public class Database
    {
        private const string ConnectionString = "Server=127.0.0.1,1433;Database=dbBalade;User Id=SA;Password=<YourStrong@Passw0rd>";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}