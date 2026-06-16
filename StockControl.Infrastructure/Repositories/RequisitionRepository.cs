using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Infrastructure.Repositories
{
    public class RequisitionRepository : IRequisitionRepository
    {
        private readonly DatabaseConnection _db;

        public RequisitionRepository()
        {
            _db = new DatabaseConnection();
        }
        public void Add(Requisition requisition)
        {
            using (var connection = _db.GetConnection())
            {
                //Step 1: Insert requisition
                var cmd = new SqlCommand(@"
                    INSERT INTO Requisitions
                        (WithdrawalDate, ResponsibleEmployee)
                    VALUES
                        (@WithdrawalDate, @ResponsibleEmployee);
                    SELECT SCOPE_IDENTITY();", (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@WithdrawalDate", requisition.WithdrawalDate);
                cmd.Parameters.AddWithValue("@ResponsibleEmployee", requisition.ResponsibleEmployee);

                int newId = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                requisition.Id = newId;

                //Step 2: Insert items
                foreach (var item in requisition.Items)
                {
                    InsertItem(connection, newId, item);
                }
            }
        }

        private void InsertItem(IDbConnection connection, int requisitionId, RequisitionItem item)
        {
            var cmd = new SqlCommand(@"
                INSERT INTO RequisitionItems
                    (RequisitionId, ProductId, Quantity)
                VALUES
                    (@RequisitionId, @ProductId, @Quantity);", (SqlConnection)connection);

            cmd.Parameters.AddWithValue("@RequisitionId", requisitionId);
            cmd.Parameters.AddWithValue("@ProductId", item.Product.Id);
            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using (var connection = _db.GetConnection())
            {
                // Step 1: Delete items
                var deleteItemsCmd = new SqlCommand(
                    "DELETE FROM RequisitionItems WHERE RequisitionId = @Id;",
                    (SqlConnection)connection);

                deleteItemsCmd.Parameters.AddWithValue("@Id", id);
                deleteItemsCmd.ExecuteNonQuery();

                // Step 2: Delete requisition
                var deleteRequisitionCmd = new SqlCommand(
                    "DELETE FROM Requisitions WHERE Id = @Id;", 
                    (SqlConnection)connection);

                deleteRequisitionCmd.Parameters.AddWithValue("@Id", id);
                deleteRequisitionCmd.ExecuteNonQuery();
            }
        }

        public Requisition GetById(int id)
        {
            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT Id, WithdrawalDate, ResponsibleEmployee
                    FROM Requisitions
                    WHERE Id = @Id",
                    (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var requisition = MapRequisition(reader);
                        reader.Close();
                        requisition.Items = GetItems(connection, id);
                        return requisition;
                    }
                }
            }
            return null;
        }
        public IEnumerable<Requisition> GetAll()
        {
            var requisitions = new List<Requisition>();

            using (var connection = _db.GetConnection())
            {
               var cmd = new SqlCommand(@"
                   SELECT Id, WithdrawalDate, ResponsibleEmployee
                   FROM Requisitions
                   ORDER BY WithdrawalDate DESC",
                   (SqlConnection)connection);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var requisition = MapRequisition(reader);
                        requisitions.Add(requisition);
                    }
                }

                foreach (var req in requisitions)
                {
                    req.Items = GetItems(connection, req.Id);
                }
            }
            return requisitions;
        }
        
        public IEnumerable<Requisition> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            var requisitions = new List<Requisition>();

            using (var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    SELECT Id, WithdrawalDate, ResponsibleEmployee
                    FROM Requisitions
                    WHERE WithdrawalDate BETWEEN @StartDate AND @EndDate
                    ORDER BY WithdrawalDate DESC", 
                    (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var requisition = MapRequisition(reader);
                        requisitions.Add(requisition);
                    }
                }
                foreach (var req in requisitions)
                {
                    req.Items = GetItems(connection, req.Id);
                }
            }
            return requisitions;
        }

        //Load items for a requisition (both simple and composite products)
        private List<RequisitionItem> GetItems(IDbConnection connection, int requisitionId)
        {
            var items = new List<RequisitionItem>();

            var cmd = new SqlCommand(@"
                SELECT
                    ri.Id,
                    ri.RequisitionId,
                    ri.Quantity,
                    p.Id        AS ProductId,
                    p.Name      AS ProductName,
                    p.SalePrice AS ProductSalePrice,
                    p.Type      AS ProductType
                FROM RequisitionItems ri
                INNER JOIN Products p ON p.Id = ri.ProductId
                WHERE ri.RequisitionId = @RequisitionId",
                (SqlConnection)connection);

            cmd.Parameters.AddWithValue("@RequisitionId", requisitionId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var type = reader["ProductType"].ToString();

                    Product product = type == "S"
                        ? MapSimpleProduct(reader)
                        : MapCompositeProduct(reader);

                    var item = new RequisitionItem
                    {
                        Id = (int)reader["Id"],
                        RequisitionId = (int)reader["RequisitionId"],
                        Quantity = (int)reader["Quantity"],
                        Product = product
                    };

                    items.Add(item);
                }

                // Load composite components AFTER closing the reader
                // (cannot have two open readers on the same connection)
                foreach (var item in items)
                {
                    if (item.Product is CompositeProduct composite)
                        composite.Components = GetCompositeComponents(connection, composite.Id);
                }

                return items;
            }
        }

        private Product MapCompositeProduct(SqlDataReader reader)
        {
            return new CompositeProduct
            {
                Id          = (int)reader["ProductId"],
                Name        = reader["ProductName"].ToString(),
                SalePrice   = (decimal)reader["ProductSalePrice"]
            };
        }

        private Product MapSimpleProduct(SqlDataReader reader)
        {
            return new SimpleProduct
            {
                Id          = (int)reader["ProductId"],
                Name        = reader["ProductName"].ToString(),
                SalePrice   = (decimal)reader["ProductSalePrice"]
            };
        }

        private Requisition MapRequisition(SqlDataReader reader)
        {
            return new Requisition
            {
                Id                  = (int)reader["Id"],
                WithdrawalDate      = (DateTime)reader["WithdrawalDate"],
                ResponsibleEmployee = reader["ResponsibleEmployee"].ToString()
            };
        }

        public void Update(Requisition requisition)
        {
            // Step 1: Update requisition header

            using(var connection = _db.GetConnection())
            {
                var cmd = new SqlCommand(@"
                    UPDATE Requisitions
                    SET WithdrawalDate = @WithdrawalDate,
                        ResponsibleEmployee = @ResponsibleEmployee
                    WHERE Id = @Id;", (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@Id", requisition.Id);
                cmd.Parameters.AddWithValue("@WithdrawalDate", requisition.WithdrawalDate);
                cmd.Parameters.AddWithValue("@ResponsibleEmployee", requisition.ResponsibleEmployee);
                cmd.ExecuteNonQuery();

                // Step 2: For simplicity, delete all existing items and re-insert
                var deleteCmd = new SqlCommand("" +
                    "DELETE FROM RequisitionItems WHERE RequisitionId = @Ìd;",
                    (SqlConnection)connection);

                cmd.Parameters.AddWithValue("@Id", requisition.Id);
                deleteCmd.ExecuteNonQuery();

                foreach (var item in requisition.Items)
                {
                    InsertItem(connection, requisition.Id, item);
                }
            }
        }

        private List<ProductComponent> GetCompositeComponents(
    IDbConnection connection, int compositeProductId)
        {
            var components = new List<ProductComponent>();

            var cmd = new SqlCommand(@"
        SELECT 
            pc.Id,
            pc.Quantity,
            p.Id        AS SimpleProductId,
            p.Name      AS SimpleProductName,
            p.SalePrice AS SimpleProductSalePrice,
            s.CostPrice AS SimpleProductCostPrice
        FROM ProductComponents pc
        INNER JOIN Products p      ON p.Id = pc.SimpleProductId
        INNER JOIN SimpleProducts s ON s.Id = pc.SimpleProductId
        WHERE pc.CompositeProductId = @CompositeProductId",
                (SqlConnection)connection);

            cmd.Parameters.AddWithValue("@CompositeProductId", compositeProductId);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    components.Add(new ProductComponent
                    {
                        Id = (int)reader["Id"],
                        Quantity = (int)reader["Quantity"],
                        SimpleProduct = new SimpleProduct
                        {
                            Id = (int)reader["SimpleProductId"],
                            Name = reader["SimpleProductName"].ToString(),
                            SalePrice = (decimal)reader["SimpleProductSalePrice"],
                            CostPrice = (decimal)reader["SimpleProductCostPrice"]
                        }
                    });
                }
            }
            return components;
        }
    }
}

