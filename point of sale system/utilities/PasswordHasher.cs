using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace point_of_sale_system.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }
    }
}