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
    public partial class DiscountModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// CASHIER FORM
        CashierForm cashierForm = new CashierForm();

        public DiscountModule(CashierForm cashierForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            this.cashierForm = cashierForm;

            txtDiscountPercentage.Focus();
        }

        /// CANCEL BUTTON
        private void button3_Click(object sender, EventArgs e)
        {
            // Clear TextBox
            clear();
        }

        /// CLOSE WINDOW BUTTOn
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// SAVE BUTTON
        private void btnSave_Click(object sender, EventArgs e)
        {
            // To Update Disount in Database
            try
            {
                if (MessageBox.Show("Apply This Discount?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Update a Discount in tbCart Table
                    sqlCommand = new SqlCommand("UPDATE tbCart SET discountPercentage = @discountPercentage WHERE id = @id", connection);

                    // Add the brand Parameter to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@discountPercentage", double.Parse(txtDiscountPercentage.Text));
                    sqlCommand.Parameters.AddWithValue("@id", int.Parse(lblId.Text.ToString()));

                    // Execute the SQL Command to Insert the New Category Into the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Load Cart
                    cashierForm.loadCart();

                    // Dispose Form
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Applying Discount " + ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtTotalPrice.Clear();

            txtTotalPrice.Focus();
        }

        private void DiscountModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void txtDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = double.Parse(txtTotalPrice.Text) * double.Parse(txtDiscountPercentage.Text) * 0.01;

                txtDiscountAmount.Text = discount.ToString("#, ##0.00");
            }
            catch (Exception)
            {
                txtDiscountAmount.Text = "0.00";
            }
        }
    }
}
