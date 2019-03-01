using System;
using System.Collections.Generic;
using System.IO;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.Lump
{
    public abstract class LumpElement : ILumpElement
    {
        public IList<IElement> Children { get; }
            = new List<IElement>();

        public abstract void Read(BinaryReader reader, byte version);
        public abstract void Write(BinaryWriter writer, byte version);

        protected void ReadChild<T>(BinaryReader reader, byte version) where T : ILumpElement, new()
        {
            var element = new T();
            element.Read(reader, version);
        }

        protected void WriteChild<T>(BinaryWriter writer, byte version, T child) where T : ILumpElement
        {
            child.Write(writer, version);
        }

        public void ReadChildren<T>(BinaryReader reader, byte version, LumpElementHeaderInfo info = null) where T : ILumpElement, new()
        {
            if (info == null)
                info = new LumpElementHeaderInfo();

            // Read number of child elements
            var count = reader.ReadInt32();
            for (var i = 0; i < count; i++)
            {
                string name;

                // Read each child element
                switch (info.Type)
                {
                    case LumpElementHeaderInfo.HeaderNameType.None:
                        name = $"element{i}";
                        break;
                    case LumpElementHeaderInfo.HeaderNameType.Char:
                        name = reader.ReadPaddedString(info.CharLength);
                        break;
                    case LumpElementHeaderInfo.HeaderNameType.String:
                        name = reader.ReadNullTerminatedString();
                        break;
                    default:
                        throw new InvalidOperationException("Invalid lump header type.");
                }
                    
                var element = (ILumpElement) new T();
                element.Read(reader, version);

                if (element is INamedElement named)
                    named.Name = name;
                Children.Add(element);
            }
        }

        public void WriteChildren(BinaryWriter writer, byte version, LumpElementHeaderInfo info = null)
        {
            if (info == null)
                info = new LumpElementHeaderInfo();

            // Write number of child elements
            writer.Write(Children.Count);
            foreach (var record in Children)
            {
                // Write each child element
                if (record is INamedElement named)
                {
                    // Handle named elements
                    switch (info.Type)
                    {
                        case LumpElementHeaderInfo.HeaderNameType.None:
                            // do nothing
                            break;
                        case LumpElementHeaderInfo.HeaderNameType.Char:
                            writer.WritePaddedString(named.Name, info.CharLength);
                            break;
                        case LumpElementHeaderInfo.HeaderNameType.String:
                            writer.WriteNullTerminatedString(named.Name);
                            break;
                        default:
                            throw new InvalidOperationException("Invalid lump header type.");
                    }
                }

                var lump = (ILumpElement) record;
                lump.Write(writer, version);
            }
        }
    }
}
