using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CuaHangMayTinh.DTO.Common;
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
        
        public Receipt GetReceiptById(int receiptId)
        {
            var receipt = new Receipt();
            var dt = GetData("SELECT * FROM Receipt WHERE Receipt_Id = @Id", 
                new SqlParameter[] { new SqlParameter("@Id", receiptId) });
            
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                receipt.Receipt_Id = receiptId;
                receipt.ReceiptDate = (DateTime)row["receiptDate"];
                receipt.Customer_Id = (int)row["Customer_Id"];
                receipt.Employee_Id = (int)row["Employee_Id"];
                receipt.IsCanceled = (bool)row["IsCanceled"];

                // Lấy Details
                receipt.Details = new DetailsDAO().GetDetailsByReceiptId(receiptId);
                receipt.TotalAmount = CalculateTotal(receipt.Details);
            }
            return receipt;
        }
        #endregion
        
        private decimal CalculateTotal(List<Details> details)
        {
            decimal total = 0;
            foreach (var d in details)
            {
                if (d.AdjustmentType != "CANCEL") total += d.Quantity * d.ProductPrice;
            }
            return total;
        }

        public void CreateReceipt(Receipt receipt)
        {
            ExecuteTransaction((conn, tran) =>
            {
                // Thêm Receipt và lấy ID mới
                const string sql = @"
                    INSERT INTO Receipt (receiptDate, Customer_Id, Employee_Id, IsCanceled)
                    OUTPUT INSERTED.Receipt_Id
                    VALUES (@ReceiptDate, @CustomerId, @EmployeeId, 0)";

                using (var cmd = new SqlCommand(sql, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@ReceiptDate", receipt.ReceiptDate);
                    cmd.Parameters.AddWithValue("@CustomerId", receipt.Customer_Id);
                    cmd.Parameters.AddWithValue("@EmployeeId", receipt.Employee_Id);
                    receipt.Receipt_Id = (int)cmd.ExecuteScalar();
                }

                foreach (var d in receipt.Details)
                {
                    // kiểm kho, atomic
                    const string stockCheck = @"
                    UPDATE Product 
                    SET stockQuantity = stockQuantity - @q 
                    WHERE Product_Id = @pid 
                      AND stockQuantity >= @q";
                    using (var chk = new SqlCommand(stockCheck, conn, tran))
                    {
                        chk.Parameters.AddWithValue("@pid", d.Product_Id);
                        chk.Parameters.AddWithValue("@q", d.Quantity);
                        if (chk.ExecuteNonQuery() == 0)
                            throw new InvalidOperationException($"Không đủ tồn kho cho {d.Product_Id}");
                    }
                    d.Receipt_Id      = receipt.Receipt_Id;
                    d.AdjustmentType  = "ORIGINAL";
                    AddDetailForReceipt(conn, tran, receipt.Receipt_Id, d);
                }
            });
        }
        public void CancelReceipt(int receiptId)
        {
            ExecuteTransaction((conn, tran) =>
            {
                // Đánh dấu header
                using (var cmd = new SqlCommand(
                           "UPDATE Receipt SET IsCanceled=1 WHERE Receipt_Id=@rid", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@rid", receiptId);
                    cmd.ExecuteNonQuery();
                }

                // Lấy chi tiết gốc
                var originals = new DetailsDAO().GetDetailsByReceiptId(receiptId);
                foreach (var originalDetail in originals)
                {
                    var cancel = new Details
                    {
                        Product_Id       = originalDetail.Product_Id,
                        Quantity         = originalDetail.Quantity,
                        ProductPrice     = originalDetail.ProductPrice,
                        Receipt_Id       = receiptId,
                        AdjustmentType   = "CANCEL",
                        OriginalDetailId = originalDetail.Details_Id
                    };

                    // Trả kho bên trong AddDetailForReceipt
                    AddDetailForReceipt(conn, tran, receiptId, cancel);
                }
            });
        }
        private void AddDetailForReceipt(
            SqlConnection conn, SqlTransaction tran,
            int receiptId, Details detail)
        {
            // Chèn detail
            const string ins = @"
            INSERT INTO Details
              (Product_Id, quantity, productPrice,
               Receipt_Id, AdjustmentType, OriginalDetailId)
            VALUES(@pid,@qty,@price,@rid,@type,@orig)";
            using (var cmd = new SqlCommand(ins, conn, tran))
            {
                cmd.Parameters.AddWithValue("@pid", detail.Product_Id);
                cmd.Parameters.AddWithValue("@qty", detail.Quantity);
                cmd.Parameters.AddWithValue("@price", detail.ProductPrice);
                cmd.Parameters.AddWithValue("@rid", detail.Receipt_Id);
                cmd.Parameters.AddWithValue("@type", detail.AdjustmentType);
                cmd.Parameters.AddWithValue("@orig",
                    detail.OriginalDetailId ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            // Cập nhật kho: CANCEL thì +, ORIGINAL thì - 
            var factor = detail.AdjustmentType == "CANCEL" ? 1 : -1;
            using (var stk = new SqlCommand(
                "UPDATE Product SET stockQuantity = stockQuantity + @f*@qty WHERE Product_Id=@pid",
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
    
