﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace CuaHangMayTinh.DAL
{
    public class LaptopDAO : DbConnect
    {
        #region Read
        public DataTable GetAllLaptops()
        {
            const string sql = @"SELECT p.*, l.laptopName, l.weight, l.screenSize, l.specification, l.colour
                                  FROM Product p
                                  INNER JOIN Laptop l ON p.Product_Id = l.Product_Id
                                    WHERE p.IsDeleted = 0";
            return GetData(sql);
        }

        public DataTable GetLaptopById(int id)
        {
            const string sql = @"SELECT p.*, l.laptopName, l.weight, l.screenSize, l.specification, l.colour
                                  FROM Product p
                                  INNER JOIN Laptop l ON p.Product_Id = l.Product_Id
                                  WHERE p.Product_Id = @Id
                                     AND p.IsDeleted = 0";

            return GetData(sql, new[] { new SqlParameter("@Id", id) });
        }
        #endregion

        #region CUD
        public int Insert(string laptopName, decimal weight, decimal screenSize,
                          string specification, string colour, int supplierId,
                          string productName, decimal price, int stockQuantity)
        {
            int productId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert into Product
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
                    productId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2) Insert into Laptop
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
                                        VALUES (@ProductId, @LaptopName, @Weight, @ScreenSize, @Specification, @Colour)";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@ProductId", productId),
                        new SqlParameter("@LaptopName", laptopName),
                        new SqlParameter("@Weight", weight),
                        new SqlParameter("@ScreenSize", screenSize),
                        new SqlParameter("@Specification", specification),
                        new SqlParameter("@Colour", colour)
                    });
                    cmd.ExecuteNonQuery();
                }
            });
            return productId;
        }

        public int UpdateLaptop(int productId, string laptopName, decimal weight,
                                decimal screenSize, string specification, string colour,
                                int supplierId, string productName, decimal price, int stockQuantity)
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

                // Update Laptop
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"UPDATE Laptop SET
                                          laptopName = @LaptopName,
                                          weight = @Weight,
                                          screenSize = @ScreenSize,
                                          specification = @Specification,
                                          colour = @Colour
                                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@LaptopName", laptopName),
                        new SqlParameter("@Weight", weight),
                        new SqlParameter("@ScreenSize", screenSize),
                        new SqlParameter("@Specification", specification),
                        new SqlParameter("@Colour", colour),
                        new SqlParameter("@ProductId", productId)
                    });
                    rows += cmd.ExecuteNonQuery();
                }
            });
            return rows;
        }

        public int DeleteLaptop(int productId)
        {
            return new ProductDAO().DeleteProduct(productId);
        }
        #endregion
        public DataTable Search(string keyword)
        {
            const string sql = @"SELECT p.*, l.*
                                 FROM Product p
                                 INNER JOIN Laptop l ON p.Product_Id = l.Product_Id
                                 WHERE p.IsDeleted = 0
                                 AND (l.laptopName LIKE @Keyword
                                    OR l.colour LIKE @Keyword
                                    OR l.specification LIKE @Keyword)";
            return GetData(sql, new[] { new SqlParameter("@Keyword", $"%{keyword}%") });
        }

        #region Combobox Colour-ScreenSize
        public List<string> GetDistinctColours()
        {
            const string sql = @"
          SELECT DISTINCT l.colour 
          FROM Laptop l
          JOIN Product p ON l.Product_Id = p.Product_Id
          WHERE p.IsDeleted = 0 
            AND l.colour IS NOT NULL";
            var dt = GetData(sql);
            return dt.Rows
                     .Cast<DataRow>()
                     .Select(r => r["colour"].ToString())
                     .OrderBy(c => c)
                     .ToList();
        }
        public List<decimal> GetDistinctScreenSizes()
        {   
            const string sql = @"
          SELECT DISTINCT l.screenSize 
          FROM Laptop l
          JOIN Product p ON l.Product_Id = p.Product_Id
          WHERE p.IsDeleted = 0 
            AND l.screenSize IS NOT NULL";
            var dt = GetData(sql);
            return dt.Rows
                     .Cast<DataRow>()
                     .Select(r => Convert.ToDecimal(r["screenSize"]))
                     .OrderBy(s => s)
                     .ToList();
        }
        #endregion
    }
}
