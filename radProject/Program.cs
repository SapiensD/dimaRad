using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace radProject
{
    public class products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public products(int Id, string Name, double price)
        {   
            this.Id = Id;
            this.Name = Name;
            this.price = price;
        }
        public products() { }
    }
    public class reportProducts
    {
        public string productName { get; set; }
        public int numOrders { get; set; }
        public int numDeliveries { get; set; }
        public reportProducts(string productName, int numOrders, int numDeliveries) 
        { 
            this.productName = productName;
            this.numOrders = numOrders;
            this.numDeliveries = numDeliveries;
        }
    }
    public class preReport
    {
        public string productName { get; set; }
        public int numOrders { get; set; }
        public bool shipped { get; set; }
        public preReport(string productName, int numOrders, bool shipped)
        {
            this.productName=productName;
            this.shipped=shipped;
            this.numOrders=numOrders;
        }
    }

    public class reportClients
    {
        public string clientName { get; set; }
        public string productName { get; set; }
        public int salesAmount { get; set; }
        public double productPrice { get; set; }
        public double summ = 0;
        public reportClients(string clientName, string productName, int salesAmount, double productPrice)
        {
            this.clientName = clientName;
            this.productName = productName;
            this.salesAmount = salesAmount;
            this.productPrice = productPrice;
            summ = salesAmount * productPrice;
        }
    }
    public class sales
    {
        public int Id { get; set; }
        public int id_client { get; set; }
        public int id_product { get; set; }
        public int amount { get; set; }
        public bool shipped { get; set; }
        public DateTime date { get; set; }
        public sales(int Id, int id_client, int id_product, int amount, bool shipped, DateTime date)
        {
            this.Id = Id;
            this.id_client = id_client;
            this.id_product = id_product;
            this.amount = amount;
            this.shipped = shipped;
            this.date = date;
        }
    }
    public class client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public client(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public client() { }
    }

    internal static class Program
    {     
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
