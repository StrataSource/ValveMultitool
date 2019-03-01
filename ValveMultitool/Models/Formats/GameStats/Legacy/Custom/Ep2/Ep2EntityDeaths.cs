using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2EntityDeaths : LumpElement
    {
        public enum FloatCounterTypes
        {
            DamageTaken,
            NumTypes
        }

        public enum IntCounterTypes
        {
            CratesSmashed,
            ObjectsPunted,
            VehicularHomicides,
            DistanceInVehicle,
            DistanceOnFoot,
            DistanceOnFootSprinting,
            FallingDeaths,
            VehicleOverturned,
            LoadGameStillAlive,
            Loads,
            Saves,
            GodModes,
            NoClips,
            NumTypes
        }

        // Counters
        public float[] FloatCounters;
        public ulong[] IntCounters;

        private static readonly LumpElementHeaderInfo HeaderInfo = new LumpElementHeaderInfo
        {
            Type = LumpElementHeaderInfo.HeaderNameType.String
        };

        public override void Read(BinaryReader reader, byte version)
        {
            IntCounters = reader.ReadArray<ulong>((int)IntCounterTypes.NumTypes);
            FloatCounters = reader.ReadArray<float>((int)FloatCounterTypes.NumTypes);
            ReadChildren<Ep2EntityDeathsRecord>(reader, version, HeaderInfo);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteArray(IntCounters);
            writer.WriteArray(FloatCounters);
            WriteChildren(writer, version, HeaderInfo);
        }
    }
}
