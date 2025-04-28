using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.ViewModel;

namespace CuaHangMayTinh.BLL
{
    public class ProductBUS
    {
        private readonly ProductDAO _dao = new ProductDAO();

        public DataTable GetAllProducts()
        {
            try
            {
                return _dao.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách sản phẩm", ex);
            }
        }

        public DataTable GetProductById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");

            try
            {
                var dt = _dao.GetProductById(productId);
                if (dt.Rows.Count == 0)
                    throw new Exception("Sản phẩm không tồn tại");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin sản phẩm", ex);
            }
        }

        public int InsertProductWithLaptop(int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            ValidateProductData(supplierId, productName, price, stockQuantity);
            ValidateLaptopData(laptopName);
            try
            {
                return _dao.InsertProductWithLaptop(supplierId, productName, price, stockQuantity,
                                                    laptopName, weight, screenSize, specification, colour);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm sản phẩm Laptop thất bại", ex);
            }
        }

        public int InsertProductWithPC(int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            ValidateProductData(supplierId, productName, price, stockQuantity);
            if (string.IsNullOrWhiteSpace(pcName))
                throw new ArgumentException("Tên PC không được để trống");
            try
            {
                return _dao.InsertProductWithPC(supplierId, productName, price, stockQuantity,
                                                 pcName, specification);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm sản phẩm PC thất bại", ex);
            }
        }

        public int InsertProductWithAccessory(int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            ValidateProductData(supplierId, productName, price, stockQuantity);
            if (string.IsNullOrWhiteSpace(accessoriesName))
                throw new ArgumentException("Tên phụ kiện không được để trống");
            try
            {
                return _dao.InsertProductWithAccessory(supplierId, productName, price, stockQuantity,
                                                        accessoriesName, overview);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm sản phẩm Phụ kiện thất bại", ex);
            }
        }

        public int UpdateProductWithLaptop(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                           string laptopName, decimal weight, decimal screenSize, string specification, string colour)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");
            ValidateProductData(supplierId, productName, price, stockQuantity);
            ValidateLaptopData(laptopName);
            try
            {
                return _dao.UpdateProductWithLaptop(productId, supplierId, productName, price, stockQuantity,
                                                    laptopName, weight, screenSize, specification, colour);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật sản phẩm Laptop thất bại", ex);
            }
        }

        public int UpdateProductWithPC(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                       string pcName, string specification)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");
            ValidateProductData(supplierId, productName, price, stockQuantity);
            if (string.IsNullOrWhiteSpace(pcName))
                throw new ArgumentException("Tên PC không được để trống");
            try
            {
                return _dao.UpdateProductWithPC(productId, supplierId, productName, price, stockQuantity,
                                                pcName, specification);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật sản phẩm PC thất bại", ex);
            }
        }

        public int UpdateProductWithAccessory(int productId, int supplierId, string productName, decimal price, int stockQuantity,
                                              string accessoriesName, string overview)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");
            ValidateProductData(supplierId, productName, price, stockQuantity);
            if (string.IsNullOrWhiteSpace(accessoriesName))
                throw new ArgumentException("Tên phụ kiện không được để trống");
            try
            {
                return _dao.UpdateProductWithAccessory(productId, supplierId, productName, price, stockQuantity,
                                                       accessoriesName, overview);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật sản phẩm Phụ kiện thất bại", ex);
            }
        }

        public int DeleteProductWithChildren(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");
            try
            {
                return _dao.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa sản phẩm thất bại", ex);
            }
        }

        public DataTable SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");
            try
            {
                return _dao.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm sản phẩm thất bại", ex);
            }
        }

        public List<ProductDetailView> GetAllProductDetails()
        {
            var dt = _dao.GetAllProductDetails();
            return dt.AsEnumerable()
                     .Select(r => new ProductDetailView
                     {
                         Product_Id = r.Field<int>("Product_Id"),
                         ProductName = r.Field<string>("productName"),
                         Category = r.Field<string>("Category"),
                         Specification = r.Field<string>("Specification"),
                         Colour = r.Field<string>("Colour"),
                         Price = r.Field<decimal>("price"),
                         StockQuantity = r.Field<int>("stockQuantity")
                     })
                     .ToList();
        }

        private void ValidateProductData(int supplierId, string productName, decimal price, int stockQuantity)
        {
            var errors = new StringBuilder();
            if (supplierId <= 0)
                errors.AppendLine("ID nhà cung cấp không hợp lệ");
            if (string.IsNullOrWhiteSpace(productName))
                errors.AppendLine("Tên sản phẩm không được để trống");
            if (price < 0)
                errors.AppendLine("Giá sản phẩm không được âm");
            if (stockQuantity < 0)
                errors.AppendLine("Số lượng tồn kho không được âm");
            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }

        private void ValidateLaptopData(string laptopName)
        {
            if (string.IsNullOrWhiteSpace(laptopName))
                throw new ArgumentException("Tên laptop không được để trống");
        }
    }
}
