using System;
using System.Windows.Forms;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class ConfirmDeleteAccessoriesForm : Form
    {
        private int productId;
        private string accessoriesName;

        public ConfirmDeleteAccessoriesForm(int productId, string accessoriesName)
        {
            InitializeComponent();
            this.productId = productId;
            this.accessoriesName = accessoriesName;
            lblMessage.Text = $"Bạn có chắc chắn muốn xóa {accessoriesName}?";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var bus = new AccessoriesBUS();
                int deleteResult = bus.DeleteAccessories(productId);
                if (deleteResult > 0)
                {
                    MessageBox.Show("Xóa thiết bị thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Xóa thiết bị thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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