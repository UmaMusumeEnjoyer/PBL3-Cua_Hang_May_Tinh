using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DTO.Staff;
using System.Data.SqlClient;
using System.Data;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.DAL
{
    public class ChartDAO : DbConnect
    {
        public List<ChartData> GetMonthlyRevenue(int year)
        {
            var list = new List<ChartData>();
            for (int m = 1; m <= 12; m++)
                list.Add(new ChartData { Label = $"Tháng {m}", Value = 0 });

            string sql = @"
                SELECT MONTH(r.receiptDate) AS Month,
                       SUM(d.quantity * d.productPrice) AS TotalRevenue
                FROM Receipt r
                JOIN Details d ON r.Receipt_Id = d.Receipt_Id
                WHERE r.IsCanceled = 0 AND YEAR(r.receiptDate) = @year
                GROUP BY MONTH(r.receiptDate)
                ORDER BY Month";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@year", year)
            };

            DataTable dt = GetData(sql, parameters);

            foreach (DataRow row in dt.Rows)
            {
                int month = Convert.ToInt32(row["Month"]);
                decimal revenue = Convert.ToDecimal(row["TotalRevenue"]);
                list[month - 1].Value = revenue;
            }

            return list;
        }
    }
}
