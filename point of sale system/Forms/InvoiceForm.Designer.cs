namespace point_of_sale_system.Forms
{
    partial class InvoiceForm
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
            this.lblTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.invoiceDataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
<<<<<<< HEAD
            this.exportBox = new System.Windows.Forms.CheckBox();
=======
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
<<<<<<< HEAD
            this.lblTotal.Location = new System.Drawing.Point(285, 557);
=======
            this.lblTotal.Location = new System.Drawing.Point(325, 557);
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 24);
            this.lblTotal.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(165, 553);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 28);
            this.label4.TabIndex = 22;
            this.label4.Text = "Total DL :";
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.AutoSize = true;
            this.lblInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
<<<<<<< HEAD
            this.lblInvoiceNumber.Location = new System.Drawing.Point(227, 193);
=======
            this.lblInvoiceNumber.Location = new System.Drawing.Point(332, 187);
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(0, 22);
            this.lblInvoiceNumber.TabIndex = 21;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
<<<<<<< HEAD
            this.lblDate.Location = new System.Drawing.Point(162, 135);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(265, 38);
=======
            this.lblDate.Location = new System.Drawing.Point(247, 135);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(137, 38);
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            this.lblDate.TabIndex = 20;
            // 
            // invoiceDataGridView
            // 
            this.invoiceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
<<<<<<< HEAD
            this.invoiceDataGridView.Location = new System.Drawing.Point(100, 268);
            this.invoiceDataGridView.Name = "invoiceDataGridView";
            this.invoiceDataGridView.RowHeadersWidth = 51;
            this.invoiceDataGridView.RowTemplate.Height = 26;
            this.invoiceDataGridView.Size = new System.Drawing.Size(388, 259);
=======
            this.invoiceDataGridView.Location = new System.Drawing.Point(100, 252);
            this.invoiceDataGridView.Name = "invoiceDataGridView";
            this.invoiceDataGridView.RowHeadersWidth = 51;
            this.invoiceDataGridView.RowTemplate.Height = 26;
            this.invoiceDataGridView.Size = new System.Drawing.Size(388, 275);
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            this.invoiceDataGridView.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(101, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 28);
            this.label3.TabIndex = 18;
            this.label3.Text = "Invoice NO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(101, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 28);
            this.label2.TabIndex = 17;
            this.label2.Text = "Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.RoyalBlue;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label7.Location = new System.Drawing.Point(19, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 50);
            this.label7.TabIndex = 62;
            this.label7.Text = "Invoice";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(-18, -23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(702, 107);
            this.label1.TabIndex = 61;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(303, 600);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 62);
            this.button1.TabIndex = 63;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAdd.Location = new System.Drawing.Point(117, 600);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(169, 62);
            this.btnAdd.TabIndex = 64;
            this.btnAdd.Text = "Cash";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnCash_Click);
<<<<<<< HEAD
            // 
            // exportBox
            // 
            this.exportBox.AutoSize = true;
            this.exportBox.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBox.Location = new System.Drawing.Point(105, 227);
            this.exportBox.Name = "exportBox";
            this.exportBox.Size = new System.Drawing.Size(180, 35);
            this.exportBox.TabIndex = 65;
            this.exportBox.Text = "Export invoice";
            this.exportBox.UseVisualStyleBackColor = true;
            this.exportBox.CheckedChanged += new System.EventHandler(this.exportBox_CheckedChanged);
=======
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 674);
            this.Controls.Add(this.exportBox);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInvoiceNumber);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.invoiceDataGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "InvoiceForm";
            this.Text = "InvoiceForm";
            ((System.ComponentModel.ISupportInitialize)(this.invoiceDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DataGridView invoiceDataGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox exportBox;
    }
}