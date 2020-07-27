using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Utility
{
    public static Process GenerateProcess()
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
        process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        //process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        //process.StartInfo.CreateNoWindow = true;//不显示程序窗口
        return process;
    }
}