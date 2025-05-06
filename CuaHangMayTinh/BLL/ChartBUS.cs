using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.BLL
{
    public class ChartBUS
    {
        private readonly ChartDAO _chartDAO = new ChartDAO();
        public ChartBUS()
        {
            _chartDAO = new ChartDAO();
        }
        public List<ChartData> GetMonthlyRevenue(int year)
        {
            return _chartDAO.GetMonthlyRevenue(year);
        }
    }
}
