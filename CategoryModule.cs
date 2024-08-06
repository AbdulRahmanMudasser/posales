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
    public partial class CategoryModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// BRANDFORM OBJECT TO ACCESS LOADBRANDS METHOD
        CategoryFrom categoryForm;

        public CategoryModule(CategoryFrom categoryForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Assign Received brandForm Argument to Global Variable
            this.categoryForm = categoryForm;
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
            // To Insert Category Name to Brand Table
            try
            {
                if (MessageBox.Show("Save This Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Insert a New Category Into the Brand Table
                    sqlCommand = new SqlCommand("INSERT INTO tbCategory (category) VALUES (@category)", connection);

                    // Add the brand Parameter to the SQL Command With the Value from the txtCategory TextBox
                    sqlCommand.Parameters.AddWithValue("@category", txtCategory.Text);

                    // Execute the SQL Command to Insert the New Category Into the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Category was Added Successfully
                    MessageBox.Show("Category Added Successfully", "POSales");

                    // Clear TextBox
                    txtCategory.Clear();

                    // Foucs TextBox
                    txtCategory.Focus();

                    // Load Categories
                    categoryForm.loadCategories();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Saving Category" + ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtCategory.Clear();

            txtCategory.Focus();
        }

        /// UPDATE BUTTON
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // To Update Brand Name in Category Table
            try
            {
                if (MessageBox.Show("Update This Category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Update Category in Category Table with Specified id
                    sqlCommand = new SqlCommand("UPDATE tbCategory SET category = @category WHERE id LIKE'" + lblId.Text + "'", connection);

                    // Add the Updated category Parameter to the SQL Command With the Value from the txtCategory TextBox
                    sqlCommand.Parameters.AddWithValue("@category", txtCategory.Text);

                    // Execute the SQL Command to Update Category Name in the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Category was Added Successfully
                    MessageBox.Show("Category Name Updated Successfully", "POSales");

                    // To Close Window After Updating Category Name
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Updating Category" + ex.Message);
            }
        }
    }
}
