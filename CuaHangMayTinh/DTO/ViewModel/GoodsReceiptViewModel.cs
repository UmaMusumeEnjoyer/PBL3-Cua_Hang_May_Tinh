using System;
using System.Collections.Generic;
using System.Linq;

namespace CuaHangMayTinh.DTO.ViewModel
{
    public class GoodsReceiptViewModel
    {
        public int GoodsReceipt_Id { get; set; }
        public DateTime GoodsReceiptDate { get; set; }
        public string EmployeeName { get; set; }
        public string SupplierName { get; set; }  // Thêm thông tin nhà cung cấp

        // Danh sách sản phẩm nhập
        public List<GoodsReceiptLineItem> Items { get; set; } = new List<GoodsReceiptLineItem>();

        // Tổng tiền nhập hàng (tính từ Items)
        public decimal TotalAmount => Items.Sum(item => item.LineTotal);
    }
    public class GoodsReceiptLineItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal LineTotal => Quantity * ProductPrice;
    }
}