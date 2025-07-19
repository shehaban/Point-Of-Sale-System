using point_of_sale_system.DAL;
using point_of_sale_system.Forms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace point_of_sale_system
{
    public partial class UserMng : UserControl
    {

        private string currentUserRole;

        public UserMng(string userRole)
        {
            InitializeComponent();
            currentUserRole = userRole;
            InitializeRoleBasedAccess();


        }

        private void InitializeRoleBasedAccess()
        {
            // Disable all controls if not admin
            if (!currentUserRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                button6.Enabled = false;
                button5.Enabled = false;  // Password field
                button4.Enabled = false;  // Confirm password


                // Show access denied message
                MessageBox.Show("Only administrators can access user management",
                              "Access Denied",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
            }
        }

        public void showUserControl(UserControl userControl)
        {
            thePanel.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            thePanel.Controls.Add(userControl);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void SetUnderlinedButton(Button activeButton)
        {
            // List of buttons in your toggle group
            Button[] buttons = { button4, button5, button6 };

            foreach (Button btn in buttons)
            {
                if (btn == activeButton)
                {
                    // Underline the clicked button (if not already underlined)
                    if (!btn.Font.Style.HasFlag(FontStyle.Underline))
                    {
                        btn.Font = new Font(btn.Font, btn.Font.Style | FontStyle.Underline);
                    }
                }
                else
                {
                    // Remove underline from other buttons
                    if (btn.Font.Style.HasFlag(FontStyle.Underline))
                    {
                        btn.Font = new Font(btn.Font, btn.Font.Style & ~FontStyle.Underline);
                    }
                }
            }
        }



        private void button6_Click_1(object sender, EventArgs e)
        {
            showUserControl(new AddUser());
            label1.Hide();
            label6.Text = "";
            label6.Text = "Add New User";
            SetUnderlinedButton(button6);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showUserControl(new EditUser());
            label1.Hide();
            label6.Text = "";
            label6.Text = "Edit User";
            SetUnderlinedButton(button5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showUserControl(new DeleteUser());
            label1.Hide();
            label6.Text = "";
            label6.Text = "Delete User";
            SetUnderlinedButton(button4);
        }
    }
}