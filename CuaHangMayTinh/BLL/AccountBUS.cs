using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DTO.Admin;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Staff;
using System.Data;

namespace CuaHangMayTinh.BLL
{
    public class AccountBUS
    {
        private readonly AccountDAO _accountDAO =  new AccountDAO();



        public List<CBBItems> GetRoleCBB()
        {
            DataTable dt = _accountDAO.GetDistinctRoles();
            return dt.AsEnumerable()
                     .Select(r => new CBBItems
                     {
                         Text = r.Field<string>("role"),
                         Value = r.Field<string>("role")
                     })
                     .ToList();
        }
    }
}
