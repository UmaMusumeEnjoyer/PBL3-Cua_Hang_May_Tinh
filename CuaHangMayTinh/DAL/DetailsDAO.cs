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


/*
using System;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class DetailsDAO : DbConnect
    {
        #region Constants
        private const string AdjustmentTypeOriginal = "ORIGINAL";
        private const string AdjustmentTypeCancel = "CANCEL";
        private const string AdjustmentTypeAdjust = "ADJUST";
        #endregion

        #region Core Operations
        public int InsertDetail(
            int productId,
            int quantity,
            decimal productPrice,
            int? receiptId,
            int? goodsReceiptId,
            SqlConnection conn,
            SqlTransaction tran,
            string adjustmentType = AdjustmentTypeOriginal,
            int? originalDetailId = null)
        {
            // Validate rules and ensure linkage
            ValidateAdjustmentRules(adjustmentType, originalDetailId, receiptId, goodsReceiptId);

            // Validate stock if sale
            ValidateAdjustmentStock(conn, tran, productId, quantity, receiptId);

            // Insert detail record
            InsertDetailRecord(conn, tran,
                productId, quantity, productPrice,
                receiptId, goodsReceiptId,
                adjustmentType, originalDetailId);

            // Update stock once
            UpdateStock(conn, tran, productId, quantity, adjustmentType, receiptId.HasValue);

            return 1;
        }
        #endregion

        #region Business Logic
        public int CancelReceipt(int receiptId)
        {
            return ExecuteTransactionalScope((conn, tran) =>
            {
                // Mark receipt canceled
                MarkReceiptCanceled(conn, tran, receiptId);

                // Fetch details within transaction
                var dt = GetReceiptDetails(conn, tran, receiptId);
                foreach (DataRow row in dt.Rows)
                {
                    ProcessDetailCancellation(conn, tran, row);
                }

                return 1;
            });
        }

        public int AdjustDetail(int detailId, int newQuantity)
        {
            return ExecuteTransactionalScope((conn, tran) =>
            {
                // Get existing detail info
                var (productId, currentQty, price, receiptId, goodsReceiptId) =
                    GetDetailInfo(conn, tran, detailId);

                int diff = newQuantity - currentQty;
                if (diff == 0) return 0;

                // Validate stock only
                ValidateAdjustmentStock(conn, tran, productId, diff, receiptId);

                // Insert adjustment and update stock in one go
                InsertDetail(
                    productId, diff, price,
                    receiptId, goodsReceiptId,
                    conn, tran,
                    AdjustmentTypeAdjust, detailId);

                return 1;
            });
        }
        #endregion

        #region Validation Helpers
        private void ValidateAdjustmentRules(
            string adjustmentType,
            int? originalDetailId,
            int? receiptId,
            int? goodsReceiptId)
        {
            if (adjustmentType == AdjustmentTypeOriginal)
            {
                // Must belong to exactly one document
                if (!receiptId.HasValue && !goodsReceiptId.HasValue)
                    throw new ArgumentException("ORIGINAL details must belong to a receipt or goods receipt");
                if (receiptId.HasValue && goodsReceiptId.HasValue)
                    throw new ArgumentException("Cannot have both receipt and goods receipt ID");
            }
            else // CANCEL or ADJUST
            {
                if (!originalDetailId.HasValue)
                    throw new ArgumentNullException(nameof(originalDetailId),
                        $"{adjustmentType} must reference original detail");
            }
        }

        private void ValidateAdjustmentStock(
            SqlConnection conn,
            SqlTransaction tran,
            int productId,
            int quantity,
            int? receiptId)
        {
            // Only check stock; do not mutate here
            if (receiptId.HasValue && quantity > 0)
            {
                using var cmd = new SqlCommand(
                    "SELECT stockQuantity FROM Product WHERE Product_Id = @ProductId", conn, tran);
                cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId });
                int currentStock = Convert.ToInt32(cmd.ExecuteScalar());
                if (currentStock < quantity)
                    throw new InvalidOperationException("Không đủ tồn kho để điều chỉnh");
            }
        }
        #endregion

        #region Database Operations
        private void InsertDetailRecord(
            SqlConnection conn,
            SqlTransaction tran,
            int productId,
            int quantity,
            decimal price,
            int? receiptId,
            int? goodsReceiptId,
            string adjustmentType,
            int? originalDetailId)
        {
            using var cmd = new SqlCommand(@"
                INSERT INTO Details (
                    Product_Id, quantity, productPrice,
                    Receipt_Id, GoodsReceipt_Id,
                    AdjustmentType, OriginalDetailId
                ) VALUES (
                    @ProductId, @Quantity, @Price,
                    @ReceiptId, @GoodsReceiptId,
                    @AdjustmentType, @OriginalDetailId
                )", conn, tran);

            AddDetailParameters(cmd, productId, quantity, price,
                receiptId, goodsReceiptId, adjustmentType, originalDetailId);
            cmd.ExecuteNonQuery();
        }

        private void UpdateStock(
            SqlConnection conn,
            SqlTransaction tran,
            int productId,
            int quantity,
            string adjustmentType,
            bool isSale)
        {
            string operation = adjustmentType switch
            {
                AdjustmentTypeOriginal when isSale => "stockQuantity -= @Qty",
                AdjustmentTypeOriginal => "stockQuantity += @Qty",
                AdjustmentTypeCancel => "stockQuantity += @Qty",
                AdjustmentTypeAdjust when isSale => "stockQuantity -= @Qty",
                _ => "stockQuantity += @Qty"
            };

            using var cmd = new SqlCommand(
                $@"UPDATE Product SET {operation} WHERE Product_Id = @ProductId", conn, tran);
            cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Int) { Value = quantity });
            cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId });
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Parameter Handling
        private void AddDetailParameters(
            SqlCommand cmd,
            int productId,
            int quantity,
            decimal price,
            int? receiptId,
            int? goodsReceiptId,
            string adjustmentType,
            int? originalDetailId)
        {
            cmd.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.Int) { Value = productId });
            cmd.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = quantity });
            cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Precision = 10, Scale = 2, Value = price });
            cmd.Parameters.Add(new SqlParameter("@ReceiptId", SqlDbType.Int) { Value = receiptId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@GoodsReceiptId", SqlDbType.Int) { Value = goodsReceiptId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@AdjustmentType", SqlDbType.NVarChar, 20) { Value = adjustmentType });
            cmd.Parameters.Add(new SqlParameter("@OriginalDetailId", SqlDbType.Int) { Value = originalDetailId ?? (object)DBNull.Value });
        }
        #endregion

        #region Transaction Management
        private T ExecuteTransactionalScope<T>(Func<SqlConnection, SqlTransaction, T> action)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var tran = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                var result = action(conn, tran);
                tran.Commit();
                return result;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
        #endregion

        #region Query Helpers
        private DataTable GetReceiptDetails(
            SqlConnection conn,
            SqlTransaction tran,
            int receiptId)
        {
            using var cmd = new SqlCommand(@"
                SELECT Details_Id, Product_Id, quantity, productPrice
                FROM Details
                WHERE Receipt_Id = @ReceiptId", conn, tran);
            cmd.Parameters.Add(new SqlParameter("@ReceiptId", SqlDbType.Int) { Value = receiptId });

            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private (int productId, int quantity, decimal price,
            int? receiptId, int? goodsReceiptId) GetDetailInfo(
            SqlConnection conn,
            SqlTransaction tran,
            int detailId)
        {
            using var cmd = new SqlCommand(@"
                SELECT Product_Id, quantity, productPrice,
                       Receipt_Id, GoodsReceipt_Id
                FROM Details
                WHERE Details_Id = @Id", conn, tran);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = detailId });

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new ArgumentException("Detail not found");

            return (
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetDecimal(2),
                reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3),
                reader.IsDBNull(4) ? null : (int?)reader.GetInt32(4)
            );
        }
        #endregion

        #region Business Helpers
        private void MarkReceiptCanceled(
            SqlConnection conn,
            SqlTransaction tran,
            int receiptId)
        {
            using var cmd = new SqlCommand(@"
                UPDATE Receipt
                SET IsCanceled = 1
                WHERE Receipt_Id = @Id", conn, tran);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = receiptId });
            cmd.ExecuteNonQuery();
        }

        private void ProcessDetailCancellation(
            SqlConnection conn,
            SqlTransaction tran,
            DataRow row)
        {
            InsertDetail(
                productId: Convert.ToInt32(row["Product_Id"]),
                quantity: Convert.ToInt32(row["quantity"]),
                productPrice: Convert.ToDecimal(row["productPrice"]),
                receiptId: null,
                goodsReceiptId: null,
                conn: conn,
                tran: tran,
                adjustmentType: AdjustmentTypeCancel,
                originalDetailId: Convert.ToInt32(row["Details_Id"])
            );
        }
        #endregion
    }
}

*/