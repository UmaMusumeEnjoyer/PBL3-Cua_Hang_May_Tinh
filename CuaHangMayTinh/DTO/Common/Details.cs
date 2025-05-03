using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Common
{
    public class Details
    {
        public int Details_Id { get; set; }
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

        // khóa ngoại, chỉ một trong hai có giá trị
        public int? Receipt_Id { get; set; }    // Nullable
        public int? GoodsReceipt_Id { get; set; }   // Nullable
        
        public string AdjustmentType { get; set; } // "ADJUST", "CANCEL", "ORIGINAL"
        public int? OriginalDetailId { get; set; } 
    }
}
