using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CuaHangMayTinh.DAL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.SupplierManager
{
    public partial class DeleteSuppiler : Form
    {
        private int supplierId;
        private string supplierName;

        public DeleteSuppiler(int id, string name)
        {
            InitializeComponent();
            supplierId = id;
            supplierName = name;
            lblConfirm.Text = $"Bạn có chắc chắn muốn xóa nhà cung cấp {supplierName}?";
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                var dao = new SupplierDAO();
                int result = dao.Delete(supplierId);
                if (result > 0)
                {
                    MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Xóa nhà cung cấp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
