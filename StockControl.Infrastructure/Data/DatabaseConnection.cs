using System.Data;
using System.Data.SqlClient;

namespace StockControl.Infrastructure.Data
{
    public class DatabaseConnection
    {
        private const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StockControlDB;Integrated Security=True;TrustServerCertificate=True;";

        public IDbConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
