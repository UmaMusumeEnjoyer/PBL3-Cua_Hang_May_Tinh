using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Admin;
namespace CuaHangMayTinh.BLL
{
    class InventoryBUS
    {
        private readonly InventoryDAO _inventoryDAO = new InventoryDAO();
        public List<InventoryView> GetInventory()
        {
            DataTable dt = _inventoryDAO.GetInventoryRaw();
            return dt.AsEnumerable()
                     .Select(r => new InventoryView
                     {
                         Product_Id = r.Field<int>("Product_Id"),
                         ProductName = r.Field<string>("ProductName"),
                         Category = r.Field<string>("Category"),
                         Specification = r.Field<string>("Specification"),
                         Colour = r.Field<string>("Colour"),
                         Price = r.Field<decimal>("Price"),
                         StockQuantity = r.Field<int>("StockQuantity"),
                         SupplierPhone = r.Field<string>("SupplierPhone"),
                         GoodsReceiptDate = r.Field<DateTime>("GoodsReceiptDate")   
                     })
                     .ToList();
        }
    }
}
