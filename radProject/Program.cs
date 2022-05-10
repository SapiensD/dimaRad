using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace radProject
{
    public class user
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public user(int Id, string Name)
        {   
            this.Id = Id;
            this.Name = Name;
        }
        public user() { }
    }
    public class report
    {
        public string clientName { get; set; }
        public string productName { get; set; }
        public int salesAmount { get; set; }
        public double productPrice { get; set; }
        public double summ = 0;
        public report(string clientName, string productName, int salesAmount, double productPrice) 
        { 
            this.clientName = clientName;
            this.productName = productName;
            this.salesAmount = salesAmount;
            this.productPrice = productPrice;
            summ = salesAmount * productPrice;
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
