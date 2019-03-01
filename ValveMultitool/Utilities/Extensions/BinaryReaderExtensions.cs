using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ValveMultitool.Models.Formats;

namespace ValveMultitool.Utilities.Extensions
{
    public static class BinaryReaderExtensions
    {
        /// <summary>
        /// Reads the corruption identifier from the reader,
        /// and throws an exception if it is invalid.
        /// </summary>
        public static void ReadThrowCorrupted(this BinaryReader reader)
        {
            // Corruption identifier
            var corrupt = reader.ReadByte();

            if (corrupt != 0x01)
                throw new InvalidDataException("Buffer is corrupted. Decryption key may be invalid.");
        }

        /// <summary>
        /// Reads an array of null-terminated bytes from the stream.
        /// </summary>
        public static byte[] ReadNullTerminatedBytes(this BinaryReader reader)
        {
            using (var mem = new MemoryStream())
            {
                byte nextByte;
                while ((nextByte = reader.ReadByte()) != 0)
                    mem.WriteByte(nextByte);
                return mem.ToArray();
            }
        }

        /// <summary>
        /// Reads a null-terminated string from the stream.
        /// </summary>
        public static string ReadNullTerminatedString(this BinaryReader reader, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;
            var bytes = ReadNullTerminatedBytes(reader);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Reads a null-terminated string with a specified number
        /// of padding space that the string occupies.
        /// </summary>
        public static string ReadPaddedString(this BinaryReader reader, int length = 0, Encoding encoding = null)
        {
            // Read the string
            var str = ReadNullTerminatedString(reader, encoding);

            // Increment the stream by the length of the read string
            var len = length - (str.Length + 1);
            reader.ReadBytes(len);

            return str;
        }

        public static long BigEndianReadInt64(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(long));
            Array.Reverse(data);

            return BitConverter.ToInt64(data, 0);
        }

        public static int BigEndianReadInt32(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(int));
            Array.Reverse(data);

            return BitConverter.ToInt32(data, 0);
        }

        public static short BigEndianReadInt16(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(short));
            Array.Reverse(data);

            return BitConverter.ToInt16(data, 0);
        }

        public static ulong BigEndianReadUInt64(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(ulong));
            Array.Reverse(data);

            return BitConverter.ToUInt64(data, 0);
        }

        public static uint BigEndianReadUInt32(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(uint));
            Array.Reverse(data);

            return BitConverter.ToUInt32(data, 0);
        }

        public static ushort BigEndianReadUInt16(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(ushort));
            Array.Reverse(data);

            return BitConverter.ToUInt16(data, 0);
        }

        public static float BigEndianReadSingle(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(float));
            Array.Reverse(data);

            return BitConverter.ToSingle(data, 0);
        }

        public static double BigEndianReadDouble(this BinaryReader reader)
        {
            var data = reader.ReadBytes(sizeof(double));
            Array.Reverse(data);

            return BitConverter.ToDouble(data, 0);
        }

        /// <summary>
        /// Reads an array of objects of the specified type.
        /// </summary>
        /// <typeparam name="T">Body type to read.</typeparam>
        /// <param name="reader">Binary reader.</param>
        /// <param name="count">Number of objects to read.</param>
        public static T[] ReadArray<T>(this BinaryReader reader, int count)
        {
            var array = new T[count];
            var mapper = new Dictionary<Type, Func<BinaryReader, dynamic>>
            {
                { typeof(long), r => r.ReadInt64() },
                { typeof(int), r => r.ReadInt32() },
                { typeof(short), r => r.ReadInt16() },
                { typeof(ulong), r => r.ReadUInt64() },
                { typeof(uint), r => r.ReadInt32() },
                { typeof(ushort), r => r.ReadInt16() },
                { typeof(float), r => r.ReadSingle() },
                { typeof(double), r => r.ReadDouble() },
                { typeof(bool), r => r.ReadBoolean() },
                { typeof(string), r => r.ReadNullTerminatedString() },
            };

            // Iterate through and add to the array.
            for (var i = 0; i < count; i++)
            {
                var val = mapper[typeof(T)](reader);
                array[i] = (T)val;
            }

            return array;
        }

        /// <summary>
        /// Reads a set of coordinates as an array from the reader.
        /// </summary>
        public static Vector3<T> ReadVector<T>(this BinaryReader reader)
        {
            // float type is equivalent to vec_t in C (Vector and QAngle)
            // short type is equivalent to short[3] in C
            return new Vector3<T>(ReadArray<T>(reader, 3));
        }
    }
}
