using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam
{
    public class LibraryFolders
    {
        // keyvalues1 steamapps/libraryfolders.vdf
        public uint TimeNextStatsReport { get; set; }
        [KvProperty("ContentStatsID")]
        public long ContentStatsId { get; set; }
    }
}
