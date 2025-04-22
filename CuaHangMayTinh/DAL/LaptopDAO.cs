using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
using Microsoft.SqlServer.Server;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Windows.Forms;

namespace CuaHangMayTinh.DAL
{
    public class LaptopDAO : DbConnect
    {
        public DataTable GetAllLaptops()
        {
            string sql = @"SELECT p.*, l.laptopName, l.weight, l.screenSize, l.specification, l.colour 
                         FROM Product p
                         INNER JOIN Laptop l ON p.Product_Id = l.Product_Id";
            return GetData(sql);
        }
        public DataTable GetLaptopById(int id)
        {
            string sql = @"SELECT p.*, l.laptopName, l.weight, l.screenSize, l.specification, l.colour 
                         FROM Product p
                         INNER JOIN Laptop l ON p.Product_Id = l.Product_Id
                         WHERE p.Product_Id = @Id";
            SqlParameter[] para = { new SqlParameter("@Id", id) };
            return GetData(sql, para);
        }

        public int InsertLaptop(string laptopName, decimal weight, decimal screenSize,
                            string specification, string colour, int supplierId,
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

            // Thêm vào Laptop
            var laptopSql = @"INSERT INTO Laptop (Product_Id, laptopName, weight, screenSize, specification, colour) 
                               VALUES (@ProductId, @LaptopName, @Weight, @ScreenSize, @Specification, @Colour)";

            var laptopParams = new SqlParameter[] {
                    new SqlParameter("@ProductId", productId),
                    new SqlParameter("@LaptopName", laptopName),
                    new SqlParameter("@Weight", weight),
                    new SqlParameter("@ScreenSize", screenSize),
                    new SqlParameter("@Specification", specification),
                    new SqlParameter("@Colour", colour)
                };

                ExecuteNonQuery(laptopSql, laptopParams);
                return productId;
            }


        public int UpdateLaptop(int productId, string laptopName, decimal weight,
                              decimal screenSize, string specification, string colour,
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

                // Cập nhật Laptop
                var laptopSql = @"UPDATE Laptop SET 
                                laptopName = @LaptopName,
                                weight = @Weight,
                                screenSize = @ScreenSize,
                                specification = @Specification,
                                colour = @Colour
                                WHERE Product_Id = @ProductId";

                var laptopParams = new SqlParameter[] {
                    new SqlParameter("@LaptopName", laptopName),
                    new SqlParameter("@Weight", weight),
                    new SqlParameter("@ScreenSize", screenSize),
                    new SqlParameter("@Specification", specification),
                    new SqlParameter("@Colour", colour),
                    new SqlParameter("@ProductId", productId)
                };

                rowsAffected += ExecuteNonQuery(laptopSql, laptopParams);
                return rowsAffected;
            }
            
        

        public int DeleteLaptop(int productId)
        {
            {
                // Xóa Laptop trước
                var laptopSql = "DELETE FROM Laptop WHERE Product_Id = @ProductId";
                var laptopParams = new SqlParameter[] {
                    new SqlParameter("@ProductId", productId)
                };

                int rowsAffected = ExecuteNonQuery(laptopSql, laptopParams);

                // Xóa Product
                var productSql = "DELETE FROM Product WHERE Product_Id = @ProductId";
                rowsAffected += ExecuteNonQuery(productSql, laptopParams);

                return rowsAffected;
            }
            
        }

    }
}
