using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.BLL;
using CuaHangMayTinh.DTO;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager
{
    public partial class SupplierManager : UserControl
    {
        private SupplierDAO supplierDAO;
        private DataTable allSuppliers;

        public SupplierManager()
        {
            InitializeComponent();
            supplierDAO = new SupplierDAO();
            
            // Thiết lập màu nền cho header
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255); // Xanh dương đậm
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Thiết lập font và màu nền cho các cột
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;

            // Thiết lập màu nền cho từng cột
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // ID
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250); // Tên nhà cung cấp
            dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Số điện thoại
            dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250); // Email
            dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Địa chỉ

            // Căn giữa nội dung cho các cột
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            LoadSuppliers();
        }

        private void LoadSuppliers()
        {
            try
            {
                allSuppliers = supplierDAO.GetAll();
                DisplaySuppliers(allSuppliers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplaySuppliers(DataTable dt)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dataGridView1.Rows.Add(
                    row["Supplier_Id"],
                    row["supplierName"],
                    row["phoneNumber"],
                    row["email"],
                    row["address"]
                );
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                DisplaySuppliers(allSuppliers);
                return;
            }

            try
            {
                DataTable filteredTable = allSuppliers.Clone();
                foreach (DataRow row in allSuppliers.Rows)
                {
                    if (row["supplierName"].ToString().ToLower().Contains(keyword) ||
                        row["phoneNumber"].ToString().ToLower().Contains(keyword) ||
                        row["email"].ToString().ToLower().Contains(keyword) ||
                        row["address"].ToString().ToLower().Contains(keyword))
                    {
                        filteredTable.ImportRow(row);
                    }
                }
                DisplaySuppliers(filteredTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddSuppiler addForm = new AddSuppiler();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadSuppliers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int supplierId = Convert.ToInt32(row.Cells[0].Value);
                EditSuppiler editForm = new EditSuppiler(supplierId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                int supplierId = Convert.ToInt32(row.Cells[0].Value);
                string supplierName = row.Cells[1].Value.ToString();
                DeleteSuppiler deleteForm = new DeleteSuppiler(supplierId, supplierName);
                if (deleteForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSuppliers();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
