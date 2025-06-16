using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace point_of_sale_system.DAL
{
    public class DbHelper : IDisposable
    {
        protected string connectionString = "Server=DESKTOP-3P19VR9;Database=pos;Trusted_Connection=True;";
        private SqlConnection connection;

        public DbHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        protected void OpenConnection()
        {
            if (connection != null && connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database connection error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}