using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution
{
    public class CountryList
    {
        public Dictionary<int, Distributor> Distributors { get; set; }
    }
}
