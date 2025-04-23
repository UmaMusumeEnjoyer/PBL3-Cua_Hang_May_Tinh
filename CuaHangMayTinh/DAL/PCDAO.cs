using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class PCDAO : DbConnect
    {
        public DataTable GetAllPCs()
        {
            string sql = @"SELECT p.*, pc.pcName, pc.specification 
                         FROM Product p
                         INNER JOIN PC pc ON p.Product_Id = pc.Product_Id";
            return GetData(sql);
        }

        public DataTable GetPCById(int id)
        {
            string sql = @"SELECT p.*, pc.pcName, pc.specification 
                         FROM Product p
                         INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                         WHERE p.Product_Id = @Id";
            SqlParameter[] para = { new SqlParameter("@Id", id) };
            return GetData(sql, para);
        }

        public int Insert(string pcName, string specification, int supplierId,
                          string productName, decimal price, int stockQuantity)
        {
            // Thêm vào Product
            var productSql = @"INSERT INTO Product (Supplier_Id, productName, price, stockQuantity) 
                                 VALUES (@SupplierId, @ProductName, @Price, @StockQuantity);
                                 SELECT SCOPE_IDENTITY();";

            var productParams = new SqlParameter[] {
                    new SqlParameter("@SupplierId", supplierId),
                    new SqlParameter("@ProductName", productName),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@StockQuantity", stockQuantity)
                };

            int productId = Convert.ToInt32(ExecuteScalar(productSql, productParams));

            // Thêm vào PC
            var pcSql = @"INSERT INTO PC (Product_Id, pcName, specification) 
                           VALUES (@ProductId, @PCName, @Specification)";

            var pcParams = new SqlParameter[] {
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@PCName", pcName),
                    new SqlParameter("@Specification", specification)
                };

            ExecuteNonQuery(pcSql, pcParams);
            return productId;
        }

        public int UpdatePC(int productId, string pcName, string specification,
                            int supplierId, string productName, decimal price, int stockQuantity)
        {
            // Cập nhật Product
            var productSql = @"UPDATE Product SET 
                              Supplier_Id = @SupplierId,
                              productName = @ProductName,
                              price = @Price,
                              stockQuantity = @StockQuantity
                              WHERE Product_Id = @ProductId";

            var productParams = new SqlParameter[] {
                new SqlParameter("@SupplierId", supplierId),
                new SqlParameter("@ProductName", productName),
                new SqlParameter("@Price", price),
                new SqlParameter("@StockQuantity", stockQuantity),
                new SqlParameter("@ProductId", productId)
            };

            int rowsAffected = ExecuteNonQuery(productSql, productParams);

            // Cập nhật PC
            var pcSql = @"UPDATE PC SET 
                        pcName = @PCName,
                        specification = @Specification
                        WHERE Product_Id = @ProductId";

            var pcParams = new SqlParameter[] {
                new SqlParameter("@PCName", pcName),
                new SqlParameter("@Specification", specification),
                new SqlParameter("@ProductId", productId)
            };

            rowsAffected += ExecuteNonQuery(pcSql, pcParams);
            return rowsAffected;
        }

        public int DeletePC(int productId)
        {
            // Xóa PC trước
            var pcSql = "DELETE FROM PC WHERE Product_Id = @ProductId";
            var pcParams = new SqlParameter[] {
                new SqlParameter("@ProductId", productId)
            };

            int rowsAffected = ExecuteNonQuery(pcSql, pcParams);

            // Xóa Product
            var productSql = "DELETE FROM Product WHERE Product_Id = @ProductId";
            rowsAffected += ExecuteNonQuery(productSql, pcParams);

            return rowsAffected;
        }

        public DataTable Search(string keyword)
        {
            string sql = @"SELECT p.*, pc.* 
                 FROM Product p
                 INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                 WHERE pc.pcName LIKE @Keyword 
                 OR pc.specification LIKE @Keyword";
            SqlParameter[] param = { new SqlParameter("@Keyword", $"%{keyword}%") };
            return GetData(sql, param);
        }
    }
}