using System;
using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.Inf
{
    public class SteamInf : Dictionary<string, string>
    {
        private SteamInf(StringComparer comparer) : base(comparer) { }

        public static SteamInf Parse(IEnumerable<string> lines)
        {
            var inf = new SteamInf(StringComparer.OrdinalIgnoreCase);

            foreach (var line in lines)
            {
                var spl = line.Split('=');
                if (spl.Length != 2)
                    throw new InvalidOperationException("Key or value in pair does not exist.");
                if (string.IsNullOrWhiteSpace(spl[0]))
                    throw new InvalidOperationException("Key is null or whitespace.");
                inf.Add(spl[0], spl[1]);
            }

            return inf;
        }
    }
}
