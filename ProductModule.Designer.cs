namespace POSales
{
    partial class ProductModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.lblBrandModule = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.lblCategoryName = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboBrand = new System.Windows.Forms.ComboBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.nupQuantity = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nupReOrder = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupReOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.picClose);
            this.panel1.Controls.Add(this.lblBrandModule);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 48);
            this.panel1.TabIndex = 0;
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(811, 9);
            this.picClose.Margin = new System.Windows.Forms.Padding(0);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(30, 30);
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // lblBrandModule
            // 
            this.lblBrandModule.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandModule.Image = ((System.Drawing.Image)(resources.GetObject("lblBrandModule.Image")));
            this.lblBrandModule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblBrandModule.Location = new System.Drawing.Point(12, 12);
            this.lblBrandModule.Margin = new System.Windows.Forms.Padding(0);
            this.lblBrandModule.Name = "lblBrandModule";
            this.lblBrandModule.Size = new System.Drawing.Size(209, 23);
            this.lblBrandModule.TabIndex = 0;
            this.lblBrandModule.Text = "New Product";
            this.lblBrandModule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(432, 478);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtProductCode
            // 
            this.txtProductCode.Location = new System.Drawing.Point(199, 82);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(215, 30);
            this.txtProductCode.TabIndex = 2;
            // 
            // lblCategoryName
            // 
            this.lblCategoryName.AutoSize = true;
            this.lblCategoryName.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoryName.Location = new System.Drawing.Point(34, 86);
            this.lblCategoryName.Name = "lblCategoryName";
            this.lblCategoryName.Size = new System.Drawing.Size(133, 22);
            this.lblCategoryName.TabIndex = 3;
            this.lblCategoryName.Text = "Product Code";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.SeaGreen;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(569, 478);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(104, 40);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(706, 478);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(34, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "Descripton";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(199, 271);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(611, 30);
            this.txtDescription.TabIndex = 10;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrand.Location = new System.Drawing.Point(34, 334);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(61, 22);
            this.lblBrand.TabIndex = 13;
            this.lblBrand.Text = "Brand";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(34, 395);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(93, 22);
            this.lblCategory.TabIndex = 15;
            this.lblCategory.Text = "Category";
            // 
            // cboBrand
            // 
            this.cboBrand.FormattingEnabled = true;
            this.cboBrand.Location = new System.Drawing.Point(199, 333);
            this.cboBrand.Name = "cboBrand";
            this.cboBrand.Size = new System.Drawing.Size(611, 29);
            this.cboBrand.TabIndex = 16;
            // 
            // cboCategory
            // 
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(199, 394);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(611, 29);
            this.cboCategory.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(462, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 22);
            this.label1.TabIndex = 23;
            this.label1.Text = "Barcode";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(595, 82);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(215, 30);
            this.txtBarcode.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 22);
            this.label4.TabIndex = 25;
            this.label4.Text = "Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(199, 146);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(215, 30);
            this.txtPrice.TabIndex = 24;
            // 
            // nupQuantity
            // 
            this.nupQuantity.Location = new System.Drawing.Point(595, 210);
            this.nupQuantity.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupQuantity.Name = "nupQuantity";
            this.nupQuantity.Size = new System.Drawing.Size(215, 30);
            this.nupQuantity.TabIndex = 32;
            this.nupQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(462, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 22);
            this.label5.TabIndex = 31;
            this.label5.Text = "Quantity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(462, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 22);
            this.label6.TabIndex = 30;
            this.label6.Text = "Weight";
            // 
            // nupReOrder
            // 
            this.nupReOrder.Location = new System.Drawing.Point(199, 210);
            this.nupReOrder.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupReOrder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupReOrder.Name = "nupReOrder";
            this.nupReOrder.Size = new System.Drawing.Size(215, 30);
            this.nupReOrder.TabIndex = 35;
            this.nupReOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupReOrder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 22);
            this.label3.TabIndex = 34;
            this.label3.Text = "Re-Order";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(595, 148);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(215, 30);
            this.txtWeight.TabIndex = 36;
            // 
            // ProductModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(850, 540);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.nupReOrder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nupQuantity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.cboBrand);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblCategoryName);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProductModule";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupReOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label lblBrandModule;
        private System.Windows.Forms.Label lblCategoryName;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.TextBox txtProductCode;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cboBrand;
        public System.Windows.Forms.ComboBox cboCategory;
        public System.Windows.Forms.NumericUpDown nupQuantity;
        public System.Windows.Forms.NumericUpDown nupReOrder;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtWeight;
    }
}