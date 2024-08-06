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
    public partial class StockInModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public StockInModule()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Suppliers
            loadSuppliers();

            // Generate Reference Number
            generateReferenceNumber();
        }

        /// LOAD SUPPLIERS
        public void loadSuppliers()
        {
            // Clear Existing Items in ComboBox
            cboSupplier.Items.Clear();

            // Set Data Source to Category Table
            cboSupplier.DataSource = connectionClass.getTable("SELECT * FROM tbSupplier");

            // Display Category Name
            cboSupplier.DisplayMember = "supplierName";

            // Use Supplier ID as Value
            cboSupplier.ValueMember = "id";

        }

        private void btnAccountSave_Click(object sender, EventArgs e)
        {
            // To Insert New User in User Table
            try
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passord & Confirm Password Don't Match", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                else
                {
                    if (MessageBox.Show("Save This User?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Insert a New User in the User Table
                        sqlCommand = new SqlCommand("INSERT INTO tbUser (username, password, role, fullName) VALUES (@username, @password, @role, @fullName)", connection);

                        // Add the user Parameters to the SQL Command With the Value
                        sqlCommand.Parameters.AddWithValue("@username", txtUserName.Text);
                        sqlCommand.Parameters.AddWithValue("@password", txtPassword.Text);
                        sqlCommand.Parameters.AddWithValue("@role", cboRole.Text);
                        sqlCommand.Parameters.AddWithValue("@fullName", txtFullName.Text);

                        // Execute the SQL Command to Insert the New User Into the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Category was Added Successfully
                        MessageBox.Show("User Added Successfully", "POSales");

                        // Clear Supllier Module Form
                        clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Open Connection
            connection.Open();

            // SQL Command to Insert a New User in the User Table
            sqlCommand = new SqlCommand("SELECT * FROM tbSupplier WHERE supplierName LIKE '" + cboSupplier.Text + "' ", connection);

            // Execute the SQL Command and Retrieve Data
            dataReader = sqlCommand.ExecuteReader();

            // Read the Retrieved Data
            dataReader.Read();

            // Check if the Data Reader Has Rows (Records)
            if (dataReader.HasRows)
            {
                // Set the Supplier ID Label with the Retrieved Data
                lblId.Text = dataReader["id"].ToString();

                // Set the Contact Person Textbox with the Retrieved Data
                txtContactPerson.Text = dataReader["contactPerson"].ToString();

                // Set the Address Textbox with the Retrieved Data
                txtAddress.Text = dataReader["address"].ToString();
            }

            // Close the Data Reader
            dataReader.Close();

            // Close the Database Connection
            connection.Close();
        }

        private void cboSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Set the Handled Property to True to Prevent the User from Typing in the Combo Box
            e.Handled = true;
        }

        /// GENERATE REFERENCE NUMBER
        public void generateReferenceNumber()
        {
            Random random = new Random();

            txtReferenceNumber.Clear();

            txtReferenceNumber.Text += random.Next();
        }

        private void btnGenerateReferenceNumber_Click(object sender, EventArgs e)
        {
            generateReferenceNumber();
        }

        /// LOAD STOCK IN
        public void loadStockIn()
        {
            // To Keep Track of Row Number
            int i = 0;

            // Clear All Rows from DataGridView to Prepare for Fresh Data
            dgvStockIn.Rows.Clear();

            // Open Database Connection
            connection.Open();

            // Load Stock In Table in Database
            sqlCommand = new SqlCommand("SELECT * FROM vwStockIn WHERE referenceNumber LIKE '" + txtReferenceNumber.Text + "' AND status LIKE 'Pending'", connection);

            // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
            dataReader = sqlCommand.ExecuteReader();

            // Iterate through the DataReader to Read Each Row of Data
            while (dataReader.Read())
            {
                // Increment Counter for Each Row
                i++;

                // Add New Row to DataGridView With Values from the Current Row
                dgvStockIn.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString(), dataReader[7].ToString());
            }

            // Close DataReader After Reading All Data
            dataReader.Close();

            // Close Database Connection
            connection.Close();
        }

        private void btnBrowseProducts_Click(object sender, EventArgs e)
        {
            ProductStockIn productStockIn = new ProductStockIn(this);

            productStockIn.ShowDialog();
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStockIn.Rows.Count > 0)
                {
                    if (MessageBox.Show("Save This Stock In Record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i <  dgvStockIn.Rows.Count; i++)
                        {
                            /// Update Product Quantity

                            // Open Database Connection
                            connection.Open();

                            // SQL Command to Update a Product Quantity in the Product Table
                            sqlCommand = new SqlCommand("UPDATE tbProduct SET quantity = quantity + " + int.Parse(dgvStockIn.Rows[i].Cells[5].Value.ToString()) + "WHERE productCode LIKE '" + dgvStockIn.Rows[i].Cells[3].Value.ToString() + "'", connection);

                            // Execute the SQL Command and Retrieve Data
                            dataReader = sqlCommand.ExecuteReader();

                            // Close the Database Connection
                            connection.Close();

                            /// Update Stock In Quantity

                            // Open Database Connection
                            connection.Open();

                            // SQL Command to Update a Stock In Quantity in the Stock In Table
                            sqlCommand = new SqlCommand("UPDATE tbStockIn SET quantity = quantity + " + int.Parse(dgvStockIn.Rows[i].Cells[5].Value.ToString()) + ", status = 'Done' WHERE id LIKE '" + dgvStockIn.Rows[i].Cells[1].Value.ToString() + "'", connection);

                            // Execute the SQL Command and Retrieve Data
                            dataReader = sqlCommand.ExecuteReader();

                            // Close the Database Connection
                            connection.Close();

                            // Display User that the Stock In Record was Added Successfully
                            MessageBox.Show("Stock In Record Added Successfully", "POSales");
                        }

                        // Load Stock In
                        loadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void clear()
        {
            txtReferenceNumber.Clear();

            txtStockInBy.Clear();

            dtpStockInDate.Value = DateTime.Now;
        }

        private void dgvStockIn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Add Product By Cell Click from in tbCart
            string databaseOperation = dgvStockIn.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "Delete")
            {
                if (MessageBox.Show("Remove This Stock In Record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        // Open Database Connection
                        connection.Open();

                        // SQL Command to Delete a Stock In Record from the StockIn Table
                        sqlCommand = new SqlCommand("DELETE FROM tbStockIn WHERE id = '" + dgvStockIn.Rows[e.RowIndex].Cells[1].Value.ToString() +  "' ", connection);

                        // Execute the SQL Command to Delete Record the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        clear();

                        // Dispose Form
                        // this.Dispose();

                        // Load Stock In
                        loadStockIn();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchUserSettings_Click(object sender, EventArgs e)
        {

        }
    }
}
