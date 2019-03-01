using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.Steam.Config.InstallConfig
{
    public class InstallConfigStore
    {
        public Dictionary<string, Dictionary<string, InstallConfigSteam>> Software { get; set; }
        public InstallConfigStreaming Streaming { get; set; }
        public Dictionary<string, InstallConfigMusicLibrary> Music { get; set; }
    }
}