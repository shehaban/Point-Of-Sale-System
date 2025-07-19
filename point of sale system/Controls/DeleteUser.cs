using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class DeleteUser : UserControl
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string targetUsername = txtUsername.Text.Trim();
                string enteredPassword = txtPassword.Text;

                if (string.IsNullOrEmpty(targetUsername))
                {
                    MessageBox.Show("Please enter a username", "Validation Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (UserDAL userDal = new UserDAL())
                {
                    // Get the target user's information
                    User targetUser = userDal.GetUserByUsername(targetUsername);
                    if (targetUser == null)
                    {
                        MessageBox.Show("User not found", "Error",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verify credentials based on target user's role
                    bool isAuthorized;
                    string requiredPasswordType;

                    if (targetUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        // For deleting admin: Require the TO-BE-DELETED admin's password
                        isAuthorized = userDal.VerifyCredentials(targetUsername, enteredPassword);
                        requiredPasswordType = "the ADMIN'S password you're deleting";
                    }
                    else
                    {
                        // For deleting non-admin: Require current admin's password
                        isAuthorized = userDal.VerifyCredentials(UserSession.CurrentUsername, enteredPassword);
                        requiredPasswordType = "your ADMIN password";
                    }

                    if (!isAuthorized)
                    {
                        MessageBox.Show($"Authentication failed. Please enter {requiredPasswordType}",
                                     "Access Denied",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Warning);
                        return;
                    }

                    // Confirmation dialog
                    var confirm = MessageBox.Show($"Permanently delete {targetUser.Role} user '{targetUsername}'?",
                                               "Confirm Deletion",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        bool success = userDal.DeleteUser(targetUsername);
                        MessageBox.Show(success ? "User deleted successfully" : "Deletion failed",
                                      "Operation Result",
                                      MessageBoxButtons.OK,
                                      success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                        if (success) ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Operation Failed",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void DeleteUser_Load(object sender, EventArgs e)
        {

        }
    }
}