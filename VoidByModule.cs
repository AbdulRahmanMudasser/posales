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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;

namespace POSales
{
    public partial class VoidByModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        CancelOrderForm cancelOrderForm = new CancelOrderForm();

        public VoidByModule(CancelOrderForm cancelOrderForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            this.cancelOrderForm = cancelOrderForm;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void VoidByModiule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text.ToLower() == cancelOrderForm.txtCancelledBy.Text.ToLower())
                {
                    MessageBox.Show("Void By & Cancelled By Name Are Same\n\nPlease Void By Another Person", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                string user;

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
                    user = dataReader["name"].ToString();

                    // Close The Data Reader
                    dataReader.Close();

                    // Close The Database Connection
                    connection.Close();

                    saveCancelOrder(user);

                    if (cancelOrderForm.cboAddToInventory.Text == "Yes")
                    {
                        string innerQuery = "UPDATE tbProduct SET quantity = @quantity WHERE productCode = @productCode";
                        string innerExceptionTitle = "Updating Product Quantity";

                        connectionClass.ExecuteQueryWithTwoParameters(innerQuery, innerExceptionTitle, "quantity", cancelOrderForm.txtQuantity.Text, "productCode", cancelOrderForm.txtProductCode.Text);
                    }

                    string query = "UPDATE tbCart SET quantity = @quantity WHERE id = @id";
                    string exceptionTitle = "Updating Product Quantity";

                    connectionClass.ExecuteQueryWithTwoParameters(query, exceptionTitle, "quantity", cancelOrderForm.txtQuantity.Text, "productCode", cancelOrderForm.txtId.Text);

                    MessageBox.Show("Order Transaction Successfully Cancelled\n\nCancel Order", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Dispose();

                    cancelOrderForm.Dispose();
                }

                dataReader.Close();

                connection.Close();
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
                MessageBox.Show("An Unexpected Exception Has Occurred While Confirming Credentials: " + ex.Message);
            }
        }

        private void saveCancelOrder(string user)
        {
            try
            {
                // Open The Database Connection
                connection.Open();

                // Prepare SQL Query To Insert In tbCancel
                sqlCommand = new SqlCommand("INSERT INTO tbCancel (transactionNumber, productCode, price, quantity, total, sDate, voidBy, cancelledBy, reason, action) VALUES (@transactionNumber, @productCode, @price, @quantity, @total, @sDate, @voidBy, @cancelledBy, @reason, @action)", connection);

                // Add Transaction Number Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@transactionNumber", cancelOrderForm.txtTransactionNumber.Text);

                // Add Product Code Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@productCode", cancelOrderForm.txtProductCode.Text);

                // Add Price Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@price", cancelOrderForm.txtProductCode.Text);

                // Add Total Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@total", double.Parse(cancelOrderForm.txtTotal.Text));

                // Add Quantity Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@quantity", int.Parse(cancelOrderForm.txtQuantity.Text));

                // Add Date Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now);

                // Add Void By Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@voidBy", user);

                // Add Cancelled By Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@cancelledBy", cancelOrderForm.txtCancelledBy.Text);

                // Add Reason Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@reason", cancelOrderForm.txtReason.Text);

                // Add Action Parameter To The SQL Query
                sqlCommand.Parameters.AddWithValue("@action", cancelOrderForm.cboAddToInventory.Text);

                // Execute The Query
                sqlCommand.ExecuteNonQuery();

                // Close The Database Connection
                connection.Close();
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
                MessageBox.Show("An Unexpected Exception Has Occurred While Confirming Credentials: " + ex.Message);
            }
        }
    }
}
