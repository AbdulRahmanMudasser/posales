using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSales
{
    public partial class QuantityModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        /// PRODUCT CODE VARIABLE
        private string productCode;

        /// PRODUCT PRICE VARIABLE
        private double price;

        /// TRANSACTION NUMBER VARIABLE
        private string transactionNumber;

        /// PRODUCT QUANTITY VARIABLE
        private int quantity;

        /// CASHIER FORM
        CashierForm cashierForm;

        public QuantityModule(CashierForm cashierForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Assign Received cashierForm Argument to Global Variable
            this.cashierForm = cashierForm;

            this.KeyPreview = true;
        }

        public void productDetails(string productCode, double price, string transactionNumber, int quantity)
        {
            this.productCode = productCode;
            this.price = price;
            this.transactionNumber = transactionNumber;
            this.quantity = quantity;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQuantity.Text != string.Empty))
            {
                try
                {
                    // Declare Variables
                    string id = ""; // Variable to store the cart ID
                    int cartQuantity = 0; // Variable to store the quantity of the product in the cart
                    bool productFound = false; // Flag to indicate if the product is found in the cart

                    // Open Database Connection
                    connection.Open();

                    // SQL Command to Check if Product Exists in Cart
                    sqlCommand = new SqlCommand("SELECT id, quantity FROM tbCart WHERE transactionNumber = @transactionNumber AND productCode = @productCode", connection);
                    sqlCommand.Parameters.AddWithValue("@transactionNumber", transactionNumber); // Add transaction number parameter
                    sqlCommand.Parameters.AddWithValue("@productCode", productCode); // Add product code parameter

                    // Execute SQL Command and Read Data
                    using (dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.Read()) // Check if data is read from the DataReader
                        {
                            // Retrieve Cart ID and Quantity from DataReader
                            id = dataReader["id"].ToString();
                            cartQuantity = int.Parse(dataReader["quantity"].ToString());
                            productFound = true; // Set product found flag
                        }
                    }

                    // Close Database Connection
                    connection.Close();

                    // Check if Product is Found and Update or Insert as Needed
                    if (productFound)
                    {
                        // Check if Available Quantity Is Sufficient
                        if (quantity < (int.Parse(txtQuantity.Text) + cartQuantity))
                        {
                            // Display Warning Message If Quantity Is Insufficient
                            MessageBox.Show($"Unable to proceed. Remaining quantity on hand is {quantity}.", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Exit the function
                        }

                        // Open Database Connection
                        connection.Open();

                        // SQL Command to Update Quantity in Cart
                        sqlCommand = new SqlCommand("UPDATE tbCart SET quantity = quantity + @quantity WHERE id = @id", connection);
                        sqlCommand.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text)); // Add quantity parameter
                        sqlCommand.Parameters.AddWithValue("@id", id); // Add cart ID parameter

                        // Execute SQL Command to Update Cart
                        sqlCommand.ExecuteNonQuery();

                        // Clear Barcode TextBox
                        cashierForm.txtBarcode.Clear();

                        // Focus Barcode TextBox
                        cashierForm.txtBarcode.Focus();

                        // Load Cart
                        cashierForm.loadCart();

                        // Dispose Form
                        this.Dispose();
                    }
                    else
                    {
                        // Open Database Connection
                        connection.Open();

                        // SQL Command to Insert New Cart Entry
                        sqlCommand = new SqlCommand("INSERT INTO tbCart (transactionNumber, productCode, price, quantity, sDate, cashier) VALUES (@transactionNumber, @productCode, @price, @quantity, @sDate, @cashier)", connection);
                        sqlCommand.Parameters.AddWithValue("@transactionNumber", transactionNumber); // Add transaction number parameter
                        sqlCommand.Parameters.AddWithValue("@productCode", productCode); // Add product code parameter
                        sqlCommand.Parameters.AddWithValue("@price", price); // Add price parameter
                        sqlCommand.Parameters.AddWithValue("@quantity", int.Parse(txtQuantity.Text)); // Add quantity parameter
                        sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now); // Add current date parameter
                        sqlCommand.Parameters.AddWithValue("@cashier", cashierForm.lblCashierName.Text); // Add cashier name parameter

                        // Execute SQL Command to Insert New Cart Entry
                        sqlCommand.ExecuteNonQuery();
                    }

                    // Close Database Connection
                    connection.Close();

                    // Clear Barcode TextBox
                    cashierForm.txtBarcode.Clear();

                    // Focus Barcode TextBox
                    cashierForm.txtBarcode.Focus();

                    // Load Updated Cart
                    cashierForm.loadCart();

                    // Dispose Form
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    // Ensure Database Connection is Closed in Case of Exception
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    // Display Message to User That an Unexpected Exception Has Occurred
                    MessageBox.Show($"An unexpected exception has occurred while adding to cart: {ex.Message}");
                }
            }
        }

        private void QuantityModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }


        //private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if ((e.KeyChar == 13) && (txtQuantity.Text != string.Empty))
        //    {
        //        try
        //        {
        //            // Open Connection
        //            connection.Open();

        //            // SQL Command to Insert Cart Into the Cart Table
        //            sqlCommand = new SqlCommand("INSERT INTO tbCart (transactionNumber, productCode, price, quantity, sDate, cashier) VALUES (@transactionNumber, @productCode, @price, @quantity, @sDate, @cashier)", connection);

        //            // Add the product Parameters to the SQL Command With the Value
        //            sqlCommand.Parameters.AddWithValue("@transactionNumber", transactionNumber);
        //            sqlCommand.Parameters.AddWithValue("@productCode", productCode);
        //            sqlCommand.Parameters.AddWithValue("@price", price);
        //            sqlCommand.Parameters.AddWithValue("@quantity", txtQuantity.Text);
        //            sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now);
        //            sqlCommand.Parameters.AddWithValue("@cashier", cashierForm.lblCashierName.Text);

        //            // Execute the SQL Command to Insert the New Cart Into the Database
        //            sqlCommand.ExecuteNonQuery();

        //            // Close the Database Connection
        //            connection.Close();

        //            cashierForm.loadCart();

        //            // Dispose Form
        //            this.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            // Close Connection
        //            connection.Close();

        //            // Display User that an Unexpected Exception has Occurred
        //            MessageBox.Show("An Unexpected Exception has Occurred while Inserting In Cart" + ex.Message);
        //        }
        //    }
        //}
    }
}
