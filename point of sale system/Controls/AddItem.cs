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
            // Clear any previous errors
            errorProvider1.Clear();

            bool hasError = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Please enter product name");
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                errorProvider1.SetError(txtCategory, "Please enter product category");
                hasError = true;
            }

            if (!decimal.TryParse(txtSellPrice.Text, out decimal sellPrice) || sellPrice <= 0)
            {
                errorProvider1.SetError(txtSellPrice, "Enter a valid sell price greater than 0");
                hasError = true;
            }

            if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchasePrice) || purchasePrice <= 0)
            {
                errorProvider1.SetError(txtPurchasePrice, "Enter a valid purchase price greater than 0");
                hasError = true;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                errorProvider1.SetError(txtQuantity, "Enter a valid quantity (0 or more)");
                hasError = true;
            }

            if (hasError)
                return;

            try
            {
                Product existingProduct = productDal.GetProductByNameCategoryAndPrice(
                    txtName.Text.Trim(),
                    txtCategory.Text.Trim(),
                    sellPrice);

                if (existingProduct != null)
                {
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
