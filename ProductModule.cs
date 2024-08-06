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
    public partial class ProductModule : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// PRODUCT FORM OBJECT TO ACCESS LOAD PRODUCTS METHOD
        ProductForm productForm;

        public ProductModule(ProductForm productForm)
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Assign Received productForm Argument to Global Variable
            this.productForm = productForm;

            // Load Categories to Categories Combo Box
            loadCategories();

            // Load Brands to Brand Combo Box
            loadBrands();
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
            // To Insert Product to Brand Table
            try
            {
                if (MessageBox.Show("Save This Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Insert a New Product Into the Product Table
                    sqlCommand = new SqlCommand("INSERT INTO tbProduct (productCode, barcode, description, brandId, categoryId, price, reorder, quantity, weight) VALUES (@productCode, @barcode, @description, @brandId, @categoryId, @price, @reorder, @quantity, @weight)", connection);

                    // Add the product Parameters to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@productCode", txtProductCode.Text);
                    sqlCommand.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    sqlCommand.Parameters.AddWithValue("@description", txtDescription.Text);
                    sqlCommand.Parameters.AddWithValue("@brandId", cboBrand.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@categoryId", cboCategory.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    sqlCommand.Parameters.AddWithValue("@reorder", nupReOrder.Value);
                    sqlCommand.Parameters.AddWithValue("@weight", txtWeight.Text);
                    sqlCommand.Parameters.AddWithValue("@quantity", nupQuantity.Value);

                    // Execute the SQL Command to Insert the New Category Into the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Category was Added Successfully
                    MessageBox.Show("Product Added Successfully", "POSales");

                    // Clear Product Module Form
                    clear();

                    // Dispose Form
                    this.Dispose();

                    // Load Products
                    productForm.loadProducts();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Saving Product" + ex.Message);
            }
        }

        /// CLEAR TEXTBOX
        private void clear()
        {
            txtProductCode.Clear();

            txtBarcode.Clear();

            txtDescription.Clear();

            txtPrice.Clear();

            cboBrand.SelectedIndex = 0;

            cboCategory.SelectedIndex = 0;

            nupReOrder.Value = 1;
        }

        /// UPDATE BUTTON
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // To Update Product Details in Product Table
            try
            {
                if (MessageBox.Show("Update This Product?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Update Product in Product Table with Specified id
                    sqlCommand = new SqlCommand("UPDATE tbProduct SET barcode=@barcode, description=@description, brandId=@brandId, categoryId=@categoryId, price=@price, reorder=@reorder, quantity=@quantity, weight=@weight WHERE productCode LIKE @productCode", connection);

                    // Add the product Parameters to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@productCode", txtProductCode.Text);
                    sqlCommand.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    sqlCommand.Parameters.AddWithValue("@description", txtDescription.Text);
                    sqlCommand.Parameters.AddWithValue("@brandId", cboBrand.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@categoryId", cboCategory.SelectedValue);
                    sqlCommand.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    sqlCommand.Parameters.AddWithValue("@reorder", nupReOrder.Value);
                    sqlCommand.Parameters.AddWithValue("@weight", txtWeight.Text);
                    sqlCommand.Parameters.AddWithValue("@quantity", nupQuantity.Value);

                    // Execute the SQL Command to Update Product Detail in the Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Product was Added Successfully
                    MessageBox.Show("Product Details Updated Successfully", "POSales");

                    // To Close Window After Updating Product Details
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Updating Product" + ex.Message);
            }
        }

        /// LOAD CATEGORIES
        public void loadCategories()
        {
            try
            {
                // Clear Existing Items in ComboBox
                cboCategory.Items.Clear();

                // Set Data Source to Category Table
                cboCategory.DataSource = connectionClass.getTable("SELECT * FROM tbCategory");

                // Display Category Name
                cboCategory.DisplayMember = "category";

                // Use Category ID as Value
                cboCategory.ValueMember = "id";
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Categories in Combo Box" + ex.Message);
            }
        }

        /// LOAD BRANDS
        public void loadBrands()
        {
            try
            {
                // Clear Existing Items in ComboBox
                cboBrand.Items.Clear();

                // Set Data Source to Brand Table
                cboBrand.DataSource = connectionClass.getTable("SELECT * FROM tbBrand");

                // Display Brand Name
                cboBrand.DisplayMember = "brand";

                // Use Brand ID as Value
                cboBrand.ValueMember = "id";
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Brands in Combo Box" + ex.Message);
            }
        }
    }
}
