using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;

namespace point_of_sale_system.DAL
{
    internal class UserDAL : DbHelper, IDisposable
    {
        // Constants for login attempt limits
        public const int MaxAttemptsLevel1 = 3; // 1-minute lock
        public const int MaxAttemptsLevel2 = 4; // 5-minute lock
        public const int MaxAttemptsLevel3 = 5; // 24-hour lock

        public bool AddUser(User user)
        {
            string query = @"INSERT INTO Users (username, password, role, FailedAttempts) 
                            VALUES (@Username, @Password, @Role, 0)";

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Primary key violation
            {
                Debug.WriteLine($"User already exists: {ex.Message}");
                throw new Exception("Username already exists. Please choose a different username.", ex);
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in AddUser: {ex.Message}");
                throw new Exception("Failed to create user due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public User GetUserByUsername(string username)
        {
            User user = null;
            string query = @"SELECT id as UserId, username, password as PasswordHash, 
                    role, FailedAttempts, LastAttempt, is_locked 
                    FROM Users WHERE username = @Username";

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = (int)reader["UserId"],
                                Username = reader["username"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Role = reader["role"].ToString(),
                                FailedAttempts = reader["FailedAttempts"] != DBNull.Value ?
                                               (int)reader["FailedAttempts"] : 0,
                                LastAttempt = reader["LastAttempt"] != DBNull.Value ?
                                            (DateTime)reader["LastAttempt"] : (DateTime?)null,
                                IsLocked = reader["is_locked"] != DBNull.Value ?
                                          (bool)reader["is_locked"] : false
                            };
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return user;
        }

        public bool IsAccountLocked(User user)
        {
            if (user.LastAttempt == null) return false;

            TimeSpan timeSinceLast = DateTime.Now - user.LastAttempt.Value;

            if (user.FailedAttempts >= MaxAttemptsLevel3 && timeSinceLast.TotalHours < 24) return true;
            if (user.FailedAttempts >= MaxAttemptsLevel2 && timeSinceLast.TotalMinutes < 5) return true;
            if (user.FailedAttempts >= MaxAttemptsLevel1 && timeSinceLast.TotalMinutes < 1) return true;

            return false;
        }

        public bool IncrementFailedAttempts(string username)
        {
            string query = @"UPDATE Users SET 
                            FailedAttempts = FailedAttempts + 1,
                            LastAttempt = GETDATE()
                            WHERE Username = @Username";

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in IncrementFailedAttempts: {ex.Message}");
                throw new Exception("Failed to update login attempts due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ResetFailedAttempts(string username)
        {
            string query = @"UPDATE Users SET 
                           FailedAttempts = 0,
                           LastAttempt = NULL
                           WHERE Username = @Username";

            try
            {
                OpenConnection();

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine($"Database error in ResetFailedAttempts: {ex.Message}");
                throw new Exception("Failed to reset login attempts due to a database error.", ex);
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool VerifyCredentials(string username, string password)
        {
            try
            {
                OpenConnection();
                var user = GetUserByUsername(username);

                if (user == null)
                {
                    Debug.WriteLine($"User {username} not found");
                    return false;
                }

                if (IsAccountLocked(user))
                {
                    Debug.WriteLine($"Account {username} is locked");
                    return false;
                }

                bool isValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);

                if (!isValid)
                {
                    Debug.WriteLine($"Invalid password for {username}");
                    IncrementFailedAttempts(username);
                    return false;
                }

                ResetFailedAttempts(username);
                Debug.WriteLine($"Successful login for {username}");
                return true;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool DeleteUser(string username)
        {
            try
            {
                OpenConnection();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "DELETE FROM Users WHERE Username = @Username";
                        using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            int affected = cmd.ExecuteNonQuery();
                            transaction.Commit();
                            return affected > 0;
                        }
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

        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                string query = "SELECT Id, Username, Role FROM Users";
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

        public bool UpdateUser(string originalUsername, User updatedUser)
        {
            try
            {
                OpenConnection();
                string query = @"UPDATE Users SET 
                        Username = @NewUsername,
                        Password = CASE WHEN @NewPasswordHash IS NULL THEN Password ELSE @NewPasswordHash END,
                        Role = @Role
                        WHERE Username = @OriginalUsername";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@NewUsername", updatedUser.Username);
                    cmd.Parameters.AddWithValue("@Role", updatedUser.Role);
                    cmd.Parameters.AddWithValue("@OriginalUsername", originalUsername);

                    var passwordParam = cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar);
                    passwordParam.Value = string.IsNullOrEmpty(updatedUser.PasswordHash)
                        ? DBNull.Value
                        : (object)updatedUser.PasswordHash;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}