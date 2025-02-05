// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using System.Net;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var config = builder.Build();

//dotnet UserSercrets 原理解释
//dotnet user-secrets 是 .NET 提供的一种 开发环境中的密钥管理 机制，主要用于存储 敏感信息（如 API 密钥、数据库连接字符串等），避免将这些信息直接硬编码或提交到版本控制系统（如 Git）。dotnet user-secrets 是 .NET 提供的一种 开发环境中的密钥管理 机制，主要用于存储 敏感信息（如 API 密钥、数据库连接字符串等），避免将这些信息直接硬编码或提交到版本控制系统（如 Git）。
string token = config["GitHub_Token"] ?? throw new Exception("GitHub Token is missing");

AzureKeyCredential credential = new AzureKeyCredential(token);
//Uri uri = new Uri("https://api.github.com/models");
Uri uri = new Uri("https://models.inference.ai.azure.com");

string model_name = "DeepSeek-R1";
IChatClient client = new ChatCompletionsClient(uri, credential).AsChatClient(model_name);

string question = "If I have 3 apples and eat 2, how many bananas do I have?";
var response = client.CompleteStreamingAsync(question);

Console.WriteLine($">>> User: {question}");
Console.Write(">>>");
Console.WriteLine(">>> DeepSeek (might be a while): ");

await foreach (var item in response)
{
    Console.Write(item);
}
