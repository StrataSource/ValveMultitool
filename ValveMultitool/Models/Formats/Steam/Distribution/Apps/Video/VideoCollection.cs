using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Apps.Video
{
    public class VideoCollection
    {
        /// <summary>
        /// Video collection group ID.
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Index of the video in the group.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Type of the video (chapter, episode, bonus, deleted)
        /// </summary>
        public string Type { get; set; }
    }
}
