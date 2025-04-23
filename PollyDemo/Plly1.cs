using Polly;
using Polly.Retry;

namespace PollyDemo;

public class PollyRetryDemo
{
    public static void Run()
    {
        // 使用最新的策略API定义重试策略
        var retryStrategy = new RetryStrategyOptions
        {
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(1),
            ShouldHandle = new PredicateBuilder().Handle<Exception>(),
            OnRetry = args =>
            {
                Console.WriteLine($"第{args.AttemptNumber}次重试，异常：{args.Outcome.Exception?.Message}");
                return default;
            }
        };

        var retry = new ResiliencePipelineBuilder()
            .AddRetry(retryStrategy)
            .Build();

        try
        {
            retry.Execute(() =>
            {
                Console.WriteLine("尝试执行操作...");
                throw new Exception("操作失败！");
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"最终失败：{ex.Message}");
        }
    }
}