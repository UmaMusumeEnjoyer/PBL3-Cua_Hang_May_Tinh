using System;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using System.Data;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class AddAccessories : Form
    {
        private AccessoriesBUS accessoriesBUS;
        private SupplierBUS supplierBUS;

        public AddAccessories()
        {
            InitializeComponent();
            accessoriesBUS = new AccessoriesBUS();
            supplierBUS = new SupplierBUS();
            LoadSuppliers();
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
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate numeric fields
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

                // Lấy dữ liệu từ form và chuyển đổi kiểu
                string accessoriesName = textBox1.Text.Trim();
                string overview = textBox5.Text.Trim();
                string productName = textBox1.Text.Trim();
                int supplierId = (int)comboBox1.SelectedValue;

                // Gọi hàm thêm thiết bị
                int result = accessoriesBUS.InsertAccessories(
                    accessoriesName, overview, supplierId, productName, price, stockQuantity
                );

                if (result > 0)
                {
                    MessageBox.Show("Thêm thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm thiết bị thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
{
}

private void label6_Click(object sender, EventArgs e)
{
}
    }
}
