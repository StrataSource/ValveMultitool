using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    public class VpcConditional : IVpcConditional
    {
        public VpcOperator Operator { get; set; }
        public bool Negated { get; set; }
        public string Value { get; set; }

        public VpcConditional(string value, VpcOperator oper)
        {
            Value = value;
            Operator = oper;
        }

        public override string ToString()
        {
            return $"${Value} ({Operator})";
        }
    }
}
