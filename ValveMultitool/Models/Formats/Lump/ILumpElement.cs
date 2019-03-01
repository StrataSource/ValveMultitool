using System.IO;

namespace ValveMultitool.Models.Formats.Lump
{
    public interface ILumpElement : IElement
    {
        void Read(BinaryReader reader, byte version);
        void Write(BinaryWriter writer, byte version);

        /// <summary>
        /// Reads in all children of a lump element.
        /// </summary>
        void ReadChildren<T>(BinaryReader reader, byte version, LumpElementHeaderInfo info = null) where T : ILumpElement, new();

        /// <summary>
        /// Writes out all children of a lump element.
        /// </summary>
        void WriteChildren(BinaryWriter writer, byte version, LumpElementHeaderInfo info = null);
    }
}
