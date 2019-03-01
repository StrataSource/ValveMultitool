using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalLeafTimes : LumpElement
    {
        public float TimeSpentInVisLeaf;

        public override void Read(BinaryReader reader, byte version)
        {
            TimeSpentInVisLeaf = reader.ReadSingle();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(TimeSpentInVisLeaf);
        }
    }
}
