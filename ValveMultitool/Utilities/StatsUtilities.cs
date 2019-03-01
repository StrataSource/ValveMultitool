using System.IO;

namespace ValveMultitool.Utilities
{
    public class StatsUtilities
    {
        public static ushort ReadLumpHeader(BinaryReader reader)
        {
            var pos = reader.BaseStream.Position;
            var id = reader.ReadUInt16();

            // Rewind stream so we can read this again
            reader.BaseStream.Position = pos;
            return id;
        }
    }
}
