using System;
using System.Collections.Generic;
using System.IO;
using ValveMultitool.Models.Formats.GameStats.Legacy.Basic;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Css;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep1;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2;
using ValveMultitool.Utilities;

namespace ValveMultitool.Models.Formats.GameStats.Legacy
{
    public class LegacyGameStats
    {
        public enum GameStatsVersion
        {
            Old = 001,
            Old2,
            Old3,
            Old4,
            Old5,
            New
        }

        private const uint StandardNotSaved = 0xDEADBEEF;

        private static readonly IDictionary<string, Type> GameStatTypeMappings = new Dictionary<string, Type>
        {
            { "cstrike", typeof(CssGameStats) },
            { "tf", typeof(Tf2GameStats) },
            //{ "dods", typeof(DodsGameStats) },
            { "portal", typeof(PortalGameStats) },
            { "ep1", typeof(Ep1GameStats) },
            { "ep2", typeof(Ep2GameStats) }
        };

        public GameStatsVersion Version;
        public byte[] UserId;
        public uint Standard;
        public GameStatsBasic Stats;
        public ICustomGameStats CustomStats;
        
        public static LegacyGameStats Parse(byte[] data, string game)
        {
            if (!GameStatTypeMappings.ContainsKey(game))
                throw new InvalidOperationException($"The statistics format \"{game}\" is not supported.");

            // Reflection to instantiate with the type of gamestats here
            var type = GameStatTypeMappings[game];
            var stats = new LegacyGameStats();
            var statsGeneric = typeof(LegacyGameStats).GetMethod(nameof(ParseFromBuffer))?.MakeGenericMethod(type);
            if (statsGeneric == null) return null;

            // Invoke the read and return our stats
            using (var reader = BufferUtilities.CreateReader(data))
                statsGeneric.Invoke(stats, new object[] { reader });
            return stats;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        // shut up resharper, this needs to be public for reflection
        public void ParseFromBuffer<T>(BinaryReader reader) where T : ICustomGameStats, new()
        {
            Version = (GameStatsVersion)reader.ReadInt16();
            UserId = reader.ReadBytes(16);
            //UserId[UserId.Length - 1] = 0;

            Standard = reader.ReadUInt32();
            if (Standard != StandardNotSaved)
            {
                Stats = new GameStatsBasic();

                // Rewind the stream here, because the first element in GameStatsBasic is shared
                // with the binary position of Standard.
                reader.BaseStream.Position = reader.BaseStream.Position - sizeof(uint);
                Stats.ParseFromBuffer(reader, Version);
            }

            // Have we reached the end of the stream yet?
            // If not, there is custom data we should read
            var hasCustomData = reader.BaseStream.Position < reader.BaseStream.Length;
            if (!hasCustomData) return;

            // Instantiate the custom stats
            var stats = (ICustomGameStats)new T();
            stats.ParseFromBuffer(reader);
            CustomStats = stats;
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write((short)Version);
            writer.Write(UserId);

            // Only save stats and custom stats if they exist
            if (Stats != null)
            {
                Stats.SaveToBuffer(writer);
            }
            else
            {
                // Write the "standard not saved" info
                // if regular stats don't exist
                writer.Write(StandardNotSaved);
            }
            
            // Save custom stats
            CustomStats?.SaveToBuffer(writer);
        }
    }
}
