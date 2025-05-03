using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class DetailsDAO : DbConnect
    {
        #region Read Operations
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
            const string sql = @"SELECT d.*, p.productName
                                 FROM Details d
                                 INNER JOIN Product p ON d.Product_Id = p.Product_Id
                                WHERE d.Receipt_Id = @ReceiptId";
            return GetData(sql, new[] { new SqlParameter("@ReceiptId", receiptId) });
        }

        public DataTable GetDetailsByGoodsReceipt(int goodsReceiptId)
        {
            const string sql = @"SELECT d.*, p.productName
                                 FROM Details d
                                 INNER JOIN Product p ON d.Product_Id = p.Product_Id
                                WHERE d.GoodsReceipt_Id = @GoodsReceiptId";
            return GetData(sql, new[] { new SqlParameter("@GoodsReceiptId", goodsReceiptId) });
        }
        #endregion

        #region Insert/Update Operations
        public int InsertDetail(int productId, int quantity, decimal productPrice,
                                int? receiptId, int? goodsReceiptId,
                                string adjustmentType = "ORIGINAL", int? originalDetailId = null)
        {
            int rows = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Validate stock for sales
                if (adjustmentType == "ORIGINAL" && receiptId.HasValue)
                {
                    int stock = GetCurrentStock(productId);
                    if (stock < quantity)
                        throw new InvalidOperationException("Không đủ tồn kho cho sản phẩm này.");
                }

                // Insert detail
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tran;
                    cmd.CommandText = @"INSERT INTO Details 
                                            (Product_Id, quantity, productPrice, Receipt_Id, 
                                             GoodsReceipt_Id, AdjustmentType, OriginalDetailId)
                                          VALUES 
                                            (@ProductId, @Quantity, @Price, @ReceiptId, 
                                             @GoodsReceiptId, @AdjustmentType, @OriginalDetailId)";
                    cmd.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@ProductId", productId),
                        new SqlParameter("@Quantity", quantity),
                        new SqlParameter("@Price", productPrice),
                        new SqlParameter("@ReceiptId", (object)receiptId ?? DBNull.Value),
                        new SqlParameter("@GoodsReceiptId", (object)goodsReceiptId ?? DBNull.Value),
                        new SqlParameter("@AdjustmentType", adjustmentType),
                        new SqlParameter("@OriginalDetailId", (object)originalDetailId ?? DBNull.Value)
                    });
                    rows = cmd.ExecuteNonQuery();
                }
                // Update stock
                UpdateStock(productId, quantity, adjustmentType, receiptId, goodsReceiptId, tran);
            });
            return rows;
        }
        #endregion

        #region Business Logic
        public int CancelReceipt(int receiptId)
        {
            int result = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Mark receipt as canceled
                ExecuteNonQuery("UPDATE Receipt SET IsCanceled = 1 WHERE Receipt_Id = @Id",
                    new[] { new SqlParameter("@Id", receiptId) });

                // Get all details in the receipt
                DataTable dt = GetDetailsByReceipt(receiptId);
                foreach (DataRow row in dt.Rows)
                {
                    int productId = Convert.ToInt32(row["Product_Id"]);
                    int quantity = Convert.ToInt32(row["quantity"]);
                    decimal price = Convert.ToDecimal(row["productPrice"]);

                    // Insert cancellation entry
                    InsertDetail(productId, quantity, price, receiptId, null, "CANCEL");
                }
                result = 1;
            });
            return result;
        }

        public int AdjustDetail(int detailId, int newQuantity)
        {
            int result = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Get original detail
                DataTable dt = GetData("SELECT Product_Id, quantity, productPrice, Receipt_Id, GoodsReceipt_Id FROM Details WHERE Details_Id = @Id",
                    new[] { new SqlParameter("@Id", detailId) });

                if (dt.Rows.Count == 0) return;

                DataRow row = dt.Rows[0];
                int productId = Convert.ToInt32(row["Product_Id"]);
                int oldQty = Convert.ToInt32(row["quantity"]);
                decimal price = Convert.ToDecimal(row["productPrice"]);
                int? receiptId = row.IsNull("Receipt_Id") ? null : (int?)row["Receipt_Id"];
                int? goodsReceiptId = row.IsNull("GoodsReceipt_Id") ? null : (int?)row["GoodsReceipt_Id"];

                // Calculate adjustment
                int currentQty = GetCurrentQuantity(detailId);
                int diff = newQuantity - currentQty;
                if (diff == 0) return;

                // Insert adjustment
                result = InsertDetail(productId, diff, price, receiptId, goodsReceiptId, "ADJUST", detailId);
            });
            return result;
        }

        public int AdjustDetailFull(int detailId, int newProductId, int newQuantity)
        {
            int result = 0;
            ExecuteTransaction((conn, tran) =>
            {
                // Get original detail
                DataTable dt = GetData("SELECT Product_Id, quantity, productPrice, Receipt_Id, GoodsReceipt_Id FROM Details WHERE Details_Id = @Id",
                    new[] { new SqlParameter("@Id", detailId) });

                if (dt.Rows.Count == 0) return;

                DataRow row = dt.Rows[0];
                int oldProductId = Convert.ToInt32(row["Product_Id"]);
                int oldQty = Convert.ToInt32(row["quantity"]);
                decimal price = Convert.ToDecimal(row["productPrice"]);
                int? receiptId = row.IsNull("Receipt_Id") ? null : (int?)row["Receipt_Id"];
                int? goodsReceiptId = row.IsNull("GoodsReceipt_Id") ? null : (int?)row["GoodsReceipt_Id"];

                // Cancel old entry
                InsertDetail(oldProductId, oldQty, price, receiptId, goodsReceiptId, "CANCEL", detailId);

                // Insert new entry
                InsertDetail(newProductId, newQuantity, price, receiptId, goodsReceiptId, "ORIGINAL", detailId);

                result = 1;
            });
            return result;
        }
        #endregion

        #region Helpers
        private int GetCurrentStock(int productId)
        {
            const string sql = "SELECT stockQuantity FROM Product WHERE Product_Id = @ProductId";
            return Convert.ToInt32(ExecuteScalar(sql, new[] { new SqlParameter("@ProductId", productId) }));
        }

        private int GetCurrentQuantity(int detailId)
        {
            const string sql = @"SELECT SUM(quantity) FROM Details 
                               WHERE Details_Id = @DetailId 
                                  OR OriginalDetailId = @DetailId";
            return Convert.ToInt32(ExecuteScalar(sql, new[] { new SqlParameter("@DetailId", detailId) }));
        }

        private void UpdateStock(int productId, int quantity, string adjustmentType, 
                                int? receiptId, int? goodsReceiptId, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand())
            {
                cmd.Transaction = tran;
                cmd.Connection = tran.Connection;

                if (adjustmentType == "ORIGINAL")
                {
                    cmd.CommandText = receiptId.HasValue ? 
                        "UPDATE Product SET stockQuantity -= @Qty WHERE Product_Id = @ProductId" :
                        "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId";
                }
                else if (adjustmentType == "CANCEL")
                {
                    cmd.CommandText = "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId";
                }
                else if (adjustmentType == "ADJUST")
                {
                    cmd.CommandText = receiptId.HasValue ? 
                        "UPDATE Product SET stockQuantity -= @Qty WHERE Product_Id = @ProductId" :
                        "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId";
                }

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@Qty", quantity),
                    new SqlParameter("@ProductId", productId)
                });
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}