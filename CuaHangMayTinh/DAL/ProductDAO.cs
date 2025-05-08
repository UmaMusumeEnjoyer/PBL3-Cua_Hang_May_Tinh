using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class ProductDAO : DbConnect
    {
        #region Read
        // Lấy tất cả sản phẩm chưa xóa
        public DataTable GetAllProducts()
            => ExecuteSp("sp_Product_GetAll");

        // Lấy sản phẩm đã xóa (thùng rác)
        public DataTable GetDeletedProducts()
            => ExecuteSp("sp_Product_GetDeleted");

        // Lấy sản phẩm theo ID
        public DataTable GetProductById(int productId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId }
            };
            return ExecuteSp("sp_Product_GetById", parameters);
        }
        #endregion

        // Tìm kiếm sản phẩm
        public DataTable Search(string keyword)
        {
            var parameters = new[]
            {
                new SqlParameter("@Keyword", SqlDbType.NVarChar, 100) { Value = $"%{keyword}%" }
            };
            return ExecuteSp("sp_Product_Search", parameters);
        }

        // Khôi phục sản phẩm
        public int RestoreProduct(int productId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId }
            };
            return ExecuteSpNonQuery("sp_Product_Restore", parameters);
        }

        #region Insert kèm bảng con
        public int InsertProductWithLaptop(int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            var parameters = new[]
            {
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@LaptopName", SqlDbType.NVarChar, 100) { Value = laptopName },
                new SqlParameter("@Weight", SqlDbType.Decimal) { Value = weight },
                new SqlParameter("@ScreenSize", SqlDbType.Decimal) { Value = screenSize },
                new SqlParameter("@Specification", SqlDbType.NVarChar, -1) { Value = specification },
                new SqlParameter("@Colour", SqlDbType.NVarChar, 50) { Value = colour },
                new SqlParameter("@NewProductId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            ExecuteSpNonQuery("sp_Product_InsertWithLaptop", parameters);
            // Lấy giá trị output từ tham số cuối
            var lastIndex = parameters.Length - 1;
            return (int)parameters[lastIndex].Value;
        }

        public int InsertProductWithPC(int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            var parameters = new[]
            {
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@PCName", SqlDbType.NVarChar, 100) { Value = pcName },
                new SqlParameter("@Specification", SqlDbType.NVarChar, -1) { Value = specification },
                new SqlParameter("@NewProductId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            ExecuteSpNonQuery("sp_Product_InsertWithPC", parameters);
            var lastIdx = parameters.Length - 1;
            return (int)parameters[lastIdx].Value;
        }

        public int InsertProductWithAccessory(int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            var parameters = new[]
            {
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@AccessoriesName", SqlDbType.NVarChar, 100) { Value = accessoriesName },
                new SqlParameter("@Overview", SqlDbType.NVarChar, -1) { Value = overview },
                new SqlParameter("@NewProductId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            ExecuteSpNonQuery("sp_Product_InsertWithAccessory", parameters);
            var idx = parameters.Length - 1;
            return (int)parameters[idx].Value;
        }
        #endregion

        #region Update kèm bảng con
        public int UpdateProductWithLaptop(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId },
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@LaptopName", SqlDbType.NVarChar, 100) { Value = laptopName },
                new SqlParameter("@Weight", SqlDbType.Decimal) { Value = weight },
                new SqlParameter("@ScreenSize", SqlDbType.Decimal) { Value = screenSize },
                new SqlParameter("@Specification", SqlDbType.NVarChar, -1) { Value = specification },
                new SqlParameter("@Colour", SqlDbType.NVarChar, 50) { Value = colour }
            };

            return ExecuteSpNonQuery("sp_Product_UpdateWithLaptop", parameters);
        }

        public int UpdateProductWithPC(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId },
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@PCName", SqlDbType.NVarChar, 100) { Value = pcName },
                new SqlParameter("@Specification", SqlDbType.NVarChar, -1) { Value = specification }
            };

            return ExecuteSpNonQuery("sp_Product_UpdateWithPC", parameters);
        }

        public int UpdateProductWithAccessory(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId },
                new SqlParameter("@Supplier_Id", SqlDbType.Int) { Value = supplierId },
                new SqlParameter("@ProductName", SqlDbType.NVarChar, 100) { Value = productName },
                new SqlParameter("@Price", SqlDbType.Decimal) { Value = price },
                new SqlParameter("@StockQuantity", SqlDbType.Int) { Value = stockQuantity },
                new SqlParameter("@AccessoriesName", SqlDbType.NVarChar, 100) { Value = accessoriesName },
                new SqlParameter("@Overview", SqlDbType.NVarChar, -1) { Value = overview }
            };

            return ExecuteSpNonQuery("sp_Product_UpdateWithAccessory", parameters);
        }
        #endregion

        #region Delete kèm bảng con
        public int DeleteProduct(int productId)
        {
            var parameters = new[]
            {
                new SqlParameter("@Product_Id", SqlDbType.Int) { Value = productId }
            };
            return ExecuteSpNonQuery("sp_Product_Delete", parameters);
        }
        #endregion

        // Lấy chi tiết chung
        public DataTable GetAllProductDetails()
            => ExecuteSp("sp_Product_GetAllDetails");
    }
}
