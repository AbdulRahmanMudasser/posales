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
                string dateToday = DateTime.Now.ToString("yyyyMMdd");

                int count;

                string transactionNumber;

                // Open Database Connection
                connection.Open();

                // Selecting Transaction 
                sqlCommand = new SqlCommand("SELECT TOP 1 transactionNumber FROM tbCart WHERE transactionNumber LIKE '" + dateToday + "%' ORDER BY id DESC", connection);

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                dataReader.Read();
                
                // Check if Data Reader has Data
                if (dataReader.HasRows)
                {
                    transactionNumber = dataReader[0].ToString();

                    count = int.Parse(transactionNumber.Substring(8, 4));

                    lblTransactionNumberActual.Text = dateToday + (count + 1);
                }
                else
                {
                    transactionNumber = dateToday + "1001";

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

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Fetching Records Based on Transaction Number" + ex.Message);
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

                // Loading Products
                sqlCommand = new SqlCommand("SELECT c.id, c.productCode, p.description, c.price, c.quantity, c.discount, c.total FROM tbCart AS c INNER JOIN tbProduct AS p ON c.productCode = p.productCode WHERE c.transactionNumber LIKE @transactionNumber AND c.status LIKE 'Pending'", connection);

                // Add the product Parameters to the SQL Command With the Value
                sqlCommand.Parameters.AddWithValue("@transactionNumber", lblTransactionNumberActual.Text);

                // Execute SQL Command, Obtain SQLDataReader to Read Data from Database
                dataReader = sqlCommand.ExecuteReader();

                // Iterate through the DataReader to Read Each Row of Data
                while (dataReader.Read())
                {
                    // Increment Counter for Each Row
                    i++;

                    // Check for data in each column before adding to DataGridView
                    var id = dataReader["id"].ToString();
                    var productCode = dataReader["productCode"].ToString();
                    var description = dataReader["description"].ToString();
                    var price = dataReader["price"].ToString();
                    var quantity = dataReader["quantity"].ToString();
                    var discountValue = dataReader["discount"].ToString();
                    var totalValue = dataReader["total"].ToString();

                    // Ensure none of the values are null or empty
                    if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(productCode) || string.IsNullOrEmpty(description) ||
                        string.IsNullOrEmpty(price) || string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(discountValue) || string.IsNullOrEmpty(totalValue))
                    {
                        MessageBox.Show("One or more columns contain null or empty values.");
                        continue;
                    }

                    // Calculate Total
                    total += Convert.ToDouble(totalValue);

                    // Calculate Discount
                    discount += Convert.ToDouble(discountValue);

                    // Add New Row to DataGridView With Counter, Id, Brand Values from the Current Row
                    dgvCart.Rows.Add(i, productCode, description, price, quantity, discountValue, double.Parse(totalValue).ToString("#,##0.00"));
                }

                // Close DataReader After Reading All Data
                dataReader.Close();

                // Close Database Connection
                connection.Close();

                lblSalesTotalActual.Text = total.ToString("#,##0.00");
                lblDisountActual.Text = discount.ToString("#,##0.00");

                getCartTotal();
            }
            catch (Exception ex)
            {
                // Close Connection
                connection.Close();

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception has Occurred while Loading Cart" + ex.Message);
            }
        }


        /// GET CART TOTAL
        public void getCartTotal()
        {
            double discount = double.Parse(lblDisountActual.Text);

            double sales = double.Parse(lblSalesTotalActual.Text) - discount;

            // 12% of Payable Tax (Output Tax Less Input Tax)
            double VAT = sales * 0.12;

            double VATable = sales - VAT;

            lblVATActual.Text = VAT.ToString("#, ##0.00");

            lblVATableActual.Text = VATable.ToString("#, ##0.00");

            lblDisplayTotal.Text = VATable.ToString("#, ##0.00");
        }

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
