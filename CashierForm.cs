using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using System.Globalization;

namespace POSales
{
    public partial class CashierForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        int quantity;

        string productCode;

        double price;

        public CashierForm()
        {
            InitializeComponent();

            // Close All the SubMenus When Application Starts
            customizeDesign();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Get Transaction Number
            getTransactionNumber();

            // Open Connection
            // connection.Open();

            // Display if Connection to Database is Opened
            // MessageBox.Show("Database is Connected");
        }

        #region buttons
        private void customizeDesign()
        {
            //panelSubProduct.Visible = false;
            //panelSubStock.Visible = false;
            //panelSubHistory.Visible = false;
            //panelSubSetting.Visible = false;
        }

        private void hideSubMenu()
        {
            //// for sub products panel
            //if (panelSubProduct.Visible == true)
            //{
            //    panelSubProduct.Visible = false;
            //}

            //// for sub stock panel
            //if (panelSubStock.Visible == true)
            //{
            //    panelSubStock.Visible = false;
            //}

            //// for sub history panel
            //if (panelSubHistory.Visible == true)
            //{
            //    panelSubHistory.Visible = false;
            //}

            //// for sub setting panel
            //if (panelSubSetting.Visible == true)
            //{
            //    panelSubSetting.Visible = false;
            //}
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }

            else
            {
                subMenu.Visible = false;
            }
        }

        /// VARIABLE THAT HOLDS CURRENTLY ACTIVE FORM
        private Form activeForm = null;

        /// METHDO TO OPEN CHILD FORM INSIDE MAIN FORM
        public void openChildrenForm(Form childForm)
        {
            // Close the Current Form, If There is an Active Form
            activeForm?.Close();

            // Set New Child Form as Active Form
            activeForm = childForm;

            // Set Child Form to be Non-Top Level, Making it Child Control
            childForm.TopLevel = false;

            // Remove the Border of the Child Form
            childForm.FormBorderStyle = FormBorderStyle.None;

            // Dock the Child Form to Fill the Parent Container
            childForm.Dock = DockStyle.Fill;

            // Add the Child Form to the panelMain Controls Collection
            panelMain.Controls.Add(childForm);

            // Assign Panel Main's Tag Child Form
            panelMain.Tag = childForm;

            // Bring the Child Form to Front
            childForm.BringToFront();

            // Show the Child Form
            childForm.Show();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public void slider(Button button)
        {
            panelSlider.BackColor = Color.White;

            panelSlider.Height = button.Height;

            panelSlider.Top = button.Top;
        }

        private void btnNewTransactions_Click(object sender, EventArgs e)
        {
            slider(btnNewTransactions);

            // Get Transaction Number
            // getTransactionNumber();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            slider(btnSearchProduct);

            CashierLookUpProducts lookUpProducts = new CashierLookUpProducts(this);

            lookUpProducts.loadProducts();

            lookUpProducts.ShowDialog();
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            slider(btnAddDiscount);
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            slider(btnClearCart);
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            slider(btnDailySales);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            slider(btnChangePassword);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slider(btnLogout);
        }

        private void btnSettlePayment_Click(object sender, EventArgs e)
        {
            slider(btnSettlePayment);
        }

        #endregion buttons

        /// GET TRANSACTION NUMBER
        public void getTransactionNumber()
        {
            try
            {
                // Get Today's Date in the Format yyyyMMdd
                string dateToday = DateTime.Now.ToString("yyyyMMdd");

                int count;
                string transactionNumber;

                // Open Database Connection
                connection.Open();

                // Selecting Transaction 
                sqlCommand = new SqlCommand("SELECT TOP 1 transactionNumber FROM tbCart WHERE transactionNumber LIKE '" + dateToday + "%' ORDER BY id DESC", connection);

                // Execute SQL Command, Obtain SQLDataReader to Read Data From Database
                dataReader = sqlCommand.ExecuteReader();

                dataReader.Read();

                // Check if Data Reader Has Data
                if (dataReader.HasRows)
                {
                    // Get the Latest Transaction Number
                    transactionNumber = dataReader[0].ToString();

                    // Extract the Count Part of the Transaction Number and Increment It
                    count = int.Parse(transactionNumber.Substring(8, 4));

                    // Set the Label to the New Transaction Number
                    lblTransactionNumberActual.Text = dateToday + (count + 1);
                }
                else
                {
                    // If No Transaction Number Is Found, Start With 1001
                    transactionNumber = dateToday + "1001";

                    // Set the Label to the New Transaction Number
                    lblTransactionNumberActual.Text = transactionNumber;
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

                // Display to the User That an Unexpected Exception Has Occurred
                MessageBox.Show("An unexpected exception has occurred while fetching records based on transaction number: " + ex.Message);
            }
        }

        /// LOAD CART
        //public void loadCart()
        //{
        //    // To Keep Track of Row Number
        //    int i = 0;

        //    // To Calculate Total
        //    double total = 0.0;

        //    // To Calculate Discount
        //    double discount = 0.0;

        //    // Clear All Rows from DataGridView to Prepare for Fresh Data
        //    dgvCart.Rows.Clear();

        //    // Open Database Connection
        //    connection.Open();

        //    // Loading Products
        //    sqlCommand = new SqlCommand("SELECT c.id, c.productCode, p.description, c.price, c.quantity, c.discount, c.total FROM tbCart AS c INNER JOIN tbProduct AS p ON c.productCode = p.productCode WHERE c.transactionNumber LIKE @transactionNumber AND c.status LIKE 'Pending'", connection);

        //    // Add the product Parameters to the SQL Command With the Value
        //    sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);

        //    // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
        //    dataReader = sqlCommand.ExecuteReader();

        //    // Iterate through the DataReader to Read Each Row of Data
        //    while (dataReader.Read())
        //    {
        //        // Increment Counter for Each Row
        //        i++;

        //        // Calculate Total
        //        total += Convert.ToDouble(dataReader["total"].ToString());

        //        // Calculate Discount
        //        discount += Convert.ToDouble(dataReader["discount"].ToString());

        //        // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
        //        dgvCart.Rows.Add(i, dataReader["id"].ToString(), dataReader["productCode"].ToString(), dataReader["description"].ToString(), dataReader["price"].ToString(), dataReader["quantity"].ToString(), dataReader["discount"].ToString(), double.Parse(dataReader["total"].ToString()).ToString("#, ##0.00"));
        //    }

        //    // Close DataReader After Reading All Data
        //    dataReader.Close();

        //    // Close Database Connection
        //    connection.Close();

        //    lblSalesTotalActual.Text = total.ToString("#, ##0.00");

        //    lblDisountActual.Text = discount.ToString("#, ##0.00");

        //    MessageBox.Show(lblSalesTotalActual.Text);

        //    getCartTotal();
        //}

        public void loadCart()
        {
            try
            {
                // To Keep Track of Row Number
                int i = 0;

                // To Calculate Total
                double total = 0.0;

                // To Calculate Discount
                double discount = 0.0;

                // Clear All Rows from DataGridView to Prepare for Fresh Data
                dgvCart.Rows.Clear();

                // Open Database Connection
                connection.Open();

                // Define SQL Query to Load Products
                sqlCommand = new SqlCommand("SELECT c.id, c.productCode, p.description, c.price, c.quantity, c.discount, c.total FROM tbCart AS c INNER JOIN tbProduct AS p ON c.productCode = p.productCode WHERE c.transactionNumber LIKE @transactionNumber AND c.status LIKE 'Pending'", connection);

                // Add Transaction Number Parameter to SQL Command
                sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);

                // Execute SQL Command and Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate Through DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Retrieve Values from DataReader for Each Column
                    var id = dataReader["id"].ToString();
                    var productCode = dataReader["productCode"].ToString();
                    var description = dataReader["description"].ToString();
                    var price = dataReader["price"].ToString();
                    var quantity = dataReader["quantity"].ToString();
                    var discountValue = dataReader["discount"].ToString();
                    var totalValue = dataReader["total"].ToString();

                    // Check for Null or Empty Values in Each Column
                    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(productCode) || string.IsNullOrEmpty(description) ||
                        string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(discountValue) || string.IsNullOrEmpty(totalValue))
                    {
                        // Display Message If One or More Columns Contain Null or Empty Values
                        MessageBox.Show("One or more columns contain null or empty values.");
                        continue;
                    }

                    // Calculate Total Amount
                    total += Convert.ToDouble(totalValue);

                    // Calculate Total Discount
                    discount += Convert.ToDouble(discountValue);

                    // Add New Row to DataGridView with Row Number, Product Code, Description, Price, Quantity, Discount Value, and Formatted Total Value
                    dgvCart.Rows.Add(i, productCode, description, price, quantity, discountValue, double.Parse(totalValue).ToString("#,##0.00"));
                }

                // Close DataReader After Reading All Data
                dataReader.Close();

                // Close Database Connection
                connection.Close();

                // Update Sales Total Label with Formatted Total Value
                lblSalesTotalActual.Text = total.ToString("#,##0.00");

                // Update Discount Label with Formatted Discount Value
                lblDisountActual.Text = discount.ToString("#,##0.00");

                // Calculate and Display Cart Total
                getCartTotal();
            }
            catch (Exception ex)
            {
                // Close Database Connection in Case of Exception
                connection.Close();

                // Display Message to User That an Unexpected Exception Has Occurred
                MessageBox.Show("An unexpected exception has occurred while loading cart: " + ex.Message);
            }
        }


        /// GET CART TOTAL
        public void getCartTotal()
        {
            // Parse Discount From Label
            double discount = double.Parse(lblDisountActual.Text);

            // Calculate Sales After Discount
            double sales = double.Parse(lblSalesTotalActual.Text) - discount;

            // 12% Of Payable Tax (Output Tax Less Input Tax)
            double VAT = sales * 0.12;

            // Calculate VATable Amount
            double VATable = sales - VAT;

            // Display VAT Amount
            lblVATActual.Text = VAT.ToString("#, ##0.00");

            // Display VATable Amount
            lblVATableActual.Text = VATable.ToString("#, ##0.00");

            // Display Total Amount
            lblDisplayTotal.Text = VATable.ToString("#, ##0.00");
        }

        public void addToCart(string productCode, double price, int quantity)
        {
            try
            {
                // Declare Variables
                string id = ""; // Variable to store the cart ID
                int cartQuantity = 0; // Variable to store the quantity of the product in the cart
                bool productFound = false; // Flag to indicate if the product is found in the cart

                // Open Database Connection
                connection.Open();

                // SQL Command to Check if Product Exists in Cart
                sqlCommand = new SqlCommand("SELECT id, quantity FROM tbCart WHERE transactionNumber = @transactionNumber AND productCode = @productCode", connection);
                sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text); // Add transaction number parameter
                sqlCommand.Parameters.AddWithValue("@productCode", productCode); // Add product code parameter

                // Execute SQL Command and Read Data
                using (dataReader = sqlCommand.ExecuteReader())
                {
                    if (dataReader.Read()) // Check if data is read from the DataReader
                    {
                        // Retrieve Cart ID and Quantity from DataReader
                        id = dataReader["id"].ToString();
                        cartQuantity = int.Parse(dataReader["quantity"].ToString());
                        productFound = true; // Set product found flag
                    }
                }

                // Close Database Connection
                connection.Close();

                // Check if Product is Found and Update or Insert as Needed
                if (productFound)
                {
                    // Check if Available Quantity Is Sufficient
                    if (quantity < (int.Parse(txtBarcodeQuantity.Text) + cartQuantity))
                    {
                        // Display Warning Message If Quantity Is Insufficient
                        MessageBox.Show($"Unable to proceed. Remaining quantity on hand is {quantity}.", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Exit the function
                    }

                    // Open Database Connection
                    connection.Open();

                    // SQL Command to Update Quantity in Cart
                    sqlCommand = new SqlCommand("UPDATE tbCart SET quantity = quantity + @quantity WHERE id = @id", connection);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity); // Add quantity parameter
                    sqlCommand.Parameters.AddWithValue("@id", id); // Add cart ID parameter

                    // Execute SQL Command to Update Cart
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    // Open Database Connection
                    connection.Open();

                    // SQL Command to Insert New Cart Entry
                    sqlCommand = new SqlCommand("INSERT INTO tbCart (transactionNumber, productCode, price, quantity, sDate, cashier) VALUES (@transactionNumber, @productCode, @price, @quantity, @sDate, @cashier)", connection);
                    sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text); // Add transaction number parameter
                    sqlCommand.Parameters.AddWithValue("@productCode", productCode); // Add product code parameter
                    sqlCommand.Parameters.AddWithValue("@price", price); // Add price parameter
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity); // Add quantity parameter
                    sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now); // Add current date parameter
                    sqlCommand.Parameters.AddWithValue("@cashier", lblCashierName.Text); // Add cashier name parameter

                    // Execute SQL Command to Insert New Cart Entry
                    sqlCommand.ExecuteNonQuery();
                }

                // Close Database Connection
                connection.Close();

                // Load Updated Cart
                loadCart();
            }
            catch (Exception ex)
            {
                // Ensure Database Connection is Closed in Case of Exception
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Display Message to User That an Unexpected Exception Has Occurred
                MessageBox.Show($"An unexpected exception has occurred while adding to cart: {ex.Message}");
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Check If TextBox Is Empty
                if (txtBarcode.Text == string.Empty)
                {
                    // Exit If TextBox Is Empty
                    return;
                }
                else
                {
                    // Declare Product Code Variable
                    string _productCode;

                    // Declare Price Variable
                    double _price;

                    // Declare Quantity Variable
                    int _quantity;

                    // Open Database Connection
                    connection.Open();

                    // Search Product By Barcode
                    sqlCommand = new SqlCommand("SELECT * FROM tbProduct WHERE barcode LIKE '" + txtBarcode.Text + "' ", connection);

                    // Execute SQL Command, Obtain SQLDataReader to Read Data From Database
                    dataReader = sqlCommand.ExecuteReader();

                    // Read Data From DataReader
                    dataReader.Read();

                    // Check If Data Reader Has Rows
                    if (dataReader.HasRows)
                    {
                        // Get Quantity From DataReader
                        _quantity = int.Parse(dataReader["quantity"].ToString());

                        // Get Product Code From DataReader
                        _productCode = dataReader["productCode"].ToString();

                        // Get Price From DataReader
                        _price = double.Parse(dataReader["price"].ToString());

                        // Get Quantity From TextBox
                        int quantity = int.Parse(txtBarcodeQuantity.Text);

                        // Close DataReader After Reading All Data
                        dataReader.Close();

                        // Close Database Connection
                        connection.Close();

                        /// Insert Into Cart (To Be Implemented)

                        // Add Product to Cart
                        addToCart(_productCode, _price, _quantity);
                    }
                    else
                    {
                        // Close DataReader After Reading All Data
                        dataReader.Close();

                        // Close Database Connection
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Searching By Barcode" + ex.Message);
            }
        }

        //public void addToCart(string productCode, double price, int quantity)
        //{
        //    try
        //    {
        //        /// Get Product Based On Transaction Number & Product Code

        //        string id = "";

        //        int cartQuantity = 0;

        //        bool productFound = false;

        //        // Open Database Connection
        //        connection.Open();

        //        // SQL Command to Insert a New Cart In Into the tbCart Table
        //        sqlCommand = new SqlCommand("SELECT 8 FROM tbCart WHERE transactionNumbmer = @transactionNumbmer AND productCode = @productCode", connection);

        //        // Add the stock in Parameters to the SQL Command With the Value
        //        sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);
        //        sqlCommand.Parameters.AddWithValue("@productCode", productCode);

        //        // Execute SQL Command, Obtain SQLDataReader to Read Data From Database
        //        dataReader = sqlCommand.ExecuteReader();

        //        // Read Data From DataReader
        //        dataReader.Read();

        //        if (dataReader.HasRows)
        //        {
        //            id = dataReader["quantity"].ToString();

        //            cartQuantity = int.Parse(dataReader["quantity"].ToString());

        //            productFound = true;
        //        } 
        //        else
        //        {
        //            productFound = false;
        //        }

        //        // Close DataReader After Reading All Data
        //        dataReader.Close();

        //        // Close the Database Connection
        //        connection.Close();

        //        /// Insert Into Cart Table

        //        if (productFound)
        //        {
        //            if (quantity < (int.Parse(txtBarcodeQuantity.Text) + cartQuantity))
        //            {
        //                MessageBox.Show("Unable To Proceed. \nRemaining Quantity On Hand Is " + quantity, "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //                return;
        //            }
        //            else
        //            {
        //                // SQL Command to Update quantity in tbCart
        //                sqlCommand = new SqlCommand("UPDATE tbCart SET quantity = quantity + " + quantity + ") WHERE id = '" + id + "'", connection);

        //                // Execute the SQL Command to Update tbCart
        //                sqlCommand.ExecuteNonQuery();

        //                // Close the Database Connection
        //                connection.Close();

        //                textBox1.SelectionStart = 0;

        //                textBox1.SelectionLength = textBox1.Text.Length;

        //                // Load Cart
        //                loadCart();
        //            }
        //        }

        //        else
        //        {

        //            // Open Database Connection
        //            connection.Open();

        //            // SQL Command to Insert a New Cart In Into the tbCart Table
        //            sqlCommand = new SqlCommand("INSERT INTO tbCart (transactionNumber, productCode, price, quantity, sDate, cashier) VALUES (@transactionNumber, @productCode, @price, @quantity, @sDate, @cashier)", connection);

        //            // Add the stock in Parameters to the SQL Command With the Value
        //            sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);
        //            sqlCommand.Parameters.AddWithValue("@productCode", productCode);
        //            sqlCommand.Parameters.AddWithValue("@price", price);
        //            sqlCommand.Parameters.AddWithValue("@quantity", quantity);
        //            sqlCommand.Parameters.AddWithValue("@sDate", DateTime.Now);
        //            sqlCommand.Parameters.AddWithValue("@cashier", lblCashierName.Text);

        //            // Execute the SQL Command to Insert the New Category Into the Database
        //            sqlCommand.ExecuteNonQuery();

        //            // Close the Database Connection
        //            connection.Close();

        //            // Load Cart
        //            loadCart();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Close Connection
        //        connection.Close();

        //        // Display User that an Unexpected Exception has Occurred
        //        MessageBox.Show("An Unexpected Exception has Occurred while Adding to Cart" + ex.Message);
        //    }
        //}

        //public void getCartTotal()
        //{
        //    try
        //    {
        //        // Remove any unwanted characters like commas
        //        string discountText = lblDisountActual.Text.Replace(",", "").Replace(" ", "").Trim();
        //        string salesText = lblSalesTotalActual.Text.Replace(",", "").Replace(" ", "").Trim();

        //        // Try to parse the cleaned text to doubles
        //        if (!double.TryParse(discountText, out double discount))
        //        {
        //            MessageBox.Show("Invalid discount format");
        //            return;
        //        }

        //        if (!double.TryParse(salesText, out double totalSales))
        //        {
        //            MessageBox.Show("Invalid sales total format");
        //            return;
        //        }

        //        double sales = totalSales - discount;

        //        // 12% of Payable Tax (Output Tax Less Input Tax)
        //        double VAT = sales * 0.12;

        //        double VATable = sales - VAT;

        //        lblVATActual.Text = VAT.ToString("#,##0.00");
        //        lblVATableActual.Text = VATable.ToString("#,##0.00");
        //        lblDisplayTotal.Text = VATable.ToString("#,##0.00");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}");
        //    }
        //}
    }
}
