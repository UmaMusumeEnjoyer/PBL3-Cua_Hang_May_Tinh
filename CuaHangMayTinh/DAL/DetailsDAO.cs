using System;
using System.Data;
using System.Data.SqlClient;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.DTO.Staff;

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
            return GetData(sql, new SqlParameter[] { new SqlParameter("@ReceiptId", receiptId) });
        }

        public DataTable GetDetailsByGoodsReceipt(int goodsReceiptId)
        {
            const string sql = @"SELECT d.*, p.productName
                                 FROM Details d
                                 INNER JOIN Product p ON d.Product_Id = p.Product_Id
                                WHERE d.GoodsReceipt_Id = @GoodsReceiptId";
            return GetData(sql, new SqlParameter[] { new SqlParameter("@GoodsReceiptId", goodsReceiptId) });
        }
        #endregion

        #region Insert
        // Public entrypoint: insert original, adjust or cancel with own transaction
        public int InsertDetail(Details detail)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    int rows = InsertDetail(detail, conn, tran);
                    tran.Commit();
                    return rows;
                }
            }
        }
        // Internal overload to perform insert and stock update
        internal int InsertDetail(Details detail, SqlConnection conn, SqlTransaction tran)
        {
            // Validate foreign key
            if ((detail.Receipt_Id.HasValue && detail.GoodsReceipt_Id.HasValue) ||
                (!detail.Receipt_Id.HasValue && !detail.GoodsReceipt_Id.HasValue))
                throw new ArgumentException("Detail must have either Receipt_Id or GoodsReceipt_Id.");

            // Lock and check stock
            var cmdStock = new SqlCommand(
                "SELECT stockQuantity FROM Product WITH (UPDLOCK, ROWLOCK) WHERE Product_Id = @ProductId", conn, tran);
            cmdStock.Parameters.AddWithValue("@ProductId", detail.Product_Id);
            int stock = Convert.ToInt32(cmdStock.ExecuteScalar());
            if (detail.AdjustmentType == "ORIGINAL" && detail.Receipt_Id.HasValue && stock < detail.Quantity)
                throw new InvalidOperationException("Không đủ tồn kho cho sản phẩm này.");

            // Insert Details record
            using (var cmd = new SqlCommand(@"
                INSERT INTO Details
                    (Product_Id, quantity, productPrice,
                     Receipt_Id, GoodsReceipt_Id,
                     AdjustmentType, OriginalDetailId)
                VALUES
                    (@ProductId, @Quantity, @Price,
                     @ReceiptId, @GoodsReceiptId,
                     @AdjustmentType, @OriginalDetailId)", conn, tran))
            {
                cmd.Parameters.AddWithValue("@ProductId", detail.Product_Id);
                cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                cmd.Parameters.AddWithValue("@Price", detail.ProductPrice);
                cmd.Parameters.AddWithValue("@ReceiptId", (object)detail.Receipt_Id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GoodsReceiptId", (object)detail.GoodsReceipt_Id ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AdjustmentType", detail.AdjustmentType);
                cmd.Parameters.AddWithValue("@OriginalDetailId", (object)detail.OriginalDetailId ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
            // Determine stock update
            string sqlUpdate;
            switch (detail.AdjustmentType)
            {
                case "ORIGINAL":
                    sqlUpdate = detail.Receipt_Id.HasValue
                        ? "UPDATE Product SET stockQuantity -= @Qty WHERE Product_Id = @ProductId"
                        : "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId";
                    break;
                case "CANCEL":
                    sqlUpdate = detail.Receipt_Id.HasValue
                        ? "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId"
                        : "UPDATE Product SET stockQuantity -= @Qty WHERE Product_Id = @ProductId";
                    break;
                case "ADJUST":
                    sqlUpdate = detail.Receipt_Id.HasValue
                        ? "UPDATE Product SET stockQuantity -= @Qty WHERE Product_Id = @ProductId"
                        : "UPDATE Product SET stockQuantity += @Qty WHERE Product_Id = @ProductId";
                    break;
                default:
                    throw new ArgumentException("Invalid AdjustmentType.");
            }
            using (var cmd = new SqlCommand(sqlUpdate, conn, tran))
            {
                cmd.Parameters.AddWithValue("@Qty", detail.Quantity);
                cmd.Parameters.AddWithValue("@ProductId", detail.Product_Id);
                cmd.ExecuteNonQuery();
            }
            return 1;
        }
        #endregion
        
        /*
         // Cancel a detail: create a CANCEL adjustment equal to original quantity
        public int CancelDetail(int detailId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    // Fetch original detail
                    var dt = GetData(conn, tran,
                        "SELECT Product_Id, quantity, Receipt_Id, GoodsReceipt_Id FROM Details WHERE Details_Id = @Id",
                        new[] { new SqlParameter("@Id", detailId) });
                    if (dt.Rows.Count == 0) throw new ArgumentException("Detail not found.");
                    var row = dt.Rows[0];
                    var detail = new Detail
                    {
                        Product_Id = row.Field<int>("Product_Id"),
                        Quantity = row.Field<int>("quantity"),
                        ProductPrice = 0, // price doesn't affect stock
                        Receipt_Id = row.Field<int?>("Receipt_Id"),
                        GoodsReceipt_Id = row.Field<int?>("GoodsReceipt_Id"),
                        AdjustmentType = "CANCEL",
                        OriginalDetailId = detailId
                    };
                    int result = InsertDetail(detail, conn, tran);
                    tran.Commit();
                    return result;
                }
            }
        }

        // Adjust a detail: create an ADJUST adjustment with difference newQuantity - currentQuantity
        public int AdjustDetail(int originalDetailId, int newQuantity)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    // Compute current quantity sum of original and adjustments
                    var current = Convert.ToInt32(ExecuteScalar(conn, tran,
                        "SELECT SUM(quantity) FROM Details WHERE Details_Id = @Id OR OriginalDetailId = @Id",
                        new[] { new SqlParameter("@Id", originalDetailId) }));

                    int diff = newQuantity - current;
                    if (diff != 0)
                    {
                        // Fetch original detail attributes
                        var dt = GetData(conn, tran,
                            "SELECT Product_Id, productPrice, Receipt_Id, GoodsReceipt_Id FROM Details WHERE Details_Id = @Id",
                            new[] { new SqlParameter("@Id", originalDetailId) });
                        var row = dt.Rows[0];
                        var detail = new Detail
                        {
                            Product_Id = row.Field<int>("Product_Id"),
                            Quantity = Math.Abs(diff),
                            ProductPrice = row.Field<decimal>("productPrice"),
                            Receipt_Id = row.Field<int?>("Receipt_Id"),
                            GoodsReceipt_Id = row.Field<int?>("GoodsReceipt_Id"),
                            AdjustmentType = "ADJUST",
                            OriginalDetailId = originalDetailId
                        };
                        // If reducing, interpret negative diff as CANCEL
                        if (diff < 0) detail.AdjustmentType = "CANCEL";

                        int result = InsertDetail(detail, conn, tran);
                        tran.Commit();
                        return result;
                    }
                    return 0;
                }
            }
        }
         */
        
    }
}
