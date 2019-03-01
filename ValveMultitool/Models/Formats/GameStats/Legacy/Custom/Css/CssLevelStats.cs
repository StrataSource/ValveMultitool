using System.Collections.Generic;
using System.IO;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Css
{
    public class CssLevelStats : ILevelStats, ICustomLevelStats
    {
        private enum CssLevelCounterTypes
        {
            /// <summary>
            /// Number of minutes played on the level.
            /// </summary>
            MinutesPlayed,
            /// <summary>
            /// Total number of terrorist victories.
            /// </summary>
            TerroristVictories,
            /// <summary>
            /// Total number of counter terrorist victories.
            /// </summary>
            CounterTVictories,
            /// <summary>
            /// Number of purchases on the black market.
            /// </summary>
            BlackMarketPurchases,
            /// <summary>
            /// Number of purchases that were auto-bought.
            /// </summary>
            AutoBuyPurchases,
            /// <summary>
            /// Number of purchases that were bought again.
            /// </summary>
            ReBuyPurchases,

            AutoBuyM4A1Purchases,
            AutoBuyAk47Purchases,
            AutoBuyFamasPurchases,
            AutoBuyGalilPurchases,
            AutoBuyVestHelmPurchases,
            AutoBuyVestPurchases,
            NumTypes
        }

        public ILevelHeader Header { get; set; }
        public IList<IElement> Children { get; }
        public string Name { get; set; }

        // CSS specific statistics
        public short[] CssLevelCounters;

        public void PostReadLumps(BinaryReader reader)
        {
            CssLevelCounters = reader.ReadArray<short>((int) CssLevelCounterTypes.NumTypes);
        }

        public void PostWriteLumps(BinaryWriter writer)
        {
            writer.WriteArray(CssLevelCounters);
        }
    }
}
