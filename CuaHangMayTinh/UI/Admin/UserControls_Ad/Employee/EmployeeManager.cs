using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.UI.Forms;
using CuaHangMayTinh.BUS;
namespace CuaHangMayTinh.UI.UserControls_Ad
{
    public partial class EmployeeManager: UserControl
    {
        private EmployeeBUS _employeeBUS = new EmployeeBUS();
        public EmployeeManager()
        {
            InitializeComponent();
            LoadData();
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void LoadData()
        {
            string SQL = "SELECt * From Account join Employee on Account.Employee_Id = Employee.Employee_Id and Account.role != 'admin'";
            DataTable dt = new DataTable();
            DbConnect DAL = new DbConnect();
            dt = DAL.GetData(SQL);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = dt;
            
            // Ẩn các cột không cần thiết
            if (dataGridView1.Columns.Contains("Employee_id1"))
            {
                dataGridView1.Columns["Employee_id1"].Visible = false;
            }
            if (dataGridView1.Columns.Contains("Employee_id"))
            {
                dataGridView1.Columns["Employee_id"].Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeManager_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int employeeId = Convert.ToInt32(selectedRow.Cells["Employee_id"].Value);
                string employeeName = selectedRow.Cells["EmployeeName"].Value.ToString();
                string phone = selectedRow.Cells["PhoneNumber"].Value.ToString();
                string username = selectedRow.Cells["Username"].Value.ToString();
                string role = selectedRow.Cells["Role"].Value.ToString();

                var confirmForm = new CuaHangMayTinh.UI.Forms.ConfirmDeleteEmployeeForm(employeeName, phone, username, role);
                if (confirmForm.ShowDialog() == DialogResult.OK && confirmForm.IsConfirmed)
                {
                    // Xóa tài khoản trước (nếu có)
                    var accountDAO = new CuaHangMayTinh.DAL.AccountDAO();
                    string sqlDelAcc = $"DELETE FROM Account WHERE Employee_Id = {employeeId}";
                    accountDAO.ExecuteNonQuery(sqlDelAcc);
                    // Xóa nhân viên
                    var employeeBUS = new CuaHangMayTinh.BUS.EmployeeBUS();
                    var employeeDAO = new CuaHangMayTinh.DAL.EmployeeDAO();
                    employeeDAO.Delete(employeeId);
                    MessageBox.Show("Đã xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EmployeeRegistrationForm ad = new EmployeeRegistrationForm();
            if (ad.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int employeeId = Convert.ToInt32(selectedRow.Cells["Employee_id"].Value);
                string employeeName = selectedRow.Cells["EmployeeName"].Value.ToString();
                int age = Convert.ToInt32(selectedRow.Cells["Age"].Value);
                string phone = selectedRow.Cells["PhoneNumber"].Value.ToString();
                string username = selectedRow.Cells["Username"].Value.ToString();
                string role = selectedRow.Cells["Role"].Value.ToString();

                EditEmployeeForm editForm = new EditEmployeeForm(employeeId, employeeName, age, phone, username, role);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }
            try
            {
                DataTable dt = _employeeBUS.SearchEmployees(keyword);
                dataGridView1.DataSource = dt;
                // Ẩn các cột không cần thiết
                if (dataGridView1.Columns.Contains("Employee_id1"))
                    dataGridView1.Columns["Employee_id1"].Visible = false;
                if (dataGridView1.Columns.Contains("Employee_id"))
                    dataGridView1.Columns["Employee_id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
