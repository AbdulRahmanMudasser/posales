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
    public partial class BrandForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public BrandForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Brands to DataGridView
            loadBrands();
        }

        /// DISPLAY DATA RETREIVE FROM tbBrand TO dgvBrand 
        public void loadBrands()
        {
            // To Keep Track of Row Number
            int i = 0;

            // Clear All Rows from DataGridView to Prepare for Fresh Data
            dgvBrand.Rows.Clear();

            // Open Database Connection
            connection.Open();

            // SQL Command to Select All Records from tbBrand Table, Ordered By brand Column
            // sqlCommand = new SqlCommand("SELECT * FROM tbBrand ORDER BY brand", connection);

            // For Searching Brand, Also for Loading Brands
            sqlCommand = new SqlCommand("SELECT * FROM tbBrand WHERE brand LIKE '%" + txtSearchBrand.Text + "%'", connection);

            // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
            dataReader = sqlCommand.ExecuteReader();

            // Iterate through the DataReader to Read Each Row of Data
            while (dataReader.Read())
            {
                // Increment Counter for Each Row
                i++;

                // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
                dgvBrand.Rows.Add(i, dataReader["id"].ToString(), dataReader["brand"].ToString());
            }

            // Close DataReader After Reading All Data
            dataReader.Close();

            // Close Database Connection
            connection.Close();
        }

        /// CLOSE WINDOW
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            // To Edit Brand Name in Brand Table
            BrandModule brandModule = new BrandModule(this);

            // Disbale Update Button
            brandModule.btnUpdate.Enabled = false;

            // Set Background Color to White
            brandModule.btnUpdate.BackColor = SystemColors.Window;

            // Display BrandForm as a Dialog
            brandModule.ShowDialog();
        }

        private void dgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update & Delete Brand By Cell Click from in tbBrand
            string databaseOperation = dgvBrand.Columns[e.ColumnIndex].Name;

            if (databaseOperation == "Edit")
            {
                // To Edit Brand Name in Brand Table
                BrandModule brandModule = new BrandModule(this);

                // Set, With the Value from Selected Row's ID Column In DataGridView
                brandModule.lblId.Text = dgvBrand[1, e.RowIndex].Value.ToString();

                // Set, With the Value from Selected Row's Brand Name Column In DataGridView
                brandModule.txtBrand.Text = dgvBrand[2, e.RowIndex].Value.ToString();

                // Disable, Since We are Editing an Existing Brand
                brandModule.btnSave.Enabled = false;

                // Set Background Color to White
                brandModule.btnSave.BackColor = SystemColors.Window;

                // Enable, To Allow Updating Brand Name
                brandModule.btnUpdate.Enabled = true;

                // Display BrandForm as a Dialog
                brandModule.ShowDialog();
            }
            else if (databaseOperation == "Delete")
            {
                // To Delete Brand Name in Brand Table
                try
                {
                    if (MessageBox.Show("Delete This Brand?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Open Connection
                        connection.Open();

                        // SQL Command to Delete Brand in Brand Table with Specified id
                        sqlCommand = new SqlCommand("DELETE FROM tbBrand WHERE id LIKE'" + dgvBrand[1, e.RowIndex].Value.ToString() + "'", connection);

                        // Execute the SQL Command to Delete Brand Name in the Database
                        sqlCommand.ExecuteNonQuery();

                        // Close the Database Connection
                        connection.Close();

                        // Display User that the Brand was Deleted Successfully
                        MessageBox.Show("Brand Deleted Successfully", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Load Brands
            loadBrands();
        }

        private void txtSearchBrand_TextChanged(object sender, EventArgs e)
        {
            loadBrands();
        }
    }
}
