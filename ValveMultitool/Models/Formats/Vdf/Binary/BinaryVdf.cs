using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Steamworks;
using ValveKeyValue;
using ValveMultitool.Common;
using ValveMultitool.Utilities;

namespace ValveMultitool.Models.Formats.Vdf.Binary
{
    public class BinaryVdf : List<object>, IBinaryParseable
    {
        private static readonly IDictionary<Type, uint> SupportedVersionMappings = new Dictionary<Type, uint>
        {
            { typeof(BinaryVdfAppHeader), 0x07564427 },
            { typeof(BinaryVdfPackageHeader), 0x06565527 }
        };

        private Type _headerType;

        public uint Version { get; private set; }
        public EUniverse UniverseType { get; private set; } = EUniverse.k_EUniversePublic;

        public static BinaryVdf Parse(byte[] data, Type headerType = null)
        {
            if (headerType != null && !SupportedVersionMappings.ContainsKey(headerType))
                throw new ArgumentException($"Unsupported header type: {headerType}", nameof(headerType));

            var vdf = new BinaryVdf {_headerType = headerType};
            using (var reader = BufferUtilities.CreateReader(data))
                vdf.ParseFromBuffer(reader);
            return vdf;
        }

        public static Type ResolveHeaderType(string path)
        {
            // Use special header behaviour
            // for appinfo and packageinfo
            switch (Path.GetFileNameWithoutExtension(path))
            {
                case "appinfo":
                    return typeof(BinaryVdfAppHeader);
                case "packageinfo":
                    return typeof(BinaryVdfPackageHeader);
                default:
                    return null;
            }
        }

        public void ParseFromBuffer(BinaryReader reader)
        {
            if (_headerType != null)
            {
                // Version and universe are not present
                // on pure binary VDF files e.g coplay, so only read them here
                Version = reader.ReadUInt32();
                UniverseType = (EUniverse)reader.ReadUInt32();

                // Ensure there is a supported version for the type
                if (!SupportedVersionMappings.Select(t => t.Key == _headerType && t.Value == Version).Any())
                    throw new InvalidOperationException($"Unsupported binary VDF version: {Version}");

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    // If we have a terminator with 0 (for appinfo.vdf)
                    // or -1 (for packageinfo.vdf), no more sections to read
                    var terminator = reader.ReadUInt32();
                    if (_headerType == typeof(BinaryVdfAppHeader) 
                        && terminator == BinaryVdfAppHeader.Terminator)
                        break;
                    if (_headerType == typeof(BinaryVdfPackageHeader) 
                        && terminator == BinaryVdfPackageHeader.Terminator)
                        break;

                    // Rewind stream after peek
                    reader.BaseStream.Position = reader.BaseStream.Position - sizeof(uint);

                    // Create the node instance
                    var node = (IBinaryParseable)Activator.CreateInstance(_headerType);
                    node.ParseFromBuffer(reader);
                    Add(node);
                }
            }
            else
            {
                // No header type? Skip this entirely and parse raw keyvalues1.
                Add(KvSerializer.Create(KvSerializationFormat.KeyValues1Binary).Deserialize(new NonClosingStream(reader.BaseStream)));
            }
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            // Write header types if we have a header
            if (_headerType != null)
            {
                writer.Write(Version);
                writer.Write((uint)UniverseType);
            }

            foreach (var child in this)
            {
                switch (child)
                {
                    case IBinaryParseable parseable:
                        parseable.SaveToBuffer(writer);
                        break;
                    case KvObject obj:
                        KvSerializer.Create(KvSerializationFormat.KeyValues1Binary).Serialize(new NonClosingStream(writer.BaseStream), obj);
                        break;
                }
            }

            // Write terminator if we have a header
            if (_headerType == typeof(BinaryVdfAppHeader))
                writer.Write(BinaryVdfAppHeader.Terminator);
            if (_headerType == typeof(BinaryVdfPackageHeader))
                writer.Write(BinaryVdfPackageHeader.Terminator);
        }
    }
}
