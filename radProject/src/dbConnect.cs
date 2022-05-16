using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;


// not only connect

namespace radProject
{
    public class PostgresConnect
    {

        public static List<products> productsList = new List<products>();
        public static List<preReport> preReport = new List<preReport>();
        public static List<report> report = new List<report>();

        public static string Host = "localhost";
        public static string User = "postgres";
        public static string DBname = "someBD";
        public static string Password = "123";
        public static string Port = "5432";

        public static string connString =
            String.Format( "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
            Host,
            User,
            DBname,
            Port,
            Password);

        public static List<products> dbGetListProducts()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                //Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM products", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) { productsList.Add(new products(reader.GetInt32(0), reader.GetString(1))); }
                    reader.Close();
                }
            }
            return productsList;
        } 
        public static List<report> dbGetReport(List<string> productsID, DateTime dateFrom, DateTime dateTo)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                // debug
                string usersString = "'" + productsID[0] + "'";
                for (int i = 1; i < productsID.Count; i++)
                {
                    usersString += ", '" + productsID[i] + "'";
                }
                MessageBox.Show(usersString);
                foreach (var product in productsID)
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT products.name, sales.amount, sales.shipped " +
                                                       "FROM products, sales " +
                                                       "WHERE products.id = " + product +
                                                       "AND sales.id_product = " + product +
                                                       "AND DATE(sales.date) > '" + dateFrom.Year + "-" + dateFrom.Month + "-" + dateFrom.Day + "'" +
                                                       "AND DATE(sales.date) < '" + dateTo.Year + "-" + dateTo.Month + "-" + dateTo.Day + "'", conn))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            preReport.Add(new preReport(reader.GetString(0), reader.GetInt32(1), reader.GetBoolean(2)));
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
                for(int i=0; i<preReport.Count; i++)
                {
                    int numDeliveries = 0;
                    int numOrders = 0;

                    numOrders += preReport[i].numOrders;
                    if (preReport[i].shipped == true)
                        numDeliveries += preReport[i].numOrders;

                    for (int j=0;j<preReport.Count; j++)
                    {
                        if (preReport[i].productName == preReport[j].productName && i != j)
                        {
                            numOrders += preReport[j].numOrders;
                            if(preReport[j].shipped == true)
                                numDeliveries += preReport[j].numOrders;
                        }
                    }

                    // не записываем повторыне отчёты
                    bool flag = true;
                    for (int j = 0; j < report.Count; j++)
                        if (preReport[i].productName == report[j].productName)
                            flag = false;
                    // если не нашли совпадений - записываем
                    if (flag)
                        report.Add(new report(preReport[i].productName, numOrders, numDeliveries));
                }
            }
            return report;
        }
    }
}