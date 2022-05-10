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

        public static List<user> usersList = new List<user>();
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

        public static List<user> dbGetListUser()
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                //Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM clients", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read()) { usersList.Add(new user(reader.GetInt32(0), reader.GetString(1))); }
                    reader.Close();
                }
            }
            return usersList;
        } 
        public static List<report> dbGetReport(List<string> usersID)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                foreach (var user in usersID)
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT clients.name, products.name, sales.amount, products.price " +
                                                       "FROM clients, products, sales " +
                                                       "WHERE clients.id = " + user +
                                                       "AND sales.id_client = " + user +
                                                       "AND shipped = true " +
                                                       "AND products.id = id_product ", conn))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            report.Add(new report(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3) * reader.GetInt32(2)));
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            return report;
        }
    }
}