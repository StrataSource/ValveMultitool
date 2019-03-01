using System.Collections.Generic;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam
{
    public class AppWorkShopManifest
    {
        // keyvalues1 steamapps/workshop/appworkshop_[appid].acf
        [KvProperty("appid")]
        public uint AppId { get; set; }
        public uint SizeOnDisk { get; set; }
        public bool NeedsUpdate { get; set; }
        public bool NeedsDownload { get; set; }
        public uint TimeLastUpdated { get; set; }
        public uint TimeAppLastRan { get; set; }
        public IEnumerable<object> WorkshopItemsInstalled { get; set; }
        public IEnumerable<object> WorkshopItemDetails { get; set; }
    }
}
