using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CuaHangMayTinh.DAL
{
    public class DbConnect
    {
        private readonly string _connectionString;

        public DbConnect()
        {
            _connectionString = "Data Source=DESKTOP-HV7IPNG;Initial Catalog=PBL3;Integrated Security=True";
        }
        // Truy vấn dữ liệu từ cơ sở dữ liệu (các câu lệnh SQL dạng SELECT) và trả về kết quả dưới dạng DataTable.
        public DataTable GetData(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    
                    conn.Open();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetData Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
            return dt;
        }

        // Thực thi các câu lệnh SQL không trả về dữ liệu (các câu lệnh SQL dạng INSERT, UPDATE, DELETE) 
        // và trả về số lượng bản ghi bị ảnh hưởng.
        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExecuteNonQuery Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
        }
        // Lấy giá trị đơn lẻ từ cơ sở dữ liệu
        public object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExecuteScalar Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
        }
    }
}
