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
    public partial class DailyReportForm : Form
    {
        /// HANDLE THE CONNECTION TO DATABASE
        SqlConnection connection = new SqlConnection();

        /// TO EXECUTE SQL COMMANDS AND QUERIES
        SqlCommand sqlCommand = new SqlCommand();

        /// CUSTOM CLASS FOR MANAGING DATABASE CONNECTIONS
        DatabaseConnectionClass connectionClass = new DatabaseConnectionClass();

        /// TO READ DATA RETRIEVED FROM DATABASE
        SqlDataReader dataReader;

        public string soldUser;

        public DailyReportForm()
        {
            InitializeComponent();

            // Establish Connection
            connection = new SqlConnection(connectionClass.DatabaseConnection());

            // Load Cashier to DataGridView
            loadCashier();
        }

        // LOAD CASHIER
        private void loadCashier()
        {
            try
            {
                cboCashier.Items.Clear();

                cboCashier.Items.Add("All Cashier");

                connection.Open();

                sqlCommand = new SqlCommand("SELECT * FROM tbUser WHERE role LIKE 'Cashier'", connection);

                dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    cboCashier.Items.Add(dataReader["fullName"].ToString());
                }

                dataReader.Close();

                connection.Close();
            }

            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Display User that an Unexpected Exception has Occurred
                MessageBox.Show("An Unexpected Exception Has Occurred While Loading Cashier " + ex.Message);
            }
        }

        public void loadSold()
        {
            int i = 0;

            dgvCashier.Rows.Clear();

            double total = 0.00;

            try
            {
                connection.Open();

                if (cboCashier.Text == "All Cashier")
                {
                    sqlCommand = new SqlCommand("SELECT c.id, c.transactionNumber, c.productCode, p.description, c.price, c.quantity, c.discount, c.total FROM tbCart as c INNER JOIN tbProduct AS p ON c.productCode = p.productCode WHERE status LIKE 'Sold' AND CAST(sDate AS DATE) BETWEEN @dateFrom AND @dateTo", connection);

                    sqlCommand.Parameters.AddWithValue("@dateFrom", dtpFrom.Text);
                    sqlCommand.Parameters.AddWithValue("@dateTo", dtpTo.Text);
                }
                else
                {
                    sqlCommand = new SqlCommand("SELECT c.id, c.transactionNumber, c.productCode, p.description, c.price, c.quantity, c.discount, c.total FROM tbCart as c INNER JOIN tbProduct AS p ON c.productCode = p.productCode WHERE status LIKE 'Sold' AND CAST(sDate AS DATE) BETWEEN @dateFrom AND @dateTo AND cashier = @cashier", connection);
                    sqlCommand.Parameters.AddWithValue("@dateFrom", dtpFrom.Text);
                    sqlCommand.Parameters.AddWithValue("@dateTo", dtpTo.Text);
                    sqlCommand.Parameters.AddWithValue("@cashier", cboCashier.Text);
                }

                dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    i++;

                    total += double.Parse(dataReader["total"].ToString());

                    dgvCashier.Rows.Add(i, dataReader["id"].ToString(), dataReader["transactionNumber"].ToString(), dataReader["productCode"].ToString(), dataReader["description"].ToString(), dataReader["price"].ToString(), dataReader["quantity"].ToString(), dataReader["discount"].ToString(), dataReader["total"].ToString());
                }

                dataReader.Close();

                lblTotal.Text = total.ToString("#, ##0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        /// CLOSE WINDOW
        private void picClose_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSold();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            loadSold();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            loadSold();
        }

        private void DailyReportForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void dgvCashier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = dgvCashier.Columns[e.ColumnIndex].Name;

            if (columnName == "View")
            {
                CancelOrderForm cancelOrderForm = new CancelOrderForm(this);

                cancelOrderForm.txtId.Text = dgvCashier.Rows[e.RowIndex].Cells[1].Value.ToString();
                cancelOrderForm.txtTransactionNumber.Text = dgvCashier.Rows[e.RowIndex].Cells[2].Value.ToString();
                cancelOrderForm.txtProductCode.Text = dgvCashier.Rows[e.RowIndex].Cells[3].Value.ToString();
                cancelOrderForm.txtDescription.Text = dgvCashier.Rows[e.RowIndex].Cells[4].Value.ToString();
                cancelOrderForm.txtPrice.Text = dgvCashier.Rows[e.RowIndex].Cells[5].Value.ToString();
                cancelOrderForm.txtQuantity.Text = dgvCashier.Rows[e.RowIndex].Cells[6].Value.ToString();
                cancelOrderForm.txtDiscount.Text = dgvCashier.Rows[e.RowIndex].Cells[7].Value.ToString();
                cancelOrderForm.txtTotal.Text = dgvCashier.Rows[e.RowIndex].Cells[8].Value.ToString();

                cancelOrderForm.txtCancelledBy.Text = soldUser;

                cancelOrderForm.ShowDialog();
            }
        }
    }
}
