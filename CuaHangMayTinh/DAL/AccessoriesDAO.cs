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
        public DataTable GetAll()
        {
            string sql = @"SELECT p.*, a.accessoriesName, a.overview
                           FROM Product p
                           INNER JOIN Accessories a ON p.Product_Id = a.Product_Id";
            return GetData(sql);
        }
        public DataTable Get(int id) {
            string sql = @"SELECT p.*, a.accessoriesName, a.overview
                           FROM Product p
                           INNER JOIN Accessories a ON p.Product_Id = a.Product_Id
                           WHERE p.Product_Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            return GetData(sql, parameters);
        }
        public int Insert(string accessoriesName, string overview,
                          int supplierId, string productName,
                          decimal price, int stockQuantity)
        {
            // Insert into Product
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

            // Insert into Accessories
            var accSql = @"INSERT INTO Accessories (Product_Id, accessoriesName, overview)
                           VALUES (@ProductId, @Name, @Overview)";
            var accParams = new SqlParameter[] {
                new SqlParameter("@ProductId", productId),
                new SqlParameter("@Name", accessoriesName),
                new SqlParameter("@Overview", overview)
            };
            ExecuteNonQuery(accSql, accParams);
            return productId;
        }

    }
}
