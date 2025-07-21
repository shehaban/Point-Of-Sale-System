using point_of_sale_system.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class InvoiceForm : Form
    {
        private DataTable invoiceItems;
        private decimal totalAmount;
        private int invoiceNumber;

        public InvoiceForm(DataTable items, decimal total, int invoiceNo)
        {
            InitializeComponent();
            this.invoiceItems = items;
            this.totalAmount = total;
            this.invoiceNumber = invoiceNo;
            InitializeData();
        }

        private void InitializeData()
        {
            invoiceDataGridView.DataSource = invoiceItems;
            lblInvoiceNumber.Text = $"Invoice #: {invoiceNumber}";

            // ✅ التاريخ والوقت الآن بالتنسيق الكامل
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");

            lblTotal.Text = $"Total: ${totalAmount:0.00}";

            if (invoiceDataGridView.Columns.Contains("ProductID"))
                invoiceDataGridView.Columns["ProductID"].Visible = false;
        }

        private void ExportInvoiceAsPng()
        {
            try
            {
                using (Bitmap bmp = new Bitmap(this.Width, this.Height))
                {
                    this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    using (SaveFileDialog saveDialog = new SaveFileDialog())
                    {
                        saveDialog.Title = "Save Invoice As PNG";
                        saveDialog.Filter = "PNG Image|*.png";
                        saveDialog.FileName = $"Invoice_{invoiceNumber}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

                        if (saveDialog.ShowDialog() == DialogResult.OK)
                        {
                            bmp.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);

                            MessageBox.Show("Invoice saved successfully!", "Export Done",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export invoice:\n{ex.Message}", "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnCash_Click(object sender, EventArgs e)
        {
            try
            {
                SaleDAL saleDal = new SaleDAL();
                foreach (DataRow row in invoiceItems.Rows)
                {
                    saleDal.AddSale(new Models.Sale
                    {
                        InvoiceId = invoiceNumber,
                        ProductId = (int)row["ProductID"],
                        QuantitySold = (int)row["Quantity"],
                        TotalPrice = (decimal)row["TotalPrice"],
                        SaleDate = DateTime.Now
                    });


                }
                if (exportBox.Checked)
                {
                    ExportInvoiceAsPng();
                }
                MessageBox.Show("Payment processed successfully!", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void exportBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
