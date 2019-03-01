using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2ClassStats : LumpElement
    {
        private enum Tf2ClassStatsCounterTypes
        {
            Spawns,
            TotalTime,
            Score,
            Kills,
            Deaths,
            Assists,
            Captures,
            NumTypes
        }

        public int[] Tf2ClassStatsCounters;

        public override void Read(BinaryReader reader, byte version)
        {
            Tf2ClassStatsCounters = reader.ReadArray<int>((int)Tf2ClassStatsCounterTypes.NumTypes);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteArray(Tf2ClassStatsCounters);
        }
    }
}
