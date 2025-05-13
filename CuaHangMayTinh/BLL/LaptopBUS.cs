using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Staff;
using CuaHangMayTinh.DTO.Common;
using System.Collections.Generic;

namespace CuaHangMayTinh.BLL
{
    public class LaptopBUS
    {
        private readonly LaptopDAO _laptopDAO = new LaptopDAO();
        private readonly ProductDAO _productDAO = new ProductDAO();

        public DataTable GetAllLaptops()
        {
            try
            {
                return _laptopDAO.GetAllLaptops();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách laptop", ex);
            }
        }

        public DataTable GetLaptopById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID laptop không hợp lệ");

            try
            {
                var dt = _laptopDAO.GetLaptopById(productId);
                if (dt.Rows.Count == 0)
                    throw new Exception("Laptop không tồn tại");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin laptop", ex);
            }
        }

        public int InsertLaptop(string laptopName, decimal weight, decimal screenSize, string specification, 
            string colour, int supplierId, string productName, decimal price, int stockQuantity)
        {
            try
            {
                // Validate input data
                if (string.IsNullOrWhiteSpace(laptopName))
                    throw new ArgumentException("Tên laptop không được để trống");
                if (weight <= 0)
                    throw new ArgumentException("Cân nặng phải lớn hơn 0");
                if (screenSize <= 0)
                    throw new ArgumentException("Kích thước màn hình phải lớn hơn 0");
                if (string.IsNullOrWhiteSpace(specification))
                    throw new ArgumentException("Thông số kỹ thuật không được để trống");
                if (string.IsNullOrWhiteSpace(colour))
                    throw new ArgumentException("Màu sắc không được để trống");
                if (supplierId <= 0)
                    throw new ArgumentException("Nhà cung cấp không hợp lệ");
                if (string.IsNullOrWhiteSpace(productName))
                    throw new ArgumentException("Tên sản phẩm không được để trống");
                if (price <= 0)
                    throw new ArgumentException("Giá phải lớn hơn 0");
                if (stockQuantity < 0)
                    throw new ArgumentException("Số lượng tồn kho không được âm");

                // Chỉ gọi LaptopDAO.Insert (đã thực hiện cả hai bước insert Product và Laptop)
                int productId = _laptopDAO.Insert(laptopName, weight, screenSize, specification, colour, supplierId, productName, price, stockQuantity);
                if (productId <= 0)
                    throw new Exception("Không thể thêm sản phẩm laptop");
                return productId;
            }
            catch (ArgumentException ex)
            {
                throw ex; // Re-throw validation exceptions
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm laptop: " + ex.Message, ex);
            }
        }

        public int UpdateLaptop(int productId,
                                string laptopName, decimal weight,
                                decimal screenSize, string specification,
                                string colour, int supplierId,
                                string productName, decimal price,
                                int stockQuantity)
        {
            if (productId <= 0)
                throw new ArgumentException("ID laptop không hợp lệ");

            ValidateLaptopData(laptopName, weight, screenSize,
                                specification, colour,
                                supplierId, productName,
                                price, stockQuantity);

            try
            {
                // Cập nhật Product và Laptop
                return _laptopDAO.UpdateLaptop(productId,
                                               laptopName, weight, screenSize,
                                               specification, colour,
                                               supplierId, productName,
                                               price, stockQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật laptop thất bại", ex);
            }
        }

        public int DeleteLaptop(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID laptop không hợp lệ");

            try
            {
                return _laptopDAO.DeleteLaptop(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa laptop thất bại", ex);
            }
        }

        public DataTable SearchLaptops(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");

            try
            {
                return _laptopDAO.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm laptop thất bại", ex);
            }
        }

        private void ValidateLaptopData(string laptopName,
                                        decimal weight, decimal screenSize,
                                        string specification, string colour,
                                        int supplierId, string productName,
                                        decimal price, int stockQuantity)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(productName))
                errors.AppendLine("Tên sản phẩm không được trống");
            if (string.IsNullOrWhiteSpace(laptopName))
                errors.AppendLine("Tên laptop không được trống");
            if (supplierId <= 0)
                errors.AppendLine("ID nhà cung cấp không hợp lệ");
            if (weight <= 0)
                errors.AppendLine("Trọng lượng phải lớn hơn 0");
            if (screenSize <= 0)
                errors.AppendLine("Kích thước màn hình phải lớn hơn 0");
            if (price < 0)
                errors.AppendLine("Giá sản phẩm không được âm");
            if (stockQuantity < 0)
                errors.AppendLine("Số lượng tồn kho không được âm");
            if (string.IsNullOrWhiteSpace(specification))
                errors.AppendLine("Thông số kỹ thuật không được để trống");
            if (string.IsNullOrWhiteSpace(colour))
                errors.AppendLine("Màu sắc không được để trống");

            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }

        public List<CBBItems> GetColourItems()
        {
            var colours = _laptopDAO.GetDistinctColours();
            var list = new List<CBBItems>();
            foreach (var c in colours)
            {
                list.Add(new CBBItems { Text = c, Value = c });
            }
            return list;
        }

        
        public List<CBBItems> GetScreenSizeItems()
        {
            var sizes = _laptopDAO.GetDistinctScreenSizes();
            var list = new List<CBBItems>();
            foreach (var s in sizes)
            {
                list.Add(new CBBItems
                {
                    Text = $"{s:0.0}\"",   
                    Value = s
                });
            }
            return list;
        }
    }
}
