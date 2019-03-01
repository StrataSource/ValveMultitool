using System.IO;
using ValveKeyValue;
using ValveMultitool.Utilities;

namespace ValveMultitool.Models.Formats.Vdf.Binary
{
    public abstract class BinaryVdfHeader<T> : IBinaryParseable
    {
        public T Body { get; set; }
        public virtual void ParseFromBuffer(BinaryReader reader) => Body = KvSerializer.Create(KvSerializationFormat.KeyValues1Binary).Deserialize<T>(reader.BaseStream);
        public virtual void SaveToBuffer(BinaryWriter writer) => KvSerializer.Create(KvSerializationFormat.KeyValues1Binary).Serialize(writer.BaseStream, Body, nameof(T));
    }
}
