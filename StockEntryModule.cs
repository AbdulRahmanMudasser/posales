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
    public partial class StockEntryModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        /// STOCK IN FORM
        StockInModule stockInModule;

        public StockEntryModule(StockInModule stockInModule)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Categories to DataGridView
            loadProducts();

            // Assign Received stockInModule Argument to Global Variable
            this.stockInModule = stockInModule;

            // Load Products
            loadProducts();
        }

        /// DISPLAY DATA RETREIVE FROM tbProduct to dgvProduct
        public void loadProducts()
        {
            try
            {
                // To Keep Track of Row Number
                int i = 0;

                // Clear All Rows from DataGridView to Prepare for Fresh Data
                dgvProduct.Rows.Clear();

                // Open Database Connection
                connection.Open();

                // Search By ProductCode, Description, Also for Loading Products
                sqlCommand = new SqlCommand("SELECT productCode, description, quantity, weight FROM tbProduct WHERE productCode LIKE '%" + txtSearchProduct.Text + "%' OR description LIKE '%" + txtSearchProduct.Text + "%'", connection);

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate through the DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
                    dgvProduct.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString());
                }

                // Close DataReader After Reading All Data
                dataReader.Close();

                // Close Database Connection
                connection.Close();
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Products" + ex.Message);
            }
        }

        /// ADD TO STOCK IN MODULE
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Add Product By Cell Click from in tbCart
            string databaseOperation = dgvProduct.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "AddToCart")
            {
                if (stockInModule.txtStockInBy.Text == string.Empty)
                {
                    // Display MessageBox if Stock In By TextBox is Empty
                    MessageBox.Show("Enter Stock In By Name", "", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    this.Dispose();

                    // Focus on Stock In By TextBox
                    stockInModule.txtStockInBy.Focus();

                    return;
                }
                
                if (MessageBox.Show("Add This Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Open Database Connection
                        connection.Open();

                        // SQL Command to Insert a New Stock In Into the StockIn Table
                        sqlCommand = new SqlCommand("INSERT INTO tbStockIn (referenceNumber, productCode, sDate, stockInBy, supplierId) VALUES (@referenceNumber, @productCode, @sDate, @stockInBy, @supplierId)", connection);

                        // Add the stock in Parameters to the SQL Command With the Value
                        sqlCommand.Parameters.AddWithValue("@referenceNumber", stockInModule.txtReferenceNumber.Text);
                        sqlCommand.Parameters.AddWithValue("@productCode", dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString());
                        sqlCommand.Parameters.AddWithValue("@sDate", stockInModule.dtpStockInDate.Value);
                        sqlCommand.Parameters.AddWithValue("@stockInBy", stockInModule.txtStockInBy.Text);
                        sqlCommand.Parameters.AddWithValue("@supplierId", stockInModule.lblId.Text);

                        // Execute the SQL Command to Insert the New Category Into the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Dispose Form
                        this.Dispose();

                        // Load Stock In
                        stockInModule.loadStockIn();
                    }
                    catch (Exception ex)
                    {
                        // Close Connection
                        connection.Close();

                        // Display User that an Unexpected Exception has Occurred
                        MessageBox.Show("An Unexpected Exception has Occurred while Adding in Stock In" + ex.Message);
                    }
                }
            }
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            loadProducts();
        }

        /// CLOSE WINDOW
        private void picClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
