using System;
using System.Collections.Generic;
using System.Text;

namespace ValveMultitool.Models.Formats.Vpc
{
    public class VpcValue
    {
        public object Value;
        public VpcValueType Type;

        public override string ToString() => Value.ToString();
    }
}
