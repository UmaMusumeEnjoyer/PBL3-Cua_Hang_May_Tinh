using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    public class ReceiptDAO : DbConnect
    {
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

    }
}