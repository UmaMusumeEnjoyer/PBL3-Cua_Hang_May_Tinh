using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class ProductDAO : DbConnect
    {
        // Lấy tất cả sản phẩm cùng tên nhà cung cấp
        public DataTable GetAllProducts() => GetData(
            @"SELECT p.*, s.supplierName
               FROM Product p
               INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
               WHERE p.IsDeleted = 0");
        
        // Lấy sản phẩm đã xóa (thùng rác)
        public DataTable GetDeletedProducts() => GetData(
            @"SELECT p.*, s.supplierName
              FROM Product p
              INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
              WHERE p.IsDeleted = 1");
        
        // Lấy sản phẩm theo ID
        public DataTable GetProductById(int id) => GetData(
            @"SELECT p.*, s.supplierName
               FROM Product p
               INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
               WHERE p.Product_Id = @Id
               AND p.IsDeleted = 0",
            new[] { new SqlParameter("@Id", id) });

        #region Insert kèm bảng con
        public int InsertProductWithLaptop(int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            int newProductId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert vào Product
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
                // 2) Insert vào Laptop
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour)
                                        VALUES (@ProductId, @LaptopName, @Weight, @ScreenSize, @Specification, @Colour)";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@ProductId", newProductId),
                        new SqlParameter("@LaptopName", laptopName),
                        new SqlParameter("@Weight", weight),
                        new SqlParameter("@ScreenSize", screenSize),
                        new SqlParameter("@Specification", specification),
                        new SqlParameter("@Colour", colour)
                    });
                    cmd.ExecuteNonQuery();
                }
            });
            return newProductId;
        }

        public int InsertProductWithPC(int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            int newProductId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert vào Product
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
                // 2) Insert vào PC
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO PC (Product_Id, pcName, specification)
                                        VALUES (@ProductId, @PCName, @Specification)";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@ProductId", newProductId),
                        new SqlParameter("@PCName", pcName),
                        new SqlParameter("@Specification", specification)
                    });
                    cmd.ExecuteNonQuery();
                }
            });
            return newProductId;
        }

        public int InsertProductWithAccessory(int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            int newProductId = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert vào Product
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
                // 2) Insert vào Accessories
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
        #endregion

        #region Update kèm bảng con
        public int UpdateProductWithLaptop(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            int rowsAffected = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Cập nhật Product
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
                    rowsAffected += cmd.ExecuteNonQuery();
                }
                // Cập nhật Laptop
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
                    rowsAffected += cmd.ExecuteNonQuery();
                }
            });
            return rowsAffected;
        }

        public int UpdateProductWithPC(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            int rowsAffected = 0;
            ExecuteTransaction((conn, tran) =>
            {
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
                    rowsAffected += cmd.ExecuteNonQuery();
                }
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"UPDATE PC SET
                                        pcName = @PCName,
                                        specification = @Specification
                                        WHERE Product_Id = @ProductId";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@PCName", pcName),
                        new SqlParameter("@Specification", specification),
                        new SqlParameter("@ProductId", productId)
                    });
                    rowsAffected += cmd.ExecuteNonQuery();
                }
            });
            return rowsAffected;
        }

        public int UpdateProductWithAccessory(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            int rowsAffected = 0;
            ExecuteTransaction((conn, tran) =>
            {
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
                    rowsAffected += cmd.ExecuteNonQuery();
                }
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
                    rowsAffected += cmd.ExecuteNonQuery();
                }
            });
            return rowsAffected;
        }
        #endregion

        #region Delete kèm bảng con
        public int DeleteProduct(int productId)
        {
            int rowsAffected = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Xóa các bảng con
                string[] childTables = { "Laptop", "PC", "Accessories" };
                foreach (var table in childTables)
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = tran;
                        cmd.CommandText = $"DELETE FROM {table} WHERE Product_Id = @ProductId";
                        cmd.Parameters.Add(new SqlParameter("@ProductId", productId));
                        cmd.ExecuteNonQuery();
                    }
                }

                // Đánh dấu xóa mềm
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"UPDATE Product SET 
                                  IsDeleted = 1 
                                  WHERE Product_Id = @ProductId";
                    cmd.Parameters.Add(new SqlParameter("@ProductId", productId));
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            });
            return rowsAffected;
        }
        #endregion

        // Tìm kiếm đơn giản
        public DataTable Search(string keyword)
            => GetData(
                @"SELECT p.*, s.supplierName
                  FROM Product p
                  INNER JOIN Supplier s ON p.Supplier_Id = s.Supplier_Id
                  WHERE p.productName LIKE @Keyword
                    AND p.IsDeleted = 0",
                new[] { new SqlParameter("@Keyword", $"%{keyword}%") });

        // Lấy chi tiết category chung
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
         LEFT JOIN Accessories a ON p.Product_Id = a.Product_Id
         WHERE p.IsDeleted = 0");
        public int RestoreProduct(int productId)
        {
            const string sql = @"
                UPDATE Product 
                SET IsDeleted = 0 
                WHERE Product_Id = @ProductId";
            return ExecuteNonQuery(sql,
                new[] { new SqlParameter("@ProductId", productId) });
        }
    }
    
}
