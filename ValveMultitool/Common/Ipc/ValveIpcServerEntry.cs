using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Common.Ipc
{
    public class ValveIpcServerEntry
    {
        public string Name;
        public Guid Guid;

        public static ValveIpcServerEntry Parse(Stream stream)
        {
            var reader = new BinaryReader(stream);
            var entry = new ValveIpcServerEntry
            {
                Name = reader.ReadNullTerminatedString(),
                Guid = new Guid(reader.ReadBytes(16))

            };

            // Null name or UUID means terminator
            if (string.IsNullOrWhiteSpace(entry.Name) || 
                entry.Guid.Equals(new Guid()))
                return null;

            return entry;
        }
    }
}
