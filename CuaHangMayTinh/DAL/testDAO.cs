using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DAL
{
    public class testDAO
    {
        DbConnect db = new DbConnect();
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM Laptop";
            return db.GetData(sql);
        }
        //public DataTable GetById(int id)
        //{
        //    string sql = $"SELECT * FROM Laptop WHERE Product_Id = {id}";
        //    return db.GetData(sql);
        //}
    }
}
