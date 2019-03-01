using System.IO;
using System.Text;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2LevelHeader : LumpElement, ILevelHeader, IMultiPlayLevelHeader
    {
        private enum Tf2LevelCounterTypes
        {
            /// <summary>
            /// Number of rounds played
            /// </summary>
            RoundsPlayed,
            /// <summary>
            /// Total number of seconds of all rounds
            /// </summary>
            TotalTime,
            /// <summary>
            /// Number of blue team wins
            /// </summary>
            BlueWins,
            /// <summary>
            /// Number of red team wins
            /// </summary>
            RedWins,
            /// <summary>
            /// Number of stalemates
            /// </summary>
            StaleMates,
            /// <summary>
            /// Number of blue team wins during sudden death
            /// </summary>
            BlueSuddenDeathWins,
            /// <summary>
            /// Number of read team wins during sudden death
            /// </summary>
            RedSuddenDeathWins,
            NumTypes
        }

        public byte Version { get; set; }
        public string MapName { get; set; }
        public uint IpAddress { get; set; }
        public ushort Port { get; set; }

        public int[] Tf2LevelCounters = new int[(int)Tf2LevelCounterTypes.NumTypes];

        public override void Read(BinaryReader reader, byte version)
        {
            MapName = reader.ReadPaddedString(64);
            IpAddress = reader.ReadUInt32();
            Port = reader.ReadUInt16();
            Tf2LevelCounters = reader.ReadArray<int>((int) Tf2LevelCounterTypes.NumTypes);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Encoding.ASCII.GetBytes(MapName));
            writer.Write(IpAddress);
            writer.Write(Port);
            writer.WriteArray(Tf2LevelCounters);
        }
    }
}
