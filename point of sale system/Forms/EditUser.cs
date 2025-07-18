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
    public partial class EditUser : UserControl
    {
        private readonly UserDAL _userDal = new UserDAL();
        private DataTable _usersTable;
        public EditUser()
        {
            // In form constructor

            InitializeComponent();
            LoadUsers();
            ConfigureDataGridView();
        }

        private void LoadUsers()
        {
            _usersTable = _userDal.GetAllUsers();
            dataGridView1.DataSource = _usersTable;
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cmbRole.SelectedItem = row.Cells["Role"].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit");
                return;
            }

            string originalUsername = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
            string newUsername = txtUsername.Text.Trim();
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            // Validate inputs
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

            // Update user
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
                LoadUsers(); // Refresh DataGridView
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
        }
    }
}