using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class ProductMng : UserControl
    {
        private readonly ProductDAL productDal = new ProductDAL();

        public ProductMng()
        {
            InitializeComponent();

            // Check authorization on load
            if (!UserSession.CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Access Denied: Only Administrators can manage products",
                              "Security Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                this.ParentForm?.Close();
                return;
            }

            LoadProducts();
            SetupControls();
        }

        private void SetupControls()
        {
            // Disable buttons if not admin (redundant check but safe)
            btnAdd.Enabled = UserSession.IsAdmin();
            btnEdit.Enabled = UserSession.IsAdmin();
            btnDelete.Enabled = UserSession.IsAdmin();
        }

        private void LoadProducts()
        {
            dataGridView1.DataSource = productDal.GetAllProducts();
            dataGridView1.Columns["id"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            dataGridView1.DataSource = productDal.SearchProducts(searchTerm);
            txtSearch.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem addForm = new AddItem();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts(); // Refresh after adding
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            // Create Product object from selected row
            Product productToEdit = new Product
            {
                id = Convert.ToInt32(row.Cells["id"].Value),
                name = row.Cells["name"].Value.ToString(),
                category = row.Cells["category"].Value.ToString(),
                unit_price = Convert.ToDecimal(row.Cells["unit_price"].Value),
                purchase_price = Convert.ToDecimal(row.Cells["purchase_price"].Value),
                quantity = Convert.ToInt32(row.Cells["quantity"].Value)
            };

            // Pass the product to Edit form
            Edit editForm = new Edit(productToEdit);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts(); // Refresh grid after editing
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
            string productName = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();

            DialogResult result = MessageBox.Show($"Are you sure you want to delete {productName}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (productDal.DeleteProduct(productId))
                {
                    MessageBox.Show("Product deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts(); // Refresh after deletion
                }
                else
                {
                    MessageBox.Show("Failed to delete product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ProductMng_Load(object sender, EventArgs e)
        {
            //dataGridView1.Dock = DockStyle.Fill;

            // Font and sizing
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.RowTemplate.Height = 35;

            // Column behavior
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;

            // Visual improvements
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}