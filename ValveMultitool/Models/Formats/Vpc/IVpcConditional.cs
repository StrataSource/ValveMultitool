using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    public interface IVpcConditional
    {
        /// <summary>
        /// The operator. Example: ||, && or none.
        /// </summary>
        VpcOperator Operator { get; set; }

        /// <summary>
        /// Whether the conditional is negated. Example: !$WIN32
        /// </summary>
        bool Negated { get; set; }
    }
}
