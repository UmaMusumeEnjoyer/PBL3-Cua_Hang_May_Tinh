using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class ProductDAO : DbConnect
    {
        public DataTable GetAllProducts() => GetData(
            @"SELECT p.*, s.supplierName
               FROM Product p
               INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id");

        public DataTable GetProductById(int id) => GetData(
            @"SELECT p.*, s.supplierName
               FROM Product p
               INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
               WHERE p.Product_Id = @Id",
            new SqlParameter[] { new SqlParameter("@Id", id) });

        public int Insert(int supplierId, string productName, decimal price, int stockQuantity)
            => ExecuteNonQuery(
                @"INSERT INTO Product (Supplier_Id, productName, price, stockQuantity)
                   VALUES (@SupplierId, @ProductName, @Price, @StockQuantity)",
                new SqlParameter[] {
                    new SqlParameter("@SupplierId", supplierId),
                    new SqlParameter("@ProductName", productName),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@StockQuantity", stockQuantity)
                });

        public int UpdateProduct(int productId, int supplierId, string productName, decimal price, int stockQuantity)
            => ExecuteNonQuery(
                @"UPDATE Product SET
                    Supplier_Id = @SupplierId,
                    productName = @ProductName,
                    price = @Price,
                    stockQuantity = @StockQuantity
                   WHERE Product_Id = @ProductId",
                new SqlParameter[] {
                    new SqlParameter("@SupplierId", supplierId),
                    new SqlParameter("@ProductName", productName),
                    new SqlParameter("@Price", price),
                    new SqlParameter("@StockQuantity", stockQuantity),
                    new SqlParameter("@ProductId", productId)
                });

        public int Delete(int id)
            => ExecuteNonQuery("DELETE FROM Product WHERE Product_Id = @Id",
                new SqlParameter[] { new SqlParameter("@Id", id) });

        public DataTable Search(string keyword)
            => GetData(
                "SELECT * FROM Product WHERE productName LIKE @Keyword",
                new SqlParameter[] { new SqlParameter("@Keyword", $"%{keyword}%") });

        public DataTable GetAllProductDetails() => GetData(
            @"SELECT p.Product_Id, p.productName,
                      CASE WHEN l.Product_Id IS NOT NULL THEN 'Laptop'
                           WHEN pc.Product_Id IS NOT NULL THEN 'PC'
                           ELSE 'Accessories' END AS Category,
                   COALESCE(l.specification, pc.specification, a.overview) AS Specification,
                   COALESCE(l.colour, '') AS Colour,
                   p.price, p.stockQuantity
              FROM Product p
         LEFT JOIN Laptop l ON p.Product_Id = l.Product_Id
         LEFT JOIN PC pc ON p.Product_Id = pc.Product_Id
         LEFT JOIN Accessories a ON p.Product_Id = a.Product_Id");
    }
}

