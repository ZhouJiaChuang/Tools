using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class ExcelTranslate
{
    public static void Translate(string filePath, string outPath, EExcelVersion version)
    {
        if (!Directory.Exists(outPath))
            Directory.CreateDirectory(outPath);
        if (version == EExcelVersion.XLS_XLSX)
        {
            TranslateXlsToXlsx(filePath, outPath);
        }
        else if (version == EExcelVersion.XLS_CSV)
        {
            TranslateXlsToCsv(filePath, outPath);
        }
        else if (version == EExcelVersion.XLSX_XLS)
        {
            TranslateXlsxToXls(filePath, outPath);
        }
        else if (version == EExcelVersion.XLSX_CSV)
        {
            TranslateXlsxToCsv(filePath, outPath);
        }
        else if (version == EExcelVersion.CSV_XLS)
        {
            TranslateCsvToXls(filePath, outPath);
        }
        else if (version == EExcelVersion.CSV_XLSX)
        {
            TranslateCsvToXlsx(filePath, outPath);
        }
    }

    public static void TranslateXlsToXlsx(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".xls") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));
        //载入xls文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(filePath);
        //保存为xlsx格式
        workbook.SaveToFile(outFolder + "\\" + name + ".xlsx", ExcelVersion.Version2013);
    }

    public static void TranslateXlsToCsv(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".xls") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));
        //载入xls文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(filePath);
        //获取第一张工作表
        Worksheet sheet = workbook.Worksheets[0];
        //保存为csv格式
        string newFileCache = outFolder + "\\" + name + "____Cache.csv";
        string newFile = outFolder + "\\" + name + ".csv";

        //保存为csv格式
        sheet.SaveToFile(newFileCache, ",", Encoding.Unicode);

        if (File.Exists(newFile)) File.Delete(newFile);

        using (StreamReader sr = new StreamReader(newFileCache, Encoding.Unicode, false))
        {
            using (StreamWriter sw = new StreamWriter(newFile, false, new System.Text.UTF8Encoding(false)))
            {
                sw.Write(sr.ReadToEnd().Replace("\"", ""));
            }
        }
        File.Delete(newFileCache);
    }

    public static void TranslateXlsxToXls(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".xlsx") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));
        //载入xlsx文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(filePath);
        //保存为xls格式
        workbook.SaveToFile(outFolder + "\\" + name + ".xls", ExcelVersion.Version97to2003);
    }

    public static void TranslateXlsxToCsv(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".xlsx") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));
        //载入xlsx文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(filePath);
        //获取第一张工作表
        Worksheet sheet = workbook.Worksheets[0];

        string newFileCache = outFolder + "\\" + name + "____Cache.csv";
        string newFile = outFolder + "\\" + name + ".csv";

        if (File.Exists(newFile)) File.Delete(newFile);
        //保存为csv格式
        sheet.SaveToFile(newFileCache, ",", Encoding.Unicode);

        using (StreamReader sr = new StreamReader(newFileCache, Encoding.Unicode, false))
        {
            using (StreamWriter sw = new StreamWriter(newFile, false, Encoding.UTF8))
            {
                sw.Write(sr.ReadToEnd());
            }
        }
        File.Delete(newFileCache);
    }

    public static void TranslateCsvToXls(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".csv") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));

        FileInfo fileCopy = new FileInfo(filePath.Replace(".csv",".xml"));

        using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8, false))
        {
            using (StreamWriter sw = new StreamWriter(fileCopy.FullName, false, Encoding.Unicode))
            {
                sw.Write(sr.ReadToEnd());
            }
        }

        //载入csv文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(fileCopy.FullName, ",", 1, 1);
        string outFile = outFolder + "\\" + name + ".xls";

        //保存为xls格式
        workbook.SaveToFile(outFile, ExcelVersion.Version97to2003);
        fileCopy.Delete();

        JCExcel excecl = new JCExcel(outFile);
        excecl.ToExcelTwo(excecl.dataSet, outFile, outFile);
       // excecl.DataSetToExcel(excecl.dataSet, outFile);
        if (!excecl.CheckColumnContent())
        {
            MessageBox.Show(string.Format("{0}失败", name));
            File.Delete(outFile);
        }
        if (!excecl.DetectionOfDigits())
        {
            MessageBox.Show(string.Format("{0}失败", name));
            File.Delete(outFile);
        }
    }
  

    public static void TranslateCsvToXlsx(string filePath, string outFolder)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".csv") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));

        FileInfo fileCopy = new FileInfo(outFolder + "/" + name + "____Cache.txt");

        using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8, false))
        {
            using (StreamWriter sw = new StreamWriter(fileCopy.FullName, false, Encoding.Unicode))
            {
                sw.Write(sr.ReadToEnd());
            }
        }

        //载入csv文档
        Workbook workbook = new Workbook();
        workbook.LoadFromFile(fileCopy.FullName, ",", 1, 1);

        //保存为xlsx格式
        workbook.SaveToFile(outFolder + "\\" + name + ".xlsx", ExcelVersion.Version2013);
        fileCopy.Delete();
    }
}

public enum EExcelVersion
{
    XLS_XLSX = 1,
    XLS_CSV = 2,
    XLSX_XLS = 3,
    XLSX_CSV = 4,
    CSV_XLS = 5,
    CSV_XLSX = 6,
}