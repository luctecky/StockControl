using System.Data;
using System.Data.SqlClient;

namespace StockControl.Infrastructure.Data
{
    public class DatabaseConnection
    {
        private const string ConnectionString = "Server=.\\SQLEXPRESS;Database=StockControlDB;Integrated Security=True;";

        public IDbConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
