using CuaHangMayTinh.DAL;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace CuaHangMayTinh.BUS
{
    public class CustomerBUS
    {
        private readonly CustomerDAO _customerDAO = new CustomerDAO();

        public DataTable GetAllCustomers()
        {
            try
            {
                return _customerDAO.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách khách hàng", ex);
            }
        }


        public DataTable GetCustomerById(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("ID khách hàng không hợp lệ");

            try
            {
                return _customerDAO.GetById(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin khách hàng", ex);
            }
        }

        public int InsertCustomer(string name, string phone, string email, string address)
        {
            ValidateCustomerData(name, phone, email, address);

            try
            {
                return _customerDAO.Insert(name, phone, email, address);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm khách hàng thất bại", ex);
            }
        }

        public int UpdateCustomer(int customerId, string name, string phone, string email, string address)
        {
            if (customerId <= 0)
                throw new ArgumentException("ID khách hàng không hợp lệ");

            ValidateCustomerData(name, phone, email, address);
            var existing = _customerDAO.GetById(customerId);
            if (existing.Rows.Count == 0)
                throw new ArgumentException("Khách hàng không tồn tại");

            try
            {
                return _customerDAO.Update(customerId, name, phone, email, address);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật khách hàng thất bại", ex);
            }
        }
        public int DeleteCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("ID khách hàng không hợp lệ");

            //if (HasRelatedOrders(customerId))
            //    throw new InvalidOperationException("Không thể xóa khách hàng đã có giao dịch");

            try
            {
                return _customerDAO.Delete(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa khách hàng thất bại", ex);
            }
        }
        public DataTable SearchCustomers(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");

            try
            {
                
                return _customerDAO.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm khách hàng thất bại", ex);
            }
        }

        private void ValidateCustomerData(string name, string phone, string email, string address)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(name))
                errors.AppendLine("Tên khách hàng không được trống");

            if (!IsValidPhoneNumber(phone))
                errors.AppendLine("Số điện thoại không hợp lệ (10-11 số)");

            if (!IsValidEmail(email))
                errors.AppendLine("Email không đúng định dạng");

            if (string.IsNullOrWhiteSpace(address))
                errors.AppendLine("Địa chỉ không được trống");

            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^[0-9]{10,11}$");
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        //private bool HasRelatedOrders(int customerId)
        //{
        //    // triển khai kiểm tra bảng Receipt/Goods_Receipt
        //    // var receiptDao = new ReceiptDAO();
        //    // return receiptDao.GetByCustomer(customerId).Rows.Count > 0;
        //    return false;
        //}
    }
}
