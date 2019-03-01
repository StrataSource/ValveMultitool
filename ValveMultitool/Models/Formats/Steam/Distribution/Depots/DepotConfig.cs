using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Depots
{
    public class DepotConfig
    {
        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public string[] OsList { get; set; }
        public int OsArch { get; set; } // 32, 64
        public int OptionalDlc { get; set; } // AppID of the optional DLC
        public string Mod { get; set; } // e.g. tfc
        public string Language { get; set; }
        public bool LowViolence { get; set; }
    }
}
