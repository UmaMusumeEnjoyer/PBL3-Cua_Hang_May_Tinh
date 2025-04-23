using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
public class CustomerDAO : DbConnect
{
    //private readonly DbConnect _db = new DbConnect();
    public DataTable GetAll()
    {
        return GetData("SELECT * FROM Customer");
    }

    public DataTable GetById(int id)
    {
        string sql = "SELECT * FROM Customer WHERE Customer_Id = @Id";
        SqlParameter[] parameters = { new SqlParameter("@Id", id) };
        return GetData(sql, parameters);
    }

    public int Insert(string name, string phone, string email, string address)
    {
        string sql = @"INSERT INTO Customer(customerName, phoneNumber, email, address)
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
        string sql = @"UPDATE Customer SET 
                       customerName = @Name, 
                       phoneNumber = @Phone,
                       email = @Email,
                       address = @Address
                       WHERE Customer_Id = @Id";

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
        string sql = "DELETE FROM Customer WHERE Customer_Id = @Id";
        SqlParameter[] parameters = { new SqlParameter("@Id", id) };
        return ExecuteNonQuery(sql, parameters);
    }
    public DataTable Search(string keyword)
    {
        string sql = @"SELECT * FROM Customer
                           WHERE customerName LIKE @Keyword
                              OR phoneNumber  LIKE @Keyword
                             Or email  LIKE @Keyword";
        SqlParameter[] parameters = { new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" } };
        return GetData(sql, parameters);
    }
}