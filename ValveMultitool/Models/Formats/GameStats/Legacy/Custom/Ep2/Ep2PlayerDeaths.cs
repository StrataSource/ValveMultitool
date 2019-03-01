using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2PlayerDeaths : LumpElement
    {
        /// <summary>
        /// Position of death.
        /// </summary>
        public Vector3<short> Position;

        public override void Read(BinaryReader reader, byte version)
        {
            Position = reader.ReadVector<short>();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(Position);
        }
    }
}
