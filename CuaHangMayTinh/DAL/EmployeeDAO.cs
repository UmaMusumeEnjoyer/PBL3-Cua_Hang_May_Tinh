using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO; 

namespace CuaHangMayTinh.DAL
{
    public class EmployeeDAO : DbConnect
    {
        // private readonly DbConnect _db = new DbConnect();
        #region Read
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM Employee"; 
            return GetData(sql);
        }

        public DataTable GetById(int id)
        {
            string sql = "SELECT * FROM Employee WHERE Employee_Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            return GetData(sql, parameters); // Cần thêm overload GetData hỗ trợ parameters
        }

        public DataTable GetByName(string name)
        {
            string sql = "SELECT * FROM Employee WHERE employeeName = @Name";
            SqlParameter[] parameters = { new SqlParameter("@Name", name) };
            return GetData(sql, parameters);
        }
        #endregion

        #region CUD

        public int Insert(string name, int age, string phone)
        {
            string sql = @"INSERT INTO Employee(employeeName, age, phoneNumber) 
                       VALUES(@Name, @Age, @Phone)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                new SqlParameter("@Age", SqlDbType.Int) { Value = age },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = phone }
            };

            return ExecuteNonQuery(sql, parameters);
        }
        public int Update(int id, string name, int age, string phone)
        {
            string sql = @"UPDATE Employee SET 
                       employeeName = @Name, 
                       age = @Age, 
                       phoneNumber = @Phone 
                       WHERE Employee_Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                new SqlParameter("@Age", SqlDbType.Int) { Value = age },
                new SqlParameter("@Phone", SqlDbType.NVarChar) { Value = phone },
                new SqlParameter("@Id", SqlDbType.Int) { Value = id }
            };

            return ExecuteNonQuery(sql, parameters);
        }
        public int Delete(int id)
        {
            string sql = "DELETE FROM Employee WHERE Employee_Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            return ExecuteNonQuery(sql, parameters);
        }
        #endregion

        public DataTable Search(string keyword)
        {
            string sql = @"SELECT * FROM Employee
                           WHERE employeeName LIKE @Keyword
                              OR phoneNumber  LIKE @Keyword";
            SqlParameter[] parameters = { new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" } };
            return GetData(sql, parameters);
        }
        public DataTable GetEmployeeAccount()
        {
            string sql = @"
                        SELECT 
                          e.Employee_Id,
                          e.employeeName   AS EmployeeName,
                          e.age            AS Age,
                          e.phoneNumber    AS PhoneNumber,
                          a.username       AS Username,
                          a.role           AS Role
                        FROM Employee e
                        LEFT JOIN Account a 
                          ON e.Employee_Id = a.Employee_Id";
            return GetData(sql);
        }

        #region Combobox
        public DataTable GetIdName()
        {
            const string sql = @"
        SELECT 
            Employee_Id, 
            employeeName 
        FROM Employee ";
                    
 //       WHERE IsDeleted = 0";    // flag xoá mềm
            return GetData(sql);
        }
        #endregion
    }
}