using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSales
{
    public partial class SettingsUserForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        public SettingsUserForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());
        }

        private void btnAccountSave_Click(object sender, EventArgs e)
        {
            // To Insert New User in User Table
            try
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passord & Confirm Password Don't Match", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                else
                {
                    if (MessageBox.Show("Save This User?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Insert a New User in the User Table
                        sqlCommand = new SqlCommand("INSERT INTO tbUser (username, password, role, fullName) VALUES (@username, @password, @role, @fullName)", connection);

                        // Add the user Parameters to the SQL Command With the Value
                        sqlCommand.Parameters.AddWithValue("@username", txtUserName.Text);
                        sqlCommand.Parameters.AddWithValue("@password", txtPassword.Text);
                        sqlCommand.Parameters.AddWithValue("@role", cboRole.Text);
                        sqlCommand.Parameters.AddWithValue("@fullName", txtFullName.Text);

                        // Execute the SQL Command to Insert the New User Into the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Category was Added Successfully
                        MessageBox.Show("User Added Successfully", "POSales");

                        // Clear Supllier Module Form
                        clear();

                        // Dispose Form
                        // this.Dispose();

                        // Load Suppliers
                        // supplierForm.loadSuppliers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtUserName.Clear();

            txtPassword.Clear();

            txtConfirmPassword.Clear();

            txtFullName.Clear();

            cboRole.Text = "";
        }

        /// ACCOUNT CANCEL BUTTON
        private void btnAccountCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
