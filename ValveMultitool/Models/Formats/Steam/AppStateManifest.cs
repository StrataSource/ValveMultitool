using System.Collections.Generic;
using Steamworks;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam
{
    public class AppStateManifest
    {
        // keyvalues1 steamapps/appmanifest_[appid].acf
        [KvProperty("appid")]
        public uint AppId { get; set; }
        public EUniverse Universe { get; set; }
        [KvProperty("name")]
        public string Name { get; set; }
        public uint StateFlags { get; set; }
        [KvProperty("installdir")]
        public string InstallDir { get; set; }
        public uint LastUpdated { get; set; }
        public uint UpdateResult { get; set; }
        public uint SizeOnDisk { get; set; }
        [KvProperty("buildid")]
        public uint BuildId { get; set; }
        public uint LastOwner { get; set; }
        public uint BytesToDownload { get; set; }
        public uint BytesDownloaded { get; set; }
        public uint AutoUpdateBehavior { get; set; }
        public bool AllowOtherDownloadsWhileRunning { get; set; }
        public bool ScheduledAutoUpdate { get; set; }
        public IEnumerable<object> UserConfig { get; set; }
        public IEnumerable<object> InstalledDepots { get; set; }
        public IEnumerable<object> MountedDepots { get; set; }
        public IEnumerable<object> InstallScripts { get; set; }
    }
}
