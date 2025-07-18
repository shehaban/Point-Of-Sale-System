using point_of_sale_system.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class DeleteUser : UserControl
    {
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter a username", "Validation Error",
                                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (UserDAL userDal = new UserDAL())
                {
                    if (!userDal.VerifyCredentials(username, password))
                    {
                        MessageBox.Show("Invalid credentials or account locked", "Access Denied",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var confirm = MessageBox.Show($"Permanently delete user '{username}'?",
                                               "Confirm Deletion",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                    if (confirm == DialogResult.Yes)
                    {
                        bool success = userDal.DeleteUser(username);
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
    }
}