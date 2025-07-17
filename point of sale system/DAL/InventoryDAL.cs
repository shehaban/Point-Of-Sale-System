using point_of_sale_system.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.DAL
{
    internal class InventoryDAL : DbHelper
    {
        public void UpdateStock(int productId, int quantity)
        {
            OpenConnection();
            string query = "UPDATE Product SET quantity = @q WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@q", quantity);
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public List<Product> GetLowStock()
        {
            List<Product> lowStock = new List<Product>();
            OpenConnection();
            string query = "SELECT * FROM Product WHERE quantity < 5";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lowStock.Add(new Product
                    {
                        Id = (int)reader["id"],
                        Name = reader["name"].ToString(),
                        Quantity = (int)reader["quantity"]
                    });
                }
            }
            CloseConnection();
            return lowStock;
        }
    }
}