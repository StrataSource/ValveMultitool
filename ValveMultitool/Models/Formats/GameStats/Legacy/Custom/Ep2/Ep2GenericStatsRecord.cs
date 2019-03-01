using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2GenericStatsRecord : LumpElement, INamedElement
    {
        public string Name { get; set; }
        public Vector3<short> Position;
        public short Unknown1;
        public uint Count;
        public uint Unknown2;
        public double CurrentValue;

        public override void Read(BinaryReader reader, byte version)
        {
            Position = reader.ReadVector<short>();
            Unknown1 = reader.ReadInt16();
            Count = reader.ReadUInt32();
            Unknown2 = reader.ReadUInt32();
            CurrentValue = reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(Position);
            writer.Write(Unknown1);
            writer.Write(Count);
            writer.Write(Unknown2);
            writer.Write(CurrentValue);
        }
    }
}
