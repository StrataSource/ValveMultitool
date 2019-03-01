using System;
using System.Collections.Generic;
using System.IO;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalLevelStats : Chunk.Chunk, INamedElement
    {
        public string Name { get; set; }

        private enum PortalLevelStatsChunkIds
        {
            PlayerDeaths = 1,
            PortalPlacement,
            PlayerUse,
            StuckEvent,
            JumpEvent,
            LeafTimes
        }

        protected override IDictionary<int, Type> ChunkTypeMappings { get; }
            = new Dictionary<int, Type>
            {
                { (int)PortalLevelStatsChunkIds.PlayerDeaths, typeof(PortalPlayerDeaths) },
                { (int)PortalLevelStatsChunkIds.PortalPlacement, typeof(PortalPortalPlacement) },
                { (int)PortalLevelStatsChunkIds.PlayerUse, typeof(PortalPlayerUse) },
                { (int)PortalLevelStatsChunkIds.StuckEvent, typeof(PortalStuckEvent) },
                { (int)PortalLevelStatsChunkIds.JumpEvent, typeof(PortalJumpEvent) },
                { (int)PortalLevelStatsChunkIds.LeafTimes, typeof(PortalLeafTimes) }
            };

        public override void ParseFromBuffer(BinaryReader reader)
        {
            ChunkId = reader.ReadInt16();
            ChunkSize = reader.ReadUInt32();
            Name = reader.ReadNullTerminatedString();
            ReadSubChunks(reader);
        }

        public override void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(ChunkId);
            writer.Write(ChunkSize);
            writer.WriteNullTerminatedString(Name);
            WriteSubChunks(writer);
        }
    }
}
