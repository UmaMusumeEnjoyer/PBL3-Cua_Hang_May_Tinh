using System;
using System.Data;
using System.Text;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
namespace CuaHangMayTinh.DAL
{
    public class CustomerDAO
    {
        private readonly DbConnect _db = new DbConnect();

        public DataTable GetAll()
        {
            return _db.GetData("Select * from Customer");
        }

        public DataTable GetById(int id)
        {
            return _db.GetData($"Select * from Customer where Customer_Id = {id}");
        }

        public int Insert(string name, string phone, string email, string address)
        {
            var sql =  $@"
                INSERT INTO Customer(customerName, phoneNumber, email, address)
                VALUES(N'{name}', N'{phone}', N'{email}', N'{address}')";
            return _db.ExecuteNonQuery(sql);
        }
        public int Update(int id, string name, string phone, string email, string address)
        {
            var sql = $@"
                UPDATE Customer
                SET customerName = N'{name}',
                    phoneNumber  = N'{phone}',
                    email        = N'{email}',
                    address      = N'{address}'
                WHERE Customer_Id = {id}";
            return _db.ExecuteNonQuery(sql);
        }
        public int Delete(int id)
        {
            return _db.ExecuteNonQuery($"DELETE FROM Customer WHERE Customer_Id = {id}");
        }
    }
}