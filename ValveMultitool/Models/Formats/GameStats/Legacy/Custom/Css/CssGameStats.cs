using System;
using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Css
{
    public class CssGameStats : LumpGameStats
    {
        private enum CssGameStatsLumpIds
        {
            Header = 1,
        }

        protected override IDictionary<int, Type> LumpTypeMappings { get; }
            = new Dictionary<int, Type>
            {
                {(int) CssGameStatsLumpIds.Header, typeof(CssLevelHeader)}
            };

        protected override Type CustomLevelStatsType => typeof(CssLevelStats);
    }
}
