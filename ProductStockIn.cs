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
    public partial class ProductStockIn : Form
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

        public ProductStockIn()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Categories to DataGridView
            loadProducts();

            // Assign Received cashierForm Argument to Global Variable
            // this.cashierForm = cashierForm;

            loadProducts();
        }

        /// DISPLAY DATA RETREIVE FROM tbProduct to dgvProduct
        public void loadProducts()
        {
            // To Keep Track of Row Number
            int i = 0;

            // Clear All Rows from DataGridView to Prepare for Fresh Data
            dgvProduct.Rows.Clear();

            // Open Database Connection
            connection.Open();

            // Search By ProductCode, Description, Also for Loading Products
            sqlCommand = new SqlCommand("SELECT productCode, description, quantity, weight FROM tbProduct WHERE productCode LIKE '%" + txtSearchProduct.Text + "%' OR description LIKE '%"+ txtSearchProduct.Text + "%'", connection);

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

        /// ADD TO STOCK IN MODULE
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Add Product By Cell Click from in tbCart
            string databaseOperation = dgvProduct.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "AddToCart")
            {
                if (MessageBox.Show("Save This Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            // Load Products
            loadProducts();
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
