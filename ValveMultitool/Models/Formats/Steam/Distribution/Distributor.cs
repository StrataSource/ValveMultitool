using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution
{
    public class Distributor
    {
        public int Territory { get; set; } // string here, because sometimes quotes are empty
        public int Region { get; set; }
        public string GameCode { get; set; } // string here, because sometimes quotes are empty
        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ' ')]
        public string[] LaunchCountries { get; set; }
        public int ApplyDate { get; set; }
    }
}
