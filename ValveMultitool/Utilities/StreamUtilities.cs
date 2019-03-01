using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Text;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Utilities
{
    public static class StreamUtilities
    {
        public static byte[] Transact(this NamedPipeClientStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
            stream.WaitForPipeDrain();

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                return reader.ReadNullTerminatedBytes();
        }

        public static byte[] ReadToEnd(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
