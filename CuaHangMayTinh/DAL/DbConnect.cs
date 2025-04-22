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
        private readonly string connectionString = "Data Source=ADMIN-PC;Initial Catalog=PBL3;Integrated Security=True";
        private SqlConnection connection;
        public DbConnect()
        {
            connection = new SqlConnection(connectionString);
        }
        public DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public int ExecuteNonQuery(string sql)
        {
            int result = 0;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}
