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
        private static string connectionString = "Data Source=ADMIN-PC;Initial Catalog=PBL3;Integrated Security=True";

        public static bool KiemTraDangNhap(string username, string password)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Account WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                result = count > 0;
            }
            return result;
        }
    }

}
