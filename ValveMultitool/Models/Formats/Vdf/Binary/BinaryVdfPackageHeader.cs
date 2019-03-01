using System.IO;
using ValveKeyValue;
using ValveMultitool.Models.Formats.Steam.Distribution.Packages;

namespace ValveMultitool.Models.Formats.Vdf.Binary
{
    public class BinaryVdfPackageHeader : BinaryVdfHeader<PackageInfo>
    {
        public uint PackageId;
        public byte[] CheckSum;
        public uint ChangeNumber;

        public const uint Terminator = 0xFFFFFFFF;

        public override void ParseFromBuffer(BinaryReader reader)
        {
            PackageId = reader.ReadUInt32();
            CheckSum = reader.ReadBytes(20);
            ChangeNumber = reader.ReadUInt32();

            base.ParseFromBuffer(reader);
        }

        public override void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(PackageId);
            writer.Write(CheckSum);
            writer.Write(ChangeNumber);
            base.SaveToBuffer(writer);
        }
    }
}
