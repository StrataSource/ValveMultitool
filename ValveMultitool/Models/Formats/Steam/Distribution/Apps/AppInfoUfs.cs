using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoUfs
    {
        public bool TestOnly { get; set; }
        public ulong Quota { get; set; }       // 100000000
        public ulong MaxNumFiles { get; set; } // 10
        public bool HideCloudUi { get; set; }
        public bool IgnoreExternalFiles { get; set; }
        public Dictionary<int, AppInfoUfsFile> SaveFiles { get; set; }
    }
}
