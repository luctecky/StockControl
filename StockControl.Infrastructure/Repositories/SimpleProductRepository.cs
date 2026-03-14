using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Infrastructure.Repositories
{
    public class SimpleProductRepository : ISimpleProductRepository
    {
        private readonly DatabaseConnection _db;

        public SimpleProductRepository()
        {
            _db = new DatabaseConnection();
        }

        public void Add(SimpleProduct product)
        {
            using(var connection = _db.GetConnection())
            {
                var cmdProduct = new SqlCommand(@"
                    INSERT INTO Products (Name, SalePrice, Type)
                    VALUES (@Name, @SalePrice, 'S')
                    SELECT SCOPE_IDENTITY();", (SqlConnection)connection);
                
                cmdProduct.Parameters.AddWithValue("@Name", product.Name);
                cmdProduct.Parameters.AddWithValue("@SalePrice", product.SalePrice);
             
                int newId = int.Parse(cmdProduct.ExecuteScalar().ToString());

                var cmdSimple = new SqlCommand(@"
                    INSERT INTO SimpleProducts (Id, CostPrice)
                    VALUES (@id, @CostPrice)", (SqlConnection)connection);

                cmdSimple.Parameters.AddWithValue("@Id", newId);
                cmdSimple.Parameters.AddWithValue("@CostPrice", product.CostPrice);
                cmdSimple.ExecuteNonQuery();
               
            }
        }

        public void Delete(int id)
        {
            using(var connection = _db.GetConnection())
            {
                var cmdSimple = new SqlCommand(
                    "DELETE FROM SimpleProducts WHERE Id = @Id",
                    (SqlConnection)connection);

                cmdSimple.Parameters.AddWithValue("@Id", id);
                cmdSimple.ExecuteNonQuery();

                var cmdProduct = new SqlCommand(
                    "DELETE FROM Products WHERE Id = @Id",
                    (SqlConnection)connection);

                cmdProduct.Parameters.AddWithValue("@Id", id);
                cmdProduct.ExecuteNonQuery();
            }
        }

        public IEnumerable<SimpleProduct> GetAll()
        {
            var products = new List<SimpleProduct>();

            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT p.Id, p.Name, p.SalePrice, s.CostPrice
                    FROM Products p
                    INNER JOIN SimpleProducts s ON s.Id = p.Id
                    ORDER BY p.Name", (SqlConnection)connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        products.Add(MapProduct(reader));
                }
            }
            return products;
        }

        public SimpleProduct GetById(int id)
        {
            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT p.Id, p.Name, p.SalePrice, s.CostPrice
                    FROM Products p
                    INNER JOIN SimpleProducts s ON s.Id = p.Id
                    WHERE p.Id = @Id", (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapProduct(reader);
                }
            }
            return null;
        }

        public void update(SimpleProduct product)
        {
            using (var connection = _db.GetConnection()) 
            {
                var cmdProduct = new SqlCommand(@"
                    UPDATE Products
                    SET Name = @Name, SalePrice = @SalePrice
                    WHERE Id = @Id", (SqlConnection)connection);

                cmdProduct.Parameters.AddWithValue("@Id", product.Id);
                cmdProduct.Parameters.AddWithValue("@Name", product.Name);
                cmdProduct.Parameters.AddWithValue("@SalePrice",product.SalePrice);
                cmdProduct.ExecuteNonQuery();

                var cmdSimple = new SqlCommand(@"
                    UPDATE SimpleProducts
                    SET CostPrice = @CostPrice
                    WHERE Id = @Id", (SqlConnection)connection);

                cmdSimple.Parameters.AddWithValue("@Id", product.Id);
                cmdSimple.Parameters.AddWithValue("@CostPrice", product.CostPrice);
                cmdSimple.ExecuteNonQuery();
            }
        }
        private SimpleProduct MapProduct(IDataReader reader)
        {
            return new SimpleProduct
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                SalePrice = (decimal)reader["SalePrice"],
                CostPrice = (decimal)reader["CostPrice"]
            };
        }
    }
}
