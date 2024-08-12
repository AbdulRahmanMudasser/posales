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

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Settle Payment?", "POSales", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                    CashierForm.hasCart = false;

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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear The Text In The TxtCash Text Box
            txtCash.Clear();

            // Set The Focus Back To The TxtCash Text Box
            txtCash.Focus();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Sale = 0;
                double Cash = 0;

                // Trim leading and trailing spaces from txtSale.Text and txtCash.Text
                string saleText = txtSale.Text.Trim();
                string cashText = txtCash.Text.Trim();

                // Check if the txtSale text box contains a valid number
                if (!double.TryParse(saleText, out Sale))
                {
                    // If not, set the default value
                    txtChange.Text = "0.00";
                    return;
                }

                // Check if the txtCash text box contains a valid number
                if (!double.TryParse(cashText, out Cash))
                {
                    // If not, set the default value
                    txtChange.Text = "0.00";
                    return;
                }

                // Calculate the change by subtracting the Sale from the Cash
                double Change = Cash - Sale;

                // Display the change in the txtChange text box
                txtChange.Text = Change.ToString("#,##0.00");
            }
            catch (Exception)
            {
                // If an exception occurs, display a default value of "0.00" in the txtChange text box
                txtChange.Text = "0.00";
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
