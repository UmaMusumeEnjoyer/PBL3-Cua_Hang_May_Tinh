using CuaHangMayTinh.DAL;
using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Forms;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.BUS
{
    public class EmployeeBUS
    {
        private readonly EmployeeDAO _employeeDAO = new EmployeeDAO();

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
        //public DataTable GetEmployeeById(int employeeId)
        //{
        //    if (employeeId <= 0)
        //        throw new ArgumentException("ID nhân viên không hợp lệ");

        //    try
        //    {
        //        return _employeeDAO.GetById(employeeId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Lỗi khi lấy thông tin nhân viên", ex);
        //    }
        //}

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
                string sql = @"SELECT * FROM Employee
                               WHERE employeeName LIKE @Keyword
                                  OR phoneNumber LIKE @Keyword";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Keyword", $"%{keyword}%")
                };

                return _employeeDAO.GetData(sql, parameters);
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

        public bool AddAccount(string username, string password, int employeeId)
        {
            string connectionString = "your_connection_string_here"; // sửa lại chuỗi kết nối của bạn

            string query = @"
        INSERT INTO Account (username, password, Employee_Id, role)
        VALUES (@username, @password, @employeeId, N'Nhân viên')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Gợi ý: hash mật khẩu trong thực tế
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm tài khoản: " + ex.Message);
                    return false;
                }
            }
        }



    }
}
