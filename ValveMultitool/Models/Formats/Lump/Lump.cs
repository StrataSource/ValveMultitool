using System;
using System.Collections.Generic;
using System.IO;

namespace ValveMultitool.Models.Formats.Lump
{
    internal class Lump : ILump
    {
        public ushort LumpId { get; set; }
        public ushort LumpCount { get; set; }
        public byte LumpVersion { get; set; }
        public IList<IElement> Children { get; } = new List<IElement>();

        public Lump(byte version = 0x00)
        {
            LumpVersion = version; }

        private void VerifyLump(int maxLumpCount = 512)
        {
            // Verify the lump is valid
            if (!(LumpCount > 0 && LumpCount < maxLumpCount))
                throw new InvalidOperationException("Too many elements in lump.");
            if (LumpCount <= 0)
                throw new InvalidOperationException("No elements in lump.");
        }

        public void LoadLump<T>(BinaryReader reader) where T : ILumpElement, new()
        {
            LumpId = reader.ReadUInt16();
            LumpCount = reader.ReadUInt16();
            VerifyLump();

            // Read all the elements in the lump
            for (var i = 0; i < LumpCount; i++)
            {
                ILumpElement lumpElement = new T();
                lumpElement.Read(reader, LumpVersion);
                Children.Add(lumpElement);
            }
        }

        public void SaveLump(BinaryWriter writer)
        {
            VerifyLump();

            // Write the lump id, count, and data size.
            writer.Write(LumpId);
            writer.Write(LumpCount);

            // Write all the elements in the lump
            foreach (var element in Children)
            {
                var lumpElement = element as ILumpElement;
                lumpElement?.Write(writer, LumpVersion);
            }
        }
    }
}
