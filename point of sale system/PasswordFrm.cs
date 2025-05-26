using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system
{
    public partial class PasswordFrm : Form
    {
        public string EnteredPassword { get; private set; }

        public PasswordFrm()
        {
            InitializeComponent();
        }

        private void PasswordFrm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnteredPassword = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
