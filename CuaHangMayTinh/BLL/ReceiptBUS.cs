using System.Data;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Staff;
namespace CuaHangMayTinh.BLL
{
    public class ReceiptBUS
    {
        private readonly ReceiptDAO _receiptDAO = new ReceiptDAO();
        public DataTable GetAllReceipts() => _receiptDAO.GetAllReceipts();
        public Receipt GetReceipt(int id)   => _receiptDAO.GetFullReceipt(id);
        public int CreateReceipt(Receipt r)  => _receiptDAO.CreateReceipt(r);
        
    }
}