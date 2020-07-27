using Excel;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;


public class ExcelManager
{

    protected static ExcelPackage _ExcelPackage = null;



    public  static void Write(string Path,Dictionary<int[,],string> name)

       {
           FileInfo newFile = new FileInfo(Path);
           using (ExcelPackage package = new ExcelPackage(newFile))
           {

               ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                worksheet.Cells[1, 1].Value ="名称";
                package.Save();

           }
       }
}

