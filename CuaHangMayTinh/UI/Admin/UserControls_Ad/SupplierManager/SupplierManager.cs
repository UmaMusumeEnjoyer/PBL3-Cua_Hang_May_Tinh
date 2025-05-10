using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.UI.Admin.UserControls_Ad.SuppilerManager;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager
{
    public partial class SupplierManager: UserControl
    {
        public SupplierManager()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void addSupplier_click(object sender, EventArgs e)
        {
            AddSuppiler em = new AddSuppiler();
            em.ShowDialog();
        }
    }
}
