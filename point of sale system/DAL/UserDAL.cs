using point_of_sale_system.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace point_of_sale_system.DAL
{
    internal class UserDAL : DbHelper
    {
        public User GetUserByUsername(string username)
        {
            User user = null;
            OpenConnection();
            string query = "SELECT * FROM Users WHERE username = @u";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@u", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = (int)reader["UserId"],
                            Username = reader["username"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
            CloseConnection();
            return user;
        }

        public void AddUser(User u)
        {
            OpenConnection();
            string query = "INSERT INTO Users (username, PasswordHash, Role) VALUES (@u, @p, @r)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@u", u.Username);
                cmd.Parameters.AddWithValue("@p", u.PasswordHash);
                cmd.Parameters.AddWithValue("@r", u.Role);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }
}