using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    public enum VpcValueType
    {
        String,
        Object,

        /// <summary>
        /// A comment, this is not parsed.
        /// </summary>
        Comment
    }
}
