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
    public partial class CancelOrderForm : Form
    {
        DailyReportForm dailyReportForm;

        public CancelOrderForm(DailyReportForm dailyReportForm)
        {
            InitializeComponent();

            this.dailyReportForm = dailyReportForm;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboAddToInventory.Text != string.Empty && int.Parse(txtCancelQuantity.Text) > 1 && txtReason.Text != string.Empty)
                {
                    if (int.Parse(txtQuantity.Text) >= int.Parse(txtCancelQuantity.Text))
                    {
                        VoidByModule voidByModule = new VoidByModule(this);

                        voidByModule.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                // Display Error Message To The User About The Exception
                MessageBox.Show("An Unexpected Exception Has Occurred While Cancelling Order: " + ex.Message);
            }
        }

        public void reloadSoldList()
        {
            dailyReportForm.loadSold();
        }

        private void cboAddToInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
