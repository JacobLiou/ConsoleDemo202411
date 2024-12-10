// Core/MP4Encoder.cs

using System.Text;

namespace MP4EnDecoder;

public class MP4Encoder : IDisposable
{
    private readonly FileStream _fileStream;
    private readonly BinaryWriter _writer;

    public MP4Encoder(string outputPath)
    {
        _fileStream = new FileStream(outputPath, FileMode.Create);
        _writer = new BinaryWriter(_fileStream);
    }

    public void Encode(MP4Box rootBox)
    {
        foreach (var box in rootBox.Children)
        {
            WriteBox(box);
        }
    }

    private void WriteBox(MP4Box box)
    {
        // 写入box大小
        _writer.Write(BitConverter.GetBytes(box.Size).Reverse().ToArray());

        // 写入box类型
        _writer.Write(Encoding.ASCII.GetBytes(box.Type));

        // 写入数据
        if (box.Data != null && box.Data.Length > 0)
        {
            _writer.Write(box.Data);
        }

        // 写入子box
        foreach (var childBox in box.Children)
        {
            WriteBox(childBox);
        }
    }

    public void Dispose()
    {
        _writer?.Dispose();
        _fileStream?.Dispose();
    }
}