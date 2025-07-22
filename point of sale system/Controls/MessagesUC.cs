using point_of_sale_system.DAL;
using point_of_sale_system.Models;
using point_of_sale_system.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace point_of_sale_system.Controls
{
    public partial class MessagesUC : UserControl
    {
        private Label loadingLabel; 

        public MessagesUC()
        {
            InitializeComponent();

            loadingLabel = new Label
            {
                Text = "Loading messages...",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                AutoSize = true,
                Visible = false
            };

            loadingLabel.Location = new Point(
                (this.Width - loadingLabel.Width) / 2,
                (this.Height - loadingLabel.Height) / 2);

            this.Controls.Add(loadingLabel);
            this.Controls.SetChildIndex(loadingLabel, 0); 

            dgvMessages.AutoGenerateColumns = false;
            ConfigureDataGridView();
            dgvMessages.CellClick += dgvMessages_CellClick; 

            Load += MessagesUC_Load;
            dgvMessages.AutoGenerateColumns = false;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvMessages.Columns.Clear();

            dgvMessages.BorderStyle = BorderStyle.FixedSingle;
            dgvMessages.BackgroundColor = Color.White;
            dgvMessages.RowHeadersVisible = false;
            dgvMessages.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMessages.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvMessages.ScrollBars = ScrollBars.Vertical;

            dgvMessages.Font = new Font("Segoe UI", 9);
            dgvMessages.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvMessages.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            dgvMessages.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvMessages.DefaultCellStyle.BackColor = Color.White;
            dgvMessages.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvMessages.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvMessages.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvMessages.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 76);
            dgvMessages.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMessages.EnableHeadersVisualStyles = false;
            dgvMessages.GridColor = Color.FromArgb(240, 240, 240);

            dgvMessages.Dock = DockStyle.Fill;
            dgvMessages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMessages.ColumnHeadersHeight = 35;
            dgvMessages.RowTemplate.Height = 30;

            dgvMessages.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "UserId",
                HeaderText = "User ID",
                Width = 80,
                MinimumWidth = 80,
                FillWeight = 10
            });

            dgvMessages.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Username",
                HeaderText = "Username",
                Width = 120,
                MinimumWidth = 100,
                FillWeight = 15
            });

            dgvMessages.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Message",
                HeaderText = "Message",
                Width = 300,
                MinimumWidth = 150,
                FillWeight = 50,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    WrapMode = DataGridViewTriState.True,
                    Alignment = DataGridViewContentAlignment.TopLeft
                }
            });

            dgvMessages.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "CreatedAt",
                HeaderText = "Date",
                Width = 150,
                MinimumWidth = 120,
                FillWeight = 17,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Format = "g",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            var viewButton = new DataGridViewButtonColumn()
            {
                Name = "View",
                HeaderText = "Action",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 80,
                MinimumWidth = 80,
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle()
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvMessages.Columns.Add(viewButton);

            
        }

        private async void MessagesUC_Load(object sender, EventArgs e)
        {
            if (!UserSession.CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("You are not authorized to access this section.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Visible = false;
                return;
            }

            await LoadMessagesAsync();
        }

        private async Task LoadMessagesAsync()
        {
            try
            {
                dgvMessages.AutoGenerateColumns = false;

                var messages = await ApiMessages.GetMessagesAsync();

                if (messages == null || messages.Count == 0)
                {
                    MessageBox.Show("لا توجد رسائل للعرض.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMessages.DataSource = null;
                    return;
                }

                dgvMessages.DataSource = messages;
                dgvMessages.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading messages: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loadingLabel.Visible = false; 
            }
        }

        private void dgvMessages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMessages.Columns["View"].Index)
            {
                var message = dgvMessages.Rows[e.RowIndex].DataBoundItem as Messages;
                if (message != null)
                {
                    ShowMessageDetail(message);
                    message.IsRead = true; 
                    dgvMessages.Refresh(); 
                }
            }
        }

        private void ShowMessageDetail(Messages message)
        {
            var detailForm = new Form()
            {
                Text = $"Message from {message.Username}",
                Size = new Size(500, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false
            };

            var textBox = new TextBox()
            {
                Text = message.Message,
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10),
                Margin = new Padding(10)
            };

            detailForm.Controls.Add(textBox);
            detailForm.ShowDialog();
        }
    }
}