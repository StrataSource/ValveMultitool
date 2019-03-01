using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    /// <summary>
    /// Simple "tag" applied to all data in database (e.g., "PLAYTEST")
    /// </summary>
    public class Ep2Tag : LumpElement
    {
        public string TagText;
        public int MapVersion;

        public override void Read(BinaryReader reader, byte version)
        {
            TagText = reader.ReadPaddedString(8);
            MapVersion = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WritePaddedString(TagText, 8);
            writer.Write(MapVersion);
        }
    }
}
