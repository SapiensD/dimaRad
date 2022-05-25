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

        public static List<client> dbGetListClients()
        {
            List<client> clientsList = new List<client>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM clients", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) { clientsList.Add(new client(reader.GetInt32(0), reader.GetString(1))); }
                    reader.Close();
                }
                conn.Close();
            }
            return clientsList;
        }
        public static List<products> dbGetListProducts()
        {
            List<products> productsList = new List<products>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM products", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) { productsList.Add(new products(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2))); }
                    reader.Close();
                }
            }
            return productsList;
        }
        public static List<sales> dbGetListSales()
        {
            List<sales> salesList = new List<sales>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM sales", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) { salesList.Add(new sales(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetBoolean(4), reader.GetDateTime(5))); }
                    reader.Close();
                }
            }
            return salesList;
        }

        public static void dbInsertClients(string name)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO clients (name) VALUES ('" + name + "')", conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static void dbInsertProducts(string name, string price)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO products (name, price) VALUES ('" + name + "', '" + price + "')", conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static void dbInsertSales(string id_client, string id_product, string amount, bool shipped, string date)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO sales (id_client, id_product, amount, shipped, date) VALUES ('" + id_client + "', '" + id_product + "', '" + amount + "', " + shipped + ", '" + date + "')", conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static void dbUpdateClients(int id, string name)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("UPDATE clients " +
                                                        "SET name = '" + name + "' " +
                                                        "WHERE id = " + id, conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static void dbUpdateProducts(int id, string name, string price)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("UPDATE products " +
                                                        "SET name = '" + name + "', " + "price = " + price + " " +
                                                        "WHERE id = " + id, conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static void dbUpdateSales(int id, string id_client, string id_product, string amount, bool shipped, string date)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("UPDATE sales " +
                                                        "SET id_client = " + id_client + ", " + "id_product = " + id_product + ", amount = " + amount + ", shipped = " + shipped + ", date = '" + date + "' " +
                                                        "WHERE id = " + id, conn))
                {
                    command.ExecuteReader();
                }
                conn.Close();
            }
        }
        public static List<reportProducts> dbGetReport(List<string> productsID, DateTime dateFrom, DateTime dateTo)
        {
            List<reportProducts> report = new List<reportProducts>();
            List<preReport> preReport = new List<preReport>();
            using (var conn = new NpgsqlConnection(connString))
            {
                string usersString = "'" + productsID[0] + "'";
                for (int i = 1; i < productsID.Count; i++)
                { usersString += ", '" + productsID[i] + "'"; }
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
                
                for (int i=0; i<preReport.Count; i++)
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
                        report.Add(new reportProducts(preReport[i].productName, numOrders, numDeliveries));
                }
            }
            return report;
        }
        public static List<reportClients> dbGetReportSales(List<string> clientsID, DateTime dateFrom, DateTime dateTo)
        {
            List<reportClients> report2 = new List<reportClients>();
            using (var conn = new NpgsqlConnection(connString))
            {
                // debug
                string usersString = "" + clientsID[0] + "";
                for (int i = 1; i < clientsID.Count; i++)
                {
                    usersString += ", " + clientsID[i] + "";
                }
                MessageBox.Show(usersString);

                foreach (var client in clientsID)
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT clients.name, products.name, sales.amount, products.price " +
                                                       "FROM clients, products, sales " +
                                                       "WHERE clients.id = " + client + " " +
                                                       "AND sales.id_client = " + client + " " +
                                                       "AND shipped = false " +
                                                       "AND products.id = id_product " +
                                                       "AND DATE(sales.date) > '" + dateFrom.Year + "-" + dateFrom.Month + "-" + dateFrom.Day + "' " +
                                                       "AND DATE(sales.date) < '" + dateTo.Year + "-" + dateTo.Month + "-" + dateTo.Day + "'", conn))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            report2.Add(new reportClients(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3)));
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            MessageBox.Show("REPORT CONUT " + report2.Count);
            return report2;
        }
    }
}