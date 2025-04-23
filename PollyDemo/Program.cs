// See https://aka.ms/new-console-template for more information
using Polly;
using PollyDemo;

Console.WriteLine("Hello, World!");

// 定义一个重试策略，最多重试3次，每次间隔1秒
var retryPolicy = Policy
    .Handle<Exception>()
    .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(1),
        (exception, timeSpan, retryCount, context) =>
        {
            Console.WriteLine($"第{retryCount}次重试，异常：{exception.Message}");
        });

// 使用重试策略执行操作
try
{
    retryPolicy.Execute(() =>
    {
        Console.WriteLine("尝试执行操作...");
        // 模拟抛出异常
        throw new Exception("操作失败！");
    });
}
catch (Exception ex)
{
    Console.WriteLine($"最终失败：{ex.Message}");
}

PollyRetryDemo.Run();
// PollyRetryDemo.Run();
