namespace CuaHangMayTinh.UI.Admin.UserControls_Ad.ProductManager
{
    partial class AccessoriesManager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessoriesManager));
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.addThietbi = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IdLaptop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namelaptop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Specification = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(260, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 31);
            this.button1.TabIndex = 92;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(920, 30);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(127, 31);
            this.button5.TabIndex = 91;
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(700, 30);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(127, 31);
            this.button6.TabIndex = 90;
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // addThietbi
            // 
            this.addThietbi.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addThietbi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addThietbi.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addThietbi.Image = ((System.Drawing.Image)(resources.GetObject("addThietbi.Image")));
            this.addThietbi.Location = new System.Drawing.Point(480, 30);
            this.addThietbi.Name = "addThietbi";
            this.addThietbi.Size = new System.Drawing.Size(127, 31);
            this.addThietbi.TabIndex = 89;
            this.addThietbi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addThietbi.UseVisualStyleBackColor = true;
            this.addThietbi.Click += new System.EventHandler(this.addThietbi_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdLaptop,
            this.Namelaptop,
            this.Specification,
            this.Price,
            this.SL});
            this.dataGridView1.Location = new System.Drawing.Point(50, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1200, 650);
            this.dataGridView1.TabIndex = 88;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // IdLaptop
            // 
            this.IdLaptop.HeaderText = "Mã thiết bị";
            this.IdLaptop.Name = "IdLaptop";
            // 
            // Namelaptop
            // 
            this.Namelaptop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Namelaptop.HeaderText = "Tên ";
            this.Namelaptop.Name = "Namelaptop";
            // 
            // Specification
            // 
            this.Specification.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Specification.HeaderText = "Đặc biệt";
            this.Specification.Name = "Specification";
            // 
            // Price
            // 
            this.Price.HeaderText = "Giá";
            this.Price.Name = "Price";
            // 
            // SL
            // 
            this.SL.HeaderText = "Số Lượng";
            this.SL.Name = "SL";
            // 
            // AccessoriesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.addThietbi);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AccessoriesManager";
            this.Size = new System.Drawing.Size(1300, 800);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button addThietbi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdLaptop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namelaptop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Specification;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
    }
}
