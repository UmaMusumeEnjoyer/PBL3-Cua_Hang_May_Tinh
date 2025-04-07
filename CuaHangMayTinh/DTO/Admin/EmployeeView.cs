using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Admin
{
    public class EmployeeView
    {
        public int Employee_Id { get; set; }
        public string EmployeeName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }

}
