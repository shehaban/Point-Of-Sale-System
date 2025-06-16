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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.exitbtn = new System.Windows.Forms.Button();
            this.btnSaleMng = new System.Windows.Forms.Button();
            this.btnSale = new System.Windows.Forms.Button();
            this.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.BackgroundImage = global::point_of_sale_system.Properties.Resources.POS_Software_Pricing3;
            this.Panel.Controls.Add(this.pictureBox1);
            this.Panel.Controls.Add(this.exitbtn);
            this.Panel.Controls.Add(this.btnSaleMng);
            this.Panel.Controls.Add(this.btnSale);
            this.Panel.Location = new System.Drawing.Point(-1, 12);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(618, 742);
            this.Panel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::point_of_sale_system.Properties.Resources.POS_Software__1_2;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-178, 237);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(772, 401);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // exitbtn
            // 
            this.exitbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.exitbtn.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitbtn.Location = new System.Drawing.Point(173, 644);
            this.exitbtn.Name = "exitbtn";
            this.exitbtn.Size = new System.Drawing.Size(256, 69);
            this.exitbtn.TabIndex = 9;
            this.exitbtn.Text = "Exit";
            this.exitbtn.UseVisualStyleBackColor = false;
            this.exitbtn.Click += new System.EventHandler(this.exitbtn_Click);
            // 
            // btnSaleMng
            // 
            this.btnSaleMng.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSaleMng.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaleMng.Location = new System.Drawing.Point(353, 114);
            this.btnSaleMng.Name = "btnSaleMng";
            this.btnSaleMng.Size = new System.Drawing.Size(228, 117);
            this.btnSaleMng.TabIndex = 8;
            this.btnSaleMng.Text = "Sales Mangment";
            this.btnSaleMng.UseVisualStyleBackColor = false;
            this.btnSaleMng.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnSale
            // 
            this.btnSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSale.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSale.Location = new System.Drawing.Point(39, 114);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(228, 117);
            this.btnSale.TabIndex = 7;
            this.btnSale.Text = "Sale Form";
            this.btnSale.UseVisualStyleBackColor = false;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 757);
            this.Controls.Add(this.Panel);
            this.Name = "mainFrm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Button btnSaleMng;
        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button exitbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

