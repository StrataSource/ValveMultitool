using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2GameStats : LumpGameStats
    {
        private enum Tf2GameStatsLumpIds
        {
            Version = 1,
            MapHeader,
            MapDeath,
            MapDamage,
            Class,
            Weapon,
            EndTag
        }

        protected override IDictionary<int, Type> LumpTypeMappings { get; }
            = new Dictionary<int, Type>
            {
                { (int)Tf2GameStatsLumpIds.Version, typeof(Tf2Version) },
                { (int)Tf2GameStatsLumpIds.MapHeader, typeof(Tf2LevelHeader) },
                { (int)Tf2GameStatsLumpIds.MapDeath, typeof(Tf2PlayerDeaths) },
                { (int)Tf2GameStatsLumpIds.MapDamage, typeof(Tf2PlayerDamage) },
                { (int)Tf2GameStatsLumpIds.Class, typeof(Tf2ClassStats) },
                { (int)Tf2GameStatsLumpIds.Weapon, typeof(Tf2Weapon) },
                { (int)Tf2GameStatsLumpIds.EndTag, typeof(Tf2Version) }
            };

        protected override Type CustomLevelStatsType => typeof(Tf2LevelStats);

        public override void ParseFromBuffer(BinaryReader reader)
        {
            // This is a custom parser implementation for the TF2 gamestats format
            // TF2 has extra "version" and "end tag" lumps before and after the map ones

            byte version = 0x00;
            byte magic = 0x00;

            string currentMapName = null;
            ILump currentVersion = null;

            // Read lumps until the stream is empty
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var lump = DynamicReadLump(reader, version);
                if (lump == null)
                    continue;

                // Start parsing a new lump if we find a version descriptor
                if (lump.LumpId == (int)Tf2GameStatsLumpIds.Version)
                {
                    if (!(lump.Children.FirstOrDefault() is IVersion versionLump))
                        throw new InvalidOperationException();

                    version = versionLump.Version;
                    magic = versionLump.Magic;

                    // DON'T add this lump to the list until we get a map name
                    currentVersion = lump;
                    continue;
                }

                if (lump.LumpId == (int) Tf2GameStatsLumpIds.MapHeader)
                {
                    if (!(lump.Children.FirstOrDefault() is ILevelHeader header))
                        throw new InvalidOperationException();

                    // We're on to a new lump
                    if (header.MapName != currentMapName)
                    {
                        
                        var map = new Tf2LevelStats
                        {
                            Name = header.MapName
                        };

                        // Add the map
                        currentMapName = header.MapName;
                        Children.Add(map);

                        // Add the version lump before the map lump, so it's at the top
                        if (currentVersion != null)
                            map.Children.Add(currentVersion);
                    }
                }

                // End parsing the lump if we find an end tag
                if (lump.LumpId == (int) Tf2GameStatsLumpIds.EndTag)
                {
                    if (!(lump.Children.FirstOrDefault() is IVersion versionLump))
                        throw new InvalidOperationException();

                    // Sanity checking
                    if (version != versionLump.Version || magic != versionLump.Magic)
                        throw new InvalidOperationException("Header and end tag version and magic values did not match!");

                    currentVersion = null;
                }

                if (currentMapName == null)
                    continue;

                // Add the current lump to the map entity
                var currentMap = Children.FirstOrDefault(m => ((ILevelStats)m).Name == currentMapName);
                currentMap?.Children.Add(lump);

                // Run post-lump reading
                if (currentMap is ICustomLevelStats customMap)
                    customMap.PostReadLumps(reader);
            }
        }
    }
}
