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
        SqlConnection connection = new SqlConnection();

        SqlCommand sqlCommand = new SqlCommand();

        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

    
        public MainForm()
        {
            InitializeComponent();


            customizeDesign();


            connection = new SqlConnection(connectionClass.DatabaseConnection());

            connection.Open();

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

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubProduct);
        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
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
