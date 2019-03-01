using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoUfsFile
    {
        public string Root { get; set; }
        public string Path { get; set; }
        public string Pattern { get; set; }
        public bool Recursive { get; set; }
        public Dictionary<int, string> Platforms { get; set; } // TODO: can be List<string>? also platform enum
    }
}
