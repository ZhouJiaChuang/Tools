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
using System.Threading;

public class JCExcel
{
    private string mfilePath = string.Empty;
    public string FilePath { get { return mfilePath; } }


    private string mfileName = string.Empty;
    public string FileName { get { return mfileName; } }

    private string fileExtension = string.Empty;

    public int RowCount { get { return GetRowCollection() != null ? GetRowCollection().Count : 0; } }
    public int ColumnCount { get { return GetColumnCollection() != null ? GetColumnCollection().Count : 0; } }

    private static string xlsxExtension = ".xlsx";
    private static string xlsExtension = ".xls";
    DataTableCollection dataTable = null;

    private static StringBuilder strBuilder = new StringBuilder();

    public DataSet dataSet;



    public JCExcel(string filePath)
    {
        Load(filePath);
    }

    public void Load(string path)
    {
        this.mfilePath = path;
        this.mfileName = System.IO.Path.GetFileName(path);

        if (!File.Exists(path))
        {
            Console.WriteLine("没有找到文件:" + path);
            return;
        }
        fileExtension = System.IO.Path.GetExtension(path);

        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        IExcelDataReader excelReader = null;
        if (fileExtension == xlsxExtension)
        {
            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        }
        else if (fileExtension == xlsExtension)
        {
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
        }

        if (excelReader != null)
        {
            DataSet data = excelReader.AsDataSet();
            dataSet = data;
            if (data != null) dataTable = data.Tables;
        }
    }
    #region 合并字段
    /// <summary>
    /// 写入Excel
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="path"></param>
    public void WriteExcel(DataSet ds, string path)
    {
        //try
        //{
        //    if (ds == null)
        //    {
        //        ds = dataSet;
        //    }

        //    long totalCount = ds.Tables[0].Rows.Count;
        //    Thread.Sleep(1000);
        //    long rowRead = 0;
        //    float percent = 0;

        //    StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode);//打开写文件流
        //    StringBuilder sb = new StringBuilder();

        //    List<Dictionary<int, string[]>> combineRowList = GetCombineList(ds);
        //    //数据合并
        //    List<List<StringBuilder>> combineList = new List<List<StringBuilder>>();
        //    for (int i = 0; i < combineRowList.Count; i++)
        //    {
        //        combineList.Add(DataCombine(ds, combineRowList[i]));
        //    }
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)//遍历每行内容
        //    {
        //        rowRead++;
        //        percent = ((float)(100 * rowRead)) / totalCount;
        //        System.Windows.Forms.Application.DoEvents();

        //        for (int j = 0; j < ds.Tables[0].Columns.Count + combineList.Count; j++)//一行中的每列
        //        {
        //            if (j >= ds.Tables[0].Columns.Count)
        //            {
        //                int index = j - ds.Tables[0].Columns.Count;
        //                switch (i)
        //                {
        //                    case 0:
        //                        sb.Append("合并字段#客户端" + "\t");
        //                        break;
        //                    case 1:
        //                        sb.Append("客户端合并字段用，不可手动修改" + "\t");
        //                        break;
        //                    case 2:
        //                        sb.Append("int#" + combineList[index][3].ToString() + "\t");
        //                        break;
        //                    case 3:
        //                        sb.Append(combineList[index][3].ToString() + "\t");
        //                        break;
        //                    default:
        //                        sb.Append(combineList[index][i].ToString() + "\t");
        //                        break;
        //                }

        //            }
        //            else
        //            {
        //                //每个单元格内容，加到StringBuilder中
        //                sb.Append(ds.Tables[0].Rows[i][j].ToString() + "\t");
        //            }


        //        }
        //        sb.Append(Environment.NewLine);
        //    }
        //    sw.Write(sb.ToString());//文件流写入内容
        //    sw.Flush();
        //    sw.Close();
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}
    }
    /// <summary>
    /// 得到合并列集合
    /// </summary>
    /// <returns></returns>
    private List<Dictionary<uint, string[]>> GetCombineList(DataSet ds, out  List<uint> fieldPlaceList)
    {
        //筛选集合
        Dictionary<uint, string[]> screenList = new Dictionary<uint, string[]>();
        fieldPlaceList = new List<uint>();
        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        {
            string[] typeList = ds.Tables[0].Rows[2][i].ToString().Split('#');
            if (typeList.Length == 3)
            {
                if (typeList[0] == "ushort" || typeList[0] == "int" || typeList[0] == "uint" || typeList[0] == "short" || typeList[0] == "ubyte" || typeList[0] == "byte")
                {
                    string[] strList = new string[2];
                    //类型
                    strList[0] = typeList[1];
                    //位数
                    strList[1] = typeList[2];
                    if (!screenList.ContainsKey((uint)i))
                    {
                        screenList.Add((uint)i, strList);
                        fieldPlaceList.Add((uint)i);
                    }

                }
            }
        }
        //合并列集合
        List<Dictionary<uint, string[]>> combineRowList = new List<Dictionary<uint, string[]>>();
        uint tempInt32 = 0;
        Dictionary<uint, string[]> tempList = new Dictionary<uint, string[]>();
        foreach (var item in screenList)
        {
            uint t = uint.Parse(item.Value[1]) + tempInt32;
            if (t < 32)
            {
                tempInt32 = t;
                tempList.Add(item.Key, item.Value);
            }
            else
            {
                tempInt32 = 0;
                if (tempList.Count > 1)
                {
                    combineRowList.Add(tempList);
                }
                else
                {
                    foreach (var temp in tempList)
                    {
                        if (fieldPlaceList.Contains(temp.Key))
                        {
                            fieldPlaceList.Remove(temp.Key);
                        }
                    }
                }
                tempList = new Dictionary<uint, string[]>();
                tempList.Add(item.Key, item.Value);
                tempInt32 = uint.Parse(item.Value[1]);
            }
        }
        if (tempList.Count != 0)
        {
            if (tempList.Count > 1)
            {
                combineRowList.Add(tempList);
            }
            else
            {
                foreach (var item in tempList)
                {
                    if (fieldPlaceList.Contains(item.Key))
                    {
                        fieldPlaceList.Remove(item.Key);
                    }
                }
            }
        }
        return combineRowList;
    }

    /// <summary>
    /// 数据合并
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="screenList"></param>
    /// <returns></returns>
    private List<StringBuilder> DataCombine(DataSet ds, Dictionary<uint, string[]> screenList)
    {
        List<StringBuilder> combineList = new List<StringBuilder>();

        for (uint i = 0; i < ds.Tables[0].Rows.Count; i++)//遍历每行内容
        {
            StringBuilder tempSB = new StringBuilder();
            uint temp = 0;
            uint digit = 0;
            uint index = 0;
            uint indexTwo = 0;
            for (uint j = 0; j < ds.Tables[0].Columns.Count; j++)//一行中的每列
            {
                if (screenList.ContainsKey(j) && i > 3)
                {
                    string digitString = screenList[j][1];
                    digit += uint.Parse(digitString == "" ? "0" : digitString);
                }
            }
            for (uint j = 0; j < ds.Tables[0].Columns.Count; j++)//一行中的每列
            {
                if (screenList.ContainsKey(j))
                {
                    switch (i)
                    {
                        case 0:
                        case 1:
                            break;
                        case 2:
                            indexTwo++;
                            if (indexTwo != screenList.Count)
                            {
                                tempSB.Append((j + 1) + "&");
                            }
                            else
                            {
                                tempSB.Append(j + 1);
                            }

                            break;
                        case 3:
                            index++;
                            if (index != screenList.Count)
                            {
                                tempSB.Append(screenList[j][0] + "#" + screenList[j][1] + '_');
                            }
                            else
                            {
                                tempSB.Append(screenList[j][0] + "#" + screenList[j][1]);
                            }

                            break;
                        default:
                            string tempString = ds.Tables[0].Rows[(int)i][(int)j].ToString();
                            if (tempString == "1002")
                            {
                                string a = "";
                            }
                            string digitString = screenList[j][1];
                            if (temp == 0)
                            {
                                digit -= uint.Parse(digitString == "" ? "0" : digitString);
                                temp = (uint)(int.Parse(tempString == "" ? "0" : tempString) << (int)digit);
                            }
                            else
                            {
                                digit -= uint.Parse(digitString == "" ? "0" : digitString);
                                temp += (uint)(int.Parse(tempString == "" ? "0" : tempString) << (int)digit);
                            }
                            //  tempSB.Append(ds.Tables[0].Rows[i][j].ToString());
                            break;
                    }
                }
            }
            if (tempSB.Length == 0)
            {
                tempSB.Append(temp.ToString());
            }
            combineList.Add(tempSB);
        }
        return combineList;

    }


    /// <summary>
    /// 创建新的EXCEL并写入数据
    /// </summary>
    /// <param name="dataSet"></param>
    /// <param name="savePath"></param>
    public void ToExcel(DataSet dataSet, string savePath)
    {

        DataTable ds = dataSet.Tables[0];
        int rowNumber = ds.Rows.Count;
        int columnNumber = ds.Columns.Count;

        if (rowNumber == 0)
        {
            MessageBox.Show("没有任何数据可以导入到Excel文件！");
        }

        //建立Excel对象
        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbooks wbks = excel.Workbooks;
        Microsoft.Office.Interop.Excel._Workbook _wbk = wbks.Add(true);
        Microsoft.Office.Interop.Excel.Sheets shs = _wbk.Sheets;
        Microsoft.Office.Interop.Excel._Worksheet _wsh = (Microsoft.Office.Interop.Excel._Worksheet)shs.get_Item(1);
        excel.Application.Workbooks.Add(true);
        excel.Visible = false;//是否打开该Excel文件
        List<uint> fieldPlaceList;
        List<Dictionary<uint, string[]>> combineRowList = GetCombineList(dataSet, out fieldPlaceList);
        //数据合并
        List<List<StringBuilder>> combineList = new List<List<StringBuilder>>();
        for (int i = 0; i < combineRowList.Count; i++)
        {
            //   combineList.Add(DataCombine(dataSet, combineRowList[i]));
        }
        //填充数据
        for (int c = 0; c < rowNumber; c++)
        {

            for (int j = 0; j < ds.Columns.Count + combineList.Count; j++)//一行中的每列
            {
                if (j >= ds.Columns.Count)
                {
                    int index = j - ds.Columns.Count;
                    switch (c)
                    {
                        case 0:
                            _wsh.Cells[c + 1, j + 1] = "合并字段#客户端";
                            break;
                        case 1:
                            _wsh.Cells[c + 1, j + 1] = "@" + combineList[index][2].ToString().Replace('&', '#') + " " + combineList[index][3].ToString().Replace('_', ' ');
                            break;
                        case 2:
                            _wsh.Cells[c + 1, j + 1] = "uint#" + combineList[index][3].ToString();
                            break;
                        case 3:
                            _wsh.Cells[c + 1, j + 1] = combineList[index][3].ToString();
                            break;
                        default:
                            _wsh.Cells[c + 1, j + 1] = combineList[index][c].ToString();
                            break;
                    }

                }
                else
                {
                    //每个单元格内容，加到StringBuilder中
                    if (fieldPlaceList.Contains((uint)j) && c == 0)
                    {
                        _wsh.Cells[c + 1, j + 1] = ds.Rows[c].ItemArray[j] + "#合并";
                    }
                    else
                    {
                        _wsh.Cells[c + 1, j + 1] = ds.Rows[c].ItemArray[j];
                    }


                }
            }
        }
        _wbk.SaveCopyAs(savePath);


    }


    /// <summary>
    /// 打开已有EXCEL并写入数据
    /// </summary>
    /// <param name="dataSet"></param>
    /// <param name="savePath"></param>
    public void ToExcelTwo(DataSet dataSet, string excelTempPath, string savePath)
    {
        DataTable ds = dataSet.Tables[0];
        int rowNumber = ds.Rows.Count;
        int columnNumber = ds.Columns.Count;

        if (rowNumber == 0)
        {
            MessageBox.Show("没有任何数据可以导入到Excel文件！");
        }

        //建立Excel对象

        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook wbks = excel.Workbooks.Open(excelTempPath);
        Microsoft.Office.Interop.Excel.Worksheet _wsh = (Microsoft.Office.Interop.Excel.Worksheet)wbks.Worksheets[1];
        excel.Application.Workbooks.Add(true);
        excel.Visible = false;//是否打开该Excel文件
        List<uint> fieldPlaceList;
        List<Dictionary<uint, string[]>> combineRowList = GetCombineList(dataSet, out fieldPlaceList);
        //数据合并
        List<List<StringBuilder>> combineList = new List<List<StringBuilder>>();
        for (int i = 0; i < combineRowList.Count; i++)
        {
            combineList.Add(DataCombine(dataSet, combineRowList[i]));
        }
        //填充数据
        for (int c = 0; c < rowNumber; c++)
        {

            for (int j = 0; j < ds.Columns.Count + combineList.Count; j++)//一行中的每列
            {
                if (j >= ds.Columns.Count)
                {
                    int index = j - ds.Columns.Count;
                    switch (c)
                    {
                        case 0:
                            _wsh.Cells[c + 1, j + 1] = "合并字段";
                            break;
                        case 1:
                            _wsh.Cells[c + 1, j + 1] = "@" + combineList[index][2].ToString().Replace('&', '#') + " " + combineList[index][3].ToString().Replace("_", " ");
                            break;
                        case 2:
                            _wsh.Cells[c + 1, j + 1] = "uint#" + combineList[index][3].ToString().Replace("#", "");
                            break;
                        case 3:
                            _wsh.Cells[c + 1, j + 1] = combineList[index][3].ToString().Replace("#", "");
                            break;
                        default:
                            _wsh.Cells[c + 1, j + 1] = combineList[index][c].ToString();
                            break;
                    }

                }
                else
                {
                    //每个单元格内容，加到StringBuilder中
                    if (fieldPlaceList.Contains((uint)j) && c == 0)
                    {
                        _wsh.Cells[c + 1, j + 1] = ds.Rows[c].ItemArray[j] + "#合并";
                    }

                }
            }
        }
        ClosePro(savePath, excel, wbks);
    }

    /// <summary>
    /// 关闭Excel进程
    /// </summary>
    /// <param name="excelPath"></param>
    /// <param name="excel"></param>
    /// <param name="wb"></param>
    public void ClosePro(string excelPath, Microsoft.Office.Interop.Excel.Application excel, Microsoft.Office.Interop.Excel.Workbook wb)
    {
        System.Diagnostics.Process[] localByNameApp = System.Diagnostics.Process.GetProcessesByName(excelPath);//获取程序名的所有进程
        if (localByNameApp.Length > 0)
        {
            foreach (var app in localByNameApp)
            {
                if (!app.HasExited)
                {
                    #region
                    ////设置禁止弹出保存和覆盖的询问提示框   
                    excel.DisplayAlerts = false;
                    excel.AlertBeforeOverwriting = false;
                    ////保存工作簿   
                    excel.Application.Workbooks.Add(true).Save();
                    ////保存excel文件   
                    excel.Save(excelPath);
                    ////确保Excel进程关闭   
                    excel.Quit();
                    excel = null;
                    #endregion
                    app.Kill();//关闭进程  
                }
            }
        }
        if (wb != null)
            wb.Close(true, Type.Missing, Type.Missing);
        excel.Quit();
        // 安全回收进程
        System.GC.GetGeneration(excel);
    }
    #endregion

    /// <summary>
    /// 得到表格中的行集合
    /// </summary>
    /// <param name="sheetIndex">页脚下标</param>
    /// <returns></returns>
    public DataRowCollection GetRowCollection(int sheetIndex = 0)
    {

        if (dataTable == null) return null;
        return dataTable[sheetIndex].Rows;
    }

    /// <summary>
    /// 得到表格中的列集合
    /// </summary>
    /// <param name="sheetIndex">页脚下标</param>
    /// <returns></returns>
    public DataColumnCollection GetColumnCollection(int sheetIndex = 0)
    {
        if (dataTable == null) return null;
        return dataTable[sheetIndex].Columns;
    }

    /// <summary>
    /// 得到表格中的指定格
    /// </summary>
    /// <param name="rowIndex">行下标0-</param>
    /// <param name="columnIndex">列下标0-</param>
    /// <returns></returns>
    public object GetCellObject(int rowIndex, int columnIndex, int sheetIndex = 0)
    {
        DataRowCollection rows = GetRowCollection(sheetIndex);
        if (rows[rowIndex] == null)
        {
            return string.Empty;
        }
        if (rows[rowIndex][columnIndex] == null)
        {
            return string.Empty;
        }
        return rows[rowIndex][columnIndex];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="columnIndex">检测第几列的类型</param>
    /// <param name="type">内容类型</param>
    /// <param name="typeName">列proto名称</param>
    /// <param name="typeProperty">列proto属性require/optional</param>
    /// <returns></returns>
    public bool IsConformTypeNameNorm(int columnIndex, out ContentType type, out string typeName, out string typeProperty)
    {
        type = ContentType.None;
        typeName = string.Empty;
        typeProperty = string.Empty;

        if (RowCount < 4)
        {
            MessageBox.Show(string.Format("表格{0}行数低于4行,不符合1.标题 2.描述 3.客户端标记 4服务器名称", FileName));
            return false;
        }

        GetProtoType(columnIndex, out type, out typeName);

        if (type == ContentType.None)
        {
            // MessageBox.Show(string.Format("文件{0}第{1}列第3行内容为{2},不能找到对应类型解析"));
            return false;
        }

        if (columnIndex == 0 || typeName == "value" || typeName == "type"
            || type == ContentType.int32 || type == ContentType.int64 || type == ContentType.uint32 || type == ContentType.floatValue)
            typeProperty = "required";
        else
            typeProperty = "optional";

        return true;
    }

    public string GetTitle(int columnIndex)
    {
        strBuilder.Clear();
        strBuilder.Append(GetCellObject(0, columnIndex).ToString());
        return strBuilder.ToString();
    }

    public string GetDesc(int columnIndex)
    {
        strBuilder.Clear();
        strBuilder.Append(GetCellObject(1, columnIndex).ToString());
        return strBuilder.ToString();
    }

    static string clientUseKey = "客户端";
    static string combine = "合并";
    static string clientUnUse = "客户端未使用";
    public bool IsClientUse(int columnIndex)
    {
        strBuilder.Clear();
        strBuilder.Append(GetCellObject(0, columnIndex).ToString());
        string[] titles = strBuilder.ToString().Split('#');
        foreach (var str in titles)
        {
            if (str == combine)
                return false;
            if (str == clientUnUse)
            {
                return false;
            }
        }
        if (titles.Length < 2)
            return true;
        else
        {
            foreach (var str in titles)
            {
                string[] keys = str.Split('&');
                foreach (var key in keys)
                {
                    if (key == clientUseKey)
                        return true;
                }

            }
            return false;
        }
    }
    public bool DetectionOfDigits()
    {

        List<string> list = new List<string>();
        for (int i = 0; i < ColumnCount; i++)
        {
            string[] Digits = GetCellObject(2, i).ToString().Split('#');
            double digit = 0;
            if (Digits.Length == 3)
            {
                digit = Math.Pow(2, int.Parse(Digits[2]));

                for (int q = 4; q < RowCount; q++)
                {
                    int number = 0;

                    if (int.TryParse(GetCellObject(q, i).ToString(), out number))
                    {
                        if (number >= digit)
                        {
                            MessageBox.Show(string.Format("{0} 第{1}列 {2}行数值大于位数最大值", FileName, i + 1, q + 1));
                            return false;
                        }
                    }
                    else
                    {
                        if (GetCellObject(q, i).ToString() != "")
                        {
                            MessageBox.Show(string.Format("{0} 第{1}列 {2}行数据类型错误", FileName, i + 1, q + 1));
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    public bool CheckColumnContent()
    {
        ContentType type = ContentType.None;
        string name = string.Empty;
        List<string> list = new List<string>();
        for (int i = 0; i < ColumnCount; i++)
        {
            GetProtoType(i, out type, out name);
            if (list.Contains(name))
            {
                MessageBox.Show(string.Format("{0} 第{1}列出现重复类型名称{2}", FileName, i + 1, name));
                return false;
            }
            list.Add(name);
            if (!CheckColumnContent(i, type) || type == ContentType.None)
            {
                return false;
            }
        }
        return true;
    }


    public bool CheckColumnContent(int columnIndex, ContentType type)
    {
        for (int i = 4; i < RowCount; i++)
        {
            strBuilder.Clear();
            strBuilder.Append(GetCellObject(i, columnIndex).ToString());
            if (!ExcelCheck.CheckContentType(type, strBuilder.ToString()))
            {
                MessageBox.Show(string.Format("文件{0}第{1}行{2}列,内容{3}不为{4}", FileName, i + 1, columnIndex + 1, strBuilder.ToString(), type));
                return false;
            }
        }
        return true;
    }

    private void GetProtoType(int columnIndex, out ContentType type, out string name)
    {
        strBuilder.Clear();
        strBuilder.Append(GetCellObject(2, columnIndex).ToString());

        name = string.Empty;
        type = ContentType.None;

        if (string.IsNullOrEmpty(strBuilder.ToString()))
        {
            MessageBox.Show(string.Format("文件{0}第{1}列第3行内容为空", FileName, columnIndex + 1));
            return;
        }

        string[] typeCells = strBuilder.ToString().Split('#');
        if (typeCells.Length == 1)
        {
            MessageBox.Show(string.Format("文件{0}第{1}列第3行内容为{2},不符合[类型#名称]格式", FileName, columnIndex + 1, strBuilder.ToString()));
            return;
        }
        type = GetProtoType(typeCells[0]);
        name = typeCells[1];
        return;
    }

    private ContentType GetProtoType(string execelType)
    {
        execelType = execelType.Replace(" ", "");
        if (string.IsNullOrEmpty(execelType)) return ContentType.None;
        switch (execelType)
        {
            case "int":
                return ContentType.int32;
            case "long":
                return ContentType.int64;
            case "byte":
            case "short":
            case "uint":
            case "ubyte":
            case "ushort":
                return ContentType.uint32;
            case "string":
                return ContentType.stringValue;
            case "float":
                return ContentType.floatValue;
            case "IntList":
                return ContentType.IntList;
            case "StringList":
                return ContentType.StringList;
            case "IntListJingHao":
                return ContentType.IntListJingHao;
            case "IntListJingHaoMeiYuan":
                return ContentType.IntListJingHaoMeiYuan;
            case "IntListList":
                return ContentType.IntListList;
            case "IntListXiaHuaXian":
                return ContentType.IntListXiaHuaXian;
            case "IntListXiaHuaXianFenHao":
                return ContentType.IntListXiaHuaXianFenHao;
            default:
                return ContentType.None;
        }
    }
}

/// <summary>
/// Excel列内容类型
/// </summary>
public enum ContentType
{
    None,
    int32,
    int64,
    uint32,
    IntList,
    StringList,
    IntListJingHao,
    IntListJingHaoMeiYuan,
    IntListList,
    IntListXiaHuaXian,
    IntListXiaHuaXianFenHao,
    stringValue,
    floatValue,
}