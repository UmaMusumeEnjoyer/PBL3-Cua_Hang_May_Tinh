using CuaHangMayTinh.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangMayTinh.DAL
{
    public class AccountDAO
    {
        private static readonly string connectionString = "Data Source=ADMIN-PC;Initial Catalog=PBL3;Integrated Security=True";

        private static int id;

        public static int KiemTraDangNhap(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Account WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) // Có dữ liệu
                {
                    id = Convert.ToInt32(reader["Id"]);
                    string role = reader["Role"].ToString();
                    if (role == "Admin")
                        return 1;
                    else if (role == "Nhân viên")
                        return 2;
                    else
                        return -1; // Role không hợp lệ
                }
                else
                {
                    return -1; // Không tìm thấy tài khoản
                }
            }
        }
        public static int GetId()
        {
            return id;
        }
        public static bool InsertAccount(string username, string password, int employeeId, string role = "Nhân viên")
        {
            

            string query = @"
        INSERT INTO Account (username, password, Employee_Id, role)
        VALUES (@username, @password, @employeeId, @role)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Có thể hash mật khẩu tại đây
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);
                        cmd.Parameters.AddWithValue("@role", role);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
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
