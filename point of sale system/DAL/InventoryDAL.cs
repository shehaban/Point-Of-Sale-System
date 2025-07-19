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
            string query = @"SELECT p.* FROM Product p 
           WHERE p.quantity <= 2 AND p.IsDeleted = 0"; // Only non-deleted products
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lowStock.Add(new Product
                    {
                        id = (int)reader["id"],
                        name = reader["name"].ToString(),
                        category = reader["category"].ToString(),
                        unit_price = (decimal)reader["unit_price"],
                        purchase_price = (decimal)reader["purchase_price"],
                        quantity = (int)reader["quantity"]
                    });
                }
            }
            CloseConnection();
            return lowStock;
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            List<Product> products = new List<Product>();
            OpenConnection();
            string query = @"SELECT * FROM Product 
           WHERE (name LIKE @search OR category LIKE @search)
           AND IsDeleted = 0"; // Only non-deleted products
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            id = (int)reader["id"],
                            name = reader["name"].ToString(),
                            category = reader["category"].ToString(),
                            unit_price = (decimal)reader["unit_price"],
                            purchase_price = (decimal)reader["purchase_price"],
                            quantity = (int)reader["quantity"]
                        });
                    }
                }
            }
            CloseConnection();
            return products;
        }

        //public List<Product> SearchProducts(string searchTerm)
        //{
        //    List<Product> products = new List<Product>();
        //    OpenConnection();
        //    string query = "SELECT * FROM Product WHERE name LIKE @search OR category LIKE @search";
        //    using (SqlCommand cmd = new SqlCommand(query, connection))
        //    {
        //        cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                products.Add(new Product
        //                {
        //                    id = (int)reader["id"],
        //                    name = reader["name"].ToString(),
        //                    category = reader["category"].ToString(),
        //                    unit_price = (decimal)reader["unit_price"],
        //                    purchase_price = (decimal)reader["purchase_price"],
        //                    quantity = (int)reader["quantity"]
        //                });
        //            }
        //        }
        //    }
        //    CloseConnection();
        //    return products;
        //}

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            OpenConnection();
            string query = "SELECT * FROM Product WHERE IsDeleted = 0"; // Only non-deleted products
            using (SqlCommand cmd = new SqlCommand(query, connection))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        id = (int)reader["id"],
                        name = reader["name"].ToString(),
                        category = reader["category"].ToString(),
                        unit_price = (decimal)reader["unit_price"],
                        purchase_price = (decimal)reader["purchase_price"],
                        quantity = (int)reader["quantity"]
                    });
                }
            }
            CloseConnection();
            return products;
        }
    }
}