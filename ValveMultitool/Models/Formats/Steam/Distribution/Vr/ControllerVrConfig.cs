using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Vr
{
    public class ControllerVrConfig
    {
        /// <summary>
        /// Whether VR input is possible with a keyboard and mouse.
        /// </summary>
        public bool Kbm { get; set; }

        /// <summary>
        /// Whether VR input is possible with XInput.
        /// </summary>
        public bool Xinput { get; set; }

        /// <summary>
        /// Whether VR input is possible through SteamVR.
        /// </summary>
        public bool SteamVr { get; set; }

        /// <summary>
        /// Whether VR input is possible through the Oculus Rift.
        /// </summary>
        public bool Oculus { get; set; }
    }
}
