using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Staff
{
    public class Receipt
    {
        public int Receipt_Id { get; set; }
        public int Details_Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int Customer_Id { get; set; }
        public int Employee_Id { get; set; }
    }
}
