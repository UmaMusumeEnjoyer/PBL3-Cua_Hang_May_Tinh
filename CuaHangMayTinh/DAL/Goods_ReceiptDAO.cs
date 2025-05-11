using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.DTO.Staff;

namespace CuaHangMayTinh.DAL
{
    public class Goods_ReceiptDAO : DbConnect
    {
        private readonly DetailsDAO _detailsDAO = new DetailsDAO();
        public int CreateGoodsReceipt(GoodsReceipt gr)
        {
            int newId = 0;
            var productDao = new ProductDAO();

            ExecuteTransaction((conn, tran) =>
            {
                // Thêm header và lấy ID
                const string hdrSql = @"
                    INSERT INTO Goods_Receipt (goodsReceiptDate, IsCanceled, Employee_Id)
                    OUTPUT INSERTED.GoodsReceipt_Id
                    VALUES (@date, 0, @emp)";
        
                using (var hdr = new SqlCommand(hdrSql, conn, tran))
                {
                    hdr.Parameters.AddWithValue("@date", gr.GoodsReceiptDate);
                    hdr.Parameters.AddWithValue("@emp", gr.Employee_Id);
                    newId = Convert.ToInt32(hdr.ExecuteScalar());
                }
                
                foreach (var d in gr.Details)
                {
                    if (!productDao.Exists(d.Product_Id, conn, tran))
                    {
                        throw new InvalidOperationException(
                            $"Sản phẩm ID {d.Product_Id} không tồn tại. Hãy thêm sản phẩm trước!");
                    }
                    d.GoodsReceipt_Id = newId;
                    d.AdjustmentType = "ORIGINAL";
                    AddDetailForGoodsReceipt(conn, tran, newId, d);
                }
            });
            return newId;
        }
        public void CancelGoodsReceipt(int goodsReceiptId)
        {
            ExecuteTransaction((conn, tran) =>
            {
                using (var cmd = new SqlCommand(
                    "UPDATE Goods_Receipt SET IsCanceled = 1 WHERE GoodsReceipt_Id = @gid",
                    conn, tran))
                {
                    cmd.Parameters.AddWithValue("@gid", goodsReceiptId);
                    cmd.ExecuteNonQuery();
                }
                List<Details> originals = _detailsDAO.GetDetailsByGoodsReceiptId(goodsReceiptId);
                foreach (var orig in originals)
                {
                    var cancel = new Details
                    {
                        Product_Id = orig.Product_Id,
                        Quantity = orig.Quantity,
                        ProductPrice = orig.ProductPrice,
                        GoodsReceipt_Id = goodsReceiptId,
                        AdjustmentType = "CANCEL",
                        OriginalDetailId = orig.Details_Id
                    };
                    AddDetailForGoodsReceipt(conn, tran, goodsReceiptId, cancel);
                }
            });
        }

        private void AddDetailForGoodsReceipt(
            SqlConnection conn, 
            SqlTransaction tran,
            int goodsReceiptId, 
            Details detail)
        {
            
            const string insSql = @"
                INSERT INTO Details
                  (Product_Id, quantity, productPrice,
                   GoodsReceipt_Id, AdjustmentType, OriginalDetailId)
                VALUES (@pid,@qty,@price,@gid,@type,@orig)";
            
            using (var ins = new SqlCommand(insSql, conn, tran))
            {
                ins.Parameters.AddWithValue("@pid", detail.Product_Id);
                ins.Parameters.AddWithValue("@qty", detail.Quantity);
                ins.Parameters.AddWithValue("@price", detail.ProductPrice);
                ins.Parameters.AddWithValue("@gid", detail.GoodsReceipt_Id);
                ins.Parameters.AddWithValue("@type", detail.AdjustmentType);
                ins.Parameters.AddWithValue("@orig", detail.OriginalDetailId ?? (object)DBNull.Value);
                ins.ExecuteNonQuery();
            }
            
            int factor = detail.AdjustmentType == "CANCEL" ? -1 : +1;
            using (var stk = new SqlCommand(
                "UPDATE Product SET stockQuantity = stockQuantity + @f*@qty WHERE Product_Id = @pid",
                conn, tran))
            {
                stk.Parameters.AddWithValue("@f", factor);
                stk.Parameters.AddWithValue("@qty", detail.Quantity);
                stk.Parameters.AddWithValue("@pid", detail.Product_Id);
                stk.ExecuteNonQuery();
            }
        }
    }
}