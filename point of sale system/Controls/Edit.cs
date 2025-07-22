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

        public Edit()
        {
            InitializeComponent();
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

            // إزالة الأخطاء القديمة عند تحميل البيانات
            errorProvider1.SetError(txtName, "");
            errorProvider1.SetError(txtCategory, "");
            errorProvider1.SetError(txtSellPrice, "");
            errorProvider1.SetError(txtPurchasePrice, "");
            errorProvider1.SetError(txtQuantity, "");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            // Validate Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "Please enter product name");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtName, "");
            }

            // Validate Category
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                errorProvider1.SetError(txtCategory, "Please enter product category");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtCategory, "");
            }

            // Validate Sell Price
            if (!decimal.TryParse(txtSellPrice.Text, out decimal sellPrice) || sellPrice <= 0)
            {
                errorProvider1.SetError(txtSellPrice, "Please enter a valid sell price");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtSellPrice, "");
            }

            // Validate Purchase Price
            if (!decimal.TryParse(txtPurchasePrice.Text, out decimal purchasePrice) || purchasePrice <= 0)
            {
                errorProvider1.SetError(txtPurchasePrice, "Please enter a valid purchase price");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtPurchasePrice, "");
            }

            // Validate Quantity
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                errorProvider1.SetError(txtQuantity, "Please enter a valid quantity");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(txtQuantity, "");
            }

            if (!isValid)
            {
                // إذا كان هناك خطأ فلا تنفذ عملية الحفظ
                return;
            }

            // تحديث بيانات المنتج
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
