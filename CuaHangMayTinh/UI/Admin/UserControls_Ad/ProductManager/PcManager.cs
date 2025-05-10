using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class PcManager: UserControl
    {
        public PcManager()
        {
            InitializeComponent();
        }

        private void AddPc_click(object sender, EventArgs e)
        {
            AddPC em  = new AddPC();
            em.ShowDialog();
        }
    }
}
