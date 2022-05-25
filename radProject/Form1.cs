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


namespace radProject
{
    public partial class Form1 : Form
    {
        public static List<string> productsID = new List<string>();
        public static List<string> clientsID = new List<string>();
        public static string fileName = "D:/report.xlsx";
        public static string fileName2 = "D:/report2.xlsx";
        public static DateTime dateFromProducts;
        public static DateTime dateToProducts;
        public static DateTime dateFromClients;
        public static DateTime dateToClients;
        public Form1()
        {
            InitializeComponent();
            updateTables();
            fromProductsDate.MinDate = new DateTime(1985, 6, 20);
            fromProductsDate.MaxDate = DateTime.Today;
            toProductsDate.MinDate = new DateTime(1985, 6, 20);
            toProductsDate.MaxDate = DateTime.Today;
        }
        private void updateTables()
        {
            dataGridView1.DataSource = PostgresConnect.dbGetListProducts();
            dataGridView2.DataSource = PostgresConnect.dbGetListClients();

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // достаём номер строки, на которую нажали
            // без повторений
            bool flag = true;
            for(int i=0;i<productsID.Count;i++)
                if(dataGridView1[1, e.RowIndex].Value.ToString() == productsID[i])
                {
                    flag = false;
                    productsID.Remove(dataGridView1[1, e.RowIndex].Value.ToString());
                }
                    
            if(flag)
                productsID.Add(dataGridView1[1, e.RowIndex].Value.ToString());

            if(dataGridView1[0, e.RowIndex].Value == "+")
                dataGridView1[0, e.RowIndex].Value = "";
            else
                dataGridView1[0, e.RowIndex].Value = "+";
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = true;
            for (int i = 0; i < clientsID.Count; i++)
                if (dataGridView2[1, e.RowIndex].Value.ToString() == clientsID[i])
                {
                    flag = false;
                    clientsID.Remove(dataGridView2[1, e.RowIndex].Value.ToString());
                }

            if (flag)
                clientsID.Add(dataGridView2[1, e.RowIndex].Value.ToString());

            if (dataGridView2[0, e.RowIndex].Value == "+")
                dataGridView2[0, e.RowIndex].Value = "";
            else
                dataGridView2[0, e.RowIndex].Value = "+";
        }

        private void label1_Click(object sender, DataGridViewCellEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) 
        {
            dateFromProducts = fromProductsDate.Value;
            dateToProducts = toProductsDate.Value;
            excelReport newReport = new excelReport(PostgresConnect.dbGetReport(productsID, dateFromProducts, dateToProducts), fileName); // получаю отчёт из бд и печатаю отчёт
            newReport.createReportProducts();
            MessageBox.Show("Отчёт о продуктах сформирован!");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dateFromClients = fromClientsDate.Value;
            dateToClients = toClientsDate.Value;
            excelReport newReport = new excelReport(PostgresConnect.dbGetReportSales(clientsID, dateFromClients, dateToClients), fileName2);
            newReport.createReportClients();
            MessageBox.Show("Отчёт о клиентах сформирован!");
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) 
        {
            DBctrl f = new DBctrl();
            f.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e) { }
        private void fromClientsDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void toClientsDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateTables();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
