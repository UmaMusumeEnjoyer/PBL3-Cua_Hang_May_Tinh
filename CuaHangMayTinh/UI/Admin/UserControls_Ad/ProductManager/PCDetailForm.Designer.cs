namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    partial class PCDetailForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSpec;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSpec;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelStock;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSpec = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSpec = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelStock = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(150, 30);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(220, 23);
            this.lblId.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(150, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(220, 23);
            this.lblName.TabIndex = 3;
            // 
            // lblSpec
            // 
            this.lblSpec.AutoEllipsis = true;
            this.lblSpec.Location = new System.Drawing.Point(150, 90);
            this.lblSpec.Name = "lblSpec";
            this.lblSpec.Size = new System.Drawing.Size(220, 60);
            this.lblSpec.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(150, 160);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(220, 23);
            this.lblPrice.TabIndex = 7;
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStock
            // 
            this.lblStock.Location = new System.Drawing.Point(150, 190);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(220, 23);
            this.lblStock.TabIndex = 9;
            // 
            // labelId
            // 
            this.labelId.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelId.Location = new System.Drawing.Point(30, 30);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(100, 23);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "Mã PC:";
            // 
            // labelName
            // 
            this.labelName.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(30, 60);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(100, 23);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Tên:";
            // 
            // labelSpec
            // 
            this.labelSpec.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSpec.Location = new System.Drawing.Point(30, 90);
            this.labelSpec.Name = "labelSpec";
            this.labelSpec.Size = new System.Drawing.Size(100, 23);
            this.labelSpec.TabIndex = 4;
            this.labelSpec.Text = "Thông số:";
            // 
            // labelPrice
            // 
            this.labelPrice.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrice.Location = new System.Drawing.Point(30, 160);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(100, 23);
            this.labelPrice.TabIndex = 6;
            this.labelPrice.Text = "Giá:";
            // 
            // labelStock
            // 
            this.labelStock.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStock.Location = new System.Drawing.Point(30, 190);
            this.labelStock.Name = "labelStock";
            this.labelStock.Size = new System.Drawing.Size(100, 23);
            this.labelStock.TabIndex = 8;
            this.labelStock.Text = "Số lượng:";
            // 
            // PCDetailForm
            // 
            this.ClientSize = new System.Drawing.Size(420, 240);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.labelSpec);
            this.Controls.Add(this.lblSpec);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.labelStock);
            this.Controls.Add(this.lblStock);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PCDetailForm";
            this.Text = "Chi tiết PC";
            this.ResumeLayout(false);

        }
    }
} 