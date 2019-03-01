using System.Collections.Generic;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfo
    {
        public int AppId { get; set; }
        [KvProperty("public_only")]
        public bool PublicOnly { get; set; }
        public AppInfoCommon Common { get; set; }
        public Dictionary<string, object> Extended { get; set; }
        public AppInfoConfig Config { get; set; }
        public object Install { get; set; }
        public Dictionary<string, object> Depots { get; set; } // TODO: WHY did you have to break convention on this one Valve?? add [ExtraProperty("overridescddb")]
        public AppInfoUfs Ufs { get; set; }
        public Dictionary<string, AppInfoSysReqsEntry> SysReqs { get; set; }
        public AppInfoLocalization Localization { get; set; }
    }
}