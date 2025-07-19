using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class AddItem : Form
    {
        private readonly ProductDAL productDal = new ProductDAL();

        public AddItem()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input Validation
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

            try
            {
                // Only check non-deleted products
                Product existingProduct = productDal.GetProductByNameCategoryAndPrice(
                    txtName.Text.Trim(),
                    txtCategory.Text.Trim(),
                    sellPrice);

                if (existingProduct != null)
                {
                    // Update quantity of existing product
                    existingProduct.quantity += quantity;
                    if (productDal.UpdateInvProduct(existingProduct))
                    {
                        MessageBox.Show("Product quantity updated successfully", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    // Add new product
                    Product newProduct = new Product
                    {
                        name = txtName.Text.Trim(),
                        category = txtCategory.Text.Trim(),
                        unit_price = sellPrice,
                        purchase_price = purchasePrice,
                        quantity = quantity
                    };

                    int newProductId = productDal.AddProductWithInventory(newProduct);
                    if (newProductId > 0)
                    {
                        MessageBox.Show("New product added successfully", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
