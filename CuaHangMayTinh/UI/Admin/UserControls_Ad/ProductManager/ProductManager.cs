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
    public partial class ProductManager: UserControl
    {

        public ProductManager()
        {
           
            InitializeComponent();
            LaptopManager em = new LaptopManager();
            AddControlsToPanel(em);
        } 

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            Panelcontrols.Controls.Clear();
            Panelcontrols.Controls.Add(c);
        }
      
        private void ProductManager_Load(object sender, EventArgs e)
        {

        }
        private void btnChonceLaptop_click(object sender, EventArgs e)
        {
            LaptopManager em = new LaptopManager();
            AddControlsToPanel(em);

        }

        private void btnChoncePc(object sender, EventArgs e)
        {
            PcManager pm = new PcManager();
            AddControlsToPanel(pm);
        }

        private void btnChonceAccessories(object sender, EventArgs e)
        {
            AccessoriesManager em = new AccessoriesManager();
            AddControlsToPanel(em);
        }
    }
}
