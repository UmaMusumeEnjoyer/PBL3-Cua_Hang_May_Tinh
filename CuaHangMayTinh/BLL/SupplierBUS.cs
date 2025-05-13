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
using CuaHangMayTinh.DTO.Admin;
using CuaHangMayTinh.DTO.Staff;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.BLL
{
    public class SupplierBUS
    {
        private readonly SupplierDAO _supplierDAO = new SupplierDAO();

        public DataTable GetAll()
        {
            try
            {
                return _supplierDAO.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách nhà cung cấp", ex);
            }
        }

        public DataTable GetById(int supplierId)
        {
            if (supplierId <= 0)
                throw new ArgumentException("ID nhà cung cấp không hợp lệ");

            try
            {
                var dt = _supplierDAO.GetById(supplierId);
                if (dt.Rows.Count == 0)
                    throw new Exception("Nhà cung cấp không tồn tại");
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin nhà cung cấp", ex);
            }
        }

        public int Insert(string supplierName, string address, string phone, string email)
        {
            try
            {
                return _supplierDAO.Insert(supplierName, address, phone, email);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm nhà cung cấp thất bại", ex);
            }
        }

        public int Update(int supplierId, string supplierName, string address, string phone, string email)
        {
            if (supplierId <= 0)
                throw new ArgumentException("ID nhà cung cấp không hợp lệ");

            try
            {
                return _supplierDAO.Update(supplierId, supplierName, address, phone, email);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật nhà cung cấp thất bại", ex);
            }
        }

        public int Delete(int supplierId)
        {
            if (supplierId <= 0)
                throw new ArgumentException("ID nhà cung cấp không hợp lệ");

            try
            {
                return _supplierDAO.Delete(supplierId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa nhà cung cấp thất bại", ex);
            }
        }

        public DataTable Search(string keyword)
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

        public List<SupplierReport> GetSupplierReports()
        {
            DataTable dt = _supplierDAO.GetSupplierReport();
            return dt.AsEnumerable()
                     .Select(r => new SupplierReport
                     {
                         Supplier_Id = r.Field<int>("Supplier_Id"),
                         SupplierName = r.Field<string>("SupplierName"),
                         TotalProducts = r.Field<int>("TotalProducts"),
                         TotalStock = r.Field<int>("TotalStock"),
                         TotalValue = r.Field<decimal>("TotalValue")
                     })
                     .ToList();
        }

        public List<CBBItems> GetSupplierCBBItems()
        {
            var dt = _supplierDAO.GetIdName();
            return dt.AsEnumerable()
                     .Select(r => new CBBItems
                     {
                         Text = r.Field<string>("supplierName"),
                         Value = r.Field<int>("Supplier_Id")
                     })
                     .ToList();
        }
    }
}
