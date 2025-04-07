using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Admin
{
    public class SupplierReport
    {
        public int Supplier_Id { get; set; }
        public string SupplierName { get; set; }
        public int TotalProducts { get; set; }
        public int TotalStock { get; set; }
        public decimal TotalValue { get; set; } // Tổng giá trị hàng tồn của NCC
    }

}
