using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Installation
{
    public class RunAction : InstallAction
    {
        // process 1, command 1
        /// <summary>
        /// Whether the setup process should run as the current user or as an administrator.
        /// </summary>
        public bool AsCurrentUser { get; set; }

        /// <summary>
        /// TODO: where is this used?
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether Steam should not clean up after extracting this executable.
        /// </summary>
        public bool NoCleanup { get; set; }

        /// <summary>
        /// Whether Steam should check the process exit code for failure or not.
        /// </summary>
        public bool IgnoreExitCode { get; set; }

        /// <summary>
        /// TODO
        /// </summary>
        public int MinimumHasRunValue { get; set; }
    }
}
