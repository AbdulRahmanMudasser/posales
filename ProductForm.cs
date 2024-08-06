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
    public partial class ProductForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public ProductForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Categories to DataGridView
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
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Product" + ex.Message);
            }
        }

        /// CLOSE WINDOW
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // To Edit Product in Product Table
            ProductModule productModule = new ProductModule(this);

            // Disbale Update Button
            productModule.btnUpdate.Enabled = false;

            // Set Background Color to White
            productModule.btnUpdate.BackColor = SystemColors.Window;

            // Display BrandForm as a Dialog
            productModule.ShowDialog();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update & Delete Product By Cell Click from in tbProduct
            string databaseOperation = dgvProduct.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "Edit")
            {
                try
                {
                    // To Edit Category Name in Category Table
                    ProductModule productModule = new ProductModule(this);

                    // Set, With the Value from Selected Row's ProductCode Column In DataGridView
                    productModule.txtProductCode.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();

                    // Set, With the Value from Selected Row's Barcode Column In DataGridView
                    productModule.txtBarcode.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

                    // Set, With the Value from Selected Row's Description Column In DataGridView
                    productModule.txtDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();

                    // Set, With the Value from Selected Row's Brand Column In DataGridView
                    productModule.cboBrand.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();

                    // Set, With the Value from Selected Row's Category Column In DataGridView
                    productModule.cboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();

                    // Set, With the Value from Selected Row's Price Column In DataGridView
                    productModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                    // Set, With the Value from Selected Row's Quantity Column In DataGridView
                    productModule.nupQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[7].Value.ToString();

                    // Set, With the Value from Selected Row's ReOrder Level Column In DataGridView
                    productModule.nupReOrder.Text = dgvProduct.Rows[e.RowIndex].Cells[8].Value.ToString();

                    // Set, With the Value from Selected Row's Weight Column In DataGridView
                    productModule.txtWeight.Text = dgvProduct.Rows[e.RowIndex].Cells[9].Value.ToString();

                    // Disable, Since We are Editing an Existing Product
                    productModule.btnSave.Enabled = false;

                    // Set Background Color to White
                    productModule.btnSave.BackColor = SystemColors.Window;

                    // Enable, To Allow Updating Product Details
                    productModule.btnUpdate.Enabled = true;

                    // Display CategoryForm as a Dialog
                    productModule.ShowDialog();
                }
                catch (Exception ex)
                {
                    // Close Connection
                    connection.Close();

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception has Occurred while Editing Product" + ex.Message);
                }
            }
            else if (databaseOperation == "Delete")
            {
                // To Delete Product in Product Table
                try
                {
                    if (MessageBox.Show("Delete This Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Delete Product in Product Table with Specified id
                        sqlCommand = new SqlCommand("DELETE FROM tbProduct WHERE productCode LIKE'" + dgvProduct[1, e.RowIndex].Value.ToString() + "'", connection);

                        // Execute the SQL Command to Delete Product in the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Product was Deleted Successfully
                        MessageBox.Show("Product Deleted Successfully", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Close Connection
                    connection.Close();

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception has Occurred while Deleting Product" + ex.Message);
                }
            }

            // Load Products
            loadProducts();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            loadProducts();
        }
    }
}
