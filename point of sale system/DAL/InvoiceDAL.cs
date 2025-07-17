using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.DAL
{
    internal class InvoiceDAL : DbHelper
    {
        public int CreateInvoice(decimal totalAmount)
        {
            OpenConnection();
            string query = "INSERT INTO Invoice (CreatedAt, TotalAmount) OUTPUT INSERTED.InvoiceNumber VALUES (@date, @total)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@date", DateTime.Now);
                cmd.Parameters.AddWithValue("@total", totalAmount);
                int invoiceId = (int)cmd.ExecuteScalar();
                CloseConnection();
                return invoiceId;
            }
        }
    }
}