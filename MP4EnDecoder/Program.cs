// See https://aka.ms/new-console-template for more information
using MP4EnDecoder;

//Console.WriteLine("Hello, World!");

//https://www.cnblogs.com/chyingp/p/mp4-file-format.html
string inputFile = "input.mp4";
string outputFile = "output.mp4";

// 解码
using (var decoder = new MP4Decoder(inputFile))
{
    var mp4Structure = decoder.Decode();

    // 处理MP4结构
    ProcessMP4Structure(mp4Structure);

    // 编码
    using (var encoder = new MP4Encoder(outputFile))
    {
        encoder.Encode(mp4Structure);
    }
}

static void ProcessMP4Structure(MP4Box rootBox)
{
    // 示例：打印MP4结构
    PrintMP4Structure(rootBox);
}

static void PrintMP4Structure(MP4Box box, int level = 0)
{
    string indent = new string(' ', level * 2);
    Console.WriteLine($"{indent}Box: {box.Type}, Size: {box.Size}");

    foreach (var child in box.Children)
    {
        PrintMP4Structure(child, level + 1);
    }
}