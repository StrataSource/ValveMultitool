using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2Version : LumpElement, IVersion
    {
        public byte Version { get; set; }
        public byte Magic { get; set; }

        public override void Read(BinaryReader reader, byte version)
        {
            Version = (byte) reader.ReadInt32();
            Magic = (byte) reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write((int)Version);
            writer.Write((int)Magic);
        }
    }
}
