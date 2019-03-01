using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Network
{
    public class CmWebSocket
    {
        public int LastPingTimeStamp { get; set; }
        public int LastPingValue { get; set; }
        public int LastLoadValue { get; set; }
    }
}
