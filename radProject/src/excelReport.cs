using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;



namespace radProject
{
    public class excelReport
    {
        public List<reportProducts> report = new List<reportProducts>();
        public List<reportClients> report2 = new List<reportClients>();
        string fileName { get; set; }
        public excelReport(List<reportProducts> report, string fileName)
        {
            this.report = report;
            this.fileName = fileName;
        }
        public excelReport(List<reportClients> report2, string fileName)
        {
            this.report2 = report2;
            this.fileName = fileName;
        }
        public void createReportProducts()
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (report != null)
            {
                Workbook wb = excelApp.Workbooks.Open(fileName,
                                                     0,
                                                     false,
                                                     5,
                                                     "",
                                                     "",
                                                     false,
                                                     XlPlatform.xlWindows,
                                                     "",
                                                     true,
                                                     false,
                                                     0,
                                                     true,
                                                     false,
                                                     false);
                Worksheet ws = wb.Sheets[1];
                ws.Cells[1][1] = "Товар";
                ws.Cells[2][1] = "Заказов";
                ws.Cells[3][1] = "Доставленно";
                for (int i = 0; i < report.Count; i++)
                {
                    ws.Cells[1][i + 2] = report[i].productName;
                    ws.Cells[2][i + 2] = report[i].numOrders;
                    ws.Cells[3][i + 2] = report[i].numDeliveries;
                }

                wb.Save();
                wb.Close();
            }
        }
        public void createReportClients()
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            if (report != null)
            {
                //Workbook wb = excelApp.Workbooks.Add(fileName);
                Workbook wb = excelApp.Workbooks.Open(fileName,
                                                     0,
                                                     false,
                                                     5,
                                                     "",
                                                     "",
                                                     false,
                                                     XlPlatform.xlWindows,
                                                     "",
                                                     true,
                                                     false,
                                                     0,
                                                     true,
                                                     false,
                                                     false);
                Worksheet ws = wb.Sheets[1];
                ws.Cells[1][1] = "Имя";
                ws.Cells[2][1] = "Продукт";
                ws.Cells[3][1] = "Количество";
                ws.Cells[4][1] = "Общая стоимость";
                MessageBox.Show(report2.Count + "");
                for (int i = 0; i < report2.Count; i++)
                {
                    ws.Cells[1][i + 2] = report2[i].clientName;
                    ws.Cells[2][i + 2] = report2[i].productName;
                    ws.Cells[3][i + 2] = report2[i].salesAmount;
                    ws.Cells[4][i + 2] = report2[i].productPrice;
                }

                wb.Save();
                wb.Close();
            }
        }
    }
}
