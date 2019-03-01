using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Packages
{
    public class PackageInfo
    {
        public int PackageId { get; set; }
        public int BillingType { get; set; }
        public int LicenseType { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string> Extended { get; set; }
        public Dictionary<int, int> AppIds { get; set; }
        public Dictionary<int, int> DepotIds { get; set; }
        public Dictionary<string, string> AppItems { get; set; }
    }
}
