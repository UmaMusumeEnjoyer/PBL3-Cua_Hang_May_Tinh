using System;

namespace CuaHangMayTinh.DTO.Common
{
    public class Accessories
    {
        public int ProductId { get; set; }
        public string AccessoriesName { get; set; }
        public string Specification { get; set; }
        public int SupplierId { get; set; }

        public Accessories()
        {
        }

        public Accessories(int productId, string accessoriesName, string specification, int supplierId)
        {
            ProductId = productId;
            AccessoriesName = accessoriesName;
            Specification = specification;
            SupplierId = supplierId;
        }
    }
} 