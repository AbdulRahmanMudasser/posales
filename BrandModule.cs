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
    public partial class BrandModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// BRANDFORM OBJECT TO ACCESS LOADBRANDS METHOD
        BrandForm brandForm;

        public BrandModule(BrandForm brandForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Assign Received brandForm Argument to Global Variable
            this.brandForm = brandForm;
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
            // To Insert Brand Name to Brand Table
            try
            {

                if (MessageBox.Show("Save This Brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Insert a New Brand Into the Brand Table
                    sqlCommand = new SqlCommand("INSERT INTO tbBrand (brand, abbreviation) VALUES (@brand, @abbreviation)", connection);

                    // Add the brand Parameter to the SQL Command With the Value from the txtBrand TextBox
                    sqlCommand.Parameters.AddWithValue("@brand", txtBrand.Text);

                    // Add the brand Parameter to the SQL Command With the Value from the txtAbbreviation TextBox
                    sqlCommand.Parameters.AddWithValue("@abbreviation", txtAbbreviation.Text);

                    // Execute the SQL Command to Insert the New Brand Into the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Brand was Added Successfully
                    MessageBox.Show("Brand Added Successfully", "POSales");

                    // Clear TextBox
                    txtBrand.Clear();

                    // Foucs TextBox
                    txtBrand.Focus();

                    // Load Brands
                    brandForm.loadBrands();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Saving Brand" + ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtBrand.Clear();

            txtBrand.Focus();
        }

        /// UPDATE BUTTON
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // To Update Brand Name in Brand Table
            try
            {
                if (MessageBox.Show("Update This Brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Update Brand in Brand Table with Specified id
                    sqlCommand = new SqlCommand("UPDATE tbBrand SET brand = @brand abbreviation = @abbreviation WHERE id LIKE'" + lblId.Text + "'", connection);

                    // Add the Updated brand Parameter to the SQL Command With the Value from the txtBrand TextBox
                    sqlCommand.Parameters.AddWithValue("@brand", txtBrand.Text);

                    // Add the Updated brand Parameter to the SQL Command With the Value from the txtAbbreviation TextBox
                    sqlCommand.Parameters.AddWithValue("@abbreviation", txtAbbreviation.Text);

                    // Execute the SQL Command to Update Brand Name in the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Brand was Added Successfully
                    MessageBox.Show("Brand Name Updated Successfully", "POSales");

                    // To Close Window After Updating Brand Name
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Updating Brand" + ex.Message);
            }
        }
    }
}
