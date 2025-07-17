namespace point_of_sale_system
{
    partial class saleMngFrm
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
            this.mngbtn = new System.Windows.Forms.Button();
            this.btnSalesMng = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.homebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mngbtn
            // 
            this.mngbtn.BackColor = System.Drawing.Color.AntiqueWhite;
            this.mngbtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mngbtn.Location = new System.Drawing.Point(12, 442);
            this.mngbtn.Name = "mngbtn";
            this.mngbtn.Size = new System.Drawing.Size(184, 95);
            this.mngbtn.TabIndex = 9;
            this.mngbtn.Text = "Manage Password";
            this.mngbtn.UseVisualStyleBackColor = false;
            this.mngbtn.Click += new System.EventHandler(this.mngbtn_Click);
            // 
            // btnSalesMng
            // 
            this.btnSalesMng.BackColor = System.Drawing.Color.Lavender;
            this.btnSalesMng.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalesMng.Location = new System.Drawing.Point(12, 294);
            this.btnSalesMng.Name = "btnSalesMng";
            this.btnSalesMng.Size = new System.Drawing.Size(184, 98);
            this.btnSalesMng.TabIndex = 8;
            this.btnSalesMng.Text = "Sales Report";
            this.btnSalesMng.UseVisualStyleBackColor = false;
            this.btnSalesMng.Click += new System.EventHandler(this.btnSalesMng_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.PaleGreen;
            this.btnInventory.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.Location = new System.Drawing.Point(12, 150);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(184, 98);
            this.btnInventory.TabIndex = 7;
            this.btnInventory.Text = "Product Mangment";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(128)))), ((int)(((byte)(175)))));
            this.btnProducts.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProducts.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnProducts.Location = new System.Drawing.Point(12, 12);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(184, 98);
            this.btnProducts.TabIndex = 6;
            this.btnProducts.Text = "Inventory";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click_1);
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(202, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1059, 697);
            this.mainPanel.TabIndex = 10;
            // 
            // homebtn
            // 
            this.homebtn.BackColor = System.Drawing.Color.Red;
            this.homebtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homebtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.homebtn.Location = new System.Drawing.Point(12, 583);
            this.homebtn.Name = "homebtn";
            this.homebtn.Size = new System.Drawing.Size(184, 95);
            this.homebtn.TabIndex = 11;
            this.homebtn.Text = "Home Page";
            this.homebtn.UseVisualStyleBackColor = false;
            this.homebtn.Click += new System.EventHandler(this.homebtn_Click);
            // 
            // saleMngFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 721);
            this.Controls.Add(this.homebtn);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.mngbtn);
            this.Controls.Add(this.btnSalesMng);
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.btnProducts);
            this.Name = "saleMngFrm";
            this.Text = "saleMngFrm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mngbtn;
        private System.Windows.Forms.Button btnSalesMng;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button homebtn;
    }
}