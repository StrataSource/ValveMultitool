using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Network
{
    public class CellMap
    {
        // keyvalues1 steam/cached/CellMap.vdf

        /// <summary>
        /// These are multipliers, to make an edge cost 50% more than usual, set the multiplier to 1.5
        /// </summary>
        [KvProperty("edge_costs")]
        public Dictionary<string, int> EdgeCosts { get; set; }

        /// <summary>
        /// Dictionary of network names and network IDs.
        /// Value is a string for now because reader can't decode comma seperated int arrays.
        /// </summary>
        [KvProperty(CollectionType = KvCollectionType.CharSeparated, CollectionTypeSeparator = ',')]
        public Dictionary<string, int[]> Networks { get; set; }

        /// <summary>
        /// Dictionary of all cells.
        /// </summary>
        public Dictionary<int, Cell> Cells { get; set; }

        /// <summary>
        /// Dictionary of country codes to cells.
        /// </summary>
        public Dictionary<string, int> Countries { get; set; }

        /// <summary>
        /// Dictionary of US states to cells.
        /// </summary>
        public Dictionary<string, int> States { get; set; }
    }
}
