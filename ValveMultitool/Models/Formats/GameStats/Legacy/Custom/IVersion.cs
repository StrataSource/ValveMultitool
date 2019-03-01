using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    public interface IVersion : ILumpElement
    {
        byte Version { get; set; }
        byte Magic { get; set; }
    }
}
