using System;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using System.Data;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class EditPC : Form
    {
        private PCBUS pcBUS;
        private SupplierBUS supplierBUS;
        private int productId;

        public EditPC(int productId)
        {
            InitializeComponent();
            this.productId = productId;
            pcBUS = new PCBUS();
            supplierBUS = new SupplierBUS();
            LoadSuppliers();
            LoadPCData();
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

        private void LoadPCData()
        {
            try
            {
                DataTable dt = pcBUS.GetPCById(productId);
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    textBox1.Text = row["pcName"].ToString();
                    textBox5.Text = row["specification"].ToString();
                    textBox2.Text = row["price"].ToString();
                    textBox3.Text = row["stockQuantity"].ToString();

                    // Tạm thời hủy event handler để tránh trigger khi set SelectedValue
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
                MessageBox.Show("Lỗi khi tải thông tin PC: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                // Kiểm tra và chuyển đổi kiểu dữ liệu
                if (!decimal.TryParse(textBox2.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Giá phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(textBox3.Text, out int stockQuantity) || stockQuantity < 0)
                {
                    MessageBox.Show("Số lượng tồn kho phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy dữ liệu từ form
                string pcName = textBox1.Text.Trim();
                string specification = textBox5.Text.Trim();
                string productName = textBox1.Text.Trim();
                int supplierId = (int)comboBox1.SelectedValue;

                // Gọi hàm cập nhật PC
                int result = pcBUS.UpdatePC(
                    productId, pcName, specification, supplierId, productName, price, stockQuantity
                );

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật PC thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật PC thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật PC: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
} 