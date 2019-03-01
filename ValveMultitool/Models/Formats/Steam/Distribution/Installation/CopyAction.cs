using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Installation
{
    public class CopyAction : InstallAction
    {
        private IList<(string, string)> CopyMappings;
        public bool IgnoreExitCode { get; set; }

        // TODO: ondeserialising
    }
}
