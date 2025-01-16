using System;
using System.Collections.Generic;
using System.Text;

namespace SharedObjects
{
    public class BigEndianBitConverter
    {
        public static byte[] GetBytes(ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] GetBytes(uint value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static byte[] GetBytes(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }

        public static ulong ToUInt64(byte[] bytes, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, 8);
            }
            return BitConverter.ToUInt64(bytes, startIndex);
        }

        public static uint ToUInt32(byte[] bytes, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, 4);
            }
            return BitConverter.ToUInt32(bytes, startIndex);
        }

        public static ushort ToUInt16(byte[] bytes, int startIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, startIndex, 2);
            }
            return BitConverter.ToUInt16(bytes, startIndex);
        }

        public static string ToString(byte[] _object)
        {
            return BitConverter.ToString(_object);
        }
    }
}
