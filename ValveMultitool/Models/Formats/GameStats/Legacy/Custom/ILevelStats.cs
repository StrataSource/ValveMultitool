namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    /// <summary>
    /// Holds game statistics for a single level.
    /// </summary>
    public interface ILevelStats : INamedElement
    {
        ILevelHeader Header { get; set; }
    }
}
