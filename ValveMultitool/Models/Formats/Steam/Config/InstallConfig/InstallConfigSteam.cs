using System.Collections.Generic;
using ValveKeyValue.Attributes;
using ValveMultitool.Models.Formats.Steam.Network;

namespace ValveMultitool.Models.Formats.Steam.Config.InstallConfig
{
    // subkey Steam in InstallConfigStore
    public class InstallConfigSteam
    {
        public bool AutoUpdateWindowEnabled { get; set; }
        public ShaderCacheManager ShaderCacheManager { get; set; }
        [KvProperty("CMWebSocket")]
        public Dictionary<string, CmWebSocket> CmWebSocket { get; set; }
        public Dictionary<string, Account> Accounts { get; set; }

        /// <summary>
        /// Overrides the Steam CM Cell ID.
        /// A Cell ID is a unique identifier which is used to group
        /// content servers based on their location throughout the world.
        /// </summary>
        public int CellIDServerOverride { get; set; }
        
        /// <summary>
        /// Mean Time Between Failure of the Steam client.
        /// </summary>
        public uint Mtbf { get; set; }
        [KvProperty("Cip")]
        public string Cip { get; set; } // really a byte[]

        /// <summary>
        /// Comma-seperated list of Steam CM (connection manager) servers.
        /// </summary>
        [KvProperty("CM")]
        public string ConnectionManagers { get; set; }
        public uint PercentDefaultWebSockets { get; set; }

        /// <summary>
        /// Steam Guard sentry file location.
        /// </summary>
        public string SentryFile { get; set; }
        public Dictionary<string, string> ConnectCache { get; set; } // value is really a byte[]

        /// <summary>
        /// Comma-separated list of Steam Content (depot) servers.
        /// </summary>
        [KvProperty("CS")]
        public string ContentServers { get; set; }
        public uint Rate { get; set; }
        public bool NoSavePersonalInfo { get; set; }
        public uint MaxServerBrowserPingsPerMin { get; set; }
        public uint DownloadThrottleKbps { get; set; }
        public bool AllowDownloadsDuringGameplay { get; set; }
        public bool StreamingThrottleEnabled { get; set; }
        public bool ClientBrowserAuth { get; set; }
        [KvProperty("CurrentCellID")]
        public uint CurrentCellId { get; set; }
        [KvProperty("TimeCellIDSet")]
        public uint TimeCellIdSet { get; set; }
        [KvProperty("PingTimeForCurrentCellID")]
        public uint PingTimeForCurrentCellId { get; set; }
        [KvProperty("depots")]
        public Dictionary<string, Depot> Depots { get; set; }
        [KvProperty("LastConfigstoreUploadTime")]
        public uint LastConfigStoreUploadTime { get; set; }
        public uint RecentDownloadRate { get; set; }
        [KvProperty("NCTF")]
        public uint Nctf { get; set; }
        public ulong SurveyDateVersion { get; set; }
        public string SurveyDate { get; set; }
        public uint SurveyDateType { get; set; }
    }
}
