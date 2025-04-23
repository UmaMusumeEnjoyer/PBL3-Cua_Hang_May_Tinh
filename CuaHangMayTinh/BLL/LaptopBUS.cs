using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;

namespace CuaHangMayTinh.BUS
{
    public class LaptopBUS
    {
        private readonly LaptopDAO _laptopDAO = new LaptopDAO();

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

        public int InsertLaptop(string laptopName,
                                decimal weight, decimal screenSize,
                                string specification, string colour,
                                int supplierId, string productName,
                                decimal price, int stockQuantity)
        {
            ValidateLaptopData(laptopName, weight, screenSize,
                                specification, colour,
                                supplierId, productName,
                                price, stockQuantity);

            try
            {
                // Thực hiện insert và nhận về ProductId
                int productId = _laptopDAO.Insert(laptopName, weight, screenSize,
                                                  specification, colour,
                                                  supplierId, productName,
                                                  price, stockQuantity);
                return productId;
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm laptop thất bại", ex);
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
    }
}
