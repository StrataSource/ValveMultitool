using System;
using System.IO;
using System.Net;

namespace ValveMultitool.Utilities
{
    public static class BufferUtilities
    {
        public static uint ToUInt32(this IPAddress ip)
        {
            var ipBytes = ip.GetAddressBytes();
            return BitConverter.ToUInt32(ipBytes, 0);
        }

        public static MemoryStream CreateStream(byte[] data, int offset = 0)
        {
            var stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = offset;
            return stream;
        }

        public static BinaryReader CreateReader(byte[] data, int offset = 0)
        {
            var stream = CreateStream(data, offset);
            var reader = new BinaryReader(stream);
            return reader;
        }
    }
}
