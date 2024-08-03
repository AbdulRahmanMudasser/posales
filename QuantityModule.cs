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
    public partial class QuantityModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

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
        }

        public void productDetails(string productCode, double price, string transactionNumber, int quantity)
        {
            this.productCode = productCode;
            this.price = price;
            this.transactionNumber = transactionNumber;
            this.quantity = quantity;
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQuantity.Text != string.Empty))
            {
                // Open Connection
                connection.Open();

                // SQL Command to Insert Cart Into the Cart Table
                sqlCommand = new SqlCommand("INSERT INTO tbCart (transactionNumber, productCode, price, quantity, sDate, cashier) VALUES (@transactionNumber, @productCode, @price, @quantity, @sDate, @cashier)", connection);

                // Add the product Parameters to the SQL Command With the Value
                sqlCommand.Parameters.AddWithValue("@transactionNumber", transactionNumber);
                sqlCommand.Parameters.AddWithValue("@productCode", productCode);
                sqlCommand.Parameters.AddWithValue("@price", price);
                sqlCommand.Parameters.AddWithValue("@quantity", txtQuantity.Text);
                sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@cashier", cashierForm.lblCashierName.Text);

                // Execute the SQL Command to Insert the New Cart Into the Database
                sqlCommand.ExecuteNonQuery();

                // Close the Database Connection
                connection.Close();

                cashierForm.loadCart();

                // Dispose Form
                this.Dispose();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
