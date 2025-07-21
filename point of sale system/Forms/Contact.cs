using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            int id = UserSession.CurrentUserID;
            string name = UserSession.CurrentUsername;
            string msg = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show(
                    "Please enter a message before sending.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                string result = await ApiMessages.SendMessageAsync(id, name, msg);

                MessageBox.Show(
                    "✅ Your message was sent successfully!\n\nServer response:\n" + result,
                    "Message Sent",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtMessage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Failed to send the message.\n\nError Details:\n" + ex.Message,
                    "Send Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm(UserSession.CurrentUserRole);
            mainForm.Show();
            this.Close();
        }

        private void Contact_Load(object sender, EventArgs e)
        {
            txtID.Text = UserSession.CurrentUserID.ToString();
            txtName.Text = UserSession.CurrentUsername;
        }
    }
}
