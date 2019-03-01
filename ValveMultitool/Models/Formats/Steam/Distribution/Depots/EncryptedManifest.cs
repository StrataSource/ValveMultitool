using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Depots
{
    public class EncryptedManifest
    {
        [KvProperty("encrypted_gid")]
        public string EncryptedGid { get; set; }
        [KvProperty("encrypted_size")]
        public string EncryptedSize { get; set; }

        [KvProperty("encrypted_gid_2")]
        public string EncryptedGid2 { get; set; }
        [KvProperty("encrypted_size_2")]
        public string EncryptedSize2 { get; set; }
    }
}
