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
    public partial class CategoryFrom : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public CategoryFrom()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Categories to DataGridView
            loadCategories();
        }

        /// DISPLAY DATA RETREIVE FROM tbCategory TO dgvCategory
        public void loadCategories()
        {
            try
            {
                // To Keep Track of Row Number
                int i = 0;

                // Clear All Rows from DataGridView to Prepare for Fresh Data
                dgvCategory.Rows.Clear();

                // Open Database Connection
                connection.Open();

                // SQL Command to Select All Records from tbCategory Table, Ordered By brand Column
                // sqlCommand = new SqlCommand("SELECT * FROM tbCategory ORDER BY category", connection);

                // For Searching Category, Also for Loading Category
                sqlCommand = new SqlCommand("SELECT * FROM tbCategory WHERE category LIKE '%" + txtSearchCategory.Text + "%'", connection);

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate through the DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
                    dgvCategory.Rows.Add(i, dataReader["id"].ToString(), dataReader["category"].ToString());
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
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Categories" + ex.Message);
            }
        }

        /// CLOSE WINDOW
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            // To Edit Category Name in Brand Table
            CategoryModule categoryModule = new CategoryModule(this);

            // Disbale Update Button
            categoryModule.btnUpdate.Enabled = false;

            // Set Background Color to White
            categoryModule.btnUpdate.BackColor = SystemColors.Window;

            // Display BrandForm as a Dialog
            categoryModule.ShowDialog();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update & Delete Category By Cell Click from in tbCategory
            string databaseOperation = dgvCategory.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "Edit")
            {
                // To Edit Category Name in Category Table
                CategoryModule categoryModule = new CategoryModule(this);

                // Set, With the Value from Selected Row's ID Column In DataGridView
                categoryModule.lblId.Text = dgvCategory[1, e.RowIndex].Value.ToString();

                // Set, With the Value from Selected Row's Category Name Column In DataGridView
                categoryModule.txtCategory.Text = dgvCategory[2, e.RowIndex].Value.ToString();

                // Disable, Since We are Editing an Existing Category
                categoryModule.btnSave.Enabled = false;

                // Set Background Color to White
                categoryModule.btnSave.BackColor = SystemColors.Window;

                // Enable, To Allow Updating Category Name
                categoryModule.btnUpdate.Enabled = true;

                // Display CategoryForm as a Dialog
                categoryModule.ShowDialog();
            }
            else if (databaseOperation == "Delete")
            {
                // To Delete Category Name in Category Table
                try
                {
                    if (MessageBox.Show("Delete This Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Delete Category in Category Table with Specified id
                        sqlCommand = new SqlCommand("DELETE FROM tbCategory WHERE id LIKE'" + dgvCategory[1, e.RowIndex].Value.ToString() + "'", connection);

                        // Execute the SQL Command to Delete Category Name in the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Brand was Deleted Successfully
                        MessageBox.Show("Category Deleted Successfully", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Close Connection
                    connection.Close();

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception has Occurred while Deleting Category" + ex.Message);
                }
            }

            // Load Categories
            loadCategories();
        }

        private void txtSearchCategory_TextChanged(object sender, EventArgs e)
        {
            loadCategories();
        }
    }
}
