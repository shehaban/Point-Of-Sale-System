using point_of_sale_system.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.DAL
{
    internal class ProductDAL : DbHelper
    {
        public void AddProduct(Product p)
        {
            OpenConnection();
            string query = "INSERT INTO Product (name, category, UnitPrice, PurchasePrice, quantity) VALUES (@n, @c, @u, @p, @q)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@c", p.Category);
                cmd.Parameters.AddWithValue("@u", p.UnitPrice);
                cmd.Parameters.AddWithValue("@p", p.PurchasePrice);
                cmd.Parameters.AddWithValue("@q", p.Quantity);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}
