using System.IO;

namespace ValveMultitool.Models.Formats
{
    public interface IBinaryParseable
    {
        /// <summary>
        /// Saves the game stats entity to a buffer.
        /// </summary>
        void ParseFromBuffer(BinaryReader reader);

        /// <summary>
        /// Reads in the game stats entity from a buffer.
        /// </summary>
        void SaveToBuffer(BinaryWriter writer);
    }
}
