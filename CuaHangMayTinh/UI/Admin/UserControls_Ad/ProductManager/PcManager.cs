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
    public partial class PcManager: UserControl
    {
        public PcManager()
        {
            InitializeComponent();
            textBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            // Thiết lập màu nền cho header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255); // Xanh dương đậm
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                PCBUS pcBUS = new PCBUS();
                DataTable dt = pcBUS.GetAllPCs();
                dataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Product_Id"], // IdPc
                        row["pcName"],     // Namelaptop (tên PC)
                        row["specification"], // Specification
                        string.Format("{0:N0} VND", row["price"]), // Price
                        row["stockQuantity"] // SL
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách PC: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddPc_click(object sender, EventArgs e)
        {
            AddPC em  = new AddPC();
            if (em.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                PCDetailForm detailForm = new PCDetailForm(productId);
                detailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                string pcName = row.Cells[1].Value.ToString();

                using (var confirmForm = new PcDeleteConfirmForm(productId, pcName))
                {
                    if (confirmForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn PC cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(row.Cells[0].Value);
                EditPC editForm = new EditPC(productId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh data after successful edit
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn PC cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
         private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBox1.Text.Trim().ToLower();
                PCBUS pcBUS = new PCBUS();
                DataTable dt = pcBUS.GetAllPCs();

                // Lọc dữ liệu dựa trên nhiều trường
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in dt.Rows)
                {
                    string pcName = row["pcName"].ToString().ToLower();
                    string specification = row["specification"].ToString().ToLower();
                    string price = row["price"].ToString().ToLower();
                    string productId = row["Product_Id"].ToString().ToLower();

                    if (pcName.Contains(searchText) ||
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
                        row["pcName"],
                        row["specification"],
                        string.Format("{0:N0} VND", row["price"]),
                        row["stockQuantity"]
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm PC: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
