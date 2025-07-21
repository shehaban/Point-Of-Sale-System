using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace point_of_sale_system.DAL
{
    public class DbHelper : IDisposable
    {
        protected string connectionString = "Server=DESKTOP-3P19VR9;Database=pos;Trusted_Connection=True;";
        public SqlConnection connection;
        public SqlTransaction transaction;



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

        public void BeginTransaction()
        {
            OpenConnection();
            transaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            transaction?.Commit();
            transaction = null;
        }

        public void RollbackTransaction()
        {
            transaction?.Rollback();
            transaction = null;
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}