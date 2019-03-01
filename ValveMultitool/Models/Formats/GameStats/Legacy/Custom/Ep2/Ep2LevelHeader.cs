using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2LevelHeader : LumpElement, ILevelHeader
    {
        public byte Version { get; set; }
        public string MapName { get; set; }

        /// <summary>
        /// Time spent in level.
        /// </summary>
        public float Time;

        public override void Read(BinaryReader reader, byte version)
        {
            Version = reader.ReadByte();
            MapName = reader.ReadPaddedString(64);
            Time = reader.ReadSingle();

            // TODO: why are there three random null bytes here??
            var testchars = reader.ReadChars(3);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Version);
            writer.WritePaddedString(MapName, 64);
            writer.Write(Time);

            // write empty bytes lol
            writer.Write(new byte[3]);
        }
    }
}
