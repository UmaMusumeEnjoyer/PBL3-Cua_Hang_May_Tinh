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
    public partial class LaptopManager: UserControl
    {
        public LaptopManager()
        {
            InitializeComponent();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddLaptop em = new AddLaptop();
            em.ShowDialog();
        }
    }
}
