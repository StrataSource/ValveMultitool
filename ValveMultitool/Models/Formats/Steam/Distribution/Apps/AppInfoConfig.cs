using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoConfig
    {
        public string UseMms { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public int ContentType { get; set; } // todo: enum for this
        public Dictionary<string, AppInfoLaunchEntry> Launch { get; set; } // todo: half-life

        public string InstallScriptSignature { get; set; }
        public bool InstallScriptOverride { get; set; }

        public bool LaunchWithoutWorkshopUpdates { get; set; }

        /// <summary>
        /// Dictionary of language to URL mappings for external registration after launch.
        /// </summary>
        public Dictionary<string, string> ExternalRegistrationUrl { get; set; }

        /// <summary>
        /// Installation directory under steamapps/common.
        /// </summary>
        public string InstallDir { get; set; }
        public string ConvertGcfs { get; set; } // really an int[]
        public bool SteamVrSupport { get; set; }
        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public int[] SteamControllerConfigFileIds { get; set; }
        public int SteamControllerTemplateIndex { get; set; }
        public Dictionary<int, AppInfoConfigControllerDetails> SteamControllerConfigDetails { get; set; }

        /// <summary>
        /// Public key for Custom Executable Generation
        /// </summary>
        public string CegPublicKey { get; set; }
        public string CheckGuid { get; set; } // name of an .exe or .dll?
        public bool SystemProfile { get; set; }

        /// <summary>
        /// Runs as this AppID instead of the app's default one.
        /// </summary>
        public int RunAsAppId { get; set; }
    }
}
