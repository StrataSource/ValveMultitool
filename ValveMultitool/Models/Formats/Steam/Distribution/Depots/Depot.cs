using System;
using System.Collections.Generic;
using System.Text;
using ValveMultitool.Models.Formats.Steam.Distribution.Depots;

namespace ValveMultitool.Models.Formats.Steam
{
    public class Depot
    {
        public int DepotId { get; set; }
        public int PackageId { get; set; }
        public int BillingType { get; set; }
        public int LicenseType { get; set; }
        public int Status { get; set; }
        public Dictionary<string, object> Extended { get; set; }
        public Dictionary<string, object> AppItems { get; set; }
        public int[] AppIds { get; set; }
        public int[] DepotIds { get; set; }

        public string Name { get; set; }
        public Dictionary<string, DepotConfig> Config { get; set; }
        public bool Optional { get; set; }
        public bool SystemDefined { get; set; }
        public Dictionary<string, long> Manifests { get; set; }
        public long MaxSize { get; set; }
        public Dictionary<string, EncryptedManifest> EncryptedManifests { get; set; }
        public int DlcAppId { get; set; }
        public int DepotFromApp { get; set; }
        public int SharedInstall { get; set; }
        public string DecryptionKey { get; set; }
    }
}
