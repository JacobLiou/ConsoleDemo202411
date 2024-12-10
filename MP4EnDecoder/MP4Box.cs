namespace MP4EnDecoder;

using System;

public class MP4Box
{
    public uint Size { get; set; }
    public string Type { get; set; }
    public byte[] Data { get; set; }
    public List<MP4Box> Children { get; set; }

    public MP4Box()
    {
        Children = new List<MP4Box>();
    }
}

// Models/MP4Atom.cs
public class MP4Atom : MP4Box
{
    public const string FTYP = "ftyp"; // 文件类型
    public const string MOOV = "moov"; // 影片信息
    public const string MDAT = "mdat"; // 媒体数据
    public const string FREE = "free"; // 空闲空间
    public const string MVHD = "mvhd"; // 影片头
    public const string TRAK = "trak"; // 轨道
    public const string MDIA = "mdia"; // 媒体
    public const string MINF = "minf"; // 媒体信息
    public const string STBL = "stbl"; // 采样表
    public const string STSD = "stsd"; // 采样描述
}

