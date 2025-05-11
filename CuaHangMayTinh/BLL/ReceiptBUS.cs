using System;
using System.Collections.Generic;
using System.Data;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Staff;

namespace CuaHangMayTinh.BUS
{
    public class ReceiptBUS
    {
        private readonly ReceiptDAO _receiptDao = new ReceiptDAO();
        
        public DataTable GetAllReceipts()
        {
            return _receiptDao.GetAllReceipts();
        }
        
        public Receipt GetReceipt(int receiptId)
        {
            if (receiptId <= 0)
                throw new ArgumentException("ReceiptId phải > 0");

            return _receiptDao.GetReceiptById(receiptId);
        }
        
        public int CreateReceipt(Receipt receipt)
        {
            if (receipt == null)
                throw new ArgumentNullException(nameof(receipt));

            if (receipt.Details == null || receipt.Details.Count == 0)
                throw new InvalidOperationException("Hóa đơn phải có ít nhất 1 sản phẩm.");
            
            return _receiptDao.CreateReceipt(receipt);
        }
        
        public void CancelReceipt(int receiptId)
        {
            if (receiptId <= 0)
                throw new ArgumentException("ReceiptId phải > 0");

            // Lấy trước, có thể kiểm xem đã hủy chưa
            var existing = _receiptDao.GetReceiptById(receiptId);
            if (existing == null)
                throw new KeyNotFoundException($"Không tìm thấy hóa đơn #{receiptId}.");
            if (existing.IsCanceled)
                throw new InvalidOperationException($"Hóa đơn #{receiptId} đã bị hủy.");
            
            _receiptDao.CancelReceipt(receiptId);
        }
        // Tính tổng tiền trên UI hoặc báo cáo
        public decimal CalculateReceiptTotal(int receiptId)
        {
            var receipt = GetReceipt(receiptId);
            return receipt.TotalAmount ?? 0m;
        }
    }
}
