using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CuaHangMayTinh.DTO.Common;

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
        public List<Details> GetDetailsByReceiptId(int receiptId)
        {
            var details = new List<Details>();
            var dt = GetData("SELECT * FROM Details WHERE Receipt_Id = @ReceiptId",
                new SqlParameter[] { new SqlParameter("@ReceiptId", receiptId) });
            
            foreach (DataRow row in dt.Rows)
            {
                details.Add(new Details
                {
                    Details_Id = (int)row["Details_Id"],
                    Product_Id = (int)row["Product_Id"],
                    Quantity = (int)row["quantity"],
                    ProductPrice = (decimal)row["productPrice"],
                    Receipt_Id = (int)row["Receipt_Id"],
                    AdjustmentType = row["AdjustmentType"].ToString(),
                    OriginalDetailId = row["OriginalDetailId"] as int?
                });
            }
            return details;
        }

        public List<Details> GetDetailsByGoodsReceiptId(int goodsReceiptId)
        {
            var details = new List<Details>();
            var dt = GetData("SELECT * FROM Details WHERE GoodsReceipt_Id = @GrId",
                new SqlParameter[] { new SqlParameter("@GrId", goodsReceiptId) });
            
            foreach (DataRow row in dt.Rows)
            {
                details.Add(new Details
                {
                    Details_Id = (int)row["Details_Id"],
                    Product_Id = (int)row["Product_Id"],
                    Quantity = (int)row["quantity"],
                    ProductPrice = (decimal)row["productPrice"],
                    GoodsReceipt_Id = (int)row["GoodsReceipt_Id"],
                    AdjustmentType = row["AdjustmentType"].ToString()
                });
            }
            return details;
        }
        
        #endregion
    }
}
