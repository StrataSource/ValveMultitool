using System;
using System.Collections.Generic;
using System.Text;
using ValveKeyValue.Attributes;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Vr
{
    public class RoomScaleVrConfig
    {
        public float Width { get; set; }
        public float Depth { get; set; }

        /// <summary>
        /// Whether 360-degree vision is required.
        /// </summary>
        [KvProperty("360_required")]
        public bool Required360 { get; set; }
    }
}
