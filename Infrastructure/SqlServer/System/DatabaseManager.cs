using System.Data.SqlClient;
using System.IO;

namespace Infrastructure.SqlServer.System
{
    public class DatabaseManager : IDatabaseManager
    {
        public void CreateDatabaseAndTables()
        { 
            ReadAndExecuteFile(
                    @"C:\Users\nicod\Documents\Ecole\HELHa\BAC3\Projet\ProjetBalade\Infrastructure\SqlServer\Resources\Init.sql");
        }

        public void FillTables()
        {
            ReadAndExecuteFile(
                @"C:\Users\nicod\Documents\Ecole\HELHa\BAC3\Projet\ProjetBalade\Infrastructure\SqlServer\Resources\Data.sql");
        }
        
        private static void ReadAndExecuteFile(string filePath)
        {
            var script = File.ReadAllText(filePath);

            var connection = Database.GetConnection();
            connection.Open();
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = script
            };

            command.ExecuteNonQuery();
        }
    }
}