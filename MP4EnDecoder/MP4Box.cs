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
    public const string FTYP = "ftyp"; // �ļ�����
    public const string MOOV = "moov"; // ӰƬ��Ϣ
    public const string MDAT = "mdat"; // ý������
    public const string FREE = "free"; // ���пռ�
    public const string MVHD = "mvhd"; // ӰƬͷ
    public const string TRAK = "trak"; // ���
    public const string MDIA = "mdia"; // ý��
    public const string MINF = "minf"; // ý����Ϣ
    public const string STBL = "stbl"; // ������
    public const string STSD = "stsd"; // ��������
}

