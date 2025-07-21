using point_of_sale_system.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace point_of_sale_system.DAL
{
    internal class SaleDAL : DbHelper
    {
        public bool AddSale(Sale sale)
        {
            string query = @"INSERT INTO Sales (invoice_id, product_id, quantity_sold, total_price, sale_date)
                     VALUES (@invoiceId, @productId, @quantitySold, @totalPrice, @saleDate)";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@invoiceId", sale.InvoiceId);
                    cmd.Parameters.AddWithValue("@productId", sale.ProductId);
                    cmd.Parameters.AddWithValue("@quantitySold", sale.QuantitySold);
                    cmd.Parameters.AddWithValue("@totalPrice", sale.TotalPrice);

                    DateTime validDate = sale.SaleDate;
                    if (sale.SaleDate < new DateTime(1753, 1, 1) || sale.SaleDate > new DateTime(9999, 12, 31))
                    {
                        validDate = DateTime.Now;
                    }

                    cmd.Parameters.Add("@saleDate", SqlDbType.DateTime).Value = validDate;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding sale: " + ex.Message, ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable GetSalesSummary()
        {
            string query = @"
                SELECT 
                    I.id AS invoice_id,
                    I.created_at,
                    I.total,
                    ISNULL(SUM(S.total_price - (P.purchase_price * S.quantity_sold)), 0) AS profit
                FROM Invoices I
                LEFT JOIN Sales S ON I.id = S.invoice_id
                LEFT JOIN Product P ON S.product_id = P.id
                GROUP BY I.id, I.created_at, I.total
                ORDER BY I.created_at DESC";

            return ExecuteDataTable(query);
        }

        public DataTable GetReturnSummary()
        {
            string query = @"
                SELECT 
                ISNULL(SUM(returned_amount), 0) AS returned_amount,
                ISNULL(SUM(profit_deduction), 0) AS profit_deduction
                FROM Returns";

            return ExecuteDataTable(query);
        }

        private DataTable ExecuteDataTable(string query)
        {
            DataTable table = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database query error: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return table;
        }

    public DataTable GetSalesByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"SELECT s.invoice_id, s.sale_date, s.total_price AS total_amount
                             FROM Sales s
                             WHERE CAST(s.sale_date AS DATE) BETWEEN @fromDate AND @toDate
                             ORDER BY s.sale_date DESC";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@toDate", toDate.Date);
                    return ExecuteSelectCommand(cmd);
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public decimal GetTotalSalesAmount(DateTime fromDate, DateTime toDate)
        {
            string query = @"SELECT ISNULL(SUM(total_price), 0)
                             FROM Sales
                             WHERE CAST(sale_date AS DATE) BETWEEN @fromDate AND @toDate";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@toDate", toDate.Date);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public decimal GetTodayNetProfit()
        {
            string query = @"
                SELECT ISNULL(SUM((p.unit_price - p.purchase_price) * s.quantity_sold), 0)
                FROM Sales s
                JOIN Product p ON s.product_id = p.id
                WHERE CAST(s.sale_date AS DATE) = CAST(GETDATE() AS DATE)";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ProcessReturn(int invoiceId, int productId, int quantity, decimal returnedAmount, decimal profitDeduction)
        {
            try
            {
                OpenConnection();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Record the return
                        string returnQuery = @"INSERT INTO Returns 
                            (invoice_id, product_id, quantity, returned_amount, profit_deduction) 
                            VALUES (@invoiceId, @productId, @quantity, @returnedAmount, @profitDeduction)";

                        SqlCommand returnCmd = new SqlCommand(returnQuery, connection, transaction);
                        returnCmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                        returnCmd.Parameters.AddWithValue("@productId", productId);
                        returnCmd.Parameters.AddWithValue("@quantity", quantity);
                        returnCmd.Parameters.AddWithValue("@returnedAmount", returnedAmount);
                        returnCmd.Parameters.AddWithValue("@profitDeduction", profitDeduction);
                        returnCmd.ExecuteNonQuery();

                        // 2. Update invoice total
                        string invoiceQuery = @"UPDATE Invoices 
                                     SET total = total - @returnedAmount 
                                     WHERE id = @invoiceId";

                        SqlCommand invoiceCmd = new SqlCommand(invoiceQuery, connection, transaction);
                        invoiceCmd.Parameters.AddWithValue("@returnedAmount", returnedAmount);
                        invoiceCmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                        invoiceCmd.ExecuteNonQuery();

                        // 3. Update product quantity
                        string productQuery = @"UPDATE Product 
                                     SET quantity = quantity + @quantity 
                                     WHERE id = @productId";

                        SqlCommand productCmd = new SqlCommand(productQuery, connection, transaction);
                        productCmd.Parameters.AddWithValue("@quantity", quantity);
                        productCmd.Parameters.AddWithValue("@productId", productId);
                        productCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public decimal GetTotalReturnsByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"SELECT ISNULL(SUM(returned_amount), 0) 
                   FROM Returns 
                   WHERE CAST(return_date AS DATE) BETWEEN @fromDate AND @toDate";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@toDate", toDate.Date);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public decimal GetNetProfitByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"
        SELECT ISNULL(SUM((p.unit_price - p.purchase_price) * s.quantity_sold), 0)
        FROM Sales s
        JOIN Product p ON s.product_id = p.id
        WHERE CAST(s.sale_date AS DATE) BETWEEN @fromDate AND @toDate";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate.Date);
                    cmd.Parameters.AddWithValue("@toDate", toDate.Date);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public decimal GetProductPurchasePrice(int productId)
        {
            string query = "SELECT purchase_price FROM Product WHERE id = @productId";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0m;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        private DataTable ExecuteSelectCommand(SqlCommand command)
        {
            DataTable table = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }
            return table;
        }
    }
}