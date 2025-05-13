using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class LaptopManager: UserControl
    {
        public LaptopManager()
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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddLaptop em = new AddLaptop();
            if (em.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh data after adding
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                LaptopDetailForm detailForm = new LaptopDetailForm(productId);
                detailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                string laptopName = row.Cells[1].Value.ToString();

                var confirmForm = new ConfirmDeleteLaptopForm(productId, laptopName);
                if (confirmForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after successful deletion
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn laptop cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadData()
        {
            try
            {
                LaptopBUS laptopBUS = new LaptopBUS();
                DataTable dt = laptopBUS.GetAllLaptops();
                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Product_Id"],
                        row["laptopName"],
                        row["weight"],
                        row["screenSize"],
                        row["colour"],
                        row["specification"],
                        string.Format("{0:N0} VND", row["price"]),
                        row["stockQuantity"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách laptop: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                EditLaptop editForm = new EditLaptop(productId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after successful edit
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn laptop cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBox1.Text.Trim().ToLower();
                LaptopBUS laptopBUS = new LaptopBUS();
                DataTable dt = laptopBUS.GetAllLaptops();

                // Lọc dữ liệu dựa trên nhiều trường
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Rows)
                {
                    string laptopName = row["laptopName"].ToString().ToLower();
                    string specification = row["specification"].ToString().ToLower();
                    string price = row["price"].ToString().ToLower();
                    string productId = row["Product_Id"].ToString().ToLower();

                    if (laptopName.Contains(searchText) ||
                        specification.Contains(searchText) ||
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
                        row["laptopName"],
                        row["weight"],
                        row["screenSize"],
                        row["colour"],
                        row["specification"],
                        string.Format("{0:N0} VND", row["price"]),
                        row["stockQuantity"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm laptop: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
