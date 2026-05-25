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
    public class CompositeProductRepository : ICompositeProductRepository
    {
        private readonly DatabaseConnection _db;
        private readonly ISimpleProductRepository _simpleProductRepository;

        public CompositeProductRepository()
        {
            _db = new DatabaseConnection();
            _simpleProductRepository = new SimpleProductRepository();
        }
        public void Add(CompositeProduct product)
        {
            using(var connection = _db.GetConnection())
            {
                // Insert into Products table
                var cmdProduct = new System.Data.SqlClient.SqlCommand(@"
                    INSERT INTO Products (Name, SalePrice, Type)
                    VALUES (@Name, @SalePrice, 'C')
                    SELECT SCOPE_IDENTITY();", (System.Data.SqlClient.SqlConnection)connection);

                cmdProduct.Parameters.AddWithValue("@Name", product.Name);
                cmdProduct.Parameters.AddWithValue("@SalePrice", product.SalePrice);

                int newId = int.Parse(cmdProduct.ExecuteScalar().ToString());
                product.Id = newId;

                // Insert into CompositeProducts table

                foreach(var component in product.Components)
                {
                    InsertComponent(connection, product.Id, component);
                }
            }
        }

        private void InsertComponent(IDbConnection connection, int compositProducId, ProductComponent component)
        {
            var cmd = new SqlCommand(@"
                INSERT INTO ProductComponents
                    (CompositProductId, SimpleProductId, Quantity)
                VALUES
                    (@CompositeProductId, @SimpleProductId, @Quantity)", (SqlConnection)connection);

            cmd.Parameters.AddWithValue("@CompositeProductId", compositProducId);
            cmd.Parameters.AddWithValue("@SimpleProductId", component.SimpleProduct.Id);
            cmd.Parameters.AddWithValue("@Quantity", component.Quantity);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using (var connection = _db.GetConnection())
            {
                //Delete components first (Foreign key)
                var cmdComponents = new SqlCommand(@"
                    'DELETE FROM ProductComponents
                     WHERE CompositeProductId = @Id", (SqlConnection)connection);

                cmdComponents.Parameters.AddWithValue("@Id", id);
                cmdComponents.ExecuteNonQuery();

                //Then delete the product
                var cmdProduct = new SqlCommand(@"
                    DELETE FROM Products WHERE Id = @Id",
                    (SqlConnection)connection);

                cmdProduct.Parameters.AddWithValue("@Id", id);
                cmdProduct.ExecuteNonQuery();
            }
        }

        public IEnumerable<CompositeProduct> GetAll()
        {
            var products = new List<CompositeProduct>();

            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT p.Id, p.Name, p.SalePrice
                    FROM Products p
                    WHERE p.Type = 'C'
                    ORDER BY p.Name", (SqlConnection)connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = MapProduct(reader);
                        products.Add(product);
                    }
                }

                // Load components for each product
                foreach (var product in products)
                {
                    product.Components = GetComponents(connection, product.Id);
                }
            }
            return products;
        }

        public CompositeProduct GetById(int id)
        {
            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT p.Id, p.Name, p.SalePrice
                    FROM Products p
                    WHERE p.Id = @Id AND p.Type = 'C'", (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var product = MapProduct(reader);
                        reader.Close();
                        product.Components = GetComponents(connection, product.Id);
                        return product;
                    }
                }
            }
            return null;
        }

        private List<ProductComponent> GetComponents(IDbConnection connection, int compositeProductId)
        {
            var components = new List<ProductComponent>();

            var cmd = new SqlCommand(@"
                SELECT
                    pc.Id,
                    pc.CompositeProductId,
                    pc.SimpleProductId,
                    pc.Quantity,
                    p.Name,
                    p.SalePrice,
                    s.CostPrice
                FROM ProductComponents pc
                INNER JOIN Products p ON p.Id = pc.SimpleProductId
                INNER JOIN SimpleProducts s ON s.Id = pc.SimpleProductId", (SqlConnection)connection);

            cmd.Parameters.AddWithValue("@CompositeProductId", compositeProductId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    components.Add(new ProductComponent
                    {
                        Id                  = (int)reader["Id"],
                        CompositeProductId  = (int)reader["CompositeProductId"],
                        Quantity            = (int)reader["Quantity"],
                        SimpleProduct       = new SimpleProduct
                        {
                            Id              = (int)reader["SimpleProductId"],
                            Name            = reader["Name"].ToString(),
                            SalePrice       = (decimal)reader["SalePrice"],
                            CostPrice       = (decimal)reader["CostPrice"]
                        }
                    });
                }
            }
            return components;
        }

        private CompositeProduct MapProduct(IDataReader reader)
        {
            return new CompositeProduct
            {
                Id          = (int)reader["Id"],
                Name        = reader["Name"].ToString(),
                SalePrice   = (decimal)reader["SalePrice"]
            };
        }

        public void Update(CompositeProduct product)
        {
            using (var connection = _db.GetConnection())
            {
                // Step 1: Update Products table
                var cmdProduct = new SqlCommand(@"
                    UPDATE Products
                    SET Name = @Name, SalePrice = @SalePrice
                    WHERE Id = @Id", (SqlConnection)connection);

                cmdProduct.Parameters.AddWithValue("@Id", product.Id);
                cmdProduct.Parameters.AddWithValue("@Name", product.Name);
                cmdProduct.Parameters.AddWithValue("@SalePrice", product.SalePrice);
                cmdProduct.ExecuteNonQuery();

                // Step 2: Delete old components and reinsert
                var cmdDelete = new SqlCommand(@"
                    DELETE FROM ProductComponents
                    WHERE CompositeProductId = @Id", (SqlConnection)connection);

                cmdDelete.Parameters.AddWithValue("@Id", product.Id);
                cmdDelete.ExecuteNonQuery();

                // Step 3: Reinsert updated components
                foreach (var component in product.Components)
                {
                    InsertComponent(connection, product.Id, component);
                }
            }
        }
    }
}
