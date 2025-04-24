using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DAL;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
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

        public int InsertProduct(int supplierId, string productName, decimal price, int stockQuantity)
        {
            ValidateProductData(supplierId, productName, price, stockQuantity);

            try
            {
                return _dao.Insert(supplierId, productName, price, stockQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm sản phẩm thất bại", ex);
            }
        }

        public int UpdateProduct(int productId, int supplierId, string productName, decimal price, int stockQuantity)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");
            ValidateProductData(supplierId, productName, price, stockQuantity);

            var existing = _dao.GetProductById(productId);
            if (existing.Rows.Count == 0)
                throw new Exception("Sản phẩm không tồn tại");

            try
            {
                return _dao.UpdateProduct(productId, supplierId, productName, price, stockQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật sản phẩm thất bại", ex);
            }
        }

        public int DeleteProduct(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID sản phẩm không hợp lệ");

            try
            {
                return _dao.Delete(productId);
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
        /// <summary>
        /// Lấy toàn bộ danh sách sản phẩm dưới dạng ViewModel,
        /// để UI bind trực tiếp List<ProductDetailView>
        /// </summary>
        public List<ProductDetailView> GetAllProductDetails()
        {
            DataTable dt = _dao.GetAllProductDetails();
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
    }
}

