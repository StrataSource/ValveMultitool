using System;
using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalGameStats : ChunkGameStats
    {
        private enum PortalGameStatsChunkIds
        {
            LevelStats = 1
        }

        protected override IDictionary<int, Type> ChunkTypeMappings { get; }
            = new Dictionary<int, Type>
            {
                { (int)PortalGameStatsChunkIds.LevelStats, typeof(PortalLevelStats) },
            };
    }
}
