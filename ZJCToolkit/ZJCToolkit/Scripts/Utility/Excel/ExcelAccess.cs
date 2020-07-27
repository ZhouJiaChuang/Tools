using Excel;
using OfficeOpenXml;
using System.Data;
using System.IO;

public class ExcelAccess
{
    #region 读
    /// <summary>
    /// 读取 Excel ; 需要添加 Excel.dll; System.Data.dll;
    /// </summary>
    /// <param name="excelName">excel文件名</param>
    /// <param name="sheetName">sheet名称</param>
    /// <returns>DataRow的集合</returns>
    public static DataRowCollection ReadExcel(string excelPath, string sheetName, ExcelType type)
    {
        DataSet result = ReadExcelDataSet(excelPath, type);
        if (result == null) return null;
        return result.Tables[sheetName].Rows;
    }

    public static DataRowCollection ReadExcel(string excelPath, int sheetIndex, ExcelType type)
    {
        DataSet result = ReadExcelDataSet(excelPath, type);
        if (result == null) return null;
        return result.Tables[sheetIndex].Rows;
    }

    public static DataColumnCollection ReadExcelColumn(string excelPath, int sheetIndex, ExcelType type)
    {
        DataSet result = ReadExcelDataSet(excelPath, type);
        if (result == null) return null;
        return result.Tables[sheetIndex].Columns;
    }

    public enum ExcelType
    {
        xls,
        xlsx,
    }

    static DataSet ReadExcelDataSet(string path, ExcelType type)
    {
        if (!File.Exists(path)) return null;
        DataSet result = null;
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = null;
        if (type == ExcelType.xls)
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        else if (type == ExcelType.xlsx)
            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //excelReader.IsFirstRowAsColumnNames = true;
        result = excelReader.AsDataSet();
        return result;
    }
    #endregion

    #region 写

    public static ExcelPackage GetExcelPackage(string path)
    {
        FileInfo newFile = new FileInfo(path);

        //通过ExcelPackage打开文件
        ExcelPackage package = new ExcelPackage(newFile);
        //using ()
        {
            return package;
        }
    }

    public static void Save(ExcelPackage package, string path)
    {
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            package.SaveAs(stream);
            stream.Close();
        }
    }

    public static void WriteExcelWorksheet(string path, string sheetName, System.Action<ExcelWorksheet> addData, System.Action<string> finishEvent = null)
    {
        //自定义excel的路径
        FileInfo newFile = new FileInfo(path);

        //通过ExcelPackage打开文件
        using (ExcelPackage package = new ExcelPackage(newFile))
        {
            //在excel空文件添加新sheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(sheetName);
            if (addData != null)
            {
                addData(worksheet);
            }

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                package.SaveAs(stream);
                stream.Close();
            }

            if (finishEvent != null)
                finishEvent(path);
        }
    }
    #endregion

}