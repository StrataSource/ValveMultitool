using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2RoundStats : LumpElement
    {
        public enum Tf2StatType
        {
            Undefined = 0,
            ShotsHit,
            ShotsFired,
            Kills,
            Deaths,
            Damage,
            Captures,
            Defenses,
            Dominations,
            Revenge,
            PointsScored,
            BuildingsDestroyed,
            Headshots,
            Playtime,
            Healing,
            Invulns,
            KillAssists,
            Backstabs,
            HealthLeached,
            BuildingsBuilt,
            MaxSentryKills,
            Teleports,
            NumTypes
        }

        public int[] RoundStatCounters;
        public override void Read(BinaryReader reader, byte version)
        {
            RoundStatCounters = reader.ReadArray<int>((int) Tf2StatType.NumTypes);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteArray(RoundStatCounters);
        }
    }
}
