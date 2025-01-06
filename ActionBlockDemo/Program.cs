// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

Console.WriteLine("Hello, World!");
await RunActionBlockAsync();
await RunBatchBlockAsync();
Console.WriteLine("Done");

partial class Program
{
    private static async Task RunActionBlockAsync()
    {
        var actionBlock = new ActionBlock<int>(async i =>
        {
            Console.WriteLine($"Processing {i}");
            await Task.Delay(1000);
            Console.WriteLine($"Processed {i}");
        });

        for (int i = 0; i < 10; i++)
        {
            await actionBlock.SendAsync(i);
        }

        actionBlock.Complete();
        await actionBlock.Completion; // wait for all processing to complete
    }

    private static async Task RunBatchBlockAsync()
    {
        var httpclient = new HttpClient();
        var downloadBlock = new ActionBlock<string>(async url =>
        {
            var content = await httpclient.GetStringAsync(url);
            Console.WriteLine($"Downloaded {url} with {content.Length} characters");
        });

        var urls = new[] { "https://www.google.com", "https://www.microsoft.com", "https://www.apple.com" };
        foreach (var url in urls)
        {
            downloadBlock.Post(url);
        }

        downloadBlock.Complete();
        await downloadBlock.Completion; // wait for all downloads to complete
    }
}