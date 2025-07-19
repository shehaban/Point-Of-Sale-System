using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;
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
    public partial class AddUser : UserControl
    {
        private string currentUserRole = UserSession.CurrentUserRole;

        public AddUser()
        {
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!currentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Unauthorized access attempt logged",
                              "Security Warning",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            // Input validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Length < 4)
            {
                MessageBox.Show("Username must be at least 4 characters");
                return;
            }

            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Passwords do not match");
                return;
            }

            if (textBox1.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters");
                return;
            }

            // Check if username exists
            UserDAL userDal = new UserDAL();
            if (!userDal.IsUsernameAvailable(txtUsername.Text))
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
                return;
            }

            // Create new user
            try
            {
                User newUser = new User
                {
                    Username = txtUsername.Text,
                    PasswordHash = PasswordHasher.HashPassword(textBox1.Text),
                    Role = cmbRole.SelectedItem.ToString(),
                    FailedAttempts = 0,
                    IsLocked = false
                };

                if (userDal.AddUser(newUser))
                {
                    MessageBox.Show("User added successfully");
                    // Clear fields
                    txtUsername.Text = string.Empty;
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
            }
        }
    }
}
