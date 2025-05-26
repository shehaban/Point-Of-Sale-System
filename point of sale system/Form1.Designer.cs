namespace point_of_sale_system
{
    partial class mainFrm
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
            this.Panel = new System.Windows.Forms.Panel();
            this.btnSaleMng = new System.Windows.Forms.Button();
            this.btnSale = new System.Windows.Forms.Button();
            this.cmboBoxUsers = new System.Windows.Forms.ComboBox();
            this.exitbtn = new System.Windows.Forms.Button();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.exitbtn);
            this.Panel.Controls.Add(this.btnSaleMng);
            this.Panel.Controls.Add(this.btnSale);
            this.Panel.Controls.Add(this.cmboBoxUsers);
            this.Panel.Location = new System.Drawing.Point(-1, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(615, 742);
            this.Panel.TabIndex = 0;
            // 
            // btnSaleMng
            // 
            this.btnSaleMng.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaleMng.Location = new System.Drawing.Point(35, 401);
            this.btnSaleMng.Name = "btnSaleMng";
            this.btnSaleMng.Size = new System.Drawing.Size(530, 180);
            this.btnSaleMng.TabIndex = 8;
            this.btnSaleMng.Text = "Sales Mangment";
            this.btnSaleMng.UseVisualStyleBackColor = true;
            this.btnSaleMng.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnSale
            // 
            this.btnSale.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSale.Location = new System.Drawing.Point(35, 151);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(530, 180);
            this.btnSale.TabIndex = 7;
            this.btnSale.Text = "Sale Form";
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // cmboBoxUsers
            // 
            this.cmboBoxUsers.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmboBoxUsers.FormattingEnabled = true;
            this.cmboBoxUsers.Items.AddRange(new object[] {
            "Admin",
            "Casheer"});
            this.cmboBoxUsers.Location = new System.Drawing.Point(184, 67);
            this.cmboBoxUsers.Name = "cmboBoxUsers";
            this.cmboBoxUsers.Size = new System.Drawing.Size(220, 29);
            this.cmboBoxUsers.TabIndex = 6;
            this.cmboBoxUsers.SelectedIndexChanged += new System.EventHandler(this.cmboBoxUsers_SelectedIndexChanged);
            // 
            // exitbtn
            // 
            this.exitbtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbtn.Location = new System.Drawing.Point(173, 608);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(256, 105);
            this.exitbtn.TabIndex = 9;
            this.exitbtn.Text = "Exit";
            this.exitbtn.UseVisualStyleBackColor = true;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 757);
            this.Controls.Add(this.Panel);
            this.Name = "mainFrm";
            this.Text = "Form1";
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Button btnSaleMng;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.ComboBox cmboBoxUsers;
        private System.Windows.Forms.Button exitbtn;
    }
}

