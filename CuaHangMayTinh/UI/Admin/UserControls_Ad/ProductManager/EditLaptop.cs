using System;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using System.Data;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class EditLaptop : Form
    {
        private LaptopBUS laptopBUS;
        private SupplierBUS supplierBUS;
        private int productId;

        public EditLaptop(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            laptopBUS = new LaptopBUS();
            supplierBUS = new SupplierBUS();
            LoadSuppliers();
            LoadLaptopData();
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = supplierBUS.GetAll();
                comboBox1.DataSource = suppliers;
                comboBox1.DisplayMember = "Supplier_Id";
                comboBox1.ValueMember = "Supplier_Id";
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLaptopData()
        {
            try
            {
                DataTable dt = laptopBUS.GetLaptopById(productId);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    textBox1.Text = row["laptopName"].ToString();
                    textBox2.Text = row["weight"].ToString();
                    textBox3.Text = row["screenSize"].ToString();
                    textBox4.Text = row["colour"].ToString();
                    textBox5.Text = row["specification"].ToString();
                    textBox7.Text = row["price"].ToString();
                    textBox8.Text = row["stockQuantity"].ToString();

                    // Tạm thời hủy event handler để tránh trigger khi set SelectedValue
                    comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                    comboBox1.SelectedValue = Convert.ToInt32(row["Supplier_Id"]);
                    comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

                    // Gán tên nhà cung cấp đúng
                    if (comboBox1.SelectedItem is DataRowView supplierRow)
                    {
                        textBox6.Text = supplierRow["supplierName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin laptop: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(textBox7.Text) ||
                    string.IsNullOrWhiteSpace(textBox8.Text) ||
                    comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate numeric fields
                if (!decimal.TryParse(textBox2.Text.Trim(), out decimal weight) || weight <= 0)
                {
                    MessageBox.Show("Cân nặng phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(textBox3.Text.Trim(), out decimal screenSize) || screenSize <= 0)
                {
                    MessageBox.Show("Kích thước màn hình phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(textBox7.Text.Trim(), out decimal price) || price <= 0)
                {
                    MessageBox.Show("Giá phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox8.Text.Trim(), out int stockQuantity) || stockQuantity < 0)
                {
                    MessageBox.Show("Số lượng tồn kho phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy dữ liệu từ form và chuyển đổi kiểu
                string laptopName = textBox1.Text.Trim();
                string colour = textBox4.Text.Trim();
                string specification = textBox5.Text.Trim();
                string productName = textBox6.Text.Trim();
                int supplierId = (int)comboBox1.SelectedValue;

                // Gọi hàm cập nhật laptop
                int result = laptopBUS.UpdateLaptop(
                    productId,
                    laptopName, weight, screenSize, specification, colour,
                    supplierId, productName, price, stockQuantity
                );

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật laptop thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật laptop thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật laptop: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 