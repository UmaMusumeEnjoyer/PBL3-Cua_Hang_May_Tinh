using System;
using System.Data;
using System.Data.SqlClient;


namespace CuaHangMayTinh.DAL
{
    public class DbConnect
    {
        protected readonly string _connectionString;

        public DbConnect()
        {
            _connectionString = "Data Source=LAPTOP-TF3R4DSP\\MSSQLSERVER01;Initial Catalog=PBL3;Integrated Security=True";
        }
        

        public DataTable GetData(string sql, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetData Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
            return dt;
        }

        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExecuteNonQuery Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
        }

        public object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExecuteScalar Error] {ex.Message}");
                throw new DataException("Database operation failed", ex);
            }
        }

        public void ExecuteTransaction(Action<SqlConnection, SqlTransaction> action)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        action(conn, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        protected DataTable ExecuteSp(string sp, SqlParameter[] parameters = null)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sp, conn))
                {
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null) cmd.Parameters.AddRange(parameters);
                        
                        conn.Open();
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        protected int ExecuteSpNonQuery(string sp, SqlParameter[] parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        protected object ExecuteSpScalar(string sp, SqlParameter[] parameters = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
