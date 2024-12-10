// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// var colorRed = "\xlb[31m红色文本]";
// Console.WriteLine(colorRed);    

var colorRed = "\x1b[31m红色文本";
Console.WriteLine(colorRed);
Console.ReadKey();

