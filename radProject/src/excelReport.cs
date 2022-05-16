﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace radProject
{
    public class excelReport
    {
        public List<report> report = new List<report>();
        string fileName { get; set; }
        public excelReport(List<report> report, string fileName)
        {
            this.report = report;
            this.fileName = fileName;
        }
        public void createReport()
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
    }
}
