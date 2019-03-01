using System.IO;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    public abstract class ChunkGameStats : Chunk.Chunk, ICustomGameStats
    {
        public ushort SaveStatsVersion;
        public override void ParseFromBuffer(BinaryReader reader)
        {
            SaveStatsVersion = reader.ReadUInt16();
            base.ParseFromBuffer(reader);
        }

        public override void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(SaveStatsVersion);
            base.SaveToBuffer(writer);
        }
    }
}
