using System;
using System.Data;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO; 

namespace CuaHangMayTinh.DAL
{
    public class EmployeeDAO
    {
        private readonly DbConnect _db = new DbConnect();

        public DataTable GetAll()
        {
            string sql = "select * from Employees";
            return _db.GetData(sql);
        }

        public DataTable GetById(int id)
        {
            string sql = "select * from Employee where Employee_Id= {id}";
            return _db.GetData(sql);
        }

        public int Insrt(string name, int age, string phone)
        {
            var insert = new StringBuilder();
            insert.Append("insert into Employee(employeeName,age, phoneNumber) ");
            insert.Append($"VALUES(N'{name}',{age},N'{phone}')");
            return _db.ExecuteNonQuery(insert.ToString());
        }
        public int Update(int id, string name, int age, string phone)
        {
            var sb = new StringBuilder();
            sb.Append($"employeeName = N'{name}', ");
            sb.Append($"age = {age}, ");
            sb.Append($"phoneNumber = N'{phone}' ");
            sb.Append($"WHERE Employee_Id = {id}");
            return _db.ExecuteNonQuery(sb.ToString());
        }
        public int Delete(int id)
        {
            string sql = $"DELETE FROM Employee WHERE Employee_Id = {id}";
            return _db.ExecuteNonQuery(sql);
        }
        
    }
}