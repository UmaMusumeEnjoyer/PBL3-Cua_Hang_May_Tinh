using System;
using System.Windows.Forms;

namespace CuaHangMayTinh.UI.Forms
{
    public partial class ConfirmDeleteEmployeeForm : Form
    {
        public bool IsConfirmed { get; private set; } = false;
        public ConfirmDeleteEmployeeForm(string employeeName, string phone, string username, string role)
        {
            InitializeComponent();
            lblName.Text = employeeName;
            lblPhone.Text = phone;
            lblUsername.Text = username;
            lblRole.Text = role;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 