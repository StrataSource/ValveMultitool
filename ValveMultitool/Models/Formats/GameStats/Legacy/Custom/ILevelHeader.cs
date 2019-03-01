using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    /// <summary>
    /// Header which contains all information about a level element.
    /// </summary>
    public interface ILevelHeader : ILumpElement
    {
        /// <summary>
        /// Version of the game stats file.
        /// </summary>
        byte Version { get; set; }

        /// <summary>
        /// Name of the map.
        /// </summary>
        string MapName { get; set; }
    }
}
