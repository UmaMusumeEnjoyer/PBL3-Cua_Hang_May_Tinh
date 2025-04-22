using CuaHangMayTinh.DTO.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }

}
