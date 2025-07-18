using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;
using System;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class LogInFrm : Form
    {
        public LogInFrm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Input validation
            if (string.IsNullOrEmpty(username))
            {
                errorProvider1.SetError(txtUsername, "Username is required");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                errorProvider1.SetError(txtPassword, "Password is required");
                return;
            }

            UserDAL userDal = new UserDAL();  
            User user = userDal.GetUserByUsername(username);
            if (user == null)
            {
                MessageBox.Show("DEBUG: User not found. Check:\n" +
                              "- Username exists in database\n" +
                              "- Database connection works");
                return;
            }

            // Debug: Show actual hash comparison
            //string inputHash = PasswordHasher.HashPassword(password);
            //MessageBox.Show($"Debug:\nStored: {user.PasswordHash}\nGenerated: {inputHash}");

            // Verify password
            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                userDal.IncrementFailedAttempts(username);
                user = userDal.GetUserByUsername(username); // Refresh user data

                int remainingAttempts = UserDAL.MaxAttemptsLevel3 - user.FailedAttempts;
                string warning = remainingAttempts > 0 ?
                    $"{remainingAttempts} attempt(s) remaining" :
                    "Account will be locked";

                MessageBox.Show($"Invalid password. {warning}", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Successful login
            userDal.ResetFailedAttempts(username);

            UserSession.CurrentUserRole = user.Role;
            UserSession.CurrentUsername = username;

            // Redirect based on role (only admin or cashier)
            if (user.Role.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                new mainFrm(UserSession.CurrentUserRole).Show();
                this.Hide();
            }
            else if (user.Role.Equals("cashier", StringComparison.OrdinalIgnoreCase))
            {
                new mainFrm(UserSession.CurrentUserRole).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid role in database");
            }
        }

        private string GetLockMessage(int attempts, TimeSpan timeSinceLast)
        {
            if (attempts >= UserDAL.MaxAttemptsLevel3 && timeSinceLast.TotalHours < 24)
                return "Account locked for 24 hours due to multiple failed attempts.";
            else if (attempts >= UserDAL.MaxAttemptsLevel2 && timeSinceLast.TotalMinutes < 5)
                return "Account locked for 5 minutes due to multiple failed attempts.";
            else
                return "Account locked for 1 minute due to multiple failed attempts.";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}