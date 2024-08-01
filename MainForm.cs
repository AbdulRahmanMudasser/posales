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
    public partial class MainForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        public MainForm()
        {
            InitializeComponent();

            // Close All the SubMenus When Application Starts
            customizeDesign();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Open Connection
            connection.Open();

            // Display if Connection to Database is Opened
            MessageBox.Show("Database is Connected");
        }

        private void panelLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        #region panelSlide
        private void customizeDesign()
        {
            panelSubProduct.Visible = false;
            panelSubStock.Visible = false;
            panelSubHistory.Visible = false;
            panelSubSetting.Visible = false;
        }

        private void hideSubMenu()
        {
            // for sub products panel
            if (panelSubProduct.Visible == true)
            {
                panelSubProduct.Visible = false;
            }

            // for sub stock panel
            if (panelSubStock.Visible == true)
            {
                panelSubStock.Visible = false;
            }

            // for sub history panel
            if (panelSubHistory.Visible == true)
            {
                panelSubHistory.Visible = false;
            }

            // for sub setting panel
            if (panelSubSetting.Visible == true)
            {
                panelSubSetting.Visible = false;
            }
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

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubProduct);
        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            // Open Product Form as Child Form
            openChildrenForm(new ProductForm());

            hideSubMenu();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            // Open Category Form as Child Form
            openChildrenForm(new CategoryFrom());

            hideSubMenu();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            // Open Brand Form as Child Form
            openChildrenForm(new BrandForm());
            
            hideSubMenu();
        }

        private void btnInStock_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubStock);
        }

        private void btnStockEntry_Click(object sender, EventArgs e)
        {

        }

        private void btnStockAdjustment_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubHistory);
        }

        private void btnSaleHistory_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnPOSRecord_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubSetting);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
