using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class SaleInfo : UserControl
    {
        private SaleDAL saleDAL = new SaleDAL();

        public SaleInfo()
        {
            InitializeComponent();

            if (!UserSession.CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("You are not authorized to access this section.",
                              "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                return;
            }

            Load += SaleInfo_Load;

            btnRefresh.Click += BtnRefresh_Click;
        }

        public void LoadSaleSummary()
        {
            try
            {
                DataTable salesData = saleDAL.GetSalesSummary();
                Reportdgv.DataSource = salesData;

                decimal totalSales = 0;
                decimal netProfit = 0;

                foreach (DataRow row in salesData.Rows)
                {
                    totalSales += Convert.ToDecimal(row["total"]);
                    netProfit += Convert.ToDecimal(row["profit"]);
                }

                DataTable returnData = saleDAL.GetReturnSummary();
                foreach (DataRow row in returnData.Rows)
                {
                    totalSales -= Convert.ToDecimal(row["returned_amount"]);
                    netProfit -= Convert.ToDecimal(row["profit_deduction"]);
                }

                txtTotalAmount.Text = $"Total Sales: {totalSales:C}";
                txtNetProfit.Text = $"Net Profit: {netProfit:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales summary: " + ex.Message);
            }
        }

        private void SaleInfo_Load(object sender, EventArgs e)
        {
            dateTimePickerFrom.Value = DateTime.Today;
            dateTimePickerTo.Value = DateTime.Today;

            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = "dd-MM-yyyy";
            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.CustomFormat = "dd-MM-yyyy";

            ConfigureDataGridView();

            comboReportType.Items.Clear();
            comboReportType.Items.Add("Daily Sales");
            comboReportType.SelectedIndex = 0;

            RefreshAllData();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshAllData();
        }

        private void ConfigureDataGridView()
        {
            Reportdgv.DefaultCellStyle.Font = new Font("Segoe UI", 12);
            Reportdgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            Reportdgv.RowTemplate.Height = 35;
            Reportdgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Reportdgv.AllowUserToAddRows = false;
            Reportdgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            Reportdgv.EnableHeadersVisualStyles = false;
            Reportdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            Reportdgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void RefreshAllData()
        {
            try
            {
                btnRefresh.Enabled = false;
                btnRefresh.Text = "Refreshing...";
                Cursor.Current = Cursors.WaitCursor;

                DateTime fromDate = dateTimePickerFrom.Value.Date;
                DateTime toDate = dateTimePickerTo.Value.Date;

                // Get fresh data with date filtering
                DataTable salesData = saleDAL.GetSalesByDateRange(fromDate, toDate);
                DataTable returnData = saleDAL.GetReturnSummaryByDateRange(fromDate, toDate);

                // Initialize all values to 0
                decimal totalSales = 0m;
                decimal totalReturns = 0m;
                decimal netProfit = 0m;
                decimal profitDeductions = 0m;

                // Calculate values only if we have data
                if (salesData.Rows.Count > 0)
                {
                    totalSales = saleDAL.GetTotalSalesAmount(fromDate, toDate);
                    netProfit = saleDAL.GetNetProfitByDateRange(fromDate, toDate);
                }

                if (returnData.Rows.Count > 0)
                {
                    totalReturns = Convert.ToDecimal(returnData.Rows[0]["returned_amount"]);
                    profitDeductions = Convert.ToDecimal(returnData.Rows[0]["profit_deduction"]);
                }

                // Update UI
                Reportdgv.DataSource = salesData;
                FormatDailySalesGrid();

                // Set the values
                txtTotalAmount.Text = (totalSales - totalReturns).ToString("0.00");
                txtNetProfit.Text = (netProfit - profitDeductions).ToString("0.00");
                txtTotalReturn.Text = totalReturns.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Refresh";
                Cursor.Current = Cursors.Default;
            }
        }
        private void FormatDailySalesGrid()
        {
            if (Reportdgv.Columns.Count > 0)
            {
                if (Reportdgv.Columns.Contains("invoice_id"))
                    Reportdgv.Columns["invoice_id"].HeaderText = "Invoice #";

                if (Reportdgv.Columns.Contains("sale_date"))
                {
                    Reportdgv.Columns["sale_date"].HeaderText = "Date/Time";
                    Reportdgv.Columns["sale_date"].DefaultCellStyle.Format = "g";
                }

                if (Reportdgv.Columns.Contains("total_amount"))
                    Reportdgv.Columns["total_amount"].HeaderText = "Amount";
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string reportType = comboReportType.SelectedItem?.ToString();
            DateTime fromDate = dateTimePickerFrom.Value.Date;
            DateTime toDate = dateTimePickerTo.Value.Date;

            if (string.IsNullOrEmpty(reportType))
            {
                MessageBox.Show("Please select a report type.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (fromDate > toDate)
            {
                MessageBox.Show("End date cannot be before start date.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RefreshAllData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (Reportdgv.Rows.Count == 0)
            {
                MessageBox.Show("No data to export", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel file (*.xlsx)|*.xlsx|CSV file (*.csv)|*.csv",
                Title = "Export Sales Report"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MessageBox.Show($"Report exported successfully to {saveFileDialog.FileName}",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var parentForm = this.ParentForm;
            parentForm?.Close();
        }

        public void UpdateReturnSummary()
        {
            try
            {
                DataTable returnSummary = saleDAL.GetReturnSummary();
                if (returnSummary.Rows.Count > 0)
                {
                    decimal totalReturned = Convert.ToDecimal(returnSummary.Rows[0]["returned_amount"]);
                    decimal totalProfitDeduction = Convert.ToDecimal(returnSummary.Rows[0]["profit_deduction"]);

                    txtTotalReturn.Text = totalReturned.ToString("0.00");
                    txtNetProfit.Text = (GetCurrentNetProfit() - totalProfitDeduction).ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating return summary: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateOnReturn(int productId, int quantity, decimal unitPrice)
        {
            try
            {
                decimal currentReturn = saleDAL.GetReturnSummary().AsEnumerable()
                                      .Sum(row => Convert.ToDecimal(row["returned_amount"]));

                decimal currentProfit = saleDAL.GetTodayNetProfit() -
                                      saleDAL.GetReturnSummary().AsEnumerable()
                                      .Sum(row => Convert.ToDecimal(row["profit_deduction"]));

                decimal costPrice = saleDAL.GetProductPurchasePrice(productId);
                decimal newReturn = currentReturn + (quantity * unitPrice);
                decimal profitDeduction = (unitPrice - costPrice) * quantity;
                decimal newProfit = currentProfit - profitDeduction;

                RefreshAllData();

                txtTotalReturn.Text = newReturn.ToString("0.00");
                txtNetProfit.Text = newProfit.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating return info: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetCurrentNetProfit()
        {
            return saleDAL.GetTodayNetProfit();
        }

        private decimal GetDecimalFromTextBox(TextBox textBox)
        {
            string cleanString = new string(textBox.Text
                .Where(c => char.IsDigit(c) || c == '.' || c == '-')
                .ToArray());

            if (decimal.TryParse(cleanString, out decimal result))
            {
                return result;
            }
            return 0m; 
        }
    }
}