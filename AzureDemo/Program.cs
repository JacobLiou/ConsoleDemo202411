using Azure;
using Azure.AI.OpenAI;
using System.Text;

// Azure OpenAI配置
string endpoint = "YOUR_AZURE_OPENAI_ENDPOINT";
string key = "YOUR_AZURE_OPENAI_KEY";
string deploymentName = "YOUR_DEPLOYMENT_NAME";

// 创建OpenAIClient
OpenAIClient client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

// 准备聊天消息
var chatMessages = new List<ChatMessage>
{
    new ChatMessage(ChatRole.System, "你是一个有帮助的AI助手。"),
    new ChatMessage(ChatRole.User, "你好，请介绍一下你自己。")
};

// 创建聊天完成选项
var chatCompletionsOptions = new ChatCompletionsOptions();
foreach (var message in chatMessages)
{
    chatCompletionsOptions.Messages.Add(message);
}

try
{
    // 调用Azure OpenAI服务
    Response<ChatCompletions> response = await client.GetChatCompletionsAsync(deploymentName, chatCompletionsOptions);
    ChatCompletions completions = response.Value;

    // 输出AI的回复
    Console.WriteLine("AI回复：");
    foreach (var choice in completions.Choices)
    {
        Console.WriteLine(choice.Message.Content);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"发生错误: {ex.Message}");
}
