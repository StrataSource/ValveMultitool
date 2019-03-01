using System;
using System.Collections.Generic;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    /// <inheritdoc />
    /// <summary>
    /// Game statistics for Half-Life 2: Episode 2
    /// </summary>
    public class Ep2GameStats : LumpGameStats
    {
        public enum Ep2GameStatsVersion
        {
            FileVersion1 = 001,
            FileVersion2 = 002
        }

        private enum Ep2GameStatsLumpIds
        {
            Header = 1,
            Death,
            Npc,
            Weapon,
            SaveGameInfo,
            Tag,
            Generic
        }

        protected override IDictionary<int, Type> LumpTypeMappings { get; }
            = new Dictionary<int, Type>
            {
                { (int)Ep2GameStatsLumpIds.Header, typeof(Ep2LevelHeader) },
                { (int)Ep2GameStatsLumpIds.Tag, typeof(Ep2Tag) },
                { (int)Ep2GameStatsLumpIds.Death, typeof(Ep2PlayerDeaths) },
                { (int)Ep2GameStatsLumpIds.Npc, typeof(Ep2EntityDeaths) },
                { (int)Ep2GameStatsLumpIds.Weapon, typeof(Ep2Weapon) },
                { (int)Ep2GameStatsLumpIds.SaveGameInfo, typeof(Ep2SaveGameInfo) },
                { (int)Ep2GameStatsLumpIds.Generic, typeof(Ep2GenericStats) }
            };

        protected override Type CustomLevelStatsType => typeof(Ep2LevelStats);
    }
}
