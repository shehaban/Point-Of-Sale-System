using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class Inventory : UserControl
    {
        private readonly InventoryDAL inventoryDAL = new InventoryDAL();

        public Inventory()
        {
            InitializeComponent();
            LoadInventory();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Font and sizing
            dataGridViewInventory.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridViewInventory.RowTemplate.Height = 35;

            // Column behavior
            dataGridViewInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewInventory.AllowUserToAddRows = false;

            // Visual improvements
            dataGridViewInventory.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewInventory.EnableHeadersVisualStyles = false;
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewInventory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Configure columns
            dataGridViewInventory.AutoGenerateColumns = false;
            dataGridViewInventory.Columns.Clear();

            // Add columns
            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Visible = false
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "name",
                HeaderText = "Name",
                DataPropertyName = "name",
                Width = 200
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "category",
                HeaderText = "Category",
                DataPropertyName = "category",
                Width = 150
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "unit_price",
                HeaderText = "Unit Price",
                DataPropertyName = "unit_price",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "N2" // Changed from "C2" to "N2" to show numbers only
                }
            });

            dataGridViewInventory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "quantity",
                HeaderText = "Quantity",
                DataPropertyName = "quantity",
                Width = 80
            });
        }

        private void LoadInventory()
        {
            dataGridViewInventory.DataSource = inventoryDAL.GetAllProducts();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            dataGridViewInventory.DataSource = inventoryDAL.GetLowStock();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                dataGridViewInventory.DataSource = inventoryDAL.SearchProducts(searchTerm);
            }
            else
            {
                LoadInventory();
            }
            txtSearch.Text = "";
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }


        
    }
}