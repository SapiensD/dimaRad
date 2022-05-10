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
        public static List<user> usersList = PostgresConnect.dbGetListUser();
        public static List<string> usersID = new List<string>();
        public static string fileName = "D:/report.xlsx";
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = usersList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   
            // достаём номер строки, на которую нажали
            usersID.Add(dataGridView1[0, e.RowIndex].Value.ToString());
        }
        private void button1_Click(object sender, EventArgs e) 
        {
            // ЗАПРОС В БД : ПОЛУЧИТЬ ВСЕХ ПОЛЬЗОВАТЕЛЕЙ из usersID И НАЙТИ их в таблице sales со значением столбца shipped = "false"
            // создать excel файл и сделать выгрузку данных 
            excelReport newReport = new excelReport(PostgresConnect.dbGetReport(usersID), fileName); // получаю отчёт из бд и печатаю отчёт
            newReport.createReport();
            MessageBox.Show("Успешно!");
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }
    }
}
