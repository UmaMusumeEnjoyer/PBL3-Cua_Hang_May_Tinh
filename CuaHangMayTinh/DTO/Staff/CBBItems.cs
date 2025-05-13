using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DTO phục vụ UI

namespace CuaHangMayTinh.DTO.Staff
{
    public class CBBItems
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public CBBItems()
        {
        }

        public CBBItems(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
