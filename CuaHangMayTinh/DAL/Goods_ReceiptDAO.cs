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
                    AddDetailForGoodsReceipt(conn, tran, d);
                }
            });
            return newId;
        }
        public void CancelGoodsReceipt(int goodsReceiptId)
        {
            ExecuteTransaction((conn, tran) =>
            {
                // Kiểm tra và đánh dấu hủy atomic
                const string updateSql = @"
            UPDATE Goods_Receipt WITH (UPDLOCK)
            SET IsCanceled = 1 
            WHERE GoodsReceipt_Id = @gid 
              AND IsCanceled = 0";

                int rowsAffected;
                using (var cmd = new SqlCommand(updateSql, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@gid", goodsReceiptId);
                    rowsAffected = cmd.ExecuteNonQuery();
                }

                if (rowsAffected == 0)
                {
                    bool exists = CheckGoodsReceiptExists(conn, tran, goodsReceiptId);
                    if (!exists) throw new KeyNotFoundException($"GoodsReceipt {goodsReceiptId} không tồn tại.");
                    return; // Đã hủy từ trước
                }

                // Lấy chi tiết gốc
                var originals = _detailsDAO.GetDetailsByGoodsReceiptId(goodsReceiptId, conn, tran);

                foreach (var originalDetail in originals)
                {
                    var cancel = new Details
                    {
                        Product_Id = originalDetail.Product_Id,
                        Quantity = originalDetail.Quantity,
                        ProductPrice = originalDetail.ProductPrice,
                        GoodsReceipt_Id = goodsReceiptId,
                        AdjustmentType = "CANCEL",
                        OriginalDetailId = originalDetail.Details_Id
                    };
                    AddDetailForGoodsReceipt(conn, tran, cancel);
                }
            });
        }

        private bool CheckGoodsReceiptExists(SqlConnection conn, SqlTransaction tran, int goodsReceiptId)
        {
            const string sql = "SELECT COUNT(*) FROM Goods_Receipt WHERE GoodsReceipt_Id = @gid";
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@gid", goodsReceiptId);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        private void AddDetailForGoodsReceipt(SqlConnection conn, 
            SqlTransaction tran,
            Details detail)
        {
            const string insSql = @"
        INSERT INTO Details
          (Product_Id, quantity, productPrice, GoodsReceipt_Id, AdjustmentType, OriginalDetailId)
        VALUES (@pid, @qty, @price, @gid, @type, @orig)";

            using (var cmd = new SqlCommand(insSql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@pid", detail.Product_Id);
                cmd.Parameters.AddWithValue("@qty", detail.Quantity);
                cmd.Parameters.AddWithValue("@price", detail.ProductPrice);
                cmd.Parameters.AddWithValue("@gid", detail.GoodsReceipt_Id);
                cmd.Parameters.AddWithValue("@type", detail.AdjustmentType);
                cmd.Parameters.AddWithValue("@orig", detail.OriginalDetailId ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            int factor = detail.AdjustmentType == "CANCEL" ? -1 : 1;
            using (var stkCmd = new SqlCommand(
                       "UPDATE Product SET stockQuantity = stockQuantity + (@factor * @qty) WHERE Product_Id = @pid",
                       conn, tran))
            {
                stkCmd.Parameters.AddWithValue("@factor", factor);
                stkCmd.Parameters.AddWithValue("@qty", detail.Quantity);
                stkCmd.Parameters.AddWithValue("@pid", detail.Product_Id);
                stkCmd.ExecuteNonQuery();
            }
        }
    }
}