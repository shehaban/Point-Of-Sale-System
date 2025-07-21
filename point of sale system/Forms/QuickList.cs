using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class QuickList : Form
    {
        private ProductDAL productDAL = new ProductDAL();
        private DataTable products;
        private saleFrm mainSaleForm;

        public QuickList(saleFrm saleForm)
        {
            if (saleForm == null || saleForm.IsDisposed)
            {
                throw new ArgumentException("Main sale form must be provided and valid");
            }

            InitializeComponent();
            mainSaleForm = saleForm;
            LoadProducts();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Quick Product List";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new Size(1000, 635);
            this.BackColor = Color.WhiteSmoke;

            // Configure flow layout panel
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.Padding = new Padding(10);
        }

        private void LoadProducts()
        {
            try
            {
                products = productDAL.GetAllProducts();
                CreateProductButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void CreateProductButtons()
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in products.Rows)
            {
                Button productButton = new Button
                {
                    Text = $"{row["name"]}\n${Convert.ToDecimal(row["unit_price"]):0.00}",
                    Tag = row["id"],
                    Size = new Size(150, 90), // Slightly larger buttons
                    Margin = new Padding(8),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };

                // Button hover effects
                productButton.FlatAppearance.BorderSize = 0;
                productButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 130, 200);
                productButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 110, 180);

                // Center-align text
                productButton.TextAlign = ContentAlignment.MiddleCenter;

                productButton.Click += ProductButton_Click;
                flowLayoutPanel1.Controls.Add(productButton);
            }
        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int productId = (int)clickedButton.Tag;

            DataRow[] productRows = products.Select($"id = {productId}");
            if (productRows.Length > 0)
            {
                DataRow productRow = productRows[0];
                Product selectedProduct = new Product
                {
                    id = (int)productRow["id"],
                    name = productRow["name"].ToString(),
                    unit_price = Convert.ToDecimal(productRow["unit_price"])
                };

                mainSaleForm.SetSelectedProduct(selectedProduct);
                mainSaleForm.AddToCart(1);
                this.Close();
            }
        }
    }
}