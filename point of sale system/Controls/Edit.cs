using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class Edit : Form
    {
        private readonly ProductDAL productDal = new ProductDAL();
        private Product product;

        // Add parameterless constructor for designer
        public Edit()
        {
            InitializeComponent(); // Required for Windows Forms designer
        }

        internal Edit(Product productToEdit) : this()
        {
            if (productToEdit == null)
            {
                throw new ArgumentNullException(nameof(productToEdit));
            }

            this.product = productToEdit;
            LoadProductData();
        }

        private void LoadProductData()
        {
            txtName.Text = product.name;
            txtCategory.Text = product.category;
            txtSellPrice.Text = product.unit_price.ToString("0.00");
            txtPurchasePrice.Text = product.purchase_price.ToString("0.00");
            txtQuantity.Text = product.quantity.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter product name", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Please enter product category", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return;
            }

            if (!decimal.TryParse(txtSellPrice.Text, out decimal sellPrice) || sellPrice <= 0)
            {
                MessageBox.Show("Please enter a valid sell price", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSellPrice.Focus();
                return;
            }

            if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchasePrice) || purchasePrice <= 0)
            {
                MessageBox.Show("Please enter a valid purchase price", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPurchasePrice.Focus();
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid quantity", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            // Update product
            product.name = txtName.Text.Trim();
            product.category = txtCategory.Text.Trim();
            product.unit_price = sellPrice;
            product.purchase_price = purchasePrice;
            product.quantity = quantity;

            if (productDal.UpdateProduct(product))
            {
                MessageBox.Show("Product updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to update product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}