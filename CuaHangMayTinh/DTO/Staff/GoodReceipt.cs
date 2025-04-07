using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Staff
{
    public class GoodsReceipt
    {
        public int GoodsReceipt_Id { get; set; }
        public int Employee_Id { get; set; }
        public int Details_Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime GoodsReceiptDate { get; set; }
    }
}
