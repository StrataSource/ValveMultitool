using System.IO;
using ValveMultitool.Common;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2KillStats : LumpElement
    {
        /// <summary>
        /// How many times this player has killed every other player
        /// </summary>
        public int[] NumKilled;

        /// <summary>
        /// How many times this player has been killed by every other player
        /// </summary>
        public int[] NumKilledBy;

        /// <summary>
        /// How many unanswered kills this player has been dealt by every other player
        /// </summary>
        public int[] NumKilledByUnanswered;

        public override void Read(BinaryReader reader, byte version)
        {
            const int maxCount = SharedConstants.Tf2MaxPlayers + 1;
            NumKilled = reader.ReadArray<int>(maxCount);
            NumKilledBy = reader.ReadArray<int>(maxCount);
            NumKilledByUnanswered = reader.ReadArray<int>(maxCount);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteArray(NumKilled);
            writer.WriteArray(NumKilledBy);
            writer.WriteArray(NumKilledByUnanswered);
        }
    }
}
