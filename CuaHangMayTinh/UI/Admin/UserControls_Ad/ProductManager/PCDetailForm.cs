using System;
using System.Data;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class PCDetailForm : Form
    {
        public PCDetailForm(int productId)
        {
            InitializeComponent();
            LoadDetail(productId);
        }
        private void LoadDetail(int productId)
        {
            PCBUS bus = new PCBUS();
            DataTable dt = bus.GetPCById(productId);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                lblId.Text = row["Product_Id"].ToString();
                lblName.Text = row["pcName"].ToString();
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
    }
} 