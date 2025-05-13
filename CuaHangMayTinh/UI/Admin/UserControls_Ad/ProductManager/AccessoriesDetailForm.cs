using System;
using System.Data;
using System.Windows.Forms;
using CuaHangMayTinh.DAL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class AccessoriesDetailForm : Form
    {
        public AccessoriesDetailForm(int productId)
        {
            InitializeComponent();
            LoadDetail(productId);
        }
        private void LoadDetail(int productId)
        {
            AccessoriesDAO dao = new AccessoriesDAO();
            DataTable dt = dao.Get(productId);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                lblId.Text = row["Product_Id"].ToString();
                lblName.Text = row["accessoriesName"].ToString();
                lblSpec.Text = row["overview"].ToString();
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
            else
            {
                MessageBox.Show("Không tìm thấy thông tin phụ kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }
    }
} 