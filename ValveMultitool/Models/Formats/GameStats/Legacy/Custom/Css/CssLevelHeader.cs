using System.IO;
using System.Text;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Css
{
    public class CssLevelHeader : LumpElement, ILevelHeader, IMultiPlayLevelHeader
    {
        public byte Version { get; set; }
        public string MapName { get; set; }

        public string GameName;
        public int ServerId;
        public uint IpAddress { get; set; }
        public ushort Port { get; set; }

        public override void Read(BinaryReader reader, byte version)
        {
            Version = reader.ReadByte();
            MapName = reader.ReadPaddedString(64);
            IpAddress = reader.ReadUInt32();
            Port = (ushort) reader.ReadUInt32();
            ServerId = reader.ReadInt32();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Version);
            writer.Write(Encoding.ASCII.GetBytes(MapName));
            writer.Write(IpAddress);
            writer.Write((uint) Port);
            writer.Write(ServerId);
        }
    }
}
