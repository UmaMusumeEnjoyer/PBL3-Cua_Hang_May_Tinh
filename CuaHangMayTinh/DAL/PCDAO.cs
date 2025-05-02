using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class PCDAO : DbConnect
    {
        public DataTable GetAllPCs()
        {
            const string sql = @"SELECT p.*, pc.pcName, pc.specification
                                FROM Product p
                                INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                                WHERE p.IsDeleted = 0";
            return GetData(sql);
        }

        public DataTable GetPCById(int id)
        {
            const string sql = @"SELECT p.*, pc.pcName, pc.specification
                                FROM Product p
                                INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                                WHERE p.Product_Id = @Id
                                AND  p.IsDeleted = 0";
            return GetData(sql, new SqlParameter[] { new SqlParameter("@Id", id) });
        }

        public int Insert(string pcName, string specification, int supplierId,
            string productName, decimal price, int stockQuantity)
        {
            int newProductId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert into Product and retrieve new ID
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"
                        INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
                        VALUES (@SupplierId, @ProductName, @Price, @StockQuantity);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@SupplierId", supplierId),
                        new SqlParameter("@ProductName", productName),
                        new SqlParameter("@Price", price),
                        new SqlParameter("@StockQuantity", stockQuantity)
                    });
                    newProductId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2) Insert into PC
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"
                        INSERT INTO PC (Product_Id, pcName, specification)
                        VALUES (@ProductId, @PCName, @Specification)";
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@ProductId", newProductId),
                        new SqlParameter("@PCName", pcName),
                        new SqlParameter("@Specification", specification)
                    });
                    cmd.ExecuteNonQuery();
                }
            });
            return newProductId;
        }

        public int UpdatePC(int productId, string pcName, string specification,
            int supplierId, string productName, decimal price, int stockQuantity)
        {
            int rows = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Update Product
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"
                        UPDATE Product SET
                          Supplier_Id = @SupplierId,
                          productName = @ProductName,
                          price = @Price,
                          stockQuantity = @StockQuantity
                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@SupplierId", supplierId),
                        new SqlParameter("@ProductName", productName),
                        new SqlParameter("@Price", price),
                        new SqlParameter("@StockQuantity", stockQuantity),
                        new SqlParameter("@ProductId", productId)
                    });
                    rows += cmd.ExecuteNonQuery();
                }

                // Update PC
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"
                        UPDATE PC SET
                          pcName = @PCName,
                          specification = @Specification
                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@PCName", pcName),
                        new SqlParameter("@Specification", specification),
                        new SqlParameter("@ProductId", productId)
                    });
                    rows += cmd.ExecuteNonQuery();
                }
            });
            return rows;
        }

        public int DeletePC(int productId)
        {
            return new ProductDAO().DeleteProduct(productId);
        }

        public DataTable Search(string keyword)
        {
            const string sql = @"SELECT p.*, pc.*
                                 FROM Product p
                                 INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                                 WHERE pc.pcName LIKE @Keyword
                                    OR pc.specification LIKE @Keyword";
            return GetData(sql, new SqlParameter[] { new SqlParameter("@Keyword", $"%{keyword}%") });
        }
    }
}