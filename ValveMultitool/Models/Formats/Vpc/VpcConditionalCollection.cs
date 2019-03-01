using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    public class VpcConditionalCollection : List<IVpcConditional>, IVpcConditional
    {
        public VpcOperator Operator { get; set; }
        public bool Negated { get; set; }

        public override string ToString()
        {
            return $"Collection ({Operator})";
        }
    }
}
