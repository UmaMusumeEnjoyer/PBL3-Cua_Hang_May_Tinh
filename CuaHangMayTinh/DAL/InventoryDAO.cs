using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CuaHangMayTinh.DAL
{
    class InventoryDAO : DbConnect
    {
        public DataTable GetInventoryRaw()
            => ExecuteSp("sp_Inventory_GetRaw");

    }
}
