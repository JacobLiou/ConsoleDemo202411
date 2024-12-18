// See https://aka.ms/new-console-template for more information
namespace CsPythonInterOp;

// Console.WriteLine("Hello, World!");

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
    }
}
