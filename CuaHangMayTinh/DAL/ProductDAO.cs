using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class ProductDAO : DbConnect
    {
        public DataTable GetAllProducts()
        {
            return GetData(@"SELECT p.*, s.supplierName 
                    FROM Product p 
                    INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id");
        }
        public DataTable GetProductById(int id)
        {
            string sql = @"SELECT p.*, s.supplierName 
                      FROM Product p 
                      INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
                      WHERE p.Product_Id = @Id";
            SqlParameter[] para = { new SqlParameter("@Id", id) };
            return GetData(sql, para);
        }
        public int Insert(int supp_id, string name, decimal price, int stock_Quanity)
        {
            string sql = @"INSERT INTO Product 
                         (Supplier_Id, productName, price, stockQuantity) 
                         VALUES 
                         (@SupplierId, @Name, @Price, @Stock)";
            SqlParameter[] para =
            {
                new SqlParameter("@Supplier_Id", supp_id),
                new SqlParameter("@productName", name),
                new SqlParameter("@Price", price),
                new SqlParameter("@stockQuanity", stock_Quanity)
            };
            return ExecuteNonQuery(sql, para);
        }
        public int UpdateProduct(int productId, int supplierId, string productName, decimal price, int stockQuantity)
        {
            string sql = @"UPDATE Product SET 
                         Supplier_Id = @SupplierId,
                         productName = @Name,
                         price = @Price,
                         stockQuantity = @Stock
                         WHERE Product_Id = @ProductId";

            SqlParameter[] param =
            {
                new SqlParameter("@SupplierId", supplierId),
                new SqlParameter("@Name", productName),
                new SqlParameter("@Price", price),
                new SqlParameter("@Stock", stockQuantity),
                new SqlParameter("@ProductId", productId)
            };

            return ExecuteNonQuery(sql, param);
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM Product WHERE Product_Id = @Id";
            SqlParameter[] param = { new SqlParameter("@Id", id) };
            return ExecuteNonQuery(sql, param);
        }
        public DataTable Search(string keyword)
        {
            string sql = @"SELECT * FROM Product 
                      WHERE productName LIKE @Keyword";
            SqlParameter[] parameters = { new SqlParameter("@Keyword", $"%{keyword}%") };
            return GetData(sql, parameters);
        }
    }

}
