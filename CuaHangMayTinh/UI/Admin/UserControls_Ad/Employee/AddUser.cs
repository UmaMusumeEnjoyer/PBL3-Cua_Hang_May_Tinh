using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.BLL;
using CuaHangMayTinh.BUS;
using CuaHangMayTinh.DAL;
using CuaHangMayTinh.DTO;
using CuaHangMayTinh.DTO.Common;

namespace CuaHangMayTinh.UI.Forms
{
    public partial class EmployeeRegistrationForm : Form
    {
        private EmployeeBUS _employeeBLL;

        public EmployeeRegistrationForm()
        {
            InitializeComponent();
            _employeeBLL = new EmployeeBUS();
        }


        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpEmployeeInfo;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.GroupBox grpAccountInfo;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;

        private void EmployeeRegistrationForm_Load(object sender, EventArgs e)
        {
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill in all required fields!", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mật khẩu xác nhận
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match!",
                               "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            try
            {
                // Lấy dữ liệu từ form
                string fullName = txtFullName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();
                DateTime birthDate = dtpBirthDate.Value;
                string address = txtAddress.Text.Trim();
                string email = txtEmail.Text.Trim();
                int age = DateTime.Now.Year - birthDate.Year;

                // Gọi phương thức thêm nhân viên
                int result = _employeeBLL.InsertEmployee(fullName, age, phone);

                if (result > 0)
                {
                    // Lấy lại nhân viên để lấy ID
                    Employee employee = _employeeBLL.GetEmployeeByName(fullName);

                    if (employee == null)
                    {
                        MessageBox.Show("Cannot retrieve newly added employee info.", "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Gọi phương thức thêm tài khoản
                    bool accountResult = AccountDAO.InsertAccount(username, password, employee.Employee_Id);
                    if (accountResult)
                    {
                        MessageBox.Show("Employee and account added successfully!", "Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Employee added, but failed to create account.", "Partial Success",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add employee. Please try again.", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeRegistrationForm_Load_1(object sender, EventArgs e)
        {

        }

        private void txtEmployeeId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click_1(object sender, EventArgs e)
        {

        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}