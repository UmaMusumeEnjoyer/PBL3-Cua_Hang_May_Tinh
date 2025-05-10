namespace CuaHangMayTinh.UI.Forms
{
    partial class ConfirmDeleteEmployeeForm
    {
        private System.ComponentModel.IContainer components = null;
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.lblPhoneTitle = new System.Windows.Forms.Label();
            this.lblUsernameTitle = new System.Windows.Forms.Label();
            this.lblRoleTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(60, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(260, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Xác nhận xóa nhân viên này";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNameTitle.Location = new System.Drawing.Point(30, 60);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(70, 13);
            this.lblNameTitle.TabIndex = 1;
            this.lblNameTitle.Text = "Họ và tên:";
            // 
            // lblPhoneTitle
            // 
            this.lblPhoneTitle.AutoSize = true;
            this.lblPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhoneTitle.Location = new System.Drawing.Point(30, 90);
            this.lblPhoneTitle.Name = "lblPhoneTitle";
            this.lblPhoneTitle.Size = new System.Drawing.Size(84, 13);
            this.lblPhoneTitle.TabIndex = 2;
            this.lblPhoneTitle.Text = "Số điện thoại:";
            // 
            // lblUsernameTitle
            // 
            this.lblUsernameTitle.AutoSize = true;
            this.lblUsernameTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsernameTitle.Location = new System.Drawing.Point(30, 120);
            this.lblUsernameTitle.Name = "lblUsernameTitle";
            this.lblUsernameTitle.Size = new System.Drawing.Size(92, 13);
            this.lblUsernameTitle.TabIndex = 3;
            this.lblUsernameTitle.Text = "Tên đăng nhập:";
            // 
            // lblRoleTitle
            // 
            this.lblRoleTitle.AutoSize = true;
            this.lblRoleTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoleTitle.Location = new System.Drawing.Point(30, 150);
            this.lblRoleTitle.Name = "lblRoleTitle";
            this.lblRoleTitle.Size = new System.Drawing.Size(49, 13);
            this.lblRoleTitle.TabIndex = 4;
            this.lblRoleTitle.Text = "Vai trò:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location = new System.Drawing.Point(130, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhone.Location = new System.Drawing.Point(130, 90);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(0, 13);
            this.lblPhone.TabIndex = 6;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsername.Location = new System.Drawing.Point(130, 120);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(0, 13);
            this.lblUsername.TabIndex = 7;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRole.Location = new System.Drawing.Point(130, 150);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(0, 13);
            this.lblRole.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(60, 190);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 35);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Xác nhận xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(200, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ConfirmDeleteEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 250);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblRoleTitle);
            this.Controls.Add(this.lblUsernameTitle);
            this.Controls.Add(this.lblPhoneTitle);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmDeleteEmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xác nhận xóa nhân viên";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.Label lblPhoneTitle;
        private System.Windows.Forms.Label lblUsernameTitle;
        private System.Windows.Forms.Label lblRoleTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
    }
}
