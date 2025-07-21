using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system.Forms
{
    public partial class AboutUS : Form
    {
        public AboutUS()
        {
            InitializeComponent();
        }

        private async void AboutUS_Load(object sender, EventArgs e)
        {
            label3.Text = "⏳ Loading data, please wait...";

            var aboutData = await ApiHelper.GetAboutApiDataAsync();

            label3.Text = $"{aboutData.title}\n\n{aboutData.description}";
        }



        private void homebtn_Click(object sender, EventArgs e)
        {
            mainFrm mainForm = new mainFrm(UserSession.CurrentUserRole);
            mainForm.Show();
            this.Close();
        }
    }
}
