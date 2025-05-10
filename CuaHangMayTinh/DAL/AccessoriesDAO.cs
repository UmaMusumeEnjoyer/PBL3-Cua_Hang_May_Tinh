using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace CuaHangMayTinh.DAL
{
    public class AccessoriesDAO : DbConnect
    {
        #region Read
        public DataTable GetAll()
        {
            const string sql = @"SELECT p.*, a.accessoriesName, a.overview
                                 FROM Product p
                                 INNER JOIN Accessories a ON p.Product_Id = a.Product_Id
                                    WHERE p.IsDeleted = 0";
            return GetData(sql);
        }

        public DataTable Get(int id)
        {
            const string sql = @"SELECT p.*, a.accessoriesName, a.overview
                                 FROM Product p
                                 INNER JOIN Accessories a ON p.Product_Id = a.Product_Id
                                 WHERE p.Product_Id = @Id
                                  AND p.IsDeleted = 0";
            return GetData(sql, new[] { new SqlParameter("@Id", id) });
        }
        #endregion

        #region CUD
        public int Insert(string accessoriesName, string overview,
                          int supplierId, string productName,
                          decimal price, int stockQuantity)
        {
            int newProductId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert into Product and get new ID
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
                                        VALUES (@SupplierId, @ProductName, @Price, @StockQuantity);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@SupplierId", supplierId),
                        new SqlParameter("@ProductName", productName),
                        new SqlParameter("@Price", price),
                        new SqlParameter("@StockQuantity", stockQuantity)
                    });
                    newProductId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2) Insert into Accessories
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Accessories (Product_Id, accessoriesName, overview)
                                        VALUES (@ProductId, @Name, @Overview)";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@ProductId", newProductId),
                        new SqlParameter("@Name", accessoriesName),
                        new SqlParameter("@Overview", overview)
                    });
                    cmd.ExecuteNonQuery();
                }
            });
            return newProductId;
        }

        public int Update(int productId, string accessoriesName, string overview,
                          int supplierId, string productName,
                          decimal price, int stockQuantity)
        {
            int rows = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Update Product
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"UPDATE Product SET
                                          Supplier_Id = @SupplierId,
                                          productName = @ProductName,
                                          price = @Price,
                                          stockQuantity = @StockQuantity
                                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@SupplierId", supplierId),
                        new SqlParameter("@ProductName", productName),
                        new SqlParameter("@Price", price),
                        
                        new SqlParameter("@StockQuantity", stockQuantity),
                        new SqlParameter("@ProductId", productId)
                    });
                    rows += cmd.ExecuteNonQuery();
                }

                // Update Accessories
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"UPDATE Accessories SET
                                          accessoriesName = @Name,
                                          overview = @Overview
                                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@Name", accessoriesName),
                        new SqlParameter("@Overview", overview),
                        new SqlParameter("@ProductId", productId)
                    });
                    rows += cmd.ExecuteNonQuery();
                }
            });
            return rows;
        }

        public int Delete(int productId)
        {
            return new ProductDAO().DeleteProduct(productId);
        }
        #endregion

        public DataTable Search(string keyword)
        {
            const string sql = @"SELECT p.*, a.accessoriesName, a.overview
                                 FROM Product p
                                 INNER JOIN Accessories a ON p.Product_Id = a.Product_Id
                                 WHERE p.IsDeleted = 0
                                   AND (a.accessoriesName LIKE @Keyword
                                    OR a.overview LIKE @Keyword)";
            return GetData(sql, new[] { new SqlParameter("@Keyword", $"%{keyword}%") });
        }
    }
}
