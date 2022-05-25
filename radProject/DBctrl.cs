using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace radProject
{
    public partial class DBctrl : Form
    {
        public DBctrl()
        {
            InitializeComponent();
            update();
        }

        public void update()
        {
            dataGridView1.DataSource = PostgresConnect.dbGetListClients();
            dataGridView2.DataSource = PostgresConnect.dbGetListProducts();
            dataGridView3.DataSource = PostgresConnect.dbGetListSales();
        }

        public static int lastClick1 = 0;
        public static int lastClick2 = 0;
        public static int lastClick3 = 0;

        public static int id1 = 0;
        public static int id2 = 0;
        public static int id3 = 0;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1[0, lastClick1].Value = "";
            dataGridView1[0, e.RowIndex].Value = "*";
            lastClick1 = e.RowIndex;
            id1 = (int)dataGridView1[1, e.RowIndex].Value;

            clientName.Text = dataGridView1[2, e.RowIndex].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2[0, lastClick2].Value = "";
            dataGridView2[0, e.RowIndex].Value = "*";
            lastClick2 = e.RowIndex;
            id2 = (int)dataGridView2[1, e.RowIndex].Value;

            productName.Text = dataGridView2[2, e.RowIndex].Value.ToString();
            productPrice.Text = dataGridView2[3, e.RowIndex].Value.ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3[0, lastClick3].Value = "";
            dataGridView3[0, e.RowIndex].Value = "*";
            lastClick3 = e.RowIndex;

            id3 = (int)dataGridView3[1, e.RowIndex].Value;

            salesClient.Text = dataGridView3[2, e.RowIndex].Value.ToString();
            salesProduct.Text = dataGridView3[3, e.RowIndex].Value.ToString();
            salesAmount.Text = dataGridView3[4, e.RowIndex].Value.ToString();
            salesShipped.Checked = (bool)dataGridView3[5, e.RowIndex].Value;
            salesDate.Value = (DateTime)dataGridView3[6, e.RowIndex].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbUpdateClients(id1, clientName.Text);
            update();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbInsertClients(clientName.Text);
            update();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbInsertProducts(productName.Text, productPrice.Text);
            update();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbUpdateProducts(id2, productName.Text, productPrice.Text);
            update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbInsertSales(salesClient.Text, salesProduct.Text, salesAmount.Text, salesShipped.Checked, salesDate.Value.Year + "-" + salesDate.Value.Month + "-" + salesDate.Value.Day);
            update();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            PostgresConnect.dbUpdateSales(id3, salesClient.Text, salesProduct.Text, salesAmount.Text, salesShipped.Checked, salesDate.Value.Year + "-" + salesDate.Value.Month + "-" + salesDate.Value.Day);
            update();
        }

        private void productPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(productPrice.Text != "")
                {
                    double result = Double.Parse(productPrice.Text);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clientName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void salesClient_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (salesClient.Text != "")
                {
                    int result = Int32.Parse(salesClient.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salesProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (salesProduct.Text != "")
                {
                    int result = Int32.Parse(salesProduct.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salesAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (salesAmount.Text != "")
                {
                    int result = Int32.Parse(salesAmount.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
