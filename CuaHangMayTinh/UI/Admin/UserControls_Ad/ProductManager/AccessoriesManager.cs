using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class AccessoriesManager: UserControl
    {
        public AccessoriesManager()
        {
            InitializeComponent();
            textBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            // Thiết lập màu nền cho header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255); // Xanh dương đậm
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                EditAccessories editForm = new EditAccessories(productId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after successful edit
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thiết bị cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                string accessoriesName = row.Cells[1].Value.ToString();

                var confirmForm = new ConfirmDeleteAccessoriesForm(productId, accessoriesName);
                if (confirmForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn thiết bị cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                // Lấy productId từ cột đầu tiên (thường là mã sản phẩm)
                int productId = Convert.ToInt32(row.Cells[0].Value);
                AccessoriesDetailForm detailForm = new AccessoriesDetailForm(productId);
                detailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void addThietbi_Click(object sender, EventArgs e)
        {
            AddAccessories em = new AddAccessories();
            if (em.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            try
            {
                AccessoriesDAO accessoriesDAO = new AccessoriesDAO();
                DataTable dt = accessoriesDAO.GetAll();
                dataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Product_Id"], // IdLaptop (mã thiết bị)
                        row["accessoriesName"], // Namelaptop (tên thiết bị)
                        row["overview"], // Specification (mô tả)
                        string.Format("{0:N0} VND", row["price"]), // Price
                        row["stockQuantity"] // SL
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBox1.Text.Trim().ToLower();
                AccessoriesDAO accessoriesDAO = new AccessoriesDAO();
                DataTable dt = accessoriesDAO.GetAll();

                // Lọc dữ liệu dựa trên nhiều trường
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Rows)
                {
                    string accessoriesName = row["accessoriesName"].ToString().ToLower();
                    string overview = row["overview"].ToString().ToLower();
                    string price = row["price"].ToString().ToLower();
                    string productId = row["Product_Id"].ToString().ToLower();

                    if (accessoriesName.Contains(searchText) ||
                        overview.Contains(searchText) ||
                        price.Contains(searchText) ||
                        productId.Contains(searchText))
                    {
                        filteredDt.ImportRow(row);
                    }
                }

                // Hiển thị kết quả tìm kiếm (đúng thứ tự và đủ cột)
                dataGridView1.Rows.Clear();
                foreach (DataRow row in filteredDt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Product_Id"],
                        row["accessoriesName"],
                        row["overview"],
                        string.Format("{0:N0} VND", row["price"]),
                        row["stockQuantity"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
