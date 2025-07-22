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

            errorProvider1.Clear();

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

            if (user.IsLocked)
            {
                MessageBox.Show("Your account is locked. Please try again later.",
                    "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userDal.IsAccountLocked(user))
            {
                return; // الرسالة تظهر في IsAccountLocked
            }

            // التحقق من كلمة المرور
            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                userDal.IncrementFailedAttempts(username);
                user = userDal.GetUserByUsername(username);

                if (user.FailedAttempts < 4)
                {
                    int remaining = 4 - user.FailedAttempts;
                    MessageBox.Show($"Invalid password. {remaining} attempts remaining before 1-minute lock.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (user.FailedAttempts == 4)
                {
                    MessageBox.Show("Too many failed attempts. Please wait 1 minute before next try.", "Temporary Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (user.FailedAttempts == 5)
                {
                    MessageBox.Show("Too many failed attempts. Please wait 5 minutes before next try.", "Temporary Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (user.FailedAttempts == 6)
                {
                    MessageBox.Show("Too many failed attempts. Please wait 1 hour before next try.", "Temporary Lock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("You have reached the maximum number of attempts. Please wait 1 hour before trying again.", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            // تسجيل دخول ناجح
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
            txtPassword.PasswordChar = checkBoxShowPassword.Checked ? '\0' : '●';
            txtPassword.SelectionStart = txtPassword.TextLength;
        }
    }
}
