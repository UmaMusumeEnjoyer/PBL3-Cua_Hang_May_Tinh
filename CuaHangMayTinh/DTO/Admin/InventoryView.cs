using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Admin
{
    public class InventoryView
    {
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; } // "Laptop", "PC", "Accessories"
        public string Specification { get; set; } // Mô tả kỹ thuật / tổng quan
        public string Colour { get; set; } // Nếu là laptop
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
    }

}
