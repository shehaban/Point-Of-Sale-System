using point_of_sale_system.DAL;
using point_of_sale_system.Forms;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class saleFrm : Form
    {
        private ProductDAL productDAL = new ProductDAL();
        private SaleDAL saleDAL = new SaleDAL();
        private InvoiceDAL invoiceDAL = new InvoiceDAL();
        private DataTable cart = new DataTable();
        private DataTable products = new DataTable();
        private int currentInvoiceId = 0;
        private Product selectedProduct = null;
        private int currentUserId = 1;
        private int currentCartRowIndex = -1;
        private bool isUpdatingCart = false;
        private SaleInfo saleInfo;

        //public saleFrm()
        //{
        //    InitializeComponent();
        //    WireUpButtonEvents();
        //    InitializeDataGridViews();

        //    dataGridViewCart.DataSource = cart;
        //    this.WindowState = FormWindowState.Maximized;
        //    this.FormBorderStyle = FormBorderStyle.None;

        //    try
        //    {
        //        currentInvoiceId = invoiceDAL.CreateInvoice(0, currentUserId);
        //        UpdateTotalPrice();
        //        LoadAllProducts();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to initialize invoice: {ex.Message}\n\n" +
        //                      "Please check your database connection and try again.",
        //                      "Initialization Error",
        //                      MessageBoxButtons.OK,
        //                      MessageBoxIcon.Error);
        //        this.Close();
        //    }
        //}

        public saleFrm(SaleInfo saleInfo)
        {

            this.saleInfo = saleInfo;
            InitializeComponent();
            WireUpButtonEvents();
            InitializeDataGridViews();

            dataGridViewCart.DataSource = cart;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            try
            {
                currentInvoiceId = invoiceDAL.CreateInvoice(0, currentUserId);
                UpdateTotalPrice();
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize invoice: {ex.Message}\n\n" +
                              "Please check your database connection and try again.",
                              "Initialization Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                this.Close();
            }
        }

        // In the InitializeDataGridViews() method, add these styling properties:
        private void InitializeDataGridViews()
        {
            // Initialize cart DataTable structure
            cart.Columns.Add("ProductID", typeof(int));
            cart.Columns.Add("Name", typeof(string));
            cart.Columns.Add("Quantity", typeof(int));
            cart.Columns.Add("UnitPrice", typeof(decimal));
            cart.Columns.Add("TotalPrice", typeof(decimal));

            //dataGridViewCart.Columns["UnitPrice"].DefaultCellStyle.Format = "0.00";
            //dataGridViewCart.Columns["TotalPrice"].DefaultCellStyle.Format = "0.00";

            //dataGridViewCart.Columns["UnitPrice"].ValueType = typeof(decimal);
            //dataGridViewCart.Columns["TotalPrice"].ValueType = typeof(decimal);

            dataGridViewCart.DataSource = cart;

            // Styling for ProductsDataGridView (product list)
            ProductsDataGridView.AutoGenerateColumns = true;
            ProductsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ProductsDataGridView.MultiSelect = false;
            ProductsDataGridView.AllowUserToAddRows = false;
            ProductsDataGridView.SelectionChanged += ProductsDataGridView_SelectionChanged;

            // Apply styling similar to ProductMng
            ProductsDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            ProductsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            ProductsDataGridView.RowTemplate.Height = 35;
            ProductsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ProductsDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            ProductsDataGridView.EnableHeadersVisualStyles = false;
            ProductsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            ProductsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Styling for dataGridViewCart (cart)
            dataGridViewCart.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            dataGridViewCart.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridViewCart.RowTemplate.Height = 35;
            dataGridViewCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCart.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewCart.EnableHeadersVisualStyles = false;
            dataGridViewCart.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewCart.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCart.SelectionChanged += dataGridViewCart_SelectionChanged;
        }

        private void WireUpButtonEvents()
        {
            btn0.Click += (s, e) => AppendToQuantity("0");
            btn1.Click += (s, e) => AppendToQuantity("1");
            btn2.Click += (s, e) => AppendToQuantity("2");
            btn3.Click += (s, e) => AppendToQuantity("3");
            btn4.Click += (s, e) => AppendToQuantity("4");
            btn5.Click += (s, e) => AppendToQuantity("5");
            btn6.Click += (s, e) => AppendToQuantity("6");
            btn7.Click += (s, e) => AppendToQuantity("7");
            btn8.Click += (s, e) => AppendToQuantity("8");
            btn9.Click += (s, e) => AppendToQuantity("9");

            button1.Click += (s, e) => txtQuantity.Text = "";
            button11.Click += (s, e) => AddToCart(1);
            button10.Click += (s, e) => AddToCart(12);
            btnSearch.Click += SearchButton_Click;
            button17.Click += NewInvoiceButton_Click;
            button18.Click += ReturnItemButton_Click;
            button13.Click += ShowInvoiceForm;
            button2.Click += (s, e) => new QuickList(this).ShowDialog();

            btnUp.Click += UpButton_Click;
            btnDown.Click += DownButton_Click;
            btnRemove.Click += RemoveButton_Click;
        }

        private void ProductsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ProductsDataGridView.SelectedRows.Count > 0 && !isUpdatingCart)
            {
                DataGridViewRow row = ProductsDataGridView.SelectedRows[0];
                selectedProduct = new Product
                {
                    id = (int)row.Cells["id"].Value,
                    name = row.Cells["name"].Value.ToString(),
                    unit_price = (decimal)row.Cells["unit_price"].Value
                };

                txtSearch.Text = selectedProduct.name;
                txtQuantity.Focus();
            }
        }

        public void AddToCart(int defaultQuantity)
        {
            if (selectedProduct == null && dataGridViewCart.SelectedRows.Count > 0)
            {
                // Get product from cart selection
                DataRowView selectedRow = (DataRowView)dataGridViewCart.SelectedRows[0].DataBoundItem;
                selectedProduct = new Product
                {
                    id = (int)selectedRow["ProductID"],
                    name = selectedRow["Name"].ToString(),
                    unit_price = (decimal)selectedRow["UnitPrice"]
                };
            }

            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product first", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                isUpdatingCart = true;
                int qty = string.IsNullOrEmpty(txtQuantity.Text) ? defaultQuantity : int.Parse(txtQuantity.Text);

                if (qty <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero", "Warning",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check available quantity
                int availableQuantity = productDAL.GetProductQuantity(selectedProduct.id);
                if (qty > availableQuantity)
                {
                    MessageBox.Show($"Not enough stock available. Only {availableQuantity} items in stock.",
                                  "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow[] existingRows = cart.Select($"ProductID = {selectedProduct.id}");

                if (existingRows.Length > 0)
                {
                    // Update existing cart item
                    int newQty = (int)existingRows[0]["Quantity"] + qty;
                    if (newQty > availableQuantity)
                    {
                        MessageBox.Show($"Adding this quantity would exceed available stock. Only {availableQuantity} items in stock.",
                                      "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    existingRows[0]["Quantity"] = newQty;
                    existingRows[0]["TotalPrice"] = newQty * (decimal)existingRows[0]["UnitPrice"];
                }
                else
                {
                    // Add new item to cart
                    DataRow row = cart.NewRow();
                    row["ProductID"] = selectedProduct.id;
                    row["Name"] = selectedProduct.name;
                    row["Quantity"] = qty;
                    row["UnitPrice"] = selectedProduct.unit_price;
                    row["TotalPrice"] = qty * selectedProduct.unit_price;
                    cart.Rows.Add(row);
                }

                UpdateTotalPrice();

                // Only clear if we're not working with cart selection
                if (dataGridViewCart.SelectedRows.Count == 0)
                {
                    ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding to cart: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingCart = false;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.SelectedRows.Count == 0) return;

            try
            {
                isUpdatingCart = true;
                DataRowView selectedRow = (DataRowView)dataGridViewCart.SelectedRows[0].DataBoundItem;
                int productId = (int)selectedRow["ProductID"];
                int currentQty = (int)selectedRow["Quantity"];
                decimal unitPrice = (decimal)selectedRow["UnitPrice"];

                if (currentQty > 1)
                {
                    selectedRow["Quantity"] = currentQty - 1;
                    selectedRow["TotalPrice"] = (currentQty - 1) * unitPrice;
                }
                else
                {
                    DataRow[] rows = cart.Select($"ProductID = {productId}");
                    if (rows.Length > 0)
                    {
                        int removeIndex = cart.Rows.IndexOf(rows[0]);
                        cart.Rows.Remove(rows[0]);

                        if (currentCartRowIndex >= removeIndex)
                        {
                            currentCartRowIndex = Math.Max(0, currentCartRowIndex - 1);
                        }

                        if (dataGridViewCart.Rows.Count > 0 && currentCartRowIndex >= 0)
                        {
                            dataGridViewCart.Rows[currentCartRowIndex].Selected = true;
                        }
                    }
                }

                UpdateTotalPrice();
                dataGridViewCart.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingCart = false;
            }
        }

        private void UpdateTotalPrice()
        {
            decimal total = 0;
            foreach (DataRow row in cart.Rows)
            {
                if (decimal.TryParse(row["TotalPrice"].ToString(), out decimal rowTotal))
                {
                    total += rowTotal;
                }
            }

            txtTotalPrice.Text = total.ToString("N2");

            if (currentInvoiceId > 0)
            {
                invoiceDAL.UpdateInvoiceTotal(currentInvoiceId, total);
            }
        }

        private void dataGridViewCart_SelectionChanged(object sender, EventArgs e)
        {
            if (isUpdatingCart || dataGridViewCart.SelectedRows.Count == 0)
                return;

            currentCartRowIndex = dataGridViewCart.SelectedRows[0].Index;

            var selectedItem = dataGridViewCart.SelectedRows[0].DataBoundItem;
            if (selectedItem is DataRowView selectedRow)
            {
                selectedProduct = new Product
                {
                    id = (int)selectedRow["ProductID"],
                    name = selectedRow["Name"].ToString(),
                    unit_price = (decimal)selectedRow["UnitPrice"]
                };

                txtSearch.Text = selectedProduct.name;
            }
            else
            {
                // Safeguard against unexpected data type or null
                selectedProduct = null;
                txtSearch.Text = string.Empty;
            }
        }

        private void ShowInvoiceForm(object sender, EventArgs e)
        {
            if (cart.Rows.Count == 0)
            {
                MessageBox.Show("No items in cart to invoice", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // First check all products have sufficient quantity
                foreach (DataRow row in cart.Rows)
                {
                    int productId = (int)row["ProductID"];
                    int qty = (int)row["Quantity"];
                    int availableQty = productDAL.GetProductQuantity(productId);

                    if (qty > availableQty)
                    {
                        MessageBox.Show($"Not enough stock for {row["Name"]}. Only {availableQty} available.",
                                      "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DataTable invoiceItems = cart.Copy();
                decimal total = decimal.Parse(txtTotalPrice.Text);
                InvoiceForm invoice = new InvoiceForm(invoiceItems, total, currentInvoiceId);
                invoice.ShowDialog();

                if (invoice.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        // Update quantities in database
                        foreach (DataRow row in cart.Rows)
                        {
                            int productId = (int)row["ProductID"];
                            int qty = (int)row["Quantity"];
                            productDAL.UpdateProductQuantity(productId, -qty); // Subtract sold quantity
                        }

                        currentInvoiceId = invoiceDAL.CreateInvoice(0);
                        cart.Rows.Clear();
                        UpdateTotalPrice();
                        ClearSelection();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing payment: {ex.Message}",
                                        "Database Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating invoice: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void saleFrm_Load(object sender, EventArgs e)
        {
            dataGridViewCart.AllowUserToAddRows = false;

            try
            {
                currentInvoiceId = invoiceDAL.CreateInvoice(0, currentUserId);
                UpdateTotalPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize invoice: {ex.Message}\n\n" +
                              "Please check your database connection and try again.",
                              "Initialization Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm(UserSession.CurrentUserRole);
            mainForm.Show();
            this.Close();
        }

        private void AppendToQuantity(string digit)
        {
            try
            {
                if (txtQuantity == null) return;
                if (txtQuantity.Text.Length >= 4) return;
                txtQuantity.Text += digit;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating quantity: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSelection()
        {
            // Only clear if we're not working with cart selection
            if (dataGridViewCart.SelectedRows.Count == 0)
            {
                selectedProduct = null;
                txtQuantity.Text = "";
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                LoadAllProducts();
                return;
            }

            try
            {
                var products = productDAL.SearchProducts(txtSearch.Text.Trim());
                ProductsDataGridView.DataSource = products;

                if (products.Rows.Count == 0)
                {
                    MessageBox.Show("No products found matching your search",
                                  "Information",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.Rows.Count == 0 || currentCartRowIndex <= 0)
                return;

            currentCartRowIndex--;
            dataGridViewCart.ClearSelection();
            dataGridViewCart.Rows[currentCartRowIndex].Selected = true;
            dataGridViewCart.CurrentCell = dataGridViewCart.Rows[currentCartRowIndex].Cells[0];
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.Rows.Count == 0 || currentCartRowIndex >= dataGridViewCart.Rows.Count - 1)
                return;

            currentCartRowIndex++;
            dataGridViewCart.ClearSelection();
            dataGridViewCart.Rows[currentCartRowIndex].Selected = true;
            dataGridViewCart.CurrentCell = dataGridViewCart.Rows[currentCartRowIndex].Cells[0];
        }

        public void SetSelectedProduct(Product product)
        {
            selectedProduct = product;
            txtSearch.Text = product.name; // This updates the search box
            txtQuantity.Focus();
        }

        private void NewInvoiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                currentInvoiceId = invoiceDAL.CreateInvoice(0);
                cart.Rows.Clear();
                UpdateTotalPrice();
                ClearSelection();
                MessageBox.Show("New invoice created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // In saleFrm.cs
        private void ReturnItemButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to return", "Warning",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get selected item details
                DataRowView selectedRow = (DataRowView)dataGridViewCart.SelectedRows[0].DataBoundItem;
                int productId = (int)selectedRow["ProductID"];
                string productName = selectedRow["Name"].ToString();
                int quantity = (int)selectedRow["Quantity"];
                decimal unitPrice = (decimal)selectedRow["UnitPrice"];
                decimal totalPrice = (decimal)selectedRow["TotalPrice"];

                // Confirm with user
                var confirmResult = MessageBox.Show($"Return {quantity} × {productName} ({unitPrice:N2} each)?\n" +
                                                  $"Total refund: {totalPrice:N2}",
                                                  "Confirm Return",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                // Process the return through DAL
                decimal costPrice = productDAL.GetProductPurchasePrice(productId);
                decimal profitDeduction = (unitPrice - costPrice) * quantity;

                bool success = saleDAL.ProcessReturn(currentInvoiceId, productId, quantity, totalPrice, profitDeduction);

                if (success)
                {
                    // Remove from cart (UI)
                    DataRow[] rows = cart.Select($"ProductID = {productId}");
                    if (rows.Length > 0)
                    {
                        cart.Rows.Remove(rows[0]);
                    }

                    // Update cart total
                    UpdateTotalPrice();

                    // Update sales info display
                    if (saleInfo != null)
                    {
                        saleInfo.UpdateOnReturn(productId, quantity, unitPrice);
                    }

                    MessageBox.Show($"Return processed successfully!\n\n" +
                                  $"Product: {productName}\n" +
                                  $"Quantity: {quantity}\n" +
                                  $"Unit Price: {unitPrice:N2}\n" +
                                  $"Total Refund: {totalPrice:N2}\n" +
                                  $"Profit Deduction: {profitDeduction:N2}",
                                  "Return Completed",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to process return: {ex.Message}\n\n{ex.StackTrace}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtSearch.Focused)
            {
                SearchButton_Click(null, null);
                return true;
            }
            else if (keyData == Keys.F1)
            {
                AddToCart(1);
                return true;
            }
            else if (keyData == Keys.F2)
            {
                AddToCart(12);
                return true;
            }
            else if (keyData == Keys.F11)
            {
                this.WindowState = (this.WindowState == FormWindowState.Maximized)
                    ? FormWindowState.Normal
                    : FormWindowState.Maximized;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadAllProducts()
        {
            try
            {
                products = productDAL.GetAllProducts();
                ProductsDataGridView.DataSource = products;
                ProductsDataGridView.Columns["IsDeleted"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}