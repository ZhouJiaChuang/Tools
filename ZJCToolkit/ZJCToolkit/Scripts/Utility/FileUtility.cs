using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

public partial class Utility 
{
    public static void OutCallTest()
    {
        //找到SharpSVNTool.exe的路径
        //string sharpSVNToolPath = FileUtility.GetBackDir(Application.dataPath, 4) + "/Tool/SharpSVNTool/SharpSVNTool.exe";
        string sharpSVNToolPath = Utility.GetBackDir(Environment.CurrentDirectory,1)+ "/SharpSVNTool/SharpSVNTool.exe";//任务工具的目录和SharpSVNTool同级，不同路径自行修改
        bool isSucess = Utility.CallProcess(sharpSVNToolPath, "", true);
        if (!isSucess)
        {
            //UnityEngine.Debug.LogError("权限获取失败!");
            return;
        }
    }

    public static bool CallProcess(string processName, string param = "", bool isCreateNoWindow = false)
    {
        ProcessStartInfo process = new ProcessStartInfo
        {
            CreateNoWindow = isCreateNoWindow,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            FileName = processName,
            Arguments = param,
        };

        Process p = Process.Start(process);

        p.WaitForExit();

        string error = p.StandardError.ReadToEnd();
        if (!string.IsNullOrEmpty(error))
        {
            //UnityEngine.Debug.LogError(processName + " " + param + "  ERROR! " + "\n" + error);

            string output = p.StandardOutput.ReadToEnd();
            if (!string.IsNullOrEmpty(output))
            {
                //UnityEngine.Debug.Log(output);
            }

            return false;
        }
        return true;
    }

    public static string GetBackDir(string dir, int time)
    {
        dir = dir.Replace("\\", "/");
        for (int i = 0; i < time; i++)
        {
            int lastIndex = dir.LastIndexOf("/");
            if (lastIndex == dir.Length - 1)
            {
                dir = dir.Substring(0, dir.Length - 1);
            }
            lastIndex = dir.LastIndexOf("/");
            dir = dir.Substring(0, lastIndex);
        }
        return dir;
    }

    public static void MoveAllFile(string assestsDir, string saveDir)
    {
        //获取文件夹所有文件和文件夹  
        string[] files = Directory.GetFileSystemEntries(assestsDir);
        foreach (string file in files)
            Utility.DelFile(file.Replace(assestsDir, saveDir));
        foreach (string file in files)
            Utility.MoveFile(file, file.Replace(assestsDir, saveDir));

        //MessageBox.Show("拷贝资源完成");
    }

    public static void MoveAllZIPFile(string assestsDir, string saveDir)
    {
        //获取文件夹所有文件和文件夹  
        string[] files = Directory.GetFileSystemEntries(assestsDir);

        foreach (string file in files)
            Utility.DelFile(file.Replace(assestsDir, saveDir));
        foreach (string file in files)
            Utility.MoveZIPFile(file, file.Replace(assestsDir, saveDir));

        //MessageBox.Show("拷贝资源完成");
    }

    public static void MoveFile(string sourceUrl, string targetUrl)
    {
        try
        {
            if (Directory.Exists(sourceUrl))
            {
                if (!Directory.Exists(targetUrl))
                    Directory.CreateDirectory(targetUrl);

                string[] files = Directory.GetFiles(sourceUrl);
                foreach (string formFileName in files)
                {
                    string fileName = Path.GetFileName(formFileName);
                    string toFileName = Path.Combine(targetUrl, fileName);
                    MoveFile(formFileName, toFileName);
                }
                string[] fromDirs = Directory.GetDirectories(sourceUrl);
                foreach (string fromDirName in fromDirs)
                {
                    string dirName = Path.GetFileName(fromDirName);
                    string toDirName = Path.Combine(targetUrl, dirName);
                    MoveFile(fromDirName, toDirName);
                }
            }
            else if (File.Exists(sourceUrl))
            {
                File.Copy(sourceUrl, targetUrl);

            }

        }
        catch (Exception e) { MessageBox.Show(sourceUrl + "移动失败:" + e); }
    }

    public static void MoveZIPFile(string sourceUrl, string targetUrl)
    {
        try
        {
            if (File.Exists(sourceUrl) && sourceUrl.Contains(".zip"))
            {
                File.Copy(sourceUrl, targetUrl);
            }

        }
        catch (Exception e) { MessageBox.Show(sourceUrl + "移动失败:" + e); }
    }

    public static void DelFile(string sourceUrl)
    {
        try
        {
            if (File.Exists(sourceUrl))
                File.Delete(sourceUrl);
            if (Directory.Exists(sourceUrl))
            {
                string[] files = Directory.GetFiles(sourceUrl);
                foreach (string formFileName in files)
                {
                    string fileName = Path.GetFileName(formFileName);
                    DelFile(formFileName);
                }
                string[] fromDirs = Directory.GetDirectories(sourceUrl);
                foreach (string fromDirName in fromDirs)
                {
                    string dirName = Path.GetFileName(fromDirName);
                    DelFile(fromDirName);
                }

                Directory.Delete(sourceUrl);
            }
        }
        catch (Exception e) { MessageBox.Show(sourceUrl + "删除失败:" + e); }
    }

    public static string LoadFileContent(string filePath)
    {
        string xmlContent = "";
        if (!File.Exists(filePath)) return xmlContent;
        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);
            xmlContent = Encoding.UTF8.GetString(bytes);

            stream.Close();
        }
        return xmlContent;
    }

    public static void CreateDictionary(string filePath)
    {
        if (!File.Exists(filePath))
            Directory.CreateDirectory(filePath);
    }

    public static void SaveFileContent(string filePath, string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Directory.Exists)
            {
                Directory.CreateDirectory(file.Directory.FullName);
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(content);
                stream.Write(myByte, 0, (int)myByte.Length);
                stream.Flush();
                stream.Close();
            }
        }
    }

    public static void SaveFileContent(string filePath, byte[] content)
    {
        if (content != null)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                byte[] myByte = content;
                stream.Write(myByte, 0, (int)myByte.Length);
                stream.Flush();
                stream.Close();
            }
        }
    }

    public static void ChooseFile(string desc, string tip, Action<string> req)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Multiselect = true;//该值确定是否可以选择多个文件
        dialog.Title = desc;
        dialog.Filter = "所有文件(*.*)|*.*";
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            if (string.IsNullOrEmpty(dialog.FileName))
                MessageBox.Show(tip, "提示");
            else
                req(dialog.FileName);
        }
    }

    public static void ChooseFolder(string desc, string tip, Action<string> req)
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.Description = desc;
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            if (string.IsNullOrEmpty(dialog.SelectedPath))
                MessageBox.Show(tip, "提示");
            else
                req(dialog.SelectedPath);
        }
    }

    /// <summary>
    /// 打开目录
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static System.Diagnostics.Process OpenFolder(string path)
    {
        System.Diagnostics.Process pp = System.Diagnostics.Process.Start(path);
        return pp;
    }
}
