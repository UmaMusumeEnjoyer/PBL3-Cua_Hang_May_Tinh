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
            SqlParameter[] parameters = { new SqlParameter("@Id", SqlDbType.Int) { Value = id } };
            return GetData(sql, parameters);
        }

        public int InsertPC(int supplierId, string productName, decimal price, int stockQuantity,
                             string pcName, string specification)
        {
            // Thêm vào Product, lấy về ProductId
            var productSql = @"INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
                                VALUES (@SupplierId, @ProductName, @Price, @StockQuantity);
                                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var productParams = new SqlParameter[] {
                new SqlParameter("@SupplierId", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity }
            };
            int productId = Convert.ToInt32(ExecuteScalar(productSql, productParams));

            // Thêm vào PC
            var pcSql = @"INSERT INTO PC (Product_Id, pcName, specification)
                          VALUES (@ProductId, @PCName, @Specification)";
            var pcParams = new SqlParameter[] {
                new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId },
                new SqlParameter("@PCName", SqlDbType.NVarChar) { Value = pcName },
                new SqlParameter("@Specification", SqlDbType.NVarChar) { Value = specification }
            };
            ExecuteNonQuery(pcSql, pcParams);
            return productId;
        }

        public int UpdatePC(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                             string pcName, string specification)
        {
            // Cập nhật Product
            var productSql = @"UPDATE Product SET
                               Supplier_Id   = @SupplierId,
                               productName   = @ProductName,
                               price         = @Price,
                               stockQuantity = @StockQuantity
                             WHERE Product_Id = @ProductId";
            var productParams = new SqlParameter[] {
                new SqlParameter("@SupplierId", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId }
            };
            int rows = ExecuteNonQuery(productSql, productParams);

            // Cập nhật PC
            var pcSql = @"UPDATE PC SET
                          pcName        = @PCName,
                          specification = @Specification
                         WHERE Product_Id = @ProductId";
            var pcParams = new SqlParameter[] {
                new SqlParameter("@PCName", SqlDbType.NVarChar) { Value = pcName },
                new SqlParameter("@Specification", SqlDbType.NVarChar) { Value = specification },
                new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId }
            };
            rows += ExecuteNonQuery(pcSql, pcParams);
            return rows;
        }

        public int DeletePC(int productId)
        {
            // Xóa bản ghi PC trước
            var pcSql = "DELETE FROM PC WHERE Product_Id = @ProductId";
            var param = new SqlParameter[] { new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId } };
            int rows = ExecuteNonQuery(pcSql, param);

            // Xóa Product
            var productSql = "DELETE FROM Product WHERE Product_Id = @ProductId";
            rows += ExecuteNonQuery(productSql, param);
            return rows;
        }

        public DataTable Search(string keyword)
        {
            string sql = @"SELECT p.*, pc.*
                           FROM Product p
                           INNER JOIN PC pc ON p.Product_Id = pc.Product_Id
                           WHERE pc.pcName LIKE @Keyword
                              OR pc.specification LIKE @Keyword";
            SqlParameter[] parameters = { new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" } };
            return GetData(sql, parameters);
        }
    }
}