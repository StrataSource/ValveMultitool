using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Network
{
    public class CellEdge
    {
        [KvProperty("dest")]
        public int Destination { get; set; }
        public string Cost { get; set; }
    }
}
