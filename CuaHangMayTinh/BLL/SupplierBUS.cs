using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
using System.Text.RegularExpressions;

namespace CuaHangMayTinh.BLL
{
    public class SupplierBUS
    {
        private readonly SupplierDAO _supplierDAO = new SupplierDAO();
        public DataTable GetAllSupplier()
        {
            try { return _supplierDAO.GetAll(); }
            catch (Exception ex) { throw new Exception("Lỗi khi tải danh sách nhà cung cấp", ex); }
        }
        public DataTable GetSupplierById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID nhà cung cấp phải lớn hơn 0.");
            try { return _supplierDAO.GetById(id); }
            catch (Exception ex) { throw new Exception("Lỗi khi tải danh sách nhà cung cấp theo Id", ex); }
        }
        public int InsertSupplier(string name,string phone ,string email, string address)
        {
            ValidateSupplierData(name, phone, email);
            try
            {
                return _supplierDAO.Insert(name, phone, email, address);
            }   catch(Exception ex)
            {
                throw new Exception("Thêm nhà cung cấp thất bại", ex);
            }
        }
        public int UpdateSupplier(int id, string name, string phone, string email, string address)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID nhà cung cấp phải lớn hơn 0.");

            ValidateSupplierData(name, phone, email);
            try { return _supplierDAO.Update(id, name, phone, email, address); }
            catch(Exception ex) { throw new Exception("Lỗi khi cập nhật nhà cung cấp", ex); }
        }
        public int Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "ID nhà cung cấp phải lớn hơn 0.");
            try
            {
                return _supplierDAO.Delete(id);
            }
            catch(Exception ex)
            {
                throw new Exception("Lỗi khi xoá nhà cung cấp", ex);
            }
        }

        public DataTable SearchSupplier(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");

            try
            {
                return _supplierDAO.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm nhà cung cấp thất bại", ex);
            }
        }
        private void ValidateSupplierData(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên nhà cung cấp không được để trống.", nameof(name));

            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, "^\\d{10,15}$"))
                throw new ArgumentException("Số điện thoại phải là dãy số dài từ 10 đến 15 chữ số.", nameof(phone));

            if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email,
                    "^[\\w-+.]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                throw new ArgumentException("Email không hợp lệ.", nameof(email));
        }
    }
}
