using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Common
{
    public class PC
    {
        public int ProductId { get; set; }
        public string PCName { get; set; }
        public string Specification { get; set; }
        public int SupplierId { get; set; }

        public PC()
        {
        }

        public PC(int productId, string pcName, string specification, int supplierId)
        {
            ProductId = productId;
            PCName = pcName;
            Specification = specification;
            SupplierId = supplierId;
        }
    }
}
