using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps
{
    public class AppInfoConfigControllerDetails
    {
        [KvProperty("controller_type")]
        public string ControllerType { get; set; }
        [KvProperty("enabled_branches", CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public string[] EnabledBranches { get; set; }
    }
}
