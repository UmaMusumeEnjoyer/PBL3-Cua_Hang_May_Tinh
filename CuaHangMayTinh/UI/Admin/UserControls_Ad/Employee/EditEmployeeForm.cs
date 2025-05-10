using System;
using System.Windows.Forms;
using CuaHangMayTinh.BUS;
using CuaHangMayTinh.DAL;

namespace CuaHangMayTinh.UI.Forms
{
    public partial class EditEmployeeForm : Form
    {
        private readonly int _employeeId;
        private readonly EmployeeBUS _employeeBUS;
        private readonly AccountDAO _accountDAO;

        public EditEmployeeForm(int employeeId, string employeeName, int age, string phone, string username, string role)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _employeeBUS = new EmployeeBUS();
            _accountDAO = new AccountDAO();

            // Gán giá trị vào các control
            txtEmployeeName.Text = employeeName;
            txtAge.Text = age.ToString();
            txtPhone.Text = phone;
            txtUsername.Text = username;
            cboRole.Text = role;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtEmployeeName.Text) ||
                    string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy dữ liệu từ form
                string employeeName = txtEmployeeName.Text.Trim();
                int age = Convert.ToInt32(txtAge.Text.Trim());
                string phone = txtPhone.Text.Trim();
                string username = txtUsername.Text.Trim();
                string role = cboRole.Text;
                string password = txtPassword.Text.Trim();

                // Cập nhật thông tin nhân viên
                int updateResult = _employeeBUS.UpdateEmployee(_employeeId, employeeName, age, phone);
                bool result = updateResult > 0; // Kiểm tra nếu số hàng bị ảnh hưởng > 0
                if (result)
                {
                    // Cập nhật thông tin tài khoản
                    int accountResult;
                    if (!string.IsNullOrEmpty(password))
                    {
                        accountResult = _accountDAO.UpdateAccount(_employeeId, username, password, role);
                    }
                    else
                    {
                        // Nếu không nhập mật khẩu mới, lấy mật khẩu cũ từ DB (hoặc bỏ qua cập nhật mật khẩu nếu muốn)
                        // Ở đây sẽ giữ nguyên mật khẩu cũ bằng cách lấy lại từ DB
                        var dt = _accountDAO.GetData($"SELECT password FROM Account WHERE Employee_Id = {_employeeId}");
                        string oldPassword = dt.Rows.Count > 0 ? dt.Rows[0]["password"].ToString() : "";
                        accountResult = _accountDAO.UpdateAccount(_employeeId, username, oldPassword, role);
                    }
                    if (accountResult > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin tài khoản thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
} 