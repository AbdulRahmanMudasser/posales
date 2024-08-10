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
    public partial class LoginForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public string password = "";

        public bool isActive;

        public LoginForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            this.KeyPreview = true;
        }

        private void picClose_Click(object sender, EventArgs e)
        {

        }

        private void picClose_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Exit POSales?\n\nThe Application Will Close Upon Confirmation", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {                
                Application.Exit();
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Are You Sure You Want To Exit POSales?\n\nThe Application Will Close Upon Confirmation", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();

            txtPassword.Clear();

            txtUsername.Focus();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            // Initialize Variables To Store Username
            string username = "";

            // Initialize Variables To Store Name
            string name = "";

            // Initialize Variables To Store Role
            string role = "";

            try
            {
                // Initialize Flag To Indicate If User Is Found
                bool userFound = false;

                // Open The Database Connection
                connection.Open();

                // Prepare SQL Query To Check User Credentials
                sqlCommand = new SqlCommand("SELECT * FROM tbUser WHERE username = @username AND password = @password", connection);

                // Add Username Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@username", txtUsername.Text);

                // Add Password Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@password", txtPassword.Text);

                // Execute The Query And Retrieve The Data Reader
                dataReader = sqlCommand.ExecuteReader();

                // Read The First Row Of The Result Set
                dataReader.Read();

                // Check If The Data Reader Has Rows (User Found)
                if (dataReader.HasRows)
                {
                    // Set User Found Flag To True
                    userFound = true;

                    // Extract Username From The Data Reader
                    username = dataReader["username"].ToString();

                    // Extract Name From The Data Reader
                    name = dataReader["fullName"].ToString();

                    // Extract Password From The Data Reader
                    this.password = dataReader["password"].ToString();

                    // Extract Role From The Data Reader
                    role = dataReader["role"].ToString();

                    // Extract And Parse IsActive Status From The Data Reader
                    this.isActive = bool.Parse(dataReader["isActive"].ToString());
                }
                else
                {
                    // Set User Found Flag To False
                    userFound = false;
                }

                // Close The Data Reader
                dataReader.Close();

                // Close The Database Connection
                connection.Close();

                // Check If User Is Found
                if (userFound)
                {
                    // Check If Account Is Inactive
                    if (!isActive)
                    {
                        // Display Error Message for Inactive Account
                        MessageBox.Show("Account Is Inactive\r\n\nUnable to Login", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Exit the Method
                        return;
                    }

                    // Check If User Role Is Cashier
                    if (role == "Cashier")
                    {
                        // Display Welcome Message for Cashier
                        MessageBox.Show($"Welcome {name}", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear Username Field
                        txtUsername.Clear();

                        // Clear Password Field
                        txtPassword.Clear();

                        // Hide the Current Form
                        this.Hide();

                        // Create a New Instance of Cashier Form
                        CashierForm cashierForm = new CashierForm();

                        // Set the Cashier Name Label
                        cashierForm.lblCashierName.Text = name;

                        // Display the Cashier Form
                        cashierForm.ShowDialog();
                    }

                    // Check If User Role Is Administrator
                    else if (role == "Administrator")
                    {
                        // Display Welcome Message for Administrator
                        MessageBox.Show($"Welcome {name}", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear Username Field
                        txtUsername.Clear();

                        // Clear Password Field
                        txtPassword.Clear();

                        // Hide the Current Form
                        this.Hide();

                        // Create a New Instance of Main Form
                        MainForm mainForm = new MainForm();

                        // Set the Username Label
                        mainForm.lblUsername.Text = name;

                        // Display the Main Form
                        mainForm.ShowDialog();
                    }
                }
                // If User Not Found
                else
                {
                    // Display Warning Message for Incorrect Credentials
                    MessageBox.Show($"Incorrect Credentials", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Perform Clear Button Click Action
                    btnClear.PerformClick();

                    // Exit the Method
                    return;
                }

            }
            catch (Exception ex)
            {
                // Check If The Database Connection Is Open
                if (connection.State == ConnectionState.Open)
                {
                    // Close The Database Connection If It Is Still Open
                    connection.Close();
                }

                // Display Error Message To The User About The Exception
                MessageBox.Show("An Unexpected Exception Has Occurred While Signing In: " + ex.Message);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSignIn.PerformClick();
            }
        }
    }
}
