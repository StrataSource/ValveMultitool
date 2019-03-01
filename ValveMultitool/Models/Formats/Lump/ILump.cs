using System.IO;

namespace ValveMultitool.Models.Formats.Lump
{
    public interface ILump : IElement
    {
        /// <summary>
        /// Lump ID of the stat entity.
        /// May be overridden in the child class.
        /// </summary>
        ushort LumpId { get; set; }

        /// <summary>
        /// Number of elements in the lump.
        /// </summary>
        ushort LumpCount { get; set; }

        /// <summary>
        /// Byte containing the version of the lump.
        /// </summary>
        byte LumpVersion { get; set; }

        /// <summary>
        /// Saves the lump to a binary writer.
        /// </summary>
        void SaveLump(BinaryWriter writer);

        /// <summary>
        /// Loads the lump and its elements from a binary reader.
        /// </summary>
        /// <typeparam name="T">Type of the lump's elements.</typeparam>
        void LoadLump<T>(BinaryReader reader) where T : ILumpElement, new();
    }
}
