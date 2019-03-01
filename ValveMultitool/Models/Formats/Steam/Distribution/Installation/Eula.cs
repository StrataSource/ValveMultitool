using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Steam.Distribution.Installation
{
    /// <summary>
    /// Represents an end-user license agreement that
    /// the user must accept in order to install the app.
    /// </summary>
    public class Eula
    {
        /// <summary>
        /// Id of the EULA. Format is [appid]_eula_[index].
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the EULA displayed to the customer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to the EULA web page shown in the acceptance dialog.
        /// </summary>
        public string Url { get; set; }
    }
}
