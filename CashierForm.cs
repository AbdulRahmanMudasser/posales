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

        public CashierForm()
        {
            InitializeComponent();

            // Close All the SubMenus When Application Starts
            customizeDesign();

            // Get Transaction Number
            getTransactionNumber();

            // Establish Connection
            // connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Open Connection
            // connection.Open();

            // Display if Connection to Database is Opened
            // MessageBox.Show("Database is Connected");
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        #region panelSlide
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
        #endregion panelSlide

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

        /// GET TRANSACTION NUMBER
        public void getTransactionNumber()
        {
            string dateToday = DateTime.Now.ToString("yyyyMMdd");

            string transactionNumber = dateToday + "1001";

            lblTransactionNumberActual.Text = transactionNumber;
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void metroPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
