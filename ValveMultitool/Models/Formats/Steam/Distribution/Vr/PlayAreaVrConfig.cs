using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Vr
{
    public class PlayAreaVrConfig
    {
        public bool Seated { get; set; }
        public bool Standing { get; set; }
        public RoomScaleVrConfig RoomScale { get; set; }
    }
}
