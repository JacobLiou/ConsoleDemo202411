using System.Text;

namespace MP4EnDecoder;

// Core/MP4Decoder.cs
public class MP4Decoder : IDisposable
{
    private readonly string _filePath;
    private readonly FileStream _fileStream;
    private readonly BinaryReader _reader;

    public MP4Decoder(string filePath)
    {
        _filePath = filePath;
        _fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        _reader = new BinaryReader(_fileStream);
    }

    public MP4Box Decode()
    {
        var rootBox = new MP4Box();
        while (_fileStream.Position < _fileStream.Length)
        {
            var box = ReadBox();
            if (box != null)
            {
                rootBox.Children.Add(box);
            }
        }
        return rootBox;
    }

    private MP4Box ReadBox()
    {
        if (_fileStream.Position >= _fileStream.Length)
            return null;

        var box = new MP4Box();

        // 读取box大小
        box.Size = _reader.ReadUInt32BigEndian();

        // 读取box类型
        byte[] typeBytes = _reader.ReadBytes(4);
        box.Type = Encoding.ASCII.GetString(typeBytes);

        // 读取数据
        long dataSize = box.Size - 8; // 减去size和type的长度
        if (dataSize > 0)
        {
            box.Data = _reader.ReadBytes((int)dataSize);
        }

        // 解析子box
        if (IsContainer(box.Type))
        {
            ParseChildren(box);
        }

        return box;
    }

    private bool IsContainer(string boxType)
    {
        return boxType == MP4Atom.MOOV ||
               boxType == MP4Atom.TRAK ||
               boxType == MP4Atom.MDIA ||
               boxType == MP4Atom.MINF ||
               boxType == MP4Atom.STBL;
    }

    private void ParseChildren(MP4Box parent)
    {
        using (var memStream = new MemoryStream(parent.Data))
        using (var reader = new BinaryReader(memStream))
        {
            while (memStream.Position < memStream.Length)
            {
                var childBox = ReadBoxFromStream(reader);
                if (childBox != null)
                {
                    parent.Children.Add(childBox);
                }
            }
        }
    }

    private MP4Box ReadBoxFromStream(BinaryReader reader)
    {
        // Box 头部最小长度 (4 字节大小 + 4 字节类型)
        const int headerSize = 8;
        byte[] header = new byte[headerSize];

        // 读取头部
        int bytesRead = reader.Read(header, 0, headerSize);
        if (bytesRead < headerSize)
            throw new EndOfStreamException("无法读取完整的 Box 头部");

        // 解析头部：前 4 字节为 Box 大小，后 4 字节为 Box 类型
        uint size = BitConverter.ToUInt32(header, 0); // 注意：MP4 使用大端字节序
        if (BitConverter.IsLittleEndian)
        {
            size = size.ReverseBytes();
        }

        string type = Encoding.ASCII.GetString(header, 4, 4);

        // 如果 size 小于 headerSize，说明数据不完整或文件损坏
        if (size < headerSize)
            throw new InvalidDataException("Box 大小非法");

        // 计算数据部分大小
        int dataSize = (int)size - headerSize;

        // 读取数据部分
        byte[] data = new byte[dataSize];
        bytesRead = reader.Read(data, 0, dataSize);
        if (bytesRead < dataSize)
            throw new EndOfStreamException("无法读取完整的 Box 数据");

        // 返回解析后的 Box
        return new MP4Box
        {
            Size = size,
            Type = type,
            Data = data
        };
    }



    public void Dispose()
    {
        _reader?.Dispose();
        _fileStream?.Dispose();
    }
}
