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
    public partial class SupplierForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public SupplierForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Suppliers to DataGridView
            loadSuppliers();
        }

        /// DISPLAY DATA RETREIVE FROM tbSupplier to dgvSupplier
        public void loadSuppliers()
        {
            try
            {
                // To Keep Track of Row Number
                int i = 0;

                // Clear All Rows from DataGridView to Prepare for Fresh Data
                dgvSupplier.Rows.Clear();

                // Open Database Connection
                connection.Open();

                // Search By Supplier Name, Phone Number, Email, Fax Number, Address, Also for Loading Supplier
                sqlCommand = new SqlCommand("SELECT * FROM tbSupplier WHERE (supplierName LIKE @searchQuery OR phoneNumber LIKE @searchQuery OR email LIKE @searchQuery OR faxNumber LIKE @searchQuery OR address LIKE @searchQuery)", connection);

                // Add the supplier Parameter to the SQL Command With the Value from the txtSearchSupplier TextBox
                sqlCommand.Parameters.AddWithValue("@searchQuery", "%" + txtSearchSupplier.Text + "%");

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate through the DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Add New Row to DataGridView With Values from the Current Row
                    dgvSupplier.Rows.Add(i, dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(), dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString());
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
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Suppliers" + ex.Message);
            }
        }

        /// CLOSE WINDOW
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// ADD NEW SUPPLIER
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // To Edit Supplier in Supplier Table
            SupplierModule supplierModule = new SupplierModule(this);

            // Disbale Update Button
            supplierModule.btnUpdate.Enabled = false;

            // Set Background Color to White
            supplierModule.btnUpdate.BackColor = SystemColors.Window;

            // Display SupplierModuleForm as a Dialog
            supplierModule.ShowDialog();
        }

        /// EDIT & DELETE SUPPLIER
        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update & Delete Supplier By Cell Click from in tbProduct
            string databaseOperation = dgvSupplier.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "Edit")
            {
                try
                {
                    // To Edit Supplier Details in Supplier Table
                    SupplierModule supplierModule = new SupplierModule(this);

                    // Set, With the Value from Selected Row's Id Column In DataGridView
                    supplierModule.lblId.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();

                    // Set, With the Value from Selected Row's SupplierName Column In DataGridView
                    supplierModule.txtSupplierName.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();

                    // Set, With the Value from Selected Row's Address Column In DataGridView
                    supplierModule.txtAddress.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();

                    // Set, With the Value from Selected Row's Contact Person Column In DataGridView
                    supplierModule.txtContactPerson.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();

                    // Set, With the Value from Selected Row's Email Column In DataGridView
                    supplierModule.txtEmail.Text = dgvSupplier.Rows[e.RowIndex].Cells[5].Value.ToString();

                    // Set, With the Value from Selected Row's Phone Number Column In DataGridView
                    supplierModule.txtPhoneNumber.Text = dgvSupplier.Rows[e.RowIndex].Cells[6].Value.ToString();

                    // Set, With the Value from Selected Row's Fax Number Column In DataGridView
                    supplierModule.txtFaxNumber.Text = dgvSupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

                    // Disable, Since We are Editing an Existing Supplier
                    supplierModule.btnSave.Enabled = false;

                    // Set Background Color to White
                    supplierModule.btnSave.BackColor = SystemColors.Window;

                    // Enable, To Allow Updating Supplier Details
                    supplierModule.btnUpdate.Enabled = true;

                    // Display SupplierModule as a Dialog
                    supplierModule.ShowDialog();
                } 
                catch (Exception ex)
                {
                    // Close Connection
                    connection.Close();

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception has Occurred while Editing Supplier" + ex.Message);
                }
            }
            else if (databaseOperation == "Delete")
            {
                // To Delete Supplier in Supplier Table
                try
                {
                    if (MessageBox.Show("Delete This Supplier?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Delete Supplier in Supplier Table with Specified id
                        sqlCommand = new SqlCommand("DELETE FROM tbSupplier WHERE id LIKE'" + dgvSupplier[1, e.RowIndex].Value.ToString() + "'", connection);

                        // Execute the SQL Command to Delete Supplier in the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Product was Deleted Successfully
                        MessageBox.Show("Supplier Deleted Successfully", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Close Connection
                    connection.Close();

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception has Occurred while Deleting Supplier" + ex.Message);
                }
            }

            // Load Suppliers
            loadSuppliers();
        }

        /// SEARCH SUPPLIER
        private void txtSearchSupplier_TextChanged(object sender, EventArgs e)
        {
            loadSuppliers();
        }
    }
}
