using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CuaHangMayTinh.DAL;
namespace CuaHangMayTinh.DAL
{
    public class DetailsDAO : DbConnect
    {
        public DataTable GetAllDetails()
        {
            const string sql = @"SELECT d.*, p.productName, r.receiptDate, gr.goodsReceiptDate
                                 FROM Details d
                            LEFT JOIN Product p ON d.Product_Id = p.Product_Id
                            LEFT JOIN Receipt r ON d.Receipt_Id = r.Receipt_Id
                            LEFT JOIN Goods_Receipt gr ON d.GoodsReceipt_Id = gr.GoodsReceipt_Id";
            return GetData(sql);
        }
        public DataTable GetDetailsByReceipt(int receiptId)
        {
            const string sql = @"SELECT d.*
                                 FROM Details d
                                WHERE d.Receipt_Id = @ReceiptId";
            return GetData(sql, new[] { new SqlParameter("@ReceiptId", receiptId) });
        }
        public DataTable GetDetailsByGoodsReceipt(int goodsReceiptId)
        {
            const string sql = @"SELECT d.*
                                 FROM Details d
                                WHERE d.GoodsReceipt_Id = @GoodsReceiptId";
            return GetData(sql, new[] { new SqlParameter("@GoodsReceiptId", goodsReceiptId) });
        }
        public int InsertDetail(int productId, int quantity, decimal productPrice, int? receiptId, int? goodsReceiptId)
        {
            int rows = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // 1) Insert detail
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Details
                                           (Product_Id, quantity, productPrice, Receipt_Id, GoodsReceipt_Id)
                                         VALUES
                                           (@ProductId, @Quantity, @Price, @ReceiptId, @GoodsReceiptId)";
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@ProductId", productId),
                        new SqlParameter("@Quantity", quantity),
                        new SqlParameter("@Price", productPrice),
                        new SqlParameter("@ReceiptId", (object)receiptId ?? DBNull.Value),
                        new SqlParameter("@GoodsReceiptId", (object)goodsReceiptId ?? DBNull.Value)
                    });
                    rows += cmd.ExecuteNonQuery();
                }

                // 2) Update stockQuantity in Product
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    if (receiptId.HasValue)
                    {
                        // Sale: decrease stock
                        cmd.CommandText = @"UPDATE Product
                                                SET stockQuantity = stockQuantity - @Qty
                                              WHERE Product_Id = @ProductId";
                    }
                    else if (goodsReceiptId.HasValue)
                    {
                        // Import: increase stock
                        cmd.CommandText = @"UPDATE Product
                                                SET stockQuantity = stockQuantity + @Qty
                                              WHERE Product_Id = @ProductId";
                    }
                    else
                    {
                        return;
                    }
                    cmd.Parameters.AddRange(new[] {
                        new SqlParameter("@Qty", quantity),
                        new SqlParameter("@ProductId", productId)
                    });
                    cmd.ExecuteNonQuery();
                }
            });

            return rows;
        }
    }
}