using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2EntityDeathsRecord : LumpElement, INamedElement
    {
        /// <summary>
        /// Number killed by player
        /// </summary>
        public uint BodyCount;

        /// <summary>
        /// Number of times entity killed player
        /// </summary>
        public uint KilledPlayer;

        public string Name { get; set; }

        public override void Read(BinaryReader reader, byte version)
        {
            BodyCount = reader.ReadUInt32();
            KilledPlayer = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(BodyCount);
            writer.Write(KilledPlayer);
        }
    }
}
