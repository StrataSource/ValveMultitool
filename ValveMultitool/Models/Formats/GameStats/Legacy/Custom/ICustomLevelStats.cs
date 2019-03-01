using System.IO;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom
{
    /// <inheritdoc />
    /// <summary>
    /// Level stats that can store extra fields.
    /// </summary>
    internal interface ICustomLevelStats : ILevelStats
    {
        /// <summary>
        /// Called immediately after all lumps are read.
        /// Override this to read any extra data that comes after the lumps.
        /// </summary>
        void PostReadLumps(BinaryReader reader);

        /// <summary>
        /// Called immediately after all lumps are written.
        /// Override this to write any extra data that comes after the lumps.
        /// </summary>
        void PostWriteLumps(BinaryWriter writer);
    }
}
