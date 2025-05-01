using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    class InventoryDAO : DbConnect
    {
        public DataTable GetInventoryRaw()
        {
            string sql = @"
            SELECT 
                p.Product_Id,
                p.productName       AS ProductName,
                CASE 
                    WHEN l.Product_Id IS NOT NULL THEN 'Laptop'
                    WHEN pc.Product_Id IS NOT NULL THEN 'PC'
                    ELSE 'Accessories' 
                END AS Category,
                COALESCE(l.specification, pc.specification, a.overview) AS Specification,
                COALESCE(l.colour, '') AS Colour,
                p.price             AS Price,
                p.stockQuantity     AS StockQuantity,
                s.supplierName      AS SupplierName,  -- Thêm tên nhà cung cấp
                s.phoneNumber       AS SupplierPhone,
                MAX(gr.goodsReceiptDate) AS LastGoodsReceiptDate  -- Đổi tên cho rõ nghĩa
            FROM Product p
            LEFT JOIN Laptop      l   ON p.Product_Id = l.Product_Id
            LEFT JOIN PC          pc  ON p.Product_Id = pc.Product_Id
            LEFT JOIN Accessories a   ON p.Product_Id = a.Product_Id
            LEFT JOIN Supplier    s   ON p.Supplier_Id = s.Supplier_Id
            LEFT JOIN Details     d   ON p.Product_Id = d.Product_Id AND d.GoodsReceipt_Id IS NOT NULL -- Chỉ lấy chi tiết nhập hàng
            LEFT JOIN Goods_Receipt gr ON d.GoodsReceipt_Id = gr.GoodsReceipt_Id  -- Sửa điều kiện JOIN
            GROUP BY 
                p.Product_Id,
                p.productName,
                CASE 
                    WHEN l.Product_Id IS NOT NULL THEN 'Laptop'
                    WHEN pc.Product_Id IS NOT NULL THEN 'PC'
                    ELSE 'Accessories' 
                END,
                COALESCE(l.specification, pc.specification, a.overview),
                COALESCE(l.colour, ''),
                p.price,
                p.stockQuantity,
                s.supplierName,
                s.phoneNumber;";
            return GetData(sql);
        }
    }
}
