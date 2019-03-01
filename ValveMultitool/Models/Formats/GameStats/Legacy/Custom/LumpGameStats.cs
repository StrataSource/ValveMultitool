using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for all game statistics
    /// </summary>
    public abstract class LumpGameStats : ICustomGameStats
    {
        /// <summary>
        /// Global gamestats lump IDs,
        /// these are the same across all games
        /// </summary>
        private enum GameStatsLumpIds
        {
            Header = 1
        }

        protected abstract IDictionary<int, Type> LumpTypeMappings { get; }
        protected abstract Type CustomLevelStatsType { get; }

        public IList<IElement> Children { get; } = new List<IElement>();

        protected ILump DynamicReadLump(BinaryReader reader, byte version)
        {
            // Peek the ID
            var id = reader.ReadUInt16();

            // We don't know the format of this lump, so bail
            if (!LumpTypeMappings.ContainsKey(id))
                throw new InvalidOperationException($"Invalid lump ID: {id}");

            // Rewind stream so the lump object can handle this
            reader.BaseStream.Position = reader.BaseStream.Position - sizeof(ushort);

            var type = LumpTypeMappings[id];
            var lump = new Lump.Lump(version);

            // Reflection to instantiate with the type of lump here
            var lumpGeneric = typeof(ILump).GetMethod(nameof(ILump.LoadLump))?.MakeGenericMethod(type);
            if (lumpGeneric == null)
                return null;

            // Invoke the read
            lumpGeneric.Invoke(lump, new object[] { reader });
            return lump;
        }

        public virtual void ParseFromBuffer(BinaryReader reader)
        {
            string currentMapName = null;
            byte version = 0x00;

            // Read lumps until the stream is empty
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                var lump = DynamicReadLump(reader, version);
                if (lump == null)
                    continue;

                // Start parsing a new lump if we find a header
                if (lump.LumpId == (int)GameStatsLumpIds.Header)
                {
                    if (!(lump.Children.FirstOrDefault() is ILevelHeader header))
                        throw new InvalidOperationException("First element in the header lump was not the header!");

                    // We're on to a new lump
                    if (header.MapName != currentMapName)
                    {
                        // Set current and next lump versions
                        version = header.Version;
                        lump.LumpVersion = header.Version;

                        // Instantiate the custom map type
                        var levelStats = (ILevelStats) Activator.CreateInstance(CustomLevelStatsType);
                        if (levelStats is INamedElement namedElement)
                            namedElement.Name = header.MapName;

                        // Add the map
                        currentMapName = header.MapName;
                        Children.Add(levelStats);
                    }
                }

                // Add the current lump to the map entity
                var currentMap = Children.FirstOrDefault(m => ((ILevelStats) m).Name == currentMapName);
                currentMap?.Children.Add(lump);

                // Run post-lump reading
                if (currentMap is ICustomLevelStats customMap)
                    customMap.PostReadLumps(reader);
            }
        }

        public virtual void SaveToBuffer(BinaryWriter writer)
        {
            foreach (var map in Children)
            {
                var mapLump = (ILevelStats) map;
                foreach (var element in mapLump.Children)
                {
                    // Save each lump to the stream
                    var lump = element as ILump;
                    lump?.SaveLump(writer);
                }

                // Run post-lump writing
                if (map is ICustomLevelStats customMap)
                    customMap.PostWriteLumps(writer);
            }
        }
    }
}
