using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Network
{
    public class Cell
    {
        /// <summary>
        /// Name of the cell. Example: Canada - Toronto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether this cell is used as a transit between two regions.
        /// </summary>
        public bool Transit { get; set; }

        /// <summary>
        /// Whether this cell is deprecated and should no longer be used.
        /// </summary>
        public bool Deprecated { get; set; }

        /// <summary>
        /// Global region of the cell. Example: canada
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Country code of the cell's region. Example: CA
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Latitude of the cell's approximate physical position.
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the cell's approximate physical position.
        /// </summary>
        public float Longitude { get; set; }

        /// <summary>
        /// Dictionary of destination and cost information for cell edges.
        /// </summary>
        public Dictionary<int, CellEdge> Edges { get; set; }

        /// <summary>
        /// Dictionary of ISP and cell affinity definitions for cost reduction per ISP.
        /// </summary>
        public Dictionary<string, CellAffinity> Affinities { get; set; }
    }
}
