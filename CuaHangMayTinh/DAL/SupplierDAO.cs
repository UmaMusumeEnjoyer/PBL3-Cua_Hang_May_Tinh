using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
using Microsoft.SqlServer.Server;
namespace CuaHangMayTinh.DAL
{
    public class SupplierDAO : DbConnect
    {
        public DataTable GetAll()
        {
            return GetData("SELECT * FROM Supplier");
        }
        public DataTable GetById(int id)
        {
            string sql = "SELECT * FROM Supplier WHERE Supplier_Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            return GetData(sql, parameters);
        }
        public int Insert(string name, string phone, string email, string address)
        {
            string sql = @"INSERT INTO Supplier(supplierName, phoneNumber, email, address)
                           VALUES(@Name, @Phone, @Email, @Address)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = phone },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email },
                new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address }
            };
            return ExecuteNonQuery(sql, parameters);
        }
        public int Update(int id, string name, string phone, string email, string address)
        {
            string sql = @"UPDATE Supplier SET 
                           supplierName = @Name, 
                           phoneNumber = @Phone,
                           email = @Email,
                           address = @Address
                           WHERE Supplier_Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = phone },
                new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email },
                new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address },
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };
            return ExecuteNonQuery(sql, parameters);
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM Supplier WHERE Supplier_Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            return ExecuteNonQuery(sql, parameters);
        }
        public DataTable Search(string keyword)
        {
            string sql = @"SELECT * FROM Supplier
                           WHERE supplierName LIKE @Keyword
                              OR phoneNumber  LIKE @Keyword
                              Or email  LIKE @Keyword";
            SqlParameter[] parameters = { new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" } };
            return GetData(sql, parameters);
        }
        /// <summary>
        ///  TotalValue: tổng (price * stockQuantity)
        ///  TotalStock: tổng stockQuantity
        /// </summary>
        public DataTable GetSupplierReport()
        {
            string sql = @"SELECT 
                          s.Supplier_Id,
                          s.supplierName      AS SupplierName,
                          COUNT(p.Product_Id) AS TotalProducts,
                          SUM(p.stockQuantity)      AS TotalStock,
                          SUM(p.price * p.stockQuantity) AS TotalValue
                        FROM Supplier s
                        LEFT JOIN Product p 
                          ON s.Supplier_Id = p.Supplier_Id
                        GROUP BY 
                          s.Supplier_Id, 
                          s.supplierName"" ";
            return GetData(sql);
        }
    }
}
