using System.Collections.Generic;
using System.IO;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep1
{
    /// <summary>
    /// Dummy class for Episode 1
    /// </summary>
    public class Ep1GameStats : ICustomGameStats
    {
        public IList<IElement> Children { get; }

        public void ParseFromBuffer(BinaryReader reader)
        {
            // no-op
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            // no-op
        }
    }
}
