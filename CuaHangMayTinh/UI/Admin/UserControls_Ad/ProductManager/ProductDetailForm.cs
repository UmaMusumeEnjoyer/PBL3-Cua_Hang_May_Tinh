using System;
using System.Data;
using System.Windows.Forms;
using CuaHangMayTinh.BUS;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class ProductDetailForm : Form
    {
        public ProductDetailForm(int productId)
        {
            InitializeComponent();
            LoadDetail(productId);
        }
        private void LoadDetail(int productId)
        {
            ProductBUS bus = new ProductBUS();
            DataTable dt = bus.GetProductById(productId);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                lblId.Text = row["Product_Id"].ToString();

                // Tên sản phẩm: ưu tiên accessoriesName, PCName, LaptopName, productName
                if (dt.Columns.Contains("accessoriesName") && !string.IsNullOrEmpty(row["accessoriesName"].ToString()))
                    lblName.Text = row["accessoriesName"].ToString();
                else if (dt.Columns.Contains("PCName") && !string.IsNullOrEmpty(row["PCName"].ToString()))
                    lblName.Text = row["PCName"].ToString();
                else if (dt.Columns.Contains("LaptopName") && !string.IsNullOrEmpty(row["LaptopName"].ToString()))
                    lblName.Text = row["LaptopName"].ToString();
                else if (dt.Columns.Contains("productName"))
                    lblName.Text = row["productName"].ToString();

                // Thông số: ưu tiên overview, specification, LaptopSpec, PCSpec
                if (dt.Columns.Contains("overview") && !string.IsNullOrEmpty(row["overview"].ToString()))
                    lblSpec.Text = row["overview"].ToString();
                else if (dt.Columns.Contains("specification") && !string.IsNullOrEmpty(row["specification"].ToString()))
                    lblSpec.Text = row["specification"].ToString();
                else if (dt.Columns.Contains("LaptopSpec") && !string.IsNullOrEmpty(row["LaptopSpec"].ToString()))
                    lblSpec.Text = row["LaptopSpec"].ToString();
                else if (dt.Columns.Contains("PCSpec") && !string.IsNullOrEmpty(row["PCSpec"].ToString()))
                    lblSpec.Text = row["PCSpec"].ToString();

                // Giá
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

                // Số lượng
                lblStock.Text = row["stockQuantity"].ToString();
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin thiết bị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }
    }
} 