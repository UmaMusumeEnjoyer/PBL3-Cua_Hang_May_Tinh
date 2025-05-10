namespace CuaHangMayTinh.UI.Form_Ad
{
    partial class FormAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.panelHome = new System.Windows.Forms.Panel();
            this.PanelControls = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.btnThietbi = new System.Windows.Forms.Button();
            this.btnNhacungcap = new System.Windows.Forms.Button();
            this.btnHoadonnhap = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.panelSide = new System.Windows.Forms.Panel();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.panelHome.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHome
            // 
            this.panelHome.Controls.Add(this.PanelControls);
            this.panelHome.Controls.Add(this.panel2);
            this.panelHome.Controls.Add(this.panel4);
            this.panelHome.Controls.Add(this.panelLeft);
            this.panelHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHome.Location = new System.Drawing.Point(0, 0);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(1170, 645);
            this.panelHome.TabIndex = 3;
            this.panelHome.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControls_Paint);
            // 
            // PanelControls
            // 
            this.PanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControls.Location = new System.Drawing.Point(215, 142);
            this.PanelControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PanelControls.Name = "PanelControls";
            this.PanelControls.Size = new System.Drawing.Size(955, 503);
            this.PanelControls.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.labelTime);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(215, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(955, 92);
            this.panel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(166, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(166, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Admin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(110, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Role:";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(804, 26);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(83, 19);
            this.labelTime.TabIndex = 0;
            this.labelTime.Text = "HH:MM:SS";
            this.labelTime.Click += new System.EventHandler(this.labelTime_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(69, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xin chào:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gray;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.button9);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(215, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(955, 50);
            this.panel4.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Quản Lý Cửa Hàng";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button9
            // 
            this.button9.Dock = System.Windows.Forms.DockStyle.Right;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(911, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(44, 50);
            this.button9.TabIndex = 2;
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelLeft.Controls.Add(this.btnThietbi);
            this.panelLeft.Controls.Add(this.btnNhacungcap);
            this.panelLeft.Controls.Add(this.btnHoadonnhap);
            this.panelLeft.Controls.Add(this.btnThongKe);
            this.panelLeft.Controls.Add(this.panelSide);
            this.panelLeft.Controls.Add(this.btnUsers);
            this.panelLeft.Controls.Add(this.btnHome);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(215, 645);
            this.panelLeft.TabIndex = 1;
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLeft_Paint);
            // 
            // btnThietbi
            // 
            this.btnThietbi.FlatAppearance.BorderSize = 0;
            this.btnThietbi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThietbi.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThietbi.ForeColor = System.Drawing.Color.White;
            this.btnThietbi.Image = ((System.Drawing.Image)(resources.GetObject("btnThietbi.Image")));
            this.btnThietbi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThietbi.Location = new System.Drawing.Point(12, 274);
            this.btnThietbi.Name = "btnThietbi";
            this.btnThietbi.Size = new System.Drawing.Size(203, 60);
            this.btnThietbi.TabIndex = 6;
            this.btnThietbi.Text = "      Thiết Bị";
            this.btnThietbi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThietbi.UseVisualStyleBackColor = true;
            this.btnThietbi.Click += new System.EventHandler(this.btnThietbi_Click);
            // 
            // btnNhacungcap
            // 
            this.btnNhacungcap.FlatAppearance.BorderSize = 0;
            this.btnNhacungcap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhacungcap.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhacungcap.ForeColor = System.Drawing.Color.White;
            this.btnNhacungcap.Image = ((System.Drawing.Image)(resources.GetObject("btnNhacungcap.Image")));
            this.btnNhacungcap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhacungcap.Location = new System.Drawing.Point(12, 327);
            this.btnNhacungcap.Name = "btnNhacungcap";
            this.btnNhacungcap.Size = new System.Drawing.Size(203, 60);
            this.btnNhacungcap.TabIndex = 5;
            this.btnNhacungcap.Text = "    Nhà Cung Cấp";
            this.btnNhacungcap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNhacungcap.UseVisualStyleBackColor = true;
            this.btnNhacungcap.Click += new System.EventHandler(this.btnSupplier_Click);
            // 
            // btnHoadonnhap
            // 
            this.btnHoadonnhap.FlatAppearance.BorderSize = 0;
            this.btnHoadonnhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoadonnhap.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoadonnhap.ForeColor = System.Drawing.Color.White;
            this.btnHoadonnhap.Image = ((System.Drawing.Image)(resources.GetObject("btnHoadonnhap.Image")));
            this.btnHoadonnhap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHoadonnhap.Location = new System.Drawing.Point(12, 393);
            this.btnHoadonnhap.Name = "btnHoadonnhap";
            this.btnHoadonnhap.Size = new System.Drawing.Size(203, 60);
            this.btnHoadonnhap.TabIndex = 3;
            this.btnHoadonnhap.Text = "    Hóa Đơn Nhập";
            this.btnHoadonnhap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHoadonnhap.UseVisualStyleBackColor = true;
            this.btnHoadonnhap.Click += new System.EventHandler(this.btnDoanhThu_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.FlatAppearance.BorderSize = 0;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThongKe.Location = new System.Drawing.Point(12, 459);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(203, 60);
            this.btnThongKe.TabIndex = 2;
            this.btnThongKe.Text = "     Thống Kê";
            this.btnThongKe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnChiPhi_Click);
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.Color.White;
            this.panelSide.Location = new System.Drawing.Point(1, 142);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(7, 60);
            this.panelSide.TabIndex = 1;
            // 
            // btnUsers
            // 
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.ForeColor = System.Drawing.Color.White;
            this.btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnUsers.Image")));
            this.btnUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.Location = new System.Drawing.Point(9, 208);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(203, 60);
            this.btnUsers.TabIndex = 2;
            this.btnUsers.Text = "      Nhân Viên";
            this.btnUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnHome
            // 
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = ((System.Drawing.Image)(resources.GetObject("btnHome.Image")));
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(8, 142);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(203, 60);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "     Trang Chủ";
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(215, 140);
            this.panel3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(215, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 578);
            this.panel1.TabIndex = 2;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(173, 8);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(38, 29);
            this.button8.TabIndex = 2;
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cửa Hàng Diện Tử";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(78, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // timerTime
            // 
            this.timerTime.Interval = 1000;
            this.timerTime.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 645);
            this.Controls.Add(this.panelHome);
            this.Name = "FormAdmin";
            this.Text = "s";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelHome.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHome;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnNhacungcap;
        private System.Windows.Forms.Button btnHoadonnhap;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Panel panelSide;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThietbi;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Panel PanelControls;
    }
}