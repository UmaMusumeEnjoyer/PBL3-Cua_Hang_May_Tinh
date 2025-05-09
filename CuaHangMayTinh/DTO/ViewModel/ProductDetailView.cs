﻿using System;
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
    }
}
