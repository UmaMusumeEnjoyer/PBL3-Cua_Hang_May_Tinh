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
            txtPhone.TextChanged += txtPhone_TextChanged;
            txtEmail.TextChanged += txtEmail_TextChanged;
            cboRole.SelectedIndex = 1; // Mặc định là staff
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
                MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc!", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số điện thoại phải là 10 số
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), "^0[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng số 0 và đủ 10 số!", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Kiểm tra định dạng email nếu có nhập
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    MessageBox.Show("Email không đúng định dạng!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            // Kiểm tra mật khẩu xác nhận
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                string role = cboRole.SelectedItem?.ToString() ?? "staff";

                // Gọi phương thức thêm nhân viên
                int result = _employeeBLL.InsertEmployee(fullName, age, phone);

                if (result > 0)
                {
                    // Lấy lại nhân viên để lấy ID
                    Employee employee = _employeeBLL.GetEmployeeByName(fullName);

                    if (employee == null)
                    {
                        MessageBox.Show("Không thể lấy thông tin nhân viên vừa thêm.", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Gọi phương thức thêm tài khoản
                    bool accountResult = AccountDAO.InsertAccount(username, password, employee.Employee_Id, role);
                    if (accountResult)
                    {
                        MessageBox.Show("Thêm nhân viên và tài khoản thành công!", "Thành công",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đã thêm nhân viên, nhưng tạo tài khoản thất bại.", "Cảnh báo",
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại. Vui lòng thử lại!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi",
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

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), "^0[0-9]{9}$"))
            {
                errorProviderPhone.SetError(txtPhone, "");
            }
            else
            {
                errorProviderPhone.SetError(txtPhone, "Số điện thoại phải bắt đầu bằng số 0 và đủ 10 số!");
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                errorProviderEmail.SetError(txtEmail, "");
            }
            else
            {
                errorProviderEmail.SetError(txtEmail, "Email không đúng định dạng!");
            }
        }
    }
}