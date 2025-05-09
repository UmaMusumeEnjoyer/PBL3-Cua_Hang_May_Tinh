﻿using System;
using System.Collections.Generic;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.DTO.Staff
{
    public class GoodsReceipt
    {
        public int      GoodsReceipt_Id   { get; set; }
        public DateTime GoodsReceiptDate  { get; set; }
        public int      Employee_Id       { get; set; }
        public bool IsCanceled { get; set; }

        // Quan hệ 1–n: một GoodsReceipt có nhiều Details
        public List<Details> Details { get; set; } = new List<Details>();

        public decimal? TotalAmount { get; set; }
    }
}