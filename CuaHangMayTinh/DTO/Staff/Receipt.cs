using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.DTO.Staff
{
    public class Receipt
    {
        public int Receipt_Id { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Customer_Id { get; set; }
        public int Employee_Id { get; set; }

        // quan hệ 1–n: một hoá đơn có nhiều details
        public List<Details> Details { get; set; } = new List<Details>();
        public decimal? TotalAmount { get; set; }
    }
}
