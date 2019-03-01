using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2Weapon : LumpElement
    {
        public int ShotsFired;
        public int CritShotsFired;
        public int Hits;
        public int TotalDamage;
        public int HitsWithKnownDistance;
        public long TotalDistance;

        public override void Read(BinaryReader reader, byte version)
        {
            ShotsFired = reader.ReadInt32();
            CritShotsFired = reader.ReadInt32();
            Hits = reader.ReadInt32();
            TotalDamage = reader.ReadInt32();
            HitsWithKnownDistance = reader.ReadInt32();
            TotalDistance = reader.ReadInt64();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(ShotsFired);
            writer.Write(CritShotsFired);
            writer.Write(Hits);
            writer.Write(TotalDamage);
            writer.Write(HitsWithKnownDistance);
            writer.Write(TotalDistance);
        }
    }
}
