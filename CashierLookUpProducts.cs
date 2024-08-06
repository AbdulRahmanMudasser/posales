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
    public partial class CashierLookUpProducts : Form
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

        public CashierLookUpProducts(CashierForm cashierForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Categories to DataGridView
            loadProducts();

            // Assign Received cashierForm Argument to Global Variable
            this.cashierForm = cashierForm;

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

                // SQL Command to Select All Records from tbProduct Table, Also for Searching
                // sqlCommand = new SqlCommand("SELECT p.productCode, p.barcode, p.description, b.brand, c.category, p.price, p.quantity, p.reorder, p.weight from tbProduct AS p INNER JOIN tbBrand as b ON b.id = p.brandId INNER JOIN tbCategory AS c on c.id = p.categoryId WHERE CONCAT(p.description, b.brand, c.category) LIKE '%" +txtSearchProduct.Text + "%'", connection);

                // Search By ProductCode, Barcode, Category, Brand, Also for Loading Products
                sqlCommand = new SqlCommand("SELECT p.productCode, p.barcode, p.description, b.brand, c.category, p.price, p.quantity, p.reorder, p.weight from tbProduct AS p INNER JOIN tbBrand as b ON b.id = p.brandId INNER JOIN tbCategory AS c on c.id = p.categoryId WHERE p.productCode LIKE '%" + txtSearchProduct.Text + "%' OR p.barcode LIKE '%" + txtSearchProduct.Text + "%' OR b.brand LIKE '%" + txtSearchProduct.Text + "%' OR c.category LIKE '%" + txtSearchProduct.Text + "%'", connection);

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate through the DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
                    dgvProduct.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString(), dataReader[7].ToString(), dataReader[8].ToString());
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

        /// CLOSE WINDOW
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Add Product By Cell Click from in tbCart
            string databaseOperation = dgvProduct.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "AddToCart")
            {
                    // To Add Product Name in Cart Table
                    QuantityModule quantityModule = new QuantityModule(cashierForm);

                    quantityModule.productDetails(dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString()), cashierForm.lblTransactionNumberActual.Text, int.Parse(dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString()));

                    quantityModule.ShowDialog();
            }

            // Load Products
            loadProducts();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            loadProducts();
        }

        private void picClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
