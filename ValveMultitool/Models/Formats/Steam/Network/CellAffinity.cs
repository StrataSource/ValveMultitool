using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Network
{
    public class CellAffinity
    {
        [KvProperty("cost_reduction")]
        public int CostReduction { get; set; }
    }
}
