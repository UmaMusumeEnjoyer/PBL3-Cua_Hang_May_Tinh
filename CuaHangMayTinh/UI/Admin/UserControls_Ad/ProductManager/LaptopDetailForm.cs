using System;
using System.Data;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class LaptopDetailForm : Form
    {
        public LaptopDetailForm(int productId)
        {
            InitializeComponent();
            LoadDetail(productId);
        }
        private void LoadDetail(int productId)
        {
            LaptopBUS bus = new LaptopBUS();
            DataTable dt = bus.GetLaptopById(productId);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                lblId.Text = row["Product_Id"].ToString();
                lblName.Text = row["laptopName"].ToString();
                lblWeight.Text = row["weight"].ToString();
                lblScreen.Text = row["screenSize"].ToString();
                lblColour.Text = row["colour"].ToString();
                lblSpec.Text = row["specification"].ToString();
                object priceObj = row["price"];
                if (priceObj != DBNull.Value)
                {
                    decimal priceValue = Convert.ToDecimal(priceObj);
                    lblPrice.Text = string.Format("{0:N0} VND", priceValue);
                }
                else
                {
                    lblPrice.Text = "Không có";
                }
                lblStock.Text = row["stockQuantity"].ToString();
            }
        }

        private void labelColour_Click(object sender, EventArgs e)
        {

        }

        private void labelSpec_Click(object sender, EventArgs e)
        {

        }

        private void labelStock_Click(object sender, EventArgs e)
        {

        }
    }
} 