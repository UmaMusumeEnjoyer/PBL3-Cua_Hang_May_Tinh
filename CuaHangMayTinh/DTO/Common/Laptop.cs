using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Common
{
    public class Laptop
    {
        public int ProductId { get; set; }
        public string LaptopName { get; set; }
        public decimal Weight { get; set; }
        public decimal ScreenSize { get; set; }
        public string Specification { get; set; }
        public string Colour { get; set; }
        public int SupplierId { get; set; }

        public Laptop()
        {
        }

        public Laptop(int productId, string laptopName, decimal weight, decimal screenSize, string specification, string colour, int supplierId)
        {
            ProductId = productId;
            LaptopName = laptopName;
            Weight = weight;
            ScreenSize = screenSize;
            Specification = specification;
            Colour = colour;
            SupplierId = supplierId;
        }
    }
}
