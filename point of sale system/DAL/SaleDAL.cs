using point_of_sale_system.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.DAL
{
    internal class SaleDAL : DbHelper
    {
        public void AddSale(Sale s)
        {
            OpenConnection();
            string query = "INSERT INTO Sale (InvoiceId, ProductId, QuantitySold, TotalPrice) VALUES (@i, @p, @q, @t)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@i", s.InvoiceId);
                cmd.Parameters.AddWithValue("@p", s.ProductId);
                cmd.Parameters.AddWithValue("@q", s.QuantitySold);
                cmd.Parameters.AddWithValue("@t", s.TotalPrice);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}