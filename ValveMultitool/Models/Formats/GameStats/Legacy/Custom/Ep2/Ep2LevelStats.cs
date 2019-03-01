using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2LevelStats : ILevelStats
    {
        public ILevelHeader Header { get; set; }
        public IList<IElement> Children { get; } = new List<IElement>();
        public string Name { get; set; }
    }
}
