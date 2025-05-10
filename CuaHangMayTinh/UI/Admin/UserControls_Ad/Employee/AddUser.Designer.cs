namespace CuaHangMayTinh.UI.Forms
{
    partial class EmployeeRegistrationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ErrorProvider errorProviderPhone;
        private System.Windows.Forms.ErrorProvider errorProviderEmail;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cboRole;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpEmployeeInfo = new System.Windows.Forms.GroupBox();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.grpAccountInfo = new System.Windows.Forms.GroupBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProviderPhone = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderEmail = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.grpEmployeeInfo.SuspendLayout();
            this.grpAccountInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPhone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(180, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(227, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông Tin Nhân Viên";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click_1);
            // 
            // grpEmployeeInfo
            // 
            this.grpEmployeeInfo.Controls.Add(this.lblBirthDate);
            this.grpEmployeeInfo.Controls.Add(this.dtpBirthDate);
            this.grpEmployeeInfo.Controls.Add(this.lblEmail);
            this.grpEmployeeInfo.Controls.Add(this.txtEmail);
            this.grpEmployeeInfo.Controls.Add(this.lblPhone);
            this.grpEmployeeInfo.Controls.Add(this.txtPhone);
            this.grpEmployeeInfo.Controls.Add(this.lblAddress);
            this.grpEmployeeInfo.Controls.Add(this.txtAddress);
            this.grpEmployeeInfo.Controls.Add(this.lblFullName);
            this.grpEmployeeInfo.Controls.Add(this.txtFullName);
            this.grpEmployeeInfo.Location = new System.Drawing.Point(12, 60);
            this.grpEmployeeInfo.Name = "grpEmployeeInfo";
            this.grpEmployeeInfo.Size = new System.Drawing.Size(550, 230);
            this.grpEmployeeInfo.TabIndex = 1;
            this.grpEmployeeInfo.TabStop = false;
            this.grpEmployeeInfo.Text = "Thông Tin Cá Nhân";
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(20, 203);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(57, 13);
            this.lblBirthDate.TabIndex = 11;
            this.lblBirthDate.Text = "Ngày Sinh:";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(150, 200);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(120, 20);
            this.dtpBirthDate.TabIndex = 10;
            this.dtpBirthDate.ValueChanged += new System.EventHandler(this.dtpBirthDate_ValueChanged);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 173);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(35, 13);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(150, 170);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 8;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 143);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 13);
            this.lblPhone.TabIndex = 7;
            this.lblPhone.Text = "Số Điện Thoại:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(150, 140);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            this.txtPhone.TabIndex = 6;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(20, 93);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 5;
            this.lblAddress.Text = "Địa Chỉ:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(150, 90);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(350, 40);
            this.txtAddress.TabIndex = 4;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(20, 63);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(57, 13);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "Họ và Tên:";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(150, 60);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(350, 20);
            this.txtFullName.TabIndex = 2;
            this.txtFullName.TextChanged += new System.EventHandler(this.txtFullName_TextChanged);
            // 
            // grpAccountInfo
            // 
            this.grpAccountInfo.Controls.Add(this.lblConfirmPassword);
            this.grpAccountInfo.Controls.Add(this.txtConfirmPassword);
            this.grpAccountInfo.Controls.Add(this.lblPassword);
            this.grpAccountInfo.Controls.Add(this.txtPassword);
            this.grpAccountInfo.Controls.Add(this.lblUsername);
            this.grpAccountInfo.Controls.Add(this.txtUsername);
            this.grpAccountInfo.Controls.Add(this.lblRole);
            this.grpAccountInfo.Controls.Add(this.cboRole);
            this.grpAccountInfo.Location = new System.Drawing.Point(12, 300);
            this.grpAccountInfo.Name = "grpAccountInfo";
            this.grpAccountInfo.Size = new System.Drawing.Size(550, 150);
            this.grpAccountInfo.TabIndex = 2;
            this.grpAccountInfo.TabStop = false;
            this.grpAccountInfo.Text = "Thông Tin Tài Khoản";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(20, 93);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(94, 13);
            this.lblConfirmPassword.TabIndex = 5;
            this.lblConfirmPassword.Text = "Xác Nhận Mật Khẩu:";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(150, 90);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(200, 20);
            this.txtConfirmPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 63);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Mật Khẩu:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(20, 33);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Tên Đăng Nhập:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 30);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(20, 120);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(49, 13);
            this.lblRole.TabIndex = 12;
            this.lblRole.Text = "Vai trò:";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.Clear();
            this.cboRole.Items.AddRange(new object[] { "admin", "staff", "warehouse" });
            this.cboRole.Location = new System.Drawing.Point(150, 117);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(200, 21);
            this.cboRole.TabIndex = 13;
            this.cboRole.SelectedIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(370, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Thêm";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(470, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
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
            this.panel1.Location = new System.Drawing.Point(0, 460);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 60);
            this.panel1.TabIndex = 3;
            // 
            // EmployeeRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 520);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpAccountInfo);
            this.Controls.Add(this.grpEmployeeInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Nhân Viên";
            this.Load += new System.EventHandler(this.EmployeeRegistrationForm_Load);
            this.grpEmployeeInfo.ResumeLayout(false);
            this.grpEmployeeInfo.PerformLayout();
            this.grpAccountInfo.ResumeLayout(false);
            this.grpAccountInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPhone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderEmail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}