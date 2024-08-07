using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSales
{
    public partial class SettlePaymentModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        /// CASHIER FORM
        CashierForm cashierForm;

        public SettlePaymentModule(CashierForm cashierForm)
        {
            InitializeComponent();

            // Set The KeyPreview Property To True, Allowing The Module To Capture Keyboard Events
            // this.KeyPreview = true;

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            this.cashierForm = cashierForm;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            // Dispose Of The Module When The Close Button Is Clicked
            this.Dispose();
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Zero Button To The Text In The TxtCash Text Box
            txtCash.Text += btnZero.Text;
        }

        private void btnDoubleZero_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Double Zero Button To The Text In The TxtCash Text Box
            txtCash.Text += btnDoubleZero.Text;
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            // Append The Text Of The One Button To The Text In The TxtCash Text Box
            txtCash.Text += btnOne.Text;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Two Button To The Text In The TxtCash Text Box
            txtCash.Text += btnTwo.Text;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Three Button To The Text In The TxtCash Text Box
            txtCash.Text += btnThree.Text;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Four Button To The Text In The TxtCash Text Box
            txtCash.Text += btnFour.Text;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Five Button To The Text In The TxtCash Text Box
            txtCash.Text += btnFive.Text;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Six Button To The Text In The TxtCash Text Box
            txtCash.Text += btnSix.Text;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Seven Button To The Text In The TxtCash Text Box
            txtCash.Text += btnSeven.Text;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Eight Button To The Text In The TxtCash Text Box
            txtCash.Text += btnEight.Text;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            // Append The Text Of The Nine Button To The Text In The TxtCash Text Box
            txtCash.Text += btnNine.Text;
        }

        //private void BtnEnter_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Check If The Change Amount Is Less Than Zero Or If The Cash Amount Is Empty
        //        if ((double.Parse(txtChange.Text) < 0) || txtCash.Text.Equals(""))
        //        {
        //            // Display A Message Box With A Warning If The Amount Is Insufficient
        //            MessageBox.Show("Insufficient Amount", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //            // Return From The Method Without Proceeding
        //            return;
        //        }
        //        else
        //        {
        //            // Iterate Through Each Row In The Cart
        //            for (int i = 0; i < cashierForm.dgvCart.Rows.Count; i++)
        //            {
        //                // Open The Database Connection
        //                connection.Open();

        //                // Create A SQL Command To Update The Product Quantity
        //                SqlCommand sqlCommand = new SqlCommand("UPDATE tbProduct SET quantity = quantity - @quantity WHERE productCode = @productCode", connection);

        //                // Add Parameters To The SQL Command
        //                sqlCommand.Parameters.AddWithValue("@quantity", int.Parse(cashierForm.dgvCart.Rows[i].Cells[5].Value.ToString()));
        //                sqlCommand.Parameters.AddWithValue("@productCode", cashierForm.dgvCart.Rows[i].Cells[2].Value.ToString());

        //                // Execute The SQL Command To Update The Product Quantity
        //                sqlCommand.ExecuteNonQuery();

        //                // Close The Database Connection
        //                connection.Close();

        //                // Open The Database Connection Again
        //                connection.Open();

        //                // Create A SQL Command To Update The Cart Status
        //                SqlCommand sqlCommand = new SqlCommand("UPDATE tbCart SET status = 'Sold' WHERE id = @id", connection);

        //                // Add Parameters To The SQL Command
        //                sqlCommand.Parameters.AddWithValue("@id", int.Parse(cashierForm.dgvCart.Rows[i].Cells[1].Value.ToString()));

        //                // Execute The SQL Command To Update The Cart Status
        //                sqlCommand.ExecuteNonQuery();

        //                // Close The Database Connection
        //                connection.Close();
        //            }

        //            // Display A Message Box To Inform The User That The Payment Has Been Charged
        //            MessageBox.Show("Payment Charged", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            // Call The getTransactionNumber Method To Get The Transaction Number
        //            cashierForm.getTransactionNumber();

        //            // Call The loadCart Method To Load The Cart
        //            cashierForm.loadCart();

        //            // Dispose Of The Current Form
        //            this.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ensure The Database Connection Is Closed In Case Of An Exception
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            connection.Close();
        //        }

        //        // Display A Message Box To Inform The User That An Unexpected Exception Has Occurred
        //        MessageBox.Show($"An Unexpected Exception Has Occurred While Charging Payment: {ex.Message}");
        //    }
        //}

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the change amount and cash amount
                if (!double.TryParse(txtChange.Text, out double change) || change < 0 || string.IsNullOrEmpty(txtCash.Text))
                {
                    MessageBox.Show("Insufficient Amount", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Open the database connection if not opened already
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                foreach (DataGridViewRow row in cashierForm.dgvCart.Rows)
                {
                    if (row.Cells[5].Value == null || row.Cells[2].Value == null || row.Cells[1].Value == null)
                        continue;

                    // Update product quantity
                    using (SqlCommand cmdProduct = new SqlCommand("UPDATE tbProduct SET quantity = quantity - @quantity WHERE productCode = @productCode", connection))
                    {
                        cmdProduct.Parameters.AddWithValue("@quantity", int.Parse(row.Cells[5].Value.ToString()));
                        cmdProduct.Parameters.AddWithValue("@productCode", row.Cells[2].Value.ToString());
                        cmdProduct.ExecuteNonQuery();
                    }

                    // Update cart status
                    using (SqlCommand cmdCart = new SqlCommand("UPDATE tbCart SET status = 'Sold' WHERE id = @id", connection))
                    {
                        cmdCart.Parameters.AddWithValue("@id", int.Parse(row.Cells[1].Value.ToString()));
                        cmdCart.ExecuteNonQuery();
                    }
                }

                // Close the database connection
                connection.Close();

                // Inform user and update the UI
                MessageBox.Show("Payment Charged", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cashierForm.getTransactionNumber();
                
                cashierForm.loadCart();
                
                this.Dispose();
            }
            catch (Exception ex)
            {
                // Ensure the database connection is closed in case of an exception
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                MessageBox.Show($"An Unexpected Exception Has Occurred While Charging Payment: {ex.Message}");
            }
        }


        //private void btnEnter_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Check If Change Amount Is Less Than Zero Or Cash Amount Is Empty
        //        if (double.Parse(txtChange.Text) < 0 || string.IsNullOrEmpty(txtCash.Text))
        //        {
        //            // Display Warning Message For Insufficient Amount
        //            MessageBox.Show("Insufficient Amount", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }


        //        // Iterate Through Each Row In The Cart
        //        foreach (DataGridViewRow row in cashierForm.dgvCart.Rows)
        //        {

        //            // Update Product Quantity In Database
        //            UpdateDatabase(
        //                "UPDATE tbProduct SET quantity = quantity - @quantity WHERE productCode = @productCode",
        //                new SqlParameter("@quantity", int.Parse(row.Cells[5].Value.ToString())),
        //                new SqlParameter("@productCode", row.Cells[2].Value.ToString())
        //            );

        //            // Update Cart Status In Database
        //            UpdateDatabase(
        //                "UPDATE tbCart SET status = 'Sold' WHERE id = @id",
        //                new SqlParameter("@id", int.Parse(row.Cells[1].Value.ToString()))
        //            );
        //        }

        //        // Display Information Message For Charged Payment
        //        MessageBox.Show("Payment Charged", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // Call Method To Get Transaction Number
        //        cashierForm.getTransactionNumber();

        //        // Call Method To Load Cart
        //        cashierForm.loadCart();

        //        // Dispose Of Current Form
        //        this.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Ensure Database Connection Is Closed In Case Of Exception
        //        if (connection.State == ConnectionState.Open)
        //            connection.Close();

        //        // Display Error Message For Unexpected Exception
        //        MessageBox.Show($"An Unexpected Exception Has Occurred While Charging Payment: {ex.Message}");
        //    }
        //}

        //private void UpdateDatabase(string query, params SqlParameter[] parameters)
        //{
        //    // Open Database Connection And Execute Query
        //    using (connection)
        //    {
        //        connection.Open();
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddRange(parameters);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}


        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear The Text In The TxtCash Text Box
            txtCash.Clear();

            // Set The Focus Back To The TxtCash Text Box
            txtCash.Focus();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            // Debug output to track event trigger
            Debug.WriteLine("txtCash_TextChanged event triggered.");

            try
            {
                double Sale = 0;
                double Cash = 0;

                // Trim leading and trailing spaces from txtSale.Text and txtCash.Text
                string saleText = txtSale.Text.Trim();
                string cashText = txtCash.Text.Trim();

                // Debug output for trimmed saleText and cashText
                Debug.WriteLine($"Trimmed txtSale.Text: {saleText}, Trimmed txtCash.Text: {cashText}");

                // Check if the txtSale text box contains a valid number
                if (!double.TryParse(saleText, out Sale))
                {
                    // If not, set the default value
                    txtChange.Text = "0.00";
                    Debug.WriteLine("Invalid Sale amount.");
                    return;
                }

                // Check if the txtCash text box contains a valid number
                if (!double.TryParse(cashText, out Cash))
                {
                    // If not, set the default value
                    txtChange.Text = "0.00";
                    Debug.WriteLine("Invalid Cash amount.");
                    return;
                }

                // Calculate the change by subtracting the Sale from the Cash
                double Change = Cash - Sale;

                // Display the change in the txtChange text box
                txtChange.Text = Change.ToString("#,##0.00");

                // Debug output for calculated change
                Debug.WriteLine($"Calculated Change: {Change}");
            }
            catch (Exception ex)
            {
                // If an exception occurs, display a default value of "0.00" in the txtChange text box
                txtChange.Text = "0.00";
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }




        private void ShowMessageBox(string message)
        {
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                MessageBox.Show(this, message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }



        private void SettlePaymentModule_KeyDown(object sender, KeyEventArgs e)
        {
            // Check If The Pressed Key Is The Escape Key
            if (e.KeyCode == Keys.Escape)
            {
                // If The Escape Key Is Pressed, Dispose Of The Module
                this.Dispose();
            }
            // Check If The Pressed Key Is The Enter Key
            else if (e.KeyCode == Keys.Enter)
            {
                // If The Enter Key Is Pressed, Perform A Click Event On The BtnEnter Button
                btnEnter.PerformClick();
            }
        }
    }
}
