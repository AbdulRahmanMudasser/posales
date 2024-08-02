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
    public partial class SupplierModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// PRODUCT FORM OBJECT TO ACCESS LOAD SUPPLIERS METHOD
        SupplierForm supplierForm;

        public SupplierModule(SupplierForm supplierForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Assign Received supplierForm Argument to Global Variable
            this.supplierForm = supplierForm;
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
            // To Insert New Supplier to Supplier Table
            try
            {
                if (MessageBox.Show("Save This Supplier?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Insert a New Supplier Into the Supplier Table
                    sqlCommand = new SqlCommand("INSERT INTO tbSupplier (supplierName, address, contactPerson, email, phoneNumber, faxNumber) VALUES (@supplierName, @address, @contactPerson, @email, @phoneNumber, @faxNumber)", connection);

                    // Add the supplier Parameters to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@supplierName", txtSupplierName.Text);
                    sqlCommand.Parameters.AddWithValue("@address", txtAddress.Text);
                    sqlCommand.Parameters.AddWithValue("@contactPerson", txtContactPerson.Text);
                    sqlCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlCommand.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text);
                    sqlCommand.Parameters.AddWithValue("@faxNumber", txtFaxNumber.Text);

                    // Execute the SQL Command to Insert the New Supplier Into the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Category was Added Successfully
                    MessageBox.Show("Supplier Added Successfully", "POSales");

                    // Clear Supllier Module Form
                    clear();

                    // Dispose Form
                    this.Dispose();

                    // Load Suppliers
                    supplierForm.loadSuppliers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtSupplierName.Clear();

            txtAddress.Clear();

            txtContactPerson.Clear();

            txtEmail.Clear();

            txtPhoneNumber.Clear();

            txtFaxNumber.Clear();
        }

        /// UPDATE BUTTON
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // To Update Supplier Details in Product Table
            try
            {
                if (MessageBox.Show("Update This Supplier?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Update Supplier in Supplier Table with Specified id
                    sqlCommand = new SqlCommand("UPDATE tbSupplier SET supplierName=@supplierName, address=@address, contactPerson=@contactPerson, email=@email, phoneNumber=@phoneNumber, faxNumber=@faxNumber WHERE id LIKE @id", connection);

                    // Add the product Parameters to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@id", lblId.Text);
                    sqlCommand.Parameters.AddWithValue("@supplierName", txtSupplierName.Text);
                    sqlCommand.Parameters.AddWithValue("@address", txtAddress.Text);
                    sqlCommand.Parameters.AddWithValue("@contactPerson", txtContactPerson.Text);
                    sqlCommand.Parameters.AddWithValue("@email", txtEmail.Text);
                    sqlCommand.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text);
                    sqlCommand.Parameters.AddWithValue("@faxNumber", txtFaxNumber.Text);

                    // Execute the SQL Command to Update Supplier Detail in the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Supplier was Added Successfully
                    MessageBox.Show("Supplier Details Updated Successfully", "POSales");

                    // To Close Window After Updating Product Details
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// LOAD CATEGORIES
        public void loadCategories()
        {
            // Clear Existing Items in ComboBox
            // cboCategory.Items.Clear();

            // Set Data Source to Category Table
            // cboCategory.DataSource = connectionClass.getTable("SELECT * FROM tbCategory");

            // Display Category Name
            // cboCategory.DisplayMember = "category";

            // Use Category ID as Value
            // cboCategory.ValueMember = "id";
        }

        /// LOAD BRANDS
        public void loadBrands()
        {
            // Clear Existing Items in ComboBox
            // cboBrand.Items.Clear();

            // Set Data Source to Brand Table
            // cboBrand.DataSource = connectionClass.getTable("SELECT * FROM tbBrand");

            // Display Brand Name
            // cboBrand.DisplayMember = "brand";

            // Use Brand ID as Value
            // cboBrand.ValueMember = "id";
        }
    }
}
