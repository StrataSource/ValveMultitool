using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoSysReqsEntry
    {
        // TODO: this class has "extra" values alongside, implement something like NewtonsoftJson's extra values dict
        /// <summary>
        /// Whether this game is supported on this system.
        /// </summary>
        public bool Supported { get; set; }

        /// <summary>
        /// Whether this game wants a "fast" GPU.
        /// </summary>
        [KvProperty("wants_fast_gpu")]
        public bool WantsFastGpu { get; set; }

        /// <summary>
        /// Minimum supported OS version.
        /// </summary>
        [KvProperty("os_min")]
        public string OsMin { get; set; }
    }
}
