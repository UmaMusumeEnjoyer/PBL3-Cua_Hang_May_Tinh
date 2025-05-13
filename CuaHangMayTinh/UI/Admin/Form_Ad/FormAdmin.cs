using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Staff;
using CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager;
using CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager;
using CuaHangMayTinh.UI.Share;
using CuaHangMayTinh.UI.UserControls_Ad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangMayTinh.UI.Form_Ad
{
    public partial class FormAdmin: Form
    {
        private string username;

        public FormAdmin(string username)
        {
            InitializeComponent();
            this.username = username;
            moveSidePanel(btnHome);
            information Uc = new information();
            AddControlsToPanel(Uc);
            timerTime.Start();
            setLabel();
        }
       
        private void setLabel()
        {
            label3.Text = username;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            PanelControls.Controls.Clear();
            PanelControls.Controls.Add(c);
        }

        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }
        private void btnChiPhi_Click(object sender, EventArgs e)
        {

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            information Uc = new information();
            AddControlsToPanel(Uc);
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHoadonnhap);
            Goods_Receipt gr = new Goods_Receipt();
            AddControlsToPanel(gr);
        }

       

        private void btnWarehouse_Click(object sender, EventArgs e)
        {

        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnUsers);
            EmployeeManager em = new EmployeeManager();
            AddControlsToPanel(em);
        }
        private void btnThietbi_Click(object sender , EventArgs e)
        {
            moveSidePanel(btnThietbi);
            ProductManager em = new ProductManager();
            AddControlsToPanel(em);
        }
        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Text = dt.ToString("HH:mm:ss");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void timeLabel_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        private void labelTime_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {
            //PanelControls.Dock = DockStyle.Fill;
            

        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnThietbi);
            SupplierManager em = new SupplierManager();
            AddControlsToPanel(em);
        }

        private void PanelControls_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
