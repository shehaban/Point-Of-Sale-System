using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class EditUser : UserControl
    {
        private readonly UserDAL _userDal = new UserDAL();
        private DataTable _usersTable;

        public EditUser()
        {
            InitializeComponent();
            LoadUsers();
            ConfigureComboBox();
        }

        private void LoadUsers()
        {
            _usersTable = _userDal.GetAllUsers();
            cmbUsers.DataSource = _usersTable;
            cmbUsers.DisplayMember = "Username"; 
            cmbUsers.ValueMember = "Username";  
        }

        private void ConfigureComboBox()
        {
            cmbUsers.DropDownStyle = ComboBoxStyle.DropDownList; 
            cmbUsers.SelectedIndexChanged += CmbUsers_SelectedIndexChanged;
        }

        private void CmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cmbUsers.SelectedItem;
                txtUsername.Text = selectedRow["Username"].ToString();
                cmbRole.SelectedItem = selectedRow["Role"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to edit");
                return;
            }

            DataRowView selectedRow = (DataRowView)cmbUsers.SelectedItem;
            string originalUsername = selectedRow["Username"].ToString();
            string newUsername = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Please enter a username");
                return;
            }

            if (!string.IsNullOrEmpty(newPassword) && newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            if (!newUsername.Equals(originalUsername, StringComparison.OrdinalIgnoreCase) &&
                !_userDal.IsUsernameAvailable(newUsername, originalUsername))
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
                return;
            }

            var updatedUser = new User
            {
                Username = newUsername,
                Role = role
            };

            if (!string.IsNullOrEmpty(newPassword))
            {
                updatedUser.PasswordHash = PasswordHasher.HashPassword(newPassword);
            }

            if (_userDal.UpdateUser(originalUsername, updatedUser))
            {
                MessageBox.Show("User updated successfully!");
                LoadUsers(); 
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to update user");
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            cmbRole.SelectedIndex = -1;
            cmbUsers.SelectedIndex = -1;
        }
    }
}