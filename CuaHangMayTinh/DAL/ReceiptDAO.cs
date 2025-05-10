using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CuaHangMayTinh.DTO.Staff;

namespace CuaHangMayTinh.DAL
{
    public class ReceiptDAO : DbConnect
    {
        private readonly DetailsDAO _detailsDao = new DetailsDAO();
        #region Read Operations
        public DataTable GetAllReceipts()
        {
            const string sql = "SELECT * FROM Receipt";
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
        #endregion
        public int CreateReceipt(Receipt receipt)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    // Insert header
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO Receipt (receiptDate, Customer_Id, Employee_Id)
                        OUTPUT INSERTED.Receipt_Id
                        VALUES (@Date, @CustomerId, @EmployeeId)", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@Date", receipt.ReceiptDate);
                        cmd.Parameters.AddWithValue("@CustomerId", receipt.Customer_Id);
                        cmd.Parameters.AddWithValue("@EmployeeId", receipt.Employee_Id);
                        receipt.Receipt_Id = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert details
                    foreach (var d in receipt.Details)
                    {
                        var detail = new CuaHangMayTinh.DTO.Common.Details
                        {
                            Product_Id = d.Product_Id,
                            Quantity = d.Quantity,
                            ProductPrice = d.ProductPrice,
                            Receipt_Id = receipt.Receipt_Id,
                            AdjustmentType = "ORIGINAL"
                        };
                        _detailsDao.InsertDetail(detail, conn, tran);
                    }

                    // Calculate total
                    receipt.TotalAmount = GetReceiptTotal(receipt.Receipt_Id, conn, tran);
                    tran.Commit();
                }
            }
            return receipt.Receipt_Id;
        }

        public Receipt GetFullReceipt(int receiptId)
        {
            var receipt = new Receipt();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Load header
                var dt = GetData(
                    "SELECT * FROM Receipt WHERE Receipt_Id = @Id",
                    new[] { new SqlParameter("@Id", receiptId) });
                if (dt.Rows.Count == 0) return null;
                var row = dt.Rows[0];
                receipt.Receipt_Id = receiptId;
                receipt.ReceiptDate = Convert.ToDateTime(row["receiptDate"]);
                receipt.Customer_Id = Convert.ToInt32(row["Customer_Id"]);
                receipt.Employee_Id = Convert.ToInt32(row["Employee_Id"]);
                receipt.IsCanceled = Convert.ToBoolean(row["IsCanceled"]);

                // Load details
                var dtDetails = new DetailsDAO().GetDetailsByReceipt(receiptId);
                receipt.Details = dtDetails.AsEnumerable()
                    .Select(r => new CuaHangMayTinh.DTO.Common.Details
                    {
                        Details_Id = r.Field<int>("Details_Id"),
                        Product_Id = r.Field<int>("Product_Id"),
                        Quantity = r.Field<int>("quantity"),
                        ProductPrice = r.Field<decimal>("productPrice"),
                        AdjustmentType = r.Field<string>("AdjustmentType"),
                        OriginalDetailId = r.Field<int?>("OriginalDetailId"),
                        Receipt_Id = receiptId
                    }).ToList();

                receipt.TotalAmount = GetReceiptTotal(receipt.Receipt_Id, conn, null);
            }
            return receipt;
        }

        private decimal GetReceiptTotal(int receiptId, SqlConnection conn, SqlTransaction tran)
        {
            using (var cmd = new SqlCommand(@"
                SELECT SUM(
                    CASE WHEN AdjustmentType = 'CANCEL' THEN -quantity * productPrice
                         ELSE quantity * productPrice END)
                FROM Details
                WHERE Receipt_Id = @Id", conn, tran))
            {
                cmd.Parameters.AddWithValue("@Id", receiptId);
                var val = cmd.ExecuteScalar();
                return val != DBNull.Value ? Convert.ToDecimal(val) : 0m;
            }
        }
    }
}