using point_of_sale_system.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace point_of_sale_system.DAL
{
    internal class ProductDAL : DbHelper
    {
        public DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = "SELECT * FROM Product WHERE IsDeleted = 0"; 
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        public void AddProduct(Product p)
        {
            if (!UserSession.IsAdmin())
            {
                throw new UnauthorizedAccessException("Only admins can add products");
            }

            try
            {
                OpenConnection();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string checkQuery = @"SELECT COUNT(*) FROM Product 
                                      WHERE name = @n AND category = @c AND unit_price = @u AND IsDeleted = 0";

                        SqlCommand checkCmd = new SqlCommand(checkQuery, connection, transaction);
                        checkCmd.Parameters.AddWithValue("@n", p.name);
                        checkCmd.Parameters.AddWithValue("@c", p.category);
                        checkCmd.Parameters.AddWithValue("@u", p.unit_price);

                        int existingCount = (int)checkCmd.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            throw new Exception("A product with the same name, category, and price already exists.");
                        }

                        string productQuery = @"INSERT INTO Product (name, category, unit_price, purchase_price, quantity, IsDeleted) 
                                        VALUES (@n, @c, @u, @p, @q, 0);
                                        SELECT SCOPE_IDENTITY();";

                        SqlCommand productCmd = new SqlCommand(productQuery, connection, transaction);
                        productCmd.Parameters.AddWithValue("@n", p.name);
                        productCmd.Parameters.AddWithValue("@c", p.category);
                        productCmd.Parameters.AddWithValue("@u", p.unit_price);
                        productCmd.Parameters.AddWithValue("@p", p.purchase_price);
                        productCmd.Parameters.AddWithValue("@q", p.quantity);

                        int newProductId = Convert.ToInt32(productCmd.ExecuteScalar());

                        string inventoryQuery = @"INSERT INTO Inventory (product_id, reorder_level) 
                                          VALUES (@product_id, 2)";
                        SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, connection, transaction);
                        inventoryCmd.Parameters.AddWithValue("@product_id", newProductId);
                        inventoryCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }


        public bool UpdateProduct(Product p)
        {
            try
            {
                OpenConnection();
                string query = @"UPDATE Product SET 
                name = @n, 
                category = @c, 
                unit_price = @u, 
                purchase_price = @p, 
                quantity = @q 
                WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", p.id);
                    cmd.Parameters.AddWithValue("@n", p.name);
                    cmd.Parameters.AddWithValue("@c", p.category);
                    cmd.Parameters.AddWithValue("@u", p.unit_price);
                    cmd.Parameters.AddWithValue("@p", p.purchase_price);
                    cmd.Parameters.AddWithValue("@q", p.quantity);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                OpenConnection();
                string query = "UPDATE Product SET IsDeleted = 1 WHERE Id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable SearchProducts(string searchTerm)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"SELECT id, name, category, unit_price, purchase_price, quantity 
                FROM Product 
                WHERE (name LIKE @search OR category LIKE @search)
                AND IsDeleted = 0"; 
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@search", $"%{searchTerm}%");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        public DataTable SearchOnProducts(string searchTerm)
        {
                DataTable results = new DataTable();

                try
                {
                    OpenConnection();
                    string query = @"SELECT id, name, unit_price 
                        FROM Product 
                        WHERE name LIKE @searchTerm + '%'
                        AND IsDeleted = 0
                        ORDER BY name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(results);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error searching products", ex);
                }
                finally
                {
                    CloseConnection();
                }

                return results;
            }

            public Product GetProductByNameCategoryAndPrice(string name, string category, decimal unitPrice)
        {
            Product product = null;
            OpenConnection();
            string query = "SELECT * FROM Product WHERE name = @name AND category = @category AND unit_price = @unitPrice AND IsDeleted = 0";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@unitPrice", unitPrice);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            id = (int)reader["id"],
                            name = reader["name"].ToString(),
                            category = reader["category"].ToString(),
                            unit_price = (decimal)reader["unit_price"],
                            purchase_price = (decimal)reader["purchase_price"],
                            quantity = (int)reader["quantity"]
                        };
                    }
                }
            }
            CloseConnection();
            return product;
        }

        public int AddProductWithInventory(Product product)
        {
            try
            {
                OpenConnection();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string productQuery = @"INSERT INTO Product (name, category, unit_price, purchase_price, quantity) 
                                             VALUES (@name, @category, @unit_price, @purchase_price, @quantity);
                                             SELECT SCOPE_IDENTITY();";

                        SqlCommand productCmd = new SqlCommand(productQuery, connection, transaction);
                        productCmd.Parameters.AddWithValue("@name", product.name);
                        productCmd.Parameters.AddWithValue("@category", product.category);
                        productCmd.Parameters.AddWithValue("@unit_price", product.unit_price);
                        productCmd.Parameters.AddWithValue("@purchase_price", product.purchase_price);
                        productCmd.Parameters.AddWithValue("@quantity", product.quantity);

                        int newProductId = Convert.ToInt32(productCmd.ExecuteScalar());

                        string inventoryQuery = @"INSERT INTO Inventory (product_id, reorder_level) 
                                               VALUES (@product_id, 2)";

                        SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, connection, transaction);
                        inventoryCmd.Parameters.AddWithValue("@product_id", newProductId);
                        inventoryCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return newProductId;
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

        public decimal GetProductPurchasePrice(int productId)
        {
            try
            {
                OpenConnection();
                string query = "SELECT purchase_price FROM Product WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool UpdateProductQuantity(int productId, int quantityChange)
        {
            try
            {
                OpenConnection();
                string query = @"UPDATE Product 
                       SET quantity = quantity + @quantityChange 
                       WHERE id = @id AND IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@quantityChange", quantityChange);
                    cmd.Parameters.AddWithValue("@id", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public int GetProductQuantity(int productId)
        {
            try
            {
                OpenConnection();
                string query = "SELECT quantity FROM Product WHERE id = @id AND IsDeleted = 0";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool UpdateInvProduct(Product product)
        {

    try
    {
        OpenConnection();
        string query = @"UPDATE Product 
                       SET quantity = @quantity 
                       WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@quantity", product.quantity);
                    cmd.Parameters.AddWithValue("@id", product.id);

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