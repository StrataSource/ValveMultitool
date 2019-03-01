using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ValveMultitool.Models.Formats;

namespace ValveMultitool.Utilities.Extensions
{
    public static class BinaryWriterExtensions
    {
        private static void WriteNullTerminatedBytes(this BinaryWriter writer, byte[] value)
        {
            writer.Write(value);
            writer.Write((byte)0);
        }

        /// <summary>
        /// Writes a null-terminated string to the stream.
        /// </summary>
        /// <returns>Numbers of bytes written.</returns>
        public static int WriteNullTerminatedString(this BinaryWriter writer, string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;
            var bytes = encoding.GetBytes(str);
            writer.WriteNullTerminatedBytes(bytes);

            // bytelen is length of string + null terminator
            return bytes.Length + 1;
        }

        /// <summary>
        /// Writes a null-terminated string, if string length is
        /// less than the length parameter it will pad with empty bytes.
        /// </summary>
        public static void WritePaddedString(this BinaryWriter writer, string str, int length, Encoding encoding = null)
        {
            var len = WriteNullTerminatedString(writer, str, encoding);
            var padding = length - len;

            // Pad with empty bytes
            writer.Write(new byte[padding]);
        }

        /// <summary>
        /// Writes an array of objects of the specified type.
        /// </summary>
        /// <typeparam name="T">Body type to write.</typeparam>
        /// <param name="writer">Binary writer.</param>
        /// <param name="array">Array containing the objects.</param>
        public static void WriteArray<T>(this BinaryWriter writer, IEnumerable<T> array)
        {
            var mapper = new Dictionary<Type, Action<BinaryWriter, dynamic>>
            {
                { typeof(long), (w, d) => w.Write((long) d) },
                { typeof(int), (w, d) => w.Write((int) d) },
                { typeof(short), (w, d) => w.Write((short) d) },
                { typeof(ulong), (w, d) => w.Write((ulong) d) },
                { typeof(uint), (w, d) => w.Write((uint) d) },
                { typeof(ushort), (w, d) => w.Write((ushort) d) },
                { typeof(float), (w, d) => w.Write((float) d) },
                { typeof(double), (w, d) => w.Write((double) d) },
                { typeof(bool), (w, d) => w.Write((bool) d) },
                { typeof(string), (w, d) => w.WriteNullTerminatedString((string) d) },
            };

            // Write each value to the array
            foreach (var value in array)
                mapper[typeof(T)](writer, value);
        }

        /// <summary>
        /// Writes a set of coordinates as an array to the writer.
        /// </summary>
        public static void WriteVector<T>(this BinaryWriter writer, Vector3<T> vector3)
        {
            var coords = new T[3];
            coords[0] = vector3.X;
            coords[1] = vector3.Y;
            coords[2] = vector3.Z;

            WriteArray(writer, coords);
        }
    }
}
