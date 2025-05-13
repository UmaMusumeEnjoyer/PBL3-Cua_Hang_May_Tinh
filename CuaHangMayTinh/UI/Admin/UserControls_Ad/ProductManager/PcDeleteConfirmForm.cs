using System;
using System.Windows.Forms;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class PcDeleteConfirmForm : Form
    {
        private int productId;
        private string pcName;

        public PcDeleteConfirmForm(int productId, string pcName)
        {
            InitializeComponent();
            this.productId = productId;
            this.pcName = pcName;
            lblMessage.Text = $"Bạn có chắc chắn muốn xóa {pcName}?";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var bus = new PCBUS();
                int deleteResult = bus.DeletePC(productId);
                if (deleteResult > 0)
                {
                    MessageBox.Show("Xóa PC thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Xóa PC thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa PC: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}