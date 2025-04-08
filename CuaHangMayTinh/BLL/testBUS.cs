using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DAL;
using System.Data;

namespace CuaHangMayTinh.BLL
{
    public class testBUS
    {
        private testDAO testDAO = new testDAO();
        public DataTable GetAll()
        {
            return testDAO.GetAll();
        }

    }
}
