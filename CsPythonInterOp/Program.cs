// See https://aka.ms/new-console-template for more information
namespace CsPythonInterOp;

// Console.WriteLine("Hello, World!");


using Python;
using Python.Runtime;
using System;

public static class CalcService
{
    public static double Add(double a, double b)
    {
        return a + b;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var result = CalcService.Add(1, 2);
        Console.WriteLine(result);

        // var python = new PythonEngine();
        // python.

        // 设置 PYTHONPATH 环境变量，指向 service.py 所在的目录
        //PythonEngine.PythonHome = @"D:\Program Files\Python310";

        //报错解决 https://stackoverflow.com/questions/78934318/call-python-from-c-sharp-error-runtime-pythondll-was-not-set-or-does-not-point
        Runtime.PythonDLL = @"D:\Program Files\Python310\python310.dll";
        Environment.SetEnvironmentVariable("PYTHONPATH", @"D:\Project Demo\Demo\Console\CsPythonInterOp\bin\Debug\net9.0", EnvironmentVariableTarget.Process);
        PythonEngine.Initialize();
        using(Py.GIL())
        {
            dynamic module = Py.Import("service");
            dynamic oked = module.Add(1, 2);
            Console.WriteLine(oked);
        }

        // //直接以命令行方式调用Python脚本
        //  // 设置Python解释器的路径
        // string pythonPath = @"C:\Path\To\Python\python.exe";  // 修改为实际路径
        // string scriptPath = @"C:\Path\To\Your\Script\test.py";  // 修改为实际路径
        // string argument = "C#";

        // // 创建一个新的进程来执行Python脚本
        // ProcessStartInfo start = new ProcessStartInfo()
        // {
        //     FileName = pythonPath,
        //     Arguments = $"{scriptPath} {argument}",
        //     RedirectStandardOutput = true,  // 重定向标准输出
        //     UseShellExecute = false,       // 禁止使用操作系统外壳
        //     CreateNoWindow = true          // 不显示命令行窗口
        // };

        // using (Process process = Process.Start(start))
        // {
        //     using (System.IO.StreamReader reader = process.StandardOutput)
        //     {
        //         string result = reader.ReadToEnd();
        //         Console.WriteLine(result);  // 输出Python脚本的结果
        //     }
        // }
    }
}
