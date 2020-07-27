using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class ExcelParse
{
    public static StringBuilder sbtemp = new StringBuilder();
    static string clientUseKey = "客户端";
    static string serverUseKey = "服务器";
    static string cehuaUseKey = "策划";
    static string clientUnUse = "客户端未使用";

    public static void ParseXlsToPro(string xmlFile)
    {
        if (!xmlFile.ToLower().Contains("cfg_")) return;
        xmlFile = xmlFile.Replace("\\", "/");

        JCExcel excel = new JCExcel(xmlFile);

    }

    public static void ParseXlsToPrto(string xmlFile, string outDirectory)
    {
        if (!xmlFile.ToLower().Contains("cfg_")) return;
        xmlFile = xmlFile.Replace("\\", "/");

        DataRowCollection data = ExcelAccess.ReadExcel(xmlFile, 0, ExcelAccess.ExcelType.xls);

        string name = Path.GetFileNameWithoutExtension(xmlFile);

        JCExcel excel = new JCExcel(xmlFile);

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("package TABLE;");
        sb.AppendLine("import \"table_common.proto\";");
        sb.AppendLine("");
        sb.AppendLine(string.Format("message {0}", name.ToUpper()));

        sb.AppendLine("{");

        ContentType type = ContentType.None;
        string typeName = string.Empty;
        string protoName = string.Empty;
        string typeProperty = string.Empty;

        List<string> idLidst = new List<string>();

        for (int i = 0; i < excel.ColumnCount; i++)
        {
            if (excel.IsConformTypeNameNorm(i, out type, out protoName, out typeProperty))
            {
                if (type == ContentType.stringValue) typeName = "string";
                else if (type == ContentType.floatValue) typeName = "float";
                else typeName = type.ToString();

                //每列数据的检查
                if (!excel.CheckColumnContent(i, type))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(protoName))
                    continue;

                //避免同名的出现
                if (idLidst.Contains(protoName))
                {
                    MessageBox.Show(string.Format("{0} {1}列出现重复名称{2}", xmlFile, i + 1, protoName));
                    continue;
                }

                idLidst.Add(protoName);
                if (excel.GetTitle(i) == "合并字段")
                {
                    sb.AppendLine("    //" + excel.GetDesc(i));
                    sb.AppendLine(string.Format("\t{0}{1} {2} {3} = {4};",
                   (excel.IsClientUse(i) ? "" : "//"), "required", typeName, protoName, i + 1));
                }
                else
                {
                  
                        sb.AppendLine(string.Format("\t{0}{1} {2} {3} = {4};//{5}  {6}",
                                     (excel.IsClientUse(i) ? "" : "//"), typeProperty, typeName, protoName, i + 1, excel.GetTitle(i), excel.GetDesc(i)));
                 
                }

            }
        }

        sb.AppendLine("}");

        sb.AppendLine("");

        sb.AppendLine(string.Format("message {0}", name.ToUpper() + "ARRAY"));

        sb.AppendLine("{");
        sb.AppendLine(string.Format("\trepeated {0} rows = 1;", name.ToUpper()));
        sb.AppendLine("}");

        Utility.SaveFileContent(outDirectory + "/c_table_" + name.ToLower() + ".proto", sb.ToString());
    }


    static StringBuilder strBuilder = new StringBuilder();
    public static void ParseProtoCsvData(string csvFile, string protoFolder)
    {
        strBuilder.Clear();
        ExcelTranslate.TranslateCsvToXls(csvFile, Directory.GetParent(csvFile).FullName);
        strBuilder.Append(csvFile.Replace(".csv", ".xls"));
        ParsePrtoXlsData(strBuilder.ToString(), protoFolder);
        File.Delete(strBuilder.ToString());
    }

    /// <summary>
    /// 解析xml数据是否和proto数据匹配
    /// </summary>
    /// <param name="xmlFile"></param>
    /// <param name="protoFolder"></param>
    public static void ParsePrtoXlsData(string xmlFile, string protoFolder)
    {
        //DataRowCollection data = ExcelAccess.ReadExcel(xmlFile, 0, ExcelAccess.ExcelType.xls);
        //if (data == null) return;
        //string name = Path.GetFileNameWithoutExtension(xmlFile);

        //DataRow titleRow = data[0];
        //DataRow descRow = data[1];
        //DataRow typeRow = data[2];
        //DataRow idRow = data[3];
        //int colCount = idRow.ItemArray.Length;

        //int rawCount = data.Count;

        //ContentType type = ContentType.None;

        ////格式需要前4行
        //if (rawCount < 4)
        //{
        //    return;
        //}

        //for (int c = 0; c < colCount; c++)
        //{
        //    for (int i = 0; i < rawCount; i++)
        //    {
        //        DataRow row = data[i];
        //        if (i == 2)
        //        {
        //            string[] typeName = row[c].ToString().Split('#');

        //            if (typeName.Length == 0)
        //            {
        //                type = ContentType.None;

        //                if (type == ContentType.None)
        //                {
        //                    MessageBox.Show(string.Format("{0}第{1}行第{2}列类型未填写", xmlFile, i + 1, c + 1));
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                type = GetProtoType(GetValidParamName(typeName[0].ToString()));
        //                if (type == ContentType.None)
        //                {
        //                    MessageBox.Show(string.Format("{0}第{1}行第{2}列{3}类型未找到", xmlFile, i + 1, c + 1, typeName[0].ToString()));
        //                    break;
        //                }
        //            }

        //        }
        //        if (i < 4) continue;//前4行是格式

        //        if (!CheckContentType(type, row[c].ToString()))
        //        {
        //            MessageBox.Show(string.Format("{0}第{1}行 {2} 不为{3}", xmlFile, i + 1, row[c].ToString(), type));
        //            break;
        //        }
        //    }
        //}
    }


    static string defaultTypeName = "string#";
    internal static void ParseGenerateClientNameType(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        if (file.Extension != ".csv") return;
        string name = file.Name.Substring(0, file.Name.LastIndexOf('.'));

        FileInfo fileCopy = new FileInfo(filePath.Replace(".csv", ".xml"));

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

        for (int i = 1; i <= workbook.ActiveSheet.Columns.Length; i++)
        {
            CellRange titleCell = workbook.ActiveSheet[1, i];
            if (titleCell.NumberText.Split('#').Length == 1)
                titleCell.Text = titleCell.NumberText + "#" + clientUseKey;

            CellRange typeCell = workbook.ActiveSheet[3, i];
            string typeName = typeCell.NumberText.ToString();
            CellRange NameCell = workbook.ActiveSheet[4, i];
            string serverName = NameCell.NumberText.ToString();
            if (string.IsNullOrEmpty(typeName))
            {
                typeName = defaultTypeName + serverName;
            }
            else
            {
                string[] strs = typeName.Split('#');
                if (strs.Length == 1)
                    typeName = typeName + "#" + serverName;
            }
            typeCell.Text = typeName;
        }
        //每行开头不留空行
        CellRange data = workbook.ActiveSheet[2, 1];
        if (string.IsNullOrEmpty(data.NumberText))
        {
            data.Text = workbook.ActiveSheet[1, 1].NumberText;
        }
        workbook.SaveToFile(fileCopy.FullName, FileFormat.CSV);

        using (StreamReader sw = new StreamReader(fileCopy.FullName, Encoding.Unicode, false))
        {
            string content = sw.ReadToEnd().Replace("\"", "");

            Utility.SaveFileContent(filePath, content);
        }



        fileCopy.Delete();
    }
    internal static void MarkUnUseField(string filePath)
    {
        string[] unUseFieldList = GetUnUseFieldList();
        FileInfo file = new FileInfo(filePath);
        if (!File.Exists(filePath))
        {
            return;
        }
        if (file.Extension != ".csv") return;
        FileInfo fileCopy = new FileInfo(filePath.Replace(".csv", ".xml"));
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
        for (int i = 1; i <= workbook.ActiveSheet.Columns.Length; i++)
        {
            CellRange titleCell = workbook.ActiveSheet[1, i];
            CellRange typeCell = workbook.ActiveSheet[3, i];
            string typeName = typeCell.NumberText.ToString();
            CellRange NameCell = workbook.ActiveSheet[4, i];
            string nameCellName = NameCell.NumberText.ToString();
            if (unUseFieldList != null && nameCellName!="")
            {
                for (int j = 0; j < unUseFieldList.Length; j++)
                {
                    if (nameCellName == unUseFieldList[j]&&unUseFieldList[i]!="")
                    {

                        titleCell.Text = titleCell.NumberText.Split('#')[0] + "#" + clientUnUse;
                        break;
                    }
                }
            }

        }
        //每行开头不留空行
        CellRange data = workbook.ActiveSheet[2, 1];
        if (string.IsNullOrEmpty(data.NumberText))
        {
            data.Text = workbook.ActiveSheet[1, 1].NumberText;
        }
        workbook.SaveToFile(fileCopy.FullName, FileFormat.CSV);

        using (StreamReader sw = new StreamReader(fileCopy.FullName, Encoding.Unicode, false))
        {
            string content = sw.ReadToEnd().Replace("\"", "");

            Utility.SaveFileContent(filePath, content);
        }

        fileCopy.Delete();
    }
    private static string[] GetUnUseFieldList()
    {
        string[] unUseFieldList;
        using (StreamReader csReader = new StreamReader(@"C:\Users\Administrator\Desktop\Field.txt"))
        {
            string csText = csReader.ReadToEnd();
            unUseFieldList = csText.Split('#');
            csReader.Close();
        }
        return unUseFieldList;
    }


}

