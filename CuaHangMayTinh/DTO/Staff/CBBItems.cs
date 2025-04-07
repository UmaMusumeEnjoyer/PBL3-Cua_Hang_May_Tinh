using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHangMayTinh.DTO.Staff
{
    public class CBBItems
    {
        public string Text { get; set; }    // Tên hiển thị
        public object Value { get; set; }   // Giá trị thực lưu lại

        public override string ToString()
        {
            return Text;
        }
    }
}
