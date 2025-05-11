
using System;
using System.Collections.Generic;
using System.Data;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.DTO.Staff;

namespace CuaHangMayTinh.BLL
{
    public class Goods_ReceiptBUS 
    {
        private readonly Goods_ReceiptDAO _goodsReceiptDao = new Goods_ReceiptDAO();
        
        public int CreateGoodsReceipt(GoodsReceipt gr)
        {
            if (gr == null)
                throw new ArgumentNullException(nameof(gr));
            if (gr.Details == null || gr.Details.Count == 0)
                throw new InvalidOperationException("Phiếu nhập phải có ít nhất 1 dòng chi tiết.");
            
            return _goodsReceiptDao.CreateGoodsReceipt(gr);
        }
        public void CancelGoodsReceipt(int goodsReceiptId)
        {
            if (goodsReceiptId <= 0)
                throw new ArgumentException("GoodsReceiptId phải > 0", nameof(goodsReceiptId));

            
            _goodsReceiptDao.CancelGoodsReceipt(goodsReceiptId);
        }
        public DataTable GetAllGoodsReceipts()
        {
            const string sql = "SELECT * FROM Goods_Receipt";
            return new DbConnect().GetData(sql);
        }
        public List<Details> GetDetails(int goodsReceiptId)
        {
            return new DetailsDAO().GetDetailsByGoodsReceiptId(goodsReceiptId);
        }
    }
}