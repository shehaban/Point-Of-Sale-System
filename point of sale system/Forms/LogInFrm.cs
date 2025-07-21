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
            txtPassword.PasswordChar = '●';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

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
                MessageBox.Show("Invalid username or password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                userDal.IncrementFailedAttempts(username);
                user = userDal.GetUserByUsername(username); 

                int remainingAttempts = UserDAL.MaxAttemptsLevel3 - user.FailedAttempts;
                string warning = remainingAttempts > 0 ?
                    $"{remainingAttempts} attempt(s) remaining" :
                    "Account will be locked";

                MessageBox.Show($"Invalid password. {warning}", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            userDal.ResetFailedAttempts(username);

            UserSession.CurrentUserRole = user.Role;
            UserSession.CurrentUsername = username;
            UserSession.CurrentUserID = user.UserId;

            new mainFrm(UserSession.CurrentUserRole).Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; 
            }
            else
            {
                txtPassword.PasswordChar = '●'; 
            }

            txtPassword.SelectionStart = txtPassword.TextLength;
        }
    }
}