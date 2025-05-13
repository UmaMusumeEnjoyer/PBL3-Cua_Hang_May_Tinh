using System;
using System.Windows.Forms;
using CuaHangMayTinh.DTO.Common;
using CuaHangMayTinh.BLL;

namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    public partial class ConfirmDeleteLaptopForm : Form
    {
        private int productId;
        private string laptopName;

        public ConfirmDeleteLaptopForm(int productId, string laptopName)
        {
            InitializeComponent();
            this.productId = productId;
            this.laptopName = laptopName;
            lblMessage.Text = $"Bạn có chắc chắn muốn xóa {laptopName}?";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var bus = new LaptopBUS();
                int deleteResult = bus.DeleteLaptop(productId);
                if (deleteResult > 0)
                {
                    MessageBox.Show("Xóa laptop thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Xóa laptop thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa laptop: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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