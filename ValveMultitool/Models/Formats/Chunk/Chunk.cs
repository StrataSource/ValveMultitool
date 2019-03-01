using System;
using System.Collections.Generic;
using System.IO;

namespace ValveMultitool.Models.Formats.Chunk
{
    public abstract class Chunk : IChunk
    {
        public short ChunkId;
        public uint ChunkSize;
        public IList<IElement> Children { get; } = new List<IElement>();
        protected abstract IDictionary<int, Type> ChunkTypeMappings { get; }

        public virtual void ParseFromBuffer(BinaryReader reader)
        {
            ChunkId = reader.ReadInt16();
            ChunkSize = reader.ReadUInt32();
            ReadSubChunks(reader);
        }

        public virtual void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(ChunkId);
            writer.Write(ChunkSize);
            WriteSubChunks(writer);
        }

        protected void ReadSubChunks(BinaryReader reader)
        {
            // If the chunk end pos is the same as our position, we have no chunks to read...
            var chunkEndPos = reader.BaseStream.Position + ChunkSize;
            while (reader.BaseStream.Position != chunkEndPos)
            {
                // Peek the ID and size
                var id = reader.ReadUInt16();
                var size = reader.ReadUInt32() - sizeof(uint);

                // We don't know the format of this chunk, so skip it
                if (!ChunkTypeMappings.ContainsKey(id))
                {
                    reader.BaseStream.Position = reader.BaseStream.Position + size;
                }

                // Rewind stream so the chunk object can handle this
                reader.BaseStream.Position = reader.BaseStream.Position - sizeof(ushort) - sizeof(uint);

                // Get the type and instantiate the custom chunk object
                var type = ChunkTypeMappings[id];
                var chunk = (IChunk) Activator.CreateInstance(type);

                // Read in the chunk
                chunk.ParseFromBuffer(reader);
                Children.Add(chunk);
            }
        }

        protected void WriteSubChunks(BinaryWriter writer)
        {
            foreach (var element in Children)
            {
                var chunk = element as IChunk;
                chunk?.SaveToBuffer(writer);
            }
        }
    }
}
