using System;
using System.Data;
using System.Data.SqlClient;

namespace point_of_sale_system.DAL
{
    internal class InvoiceDAL : DbHelper
    {
        public int CreateInvoice(decimal totalAmount, int userId = 1)
        {
            SqlConnection connection = null;
            try
            {
                // Replace with your actual connection string
                string connectionString = "Server=.;Database=pos;Integrated Security=True;";

                connection = new SqlConnection(connectionString);
                connection.Open();

                string query = @"INSERT INTO Invoices (total, user_id) 
                        OUTPUT INSERTED.id 
                        VALUES (@total, @userId)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@total", totalAmount);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    throw new Exception("Failed to retrieve new invoice ID");
                }
            }
            catch (SqlException ex)
            {
                // More detailed error information
                string errorDetails = $"SQL Error {ex.Number}: {ex.Message}\n" +
                                    $"Procedure: {ex.Procedure}\n" +
                                    $"Line Number: {ex.LineNumber}";

                throw new Exception($"Database error creating invoice.\n{errorDetails}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public bool UpdateInvoiceTotal(int invoiceId, decimal totalAmount)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE Invoices SET total = @total WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@total", totalAmount);
                    cmd.Parameters.AddWithValue("@id", invoiceId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}