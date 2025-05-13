using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager
{
    public partial class AddSuppiler: Form
    {
        public AddSuppiler(int supplierId)
        {
            InitializeComponent();
            LoadSupplierData(supplierId);
        }

        public AddSuppiler()
        {
            InitializeComponent();
        }

        private void LoadSupplierData(int supplierId)
        {
            var dao = new CuaHangMayTinh.DAL.SupplierDAO();
            var dt = dao.GetById(supplierId);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtName.Text = row["supplierName"].ToString();
                txtPhone.Text = row["phoneNumber"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtAddress.Text = row["address"].ToString();
            }
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                string address = txtAddress.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dao = new CuaHangMayTinh.DAL.SupplierDAO();
                int result = dao.Insert(name, phone, email, address);

                if (result > 0)
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm nhà cung cấp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
