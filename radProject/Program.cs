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
        public products(int Id, string Name)
        {   
            this.Id = Id;
            this.Name = Name;
        }
        public products() { }
    }
    public class report
    {
        public string productName { get; set; }
        public int numOrders { get; set; }
        public int numDeliveries { get; set; }
        public report(string productName, int numOrders, int numDeliveries) 
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
