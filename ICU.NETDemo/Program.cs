// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Icu;
using System.Globalization;

 Icu.Wrapper.Init(); // 初始化 ICU 库


// 1. 使用 ICU 的 BreakIterator 类来分词
// var text = "Hello, World!";
// var bi = BreakIterator.CreateWordInstance(CultureInfo.InvariantCulture);
// bi.SetText(text);
// for (int i = bi.First(); i != BreakIterator.Done; i = bi.Next())
// {
//     Console.WriteLine(text.Substring(i, bi.Current() - i));

// }

Span<string> testTextSpan = ["大学生活", "大学生活动", "大学生命"];
foreach (var text in testTextSpan)
{
    Console.WriteLine(text);
    foreach(var boundary in Icu.BreakIterator.GetBoundaries(BreakIterator.UBreakIteratorType.WORD, 
        Locale.GetLocaleForLCID(CultureInfo.CurrentCulture.LCID), text))
    {
        var subText = text.AsSpan().Slice(boundary.Start, boundary.End - boundary.Start);
        Console.WriteLine(subText.ToString()); // 输出分词结果
    }
}

Icu.Wrapper.Cleanup(); // 释放 ICU 库资源
