using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2SaveGameInfoRecord : LumpElement
    {
        public int FirstDeathIndex;
        public int NumDeaths;

        // Health and player pos from the save file
        public Vector3<short> Position;
        public short SaveHealth;

        public override void Read(BinaryReader reader, byte version)
        {
            FirstDeathIndex = reader.ReadInt32();
            NumDeaths = reader.ReadInt32();
            Position = reader.ReadVector<short>();
            SaveHealth = reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(FirstDeathIndex);
            writer.Write(NumDeaths);
            writer.WriteVector(Position);
            writer.Write(SaveHealth);
        }
    }
}
