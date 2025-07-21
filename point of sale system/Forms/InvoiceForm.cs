using point_of_sale_system.DAL;
using System;
using System.Data;
<<<<<<< HEAD
using System.Drawing;
=======
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
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
<<<<<<< HEAD
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
=======
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac

            lblTotal.Text = $"Total: ${totalAmount:0.00}";

            if (invoiceDataGridView.Columns.Contains("ProductID"))
                invoiceDataGridView.Columns["ProductID"].Visible = false;
        }

<<<<<<< HEAD
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



=======
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac
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

<<<<<<< HEAD

                }
                if (exportBox.Checked)
                {
                    ExportInvoiceAsPng();
                }
                MessageBox.Show("Payment processed successfully!", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
=======
                }

                MessageBox.Show("Payment processed successfully!", "Done",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
>>>>>>> 9750bad2b4b58b64229bf1f9bf5c2122d8096bac

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
