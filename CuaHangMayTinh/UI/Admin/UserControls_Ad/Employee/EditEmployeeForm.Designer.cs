namespace CuaHangMayTinh.UI.Forms
{
    partial class EditEmployeeForm
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
            this.grpEmployeeInfo = new System.Windows.Forms.GroupBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.grpAccountInfo = new System.Windows.Forms.GroupBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpEmployeeInfo.SuspendLayout();
            this.grpAccountInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(245, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(347, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sửa Thông Tin Nhân Viên";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // grpEmployeeInfo
            // 
            this.grpEmployeeInfo.Controls.Add(this.lblPhone);
            this.grpEmployeeInfo.Controls.Add(this.txtPhone);
            this.grpEmployeeInfo.Controls.Add(this.lblAge);
            this.grpEmployeeInfo.Controls.Add(this.txtAge);
            this.grpEmployeeInfo.Controls.Add(this.lblEmployeeName);
            this.grpEmployeeInfo.Controls.Add(this.txtEmployeeName);
            this.grpEmployeeInfo.Location = new System.Drawing.Point(107, 78);
            this.grpEmployeeInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEmployeeInfo.Name = "grpEmployeeInfo";
            this.grpEmployeeInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpEmployeeInfo.Size = new System.Drawing.Size(597, 185);
            this.grpEmployeeInfo.TabIndex = 1;
            this.grpEmployeeInfo.TabStop = false;
            this.grpEmployeeInfo.Text = "Thông Tin Cá Nhân";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(27, 114);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(95, 16);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Số Điện Thoại:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(200, 111);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(265, 22);
            this.txtPhone.TabIndex = 5;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(27, 78);
            this.lblAge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(37, 16);
            this.lblAge.TabIndex = 2;
            this.lblAge.Text = "Tuổi:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(200, 74);
            this.txtAge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(265, 22);
            this.txtAge.TabIndex = 3;
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.AutoSize = true;
            this.lblEmployeeName.Location = new System.Drawing.Point(27, 41);
            this.lblEmployeeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(73, 16);
            this.lblEmployeeName.TabIndex = 0;
            this.lblEmployeeName.Text = "Họ và Tên:";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(200, 37);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(265, 22);
            this.txtEmployeeName.TabIndex = 1;
            // 
            // grpAccountInfo
            // 
            this.grpAccountInfo.Controls.Add(this.lblPassword);
            this.grpAccountInfo.Controls.Add(this.txtPassword);
            this.grpAccountInfo.Controls.Add(this.lblRole);
            this.grpAccountInfo.Controls.Add(this.cboRole);
            this.grpAccountInfo.Controls.Add(this.lblUsername);
            this.grpAccountInfo.Controls.Add(this.txtUsername);
            this.grpAccountInfo.Location = new System.Drawing.Point(107, 271);
            this.grpAccountInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAccountInfo.Name = "grpAccountInfo";
            this.grpAccountInfo.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAccountInfo.Size = new System.Drawing.Size(597, 159);
            this.grpAccountInfo.TabIndex = 2;
            this.grpAccountInfo.TabStop = false;
            this.grpAccountInfo.Text = "Thông Tin Tài Khoản";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(27, 73);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 16);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(200, 69);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(265, 22);
            this.txtPassword.TabIndex = 5;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(27, 110);
            this.lblRole.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(54, 16);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Vai Trò:";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "staff",
            "manager"});
            this.cboRole.Location = new System.Drawing.Point(200, 106);
            this.cboRole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(265, 24);
            this.cboRole.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(27, 41);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(105, 16);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Tên Đăng Nhập:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(200, 37);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(265, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(493, 18);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 37);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(627, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 37);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 442);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 74);
            this.panel1.TabIndex = 3;
            // 
            // EditEmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 516);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpAccountInfo);
            this.Controls.Add(this.grpEmployeeInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditEmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sửa Thông Tin Nhân Viên";
            this.grpEmployeeInfo.ResumeLayout(false);
            this.grpEmployeeInfo.PerformLayout();
            this.grpAccountInfo.ResumeLayout(false);
            this.grpAccountInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpEmployeeInfo;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.GroupBox grpAccountInfo;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
    }
} 