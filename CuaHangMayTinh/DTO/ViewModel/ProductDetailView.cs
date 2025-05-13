using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.ViewModel
{
    public class ProductDetailView
    {
        public int Product_Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Specification { get; set; }
        public string Colour { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public string SupplierEmail { get; set; } 
        public DateTime? LastReceiptDate { get; set; }
        public string Overview { get; set; } // For accessories
        public decimal? Weight { get; set; } // For laptops
        public decimal? ScreenSize { get; set; } // For laptops
        public bool IsDeleted { get; set; }
    }
}
