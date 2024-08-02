namespace POSales
{
    partial class SupplierModule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupplierModule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.lblSupplierModule = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFaxNumber = new System.Windows.Forms.Label();
            this.txtFaxNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.picClose);
            this.panel1.Controls.Add(this.lblSupplierModule);
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
            // lblSupplierModule
            // 
            this.lblSupplierModule.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierModule.Image = ((System.Drawing.Image)(resources.GetObject("lblSupplierModule.Image")));
            this.lblSupplierModule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSupplierModule.Location = new System.Drawing.Point(12, 12);
            this.lblSupplierModule.Margin = new System.Windows.Forms.Padding(0);
            this.lblSupplierModule.Name = "lblSupplierModule";
            this.lblSupplierModule.Size = new System.Drawing.Size(209, 23);
            this.lblSupplierModule.TabIndex = 0;
            this.lblSupplierModule.Text = "New Supplier";
            this.lblSupplierModule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(160)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(432, 428);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.SeaGreen;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(569, 428);
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
            this.btnCancel.Location = new System.Drawing.Point(706, 428);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 40);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierName.Location = new System.Drawing.Point(34, 88);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(139, 22);
            this.lblSupplierName.TabIndex = 11;
            this.lblSupplierName.Text = "Supplier Name";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Location = new System.Drawing.Point(212, 84);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(598, 30);
            this.txtSupplierName.TabIndex = 10;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(34, 151);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(81, 22);
            this.lblAddress.TabIndex = 13;
            this.lblAddress.Text = "Address";
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactPerson.Location = new System.Drawing.Point(34, 214);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(145, 22);
            this.lblContactPerson.TabIndex = 15;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(212, 147);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(598, 30);
            this.txtAddress.TabIndex = 24;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(212, 210);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(598, 30);
            this.txtContactPerson.TabIndex = 25;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(212, 272);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(598, 30);
            this.txtEmail.TabIndex = 27;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(34, 276);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(56, 22);
            this.lblEmail.TabIndex = 26;
            this.lblEmail.Text = "Email";
            // 
            // lblFaxNumber
            // 
            this.lblFaxNumber.AutoSize = true;
            this.lblFaxNumber.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaxNumber.Location = new System.Drawing.Point(455, 342);
            this.lblFaxNumber.Name = "lblFaxNumber";
            this.lblFaxNumber.Size = new System.Drawing.Size(117, 22);
            this.lblFaxNumber.TabIndex = 31;
            this.lblFaxNumber.Text = "Fax Number";
            // 
            // txtFaxNumber
            // 
            this.txtFaxNumber.Location = new System.Drawing.Point(595, 338);
            this.txtFaxNumber.Name = "txtFaxNumber";
            this.txtFaxNumber.Size = new System.Drawing.Size(215, 30);
            this.txtFaxNumber.TabIndex = 30;
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneNumber.Location = new System.Drawing.Point(34, 342);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(142, 22);
            this.lblPhoneNumber.TabIndex = 29;
            this.lblPhoneNumber.Text = "Phone Number";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(212, 338);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(215, 30);
            this.txtPhoneNumber.TabIndex = 28;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(34, 427);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(95, 21);
            this.lblId.TabIndex = 32;
            this.lblId.Text = "supplier id";
            this.lblId.Visible = false;
            // 
            // SupplierModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(850, 490);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.lblFaxNumber);
            this.Controls.Add(this.txtFaxNumber);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblContactPerson);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblSupplierName);
            this.Controls.Add(this.txtSupplierName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SupplierModule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupplierModule";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Label lblSupplierModule;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSupplierName;
        public System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblContactPerson;
        public System.Windows.Forms.TextBox txtAddress;
        public System.Windows.Forms.TextBox txtContactPerson;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFaxNumber;
        public System.Windows.Forms.TextBox txtFaxNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        public System.Windows.Forms.TextBox txtPhoneNumber;
        public System.Windows.Forms.Label lblId;
    }
}