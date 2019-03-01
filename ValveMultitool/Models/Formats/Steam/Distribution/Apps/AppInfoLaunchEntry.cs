using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoLaunchEntry
    {
        /// <summary>
        /// Path to the app executable within the app's root directory
        /// </summary>
        public string Executable { get; set; }

        /// <summary>
        /// Arguments to run the executable with
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public string Type { get; set; } // none

        /// <summary>
        /// Path to the VAC2 platform specific library.
        /// </summary>
        public string VacModuleFileName { get; set; }

        /// <summary>
        /// Extended configuration.
        /// </summary>
        public AppInfoSysReqsEntryConfig Config { get; set; }
    }
}
