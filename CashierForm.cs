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
using System.Diagnostics;

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

        string price;

        string id;

        public static bool hasCart = false;

        public CashierForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Get Transaction Number
            getTransactionNumber();

            setDataGridViewColor();

            // Open Connection
            // connection.Open();

            // Display if Connection to Database is Opened
            // MessageBox.Show("Database is Connected");
        }

        #region buttons

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Exit Cashier Application?\n\nYou Will Be Returned To The Login Screen", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();

                loginForm.ShowDialog();
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
            // Slide Button Animation For btnSearchProduct
            slider(btnSearchProduct);

            // Initialize CashierLookUpProducts Window
            CashierLookUpProducts lookUpProducts = new CashierLookUpProducts(this);

            // Load Products Into LookUpProducts
            lookUpProducts.loadProducts();

            // Show LookUpProducts Dialog
            lookUpProducts.ShowDialog();
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            // Slide Button Animation For btnAddDiscount
            slider(btnAddDiscount);

            if (hasCart)
            {
                // Initialize DiscountModule Window
                DiscountModule discountModule = new DiscountModule(this);

                // Set Discount Module Label Id Text
                discountModule.lblId.Text = id;

                // Set Discount Module Total Price Text
                discountModule.txtTotalPrice.Text = price;

                // Show DiscountModule Dialog
                discountModule.ShowDialog();
            }

            else
            {
                MessageBox.Show("The Cart Is Currently Empty\r\n\nPlease Add at Least One Product Before Applying a Discount", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
        }


        private void btnClearCart_Click(object sender, EventArgs e)
        {
            slider(btnClearCart);

            if (hasCart)
            {
                // Clear Cart
                clearCart();
            }

            else
            {
                MessageBox.Show("The Cart Is Currently Empty\r\n\nPlease Add at Least One Product Before Clearing It", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            slider(btnDailySales);

            loadDailySales();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            slider(btnChangePassword);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slider(btnLogout);

            if (dgvCart.Rows.Count > 0)
            {
                MessageBox.Show("You Have Items in Your Cart\r\nPlease Cancel the Transaction Before Logging Out", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            navigateToLoginForm();
        }

        private void btnSettlePayment_Click(object sender, EventArgs e)
        {
            slider(btnSettlePayment);

            if (hasCart)
            {
                settlePaymentModule();
            }

            else
            {
                MessageBox.Show("The Cart Is Currently Empty\r\n\nPlease Add at Least One Product Before Settling the Payment", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
        }

        #endregion buttons
        
        /// LOAD DAILY SALES MODULE
        private void loadDailySales()
        {
            DailyReportForm dailyReport = new DailyReportForm();

            dailyReport.ShowDialog();
        }
        
        /// SETTLE PAYMENT
        private void settlePaymentModule()
        {
            SettlePaymentModule settlePaymentModule = new SettlePaymentModule(this);

            string cleanedText = lblDisplayTotal.Text.Trim().Replace(" ", "");

            settlePaymentModule.txtSale.Text = cleanedText;

            settlePaymentModule.ShowDialog();
        }
        
        
        /// LOGOUT
        private void navigateToLoginForm()
        {
            if (MessageBox.Show("Are You Sure You Want To Log Out?\n\nThe Application Will Close Upon Confirmation", "POSales", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();

                LoginForm loginForm = new LoginForm();

                loginForm.ShowDialog();
            }
        }
        
        /// CLEAR CART
        public void clearCart()
        {
            try
            {
                if (MessageBox.Show("Clear Cart?", "POSales", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Open Connection
                    connection.Open();

                    // SQL Command to Clear Cart
                    sqlCommand = new SqlCommand("DELETE FROM tbCart WHERE transactionNumber = @transactionNumber", connection);

                    // Add the brand Parameter to the SQL Command With the Value
                    sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);

                    // Execute the SQL Command to Delete Cart from Database
                    sqlCommand.ExecuteNonQuery();

                    // Close the Database Connection
                    connection.Close();

                    // Display User that the Cart was Deleted Successfully
                    MessageBox.Show("Cart Cleared", "POSales");

                    // Load Cart
                    loadCart();
                }
            }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Display to the User That an Unexpected Exception Has Occurred
                MessageBox.Show("An Unexpected Exception Has Occurred While Clearing Cart Based On Transaction Number: " + ex.Message);
            }

        }

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
                        MessageBox.Show("One or More Columns Contain Null or Empty Values.");
                        continue;
                    }

                    // Calculate Total Amount
                    total += Convert.ToDouble(totalValue);

                    // Calculate Total Discount
                    discount += Convert.ToDouble(discountValue);

                    // Add New Row to DataGridView with Row Number, Product Code, Description, Price, Quantity, Discount Value, and Formatted Total Value
                    dgvCart.Rows.Add(i, id, productCode, description, price, quantity, discountValue, double.Parse(totalValue).ToString("#,##0.00"));

                    // Set hasCart to true
                    hasCart = true;
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
                        MessageBox.Show($"Unable To Proceed. Remaining Quantity On Hand Is {quantity}.", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    loadCart();

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
                        addToCart(_productCode, _price, quantity);
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

        private void dgvCart_SelectionChanged(object sender, EventArgs e)
        {
            // Get Current Row Index
            int i = dgvCart.CurrentRow.Index;

            // Retrieve Item Id From Second Column
            id = dgvCart[1, i].Value.ToString();

            // Retrieve Item Price From Eighth Column
            price = dgvCart[7, i].Value.ToString();
        }

        private void setDataGridViewColor()
        {
            dgvCart.DefaultCellStyle.ForeColor = Color.Black;

            dgvCart.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }


        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Update & Delete Product By Cell Click from in tbProduct
            string databaseOperation = dgvCart.Columns[e.ColumnIndex].Name;

            int availableQuantity = 0;

            try
            {
                // Open Database Connection
                connection.Open();

                sqlCommand = new SqlCommand("SELECT SUM(quantity) as quantity FROM tbProduct WHERE productCode LIKE @productCode GROUP BY productCode", connection);

                sqlCommand.Parameters.AddWithValue("@productCode", dgvCart.Rows[e.RowIndex].Cells[2].Value.ToString());

                var result = sqlCommand.ExecuteScalar();

                if (result != null)
                {
                    availableQuantity = int.Parse(result.ToString());
                }

                // Close Connection
                connection.Close();
            }

            catch (Exception ex)
            {
                // Close Connection
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception Has Occurred While Getting Quantity of Product" + ex.Message);
            }


            if (databaseOperation == "IncrementProduct")
            {
                try
                {
                    if (int.Parse(dgvCart.Rows[e.RowIndex].Cells[5].Value.ToString()) < availableQuantity - 1)
                    {
                        // SQL Query
                        string query = "UPDATE tbCart SET quantity = quantity + @quantity WHERE transactionNumber LIKE @transactionNumber AND productCode LIKE @productCode";

                        // Execute Query
                        connectionClass.executeQueryWithThreeParameters(query, "Incrementing Product", "quantity", int.Parse(txtBarcodeQuantity.Text), "transactionNumber", lblTransactionNumberActual.Text, "productCode", dgvCart.Rows[e.RowIndex].Cells[2].Value.ToString());

                        // Load Cart
                        loadCart();
                    }

                    else if (int.Parse(dgvCart.Rows[e.RowIndex].Cells[5].Value.ToString()) < availableQuantity)
                    {
                        // Display Warning Message That Quantity Will Be Finished
                        var result = MessageBox.Show($"This Will Consume All Remaining Stock ({availableQuantity}). Do You Want to Proceed?", "POSales", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        if (result == DialogResult.Cancel)
                        {
                            return; // Exit the function if user cancels
                        }

                        else if (result == DialogResult.OK)
                        {
                            // SQL Query
                            string queryInternal = "UPDATE tbCart SET quantity = quantity + @quantity WHERE transactionNumber LIKE @transactionNumber AND productCode LIKE @productCode";

                            // Execute Query
                            connectionClass.executeQueryWithThreeParameters(queryInternal, "Incrementing Product", "quantity", int.Parse(txtBarcodeQuantity.Text), "transactionNumber", lblTransactionNumberActual.Text, "productCode", dgvCart.Rows[e.RowIndex].Cells[2].Value.ToString());

                            // Load Cart
                            loadCart();
                        }
                    }

                    else
                    {
                        MessageBox.Show($"Out of Stock\r\n\nRemaining Quantity In Hand Is {availableQuantity}", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                }
                catch (Exception ex)
                {
                    // Close Connection
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception Has Occurred While Incrementing Product" + ex.Message);
                }
            }

            else if (databaseOperation == "DecrementProduct")
            {
                try
                {
                    if (int.Parse(dgvCart.Rows[e.RowIndex].Cells[5].Value.ToString()) > 1)
                    {
                        // SQL Query
                        string query = "UPDATE tbCart SET quantity = quantity - @quantity WHERE transactionNumber LIKE @transactionNumber AND productCode LIKE @productCode";

                        // Execute Query
                        connectionClass.executeQueryWithThreeParameters(query, "Incrementing Product", "quantity", int.Parse(txtBarcodeQuantity.Text), "transactionNumber", lblTransactionNumberActual.Text, "productCode", dgvCart.Rows[e.RowIndex].Cells[2].Value.ToString());

                        // Load Cart
                        loadCart();
                    }

                    else
                    {
                        // MessageBox.Show($"Out of Stock\r\n\nRemaining Quantity In Hand Is {i}", "POSales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // return;

                        // To Delete Product in Product Table
                        if (MessageBox.Show("Remove This Product From Cart?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // SQL Query
                            string query = "DELETE FROM tbCart WHERE id LIKE @id";

                            // Execute Query
                            connectionClass.executeQuery(query, "Removing Product", "id", dgvCart.Rows[e.RowIndex].Cells[1].Value.ToString());

                            // Load Cart
                            loadCart();

                            // If Cart Is Empty Set hasCart to false
                            if (dgvCart.Rows.Count == 0)
                            {
                                hasCart = false;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    // Close Connection
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }

                    // Display User that an Unexpected Exception has Occurred
                    MessageBox.Show("An Unexpected Exception Has Occurred While Incrementing Product" + ex.Message);
                }
            }

            else if (databaseOperation == "RemoveProduct")
            {
                // To Delete Product in Product Table
                if (MessageBox.Show("Remove This Product From Cart?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // SQL Query
                    string query = "DELETE FROM tbCart WHERE id LIKE @id";

                    // Execute Query
                    connectionClass.executeQuery(query, "Removing Product", "id", dgvCart.Rows[e.RowIndex].Cells[1].Value.ToString());

                    // Load Cart
                    loadCart();

                    // If Cart Is Empty Set hasCart to false
                    if (dgvCart.Rows.Count == 0)
                    {
                        hasCart = false;
                    }
                }
            }
        }
    }
}
