using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam
{
    public class Account
    {
        public ulong SteamId { get; set; }
        public string AccountName { get; set; }
        public string PersonaName { get; set; }
        public bool RememberPassword { get; set; }
        public bool MostRecent { get; set; }
        public uint Timestamp { get; set; }
    }
}
