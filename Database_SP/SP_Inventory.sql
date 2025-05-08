CREATE PROCEDURE sp_Inventory_GetRaw
AS
BEGIN
    SET NOCOUNT ON;

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
        s.supplierName      AS SupplierName,
        s.phoneNumber       AS SupplierPhone,
        MAX(gr.goodsReceiptDate) AS LastGoodsReceiptDate
    FROM Product p
    LEFT JOIN Laptop      l   ON p.Product_Id = l.Product_Id
    LEFT JOIN PC          pc  ON p.Product_Id = pc.Product_Id
    LEFT JOIN Accessories a ON p.Product_Id = a.Product_Id
    LEFT JOIN Supplier    s   ON p.Supplier_Id = s.Supplier_Id
    LEFT JOIN Details     d   ON p.Product_Id = d.Product_Id
                             AND d.GoodsReceipt_Id IS NOT NULL
    LEFT JOIN Goods_Receipt gr ON d.GoodsReceipt_Id = gr.GoodsReceipt_Id
    WHERE p.IsDeleted = 0
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
        s.phoneNumber;
END
GO
