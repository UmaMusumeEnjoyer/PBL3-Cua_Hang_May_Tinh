using System;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using System.Data;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class EditAccessories : Form
    {
        private AccessoriesBUS accessoriesBUS;
        private SupplierBUS supplierBUS;
        private int productId;

        public EditAccessories(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            accessoriesBUS = new AccessoriesBUS();
            supplierBUS = new SupplierBUS();
            LoadSuppliers();
            LoadAccessoriesData();
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = supplierBUS.GetAll();
                comboBox1.DataSource = suppliers;
                comboBox1.DisplayMember = "Supplier_Id";
                comboBox1.ValueMember = "Supplier_Id";
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAccessoriesData()
        {
            try
            {
                var dao = new CuaHangMayTinh.DAL.AccessoriesDAO();
                DataTable dt = dao.Get(productId);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    textBox1.Text = row["accessoriesName"].ToString();
                    textBox5.Text = row["overview"].ToString();
                    textBox2.Text = row["price"].ToString();
                    textBox3.Text = row["stockQuantity"].ToString();
                    textBox6.Text = row["productName"].ToString();
                    comboBox1.SelectedValue = Convert.ToInt32(row["Supplier_Id"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is DataRowView row)
            {
                textBox6.Text = row["supplierName"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(textBox2.Text.Trim(), out decimal price) || price <= 0)
                {
                    MessageBox.Show("Giá phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!int.TryParse(textBox3.Text.Trim(), out int stockQuantity) || stockQuantity < 0)
                {
                    MessageBox.Show("Số lượng tồn kho phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string accessoriesName = textBox1.Text.Trim();
                string overview = textBox5.Text.Trim();
                string productName = textBox1.Text.Trim();
                int supplierId = (int)comboBox1.SelectedValue;
                int result = accessoriesBUS.UpdateAccessories(
                    productId, accessoriesName, overview, supplierId, productName, price, stockQuantity
                );
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thiết bị thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 