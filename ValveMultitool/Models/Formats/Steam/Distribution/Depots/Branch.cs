using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Depots
{
    public class Branch
    {
        /// <summary>
        /// Build ID that is currently set live on this branch.
        /// </summary>
        public uint BuildId { get; set; }

        /// <summary>
        /// Description of this branch.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether a password is required to select this branch.
        /// </summary>
        public bool PwdRequired { get; set; }

        /// <summary>
        /// The time that this branch was last modified.
        /// </summary>
        public uint TimeUpdated { get; set; }
    }
}
