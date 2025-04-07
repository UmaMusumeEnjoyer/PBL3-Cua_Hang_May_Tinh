using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.ViewModel
{
    public class ReceiptDetailView
    {
        public int Receipt_Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
    }

}
