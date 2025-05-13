using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Common
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }

        public Product()
        {
        }

        public Product(int productId, string productName, decimal price, int quantity, int categoryId, string description, string productType)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
            CategoryId = categoryId;
            Description = description;
            ProductType = productType;
        }
    }
}
