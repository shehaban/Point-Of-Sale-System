using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;
using System;
using System.Data;
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
            errorProvider1.Clear(); // مسح الأخطاء السابقة

            bool hasError = false;

            // التحقق من صلاحيات الأدمن
            if (!currentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Unauthorized access attempt logged",
                                "Security Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // التحقق من اسم المستخدم
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || txtUsername.Text.Length < 4)
            {
                errorProvider1.SetError(txtUsername, "Username must be at least 4 characters.");
                hasError = true;
            }

            // التحقق من تطابق كلمة المرور
            if (textBox1.Text != textBox2.Text)
            {
                errorProvider1.SetError(textBox2, "Passwords do not match.");
                hasError = true;
            }

            // التحقق من طول كلمة المرور
            if (textBox1.Text.Length < 8)
            {
                errorProvider1.SetError(textBox1, "Password must be at least 8 characters.");
                hasError = true;
            }

            // التحقق من اختيار الدور
            if (cmbRole.SelectedItem == null)
            {
                errorProvider1.SetError(cmbRole, "Please select a role.");
                hasError = true;
            }

            if (hasError)
                return;

            UserDAL userDal = new UserDAL();
            if (!userDal.IsUsernameAvailable(txtUsername.Text))
            {
                errorProvider1.SetError(txtUsername, "Username already exists.");
                return;
            }

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
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Failed to add user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            textBox1.Clear();
            textBox2.Clear();
            cmbRole.SelectedIndex = -1;
            errorProvider1.Clear();
        }
    }
}
