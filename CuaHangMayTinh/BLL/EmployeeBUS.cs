using CuaHangMayTinh.DAL;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Forms;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.DTO.Admin;
using System.Collections.Generic;
using System.Linq;
using CuaHangMayTinh.DTO.Staff;

namespace CuaHangMayTinh.BUS
{
    public class EmployeeBUS
    {
        private readonly EmployeeDAO _employeeDAO = new EmployeeDAO();
        // private readonly AccountDAO _accountDAO = new AccountDAO();

        public DataTable GetAllEmployees()
        {
            try
            {
                return _employeeDAO.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tải danh sách nhân viên", ex);
            }
        }
        public DataTable GetEmployeeById(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("ID nhân viên không hợp lệ");

            try
            {
                return _employeeDAO.GetById(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin nhân viên", ex);
            }
        }

        public Employee GetEmployeeByName(string name)
        {
            try
            {
                DataTable dt = _employeeDAO.GetByName(name);
                if (dt.Rows.Count == 0)
                    return null;
                return new Employee
                {
                    Employee_Id = Convert.ToInt32(dt.Rows[0]["Employee_Id"]),
                    employeeName = dt.Rows[0]["EmployeeName"].ToString(),
                    age = Convert.ToInt32(dt.Rows[0]["Age"]),
                    phoneNumber = dt.Rows[0]["PhoneNumber"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin nhân viên", ex);
            }
        }

        public int InsertEmployee(string name, int age, string phone)
        {
            ValidateEmployeeData(name, age, phone);

            try
            {
                return _employeeDAO.Insert(name, age, phone);
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm nhân viên thất bại", ex);
            }
        }

        public int UpdateEmployee(int employeeId, string name, int age, string phone)
        {
            if (employeeId <= 0)
                throw new ArgumentException("ID nhân viên không hợp lệ");

            ValidateEmployeeData(name, age, phone);

            // Kiểm tra tồn tại
            var existing = _employeeDAO.GetById(employeeId);
            if (existing.Rows.Count == 0)
                throw new ArgumentException("Nhân viên không tồn tại");

            try
            {
                return _employeeDAO.Update(employeeId, name, age, phone);
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật nhân viên thất bại", ex);
            }
        }

        public int DeleteEmployee(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("ID nhân viên không hợp lệ");
            // if (HasRelatedTransactions(employeeId))
            //     throw new InvalidOperationException("Không thể xóa nhân viên đã có giao dịch");
            try
            {
                return _employeeDAO.Delete(employeeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa nhân viên thất bại", ex);
            }
        }

        public DataTable SearchEmployees(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Từ khóa tìm kiếm không hợp lệ");

            try
            {

                return _employeeDAO.Search(keyword);
            }
            catch (Exception ex)
            {
                throw new Exception("Tìm kiếm nhân viên thất bại", ex);
            }
        }

        private void ValidateEmployeeData(string name, int age, string phone)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(name))
                errors.AppendLine("Tên nhân viên không được trống");

            if (age < 18 || age > 60)
                errors.AppendLine("Tuổi không hợp lệ (18-60)");

            if (!IsValidPhoneNumber(phone))
                errors.AppendLine("Số điện thoại không hợp lệ (10-11 số)");

            if (errors.Length > 0)
                throw new ArgumentException(errors.ToString());
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^[0-9]{10,11}$");
        }

        // private bool HasRelatedTransactions(int employeeId)
        // {
        //     //  Kiểm tra các bảng Receipt, Goods_Receipt có liên quan tới employeeId
        //     // var receiptDao = new ReceiptDAO();
        //     // return receiptDao.GetByEmployee(employeeId).Rows.Count > 0;
        //     return false;
        // }
        public List<EmployeeView> GetEmployeeViews()
        {
            DataTable dt = _employeeDAO.GetEmployeeAccount();
            return dt.AsEnumerable()
                     .Select(r => new EmployeeView
                     {
                         Employee_Id = r.Field<int>("Employee_Id"),
                         EmployeeName = r.Field<string>("EmployeeName"),
                         Age = r.Field<int>("Age"),
                         PhoneNumber = r.Field<string>("PhoneNumber"),
                         Username = r.Field<string>("Username"),
                         Role = r.Field<string>("Role")
                     })
                     .ToList();
        }

        public List<CBBItems> GetEmployeeCBBItems()
        {
            
            var dt = _employeeDAO.GetIdName();
            return dt.AsEnumerable()
                     .Select(r => new CBBItems
                     {
                         Text = r.Field<string>("employeeName"),
                         Value = r.Field<int>("Employee_Id")
                     })
                     .ToList();
        }
    }
}
