using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;

namespace CuaHangMayTinh.BUS
{
    public class PCBUS
    {
        private readonly PCDAO _pcDAO = new PCDAO();

        public DataTable GetAllPCs()
        {
            try
            {
                return _pcDAO.GetAllPCs();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách PC", ex);
            }
        }

        public DataTable GetPCById(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID PC không hợp lệ");

            try
            {
                var dt = _pcDAO.GetPCById(productId);
                if (dt.Rows.Count == 0)
                    throw new Exception("PC không tồn tại");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin PC", ex);
            }
        }

        public int InsertPC(string pcName, string specification,
                             int supplierId, string productName,
                             decimal price, int stockQuantity)
        {
            ValidatePCData(pcName, specification, supplierId, 
                           productName, price, stockQuantity);

            try
            {
                // Thực hiện insert và nhận về ProductId
                int productId = _pcDAO.Insert(pcName, specification,
                                              supplierId, productName,
                                              price, stockQuantity);
                return productId;
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm PC thất bại", ex);
            }
        }

        public int UpdatePC(int productId, string pcName, string specification,
                             int supplierId, string productName,
                             decimal price, int stockQuantity)
        {
            if (productId <= 0)
                throw new ArgumentException("ID PC không hợp lệ");

            ValidatePCData(pcName, specification, supplierId, 
                          productName, price, stockQuantity);

            try
            {
                // Cập nhật Product và PC
                return _pcDAO.UpdatePC(productId, pcName, specification,
                                       supplierId, productName,
                                       price, stockQuantity);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật PC thất bại", ex);
            }
        }

        public int DeletePC(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("ID PC không hợp lệ");

            try
            {
                return _pcDAO.DeletePC(productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa PC thất bại", ex);
            }
        }

        public DataTable SearchPCs(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");

            try
            {
                return _pcDAO.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm PC thất bại", ex);
            }
        }

        private void ValidatePCData(string pcName, string specification,
                                     int supplierId, string productName,
                                     decimal price, int stockQuantity)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(productName))
                errors.AppendLine("Tên sản phẩm không được trống");
            if (string.IsNullOrWhiteSpace(pcName))
                errors.AppendLine("Tên PC không được trống");
            if (supplierId <= 0)
                errors.AppendLine("ID nhà cung cấp không hợp lệ");
            if (price < 0)
                errors.AppendLine("Giá sản phẩm không được âm");
            if (stockQuantity < 0)
                errors.AppendLine("Số lượng tồn kho không được âm");
            if (string.IsNullOrWhiteSpace(specification))
                errors.AppendLine("Thông số kỹ thuật không được để trống");

            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }
    }
}