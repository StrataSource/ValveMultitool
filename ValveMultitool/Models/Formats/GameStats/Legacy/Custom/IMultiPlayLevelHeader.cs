namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    public interface IMultiPlayLevelHeader
    {
        uint IpAddress { get; set; }
        ushort Port { get; set; }
    }
}
