using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP4EnDecoder
{
    // Utils/BinaryReaderExtensions.cs
    public static class BinaryReaderExtensions
    {
        public static uint ReadUInt32BigEndian(this BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// 反转字节顺序（用于将小端转换为大端）。
        /// </summary>
        public static uint ReverseBytes(this uint value)
        {
            return (value >> 24) |
                   ((value >> 8) & 0x0000FF00) |
                   ((value << 8) & 0x00FF0000) |
                   (value << 24);
        }
    }
}
