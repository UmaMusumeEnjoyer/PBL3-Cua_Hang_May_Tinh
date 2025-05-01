using System;
using System.Collections.Generic;

namespace CuaHangMayTinh.DTO.ViewModel
{
    public class ReceiptDetailView
    {
        public int Receipt_Id { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }

        // Danh sách các dòng hàng
        public List<LineItem> Items { get; set; } = new List<LineItem>();

        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalTotal { get; set; }
    }

    public class LineItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal LineTotal => Quantity * ProductPrice;
    }
}
