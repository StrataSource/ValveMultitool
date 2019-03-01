using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2LevelStats : ILevelStats
    {
        public ILevelHeader Header { get; set; }
        public IList<IElement> Children { get; }
        public string Name { get; set; }
    }
}
