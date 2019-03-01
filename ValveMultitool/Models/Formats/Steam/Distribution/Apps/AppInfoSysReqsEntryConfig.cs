using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoSysReqsEntryConfig
    {
        public string OsList { get; set; }
        public int OsArch { get; set; } // 32, 64
        public string BetaKey { get; set; }

        /// <summary>
        /// AppID of a DLC to require before this will run.
        /// </summary>
        public int OwnsDlc { get; set; }
    }
}
