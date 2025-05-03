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
    }
}