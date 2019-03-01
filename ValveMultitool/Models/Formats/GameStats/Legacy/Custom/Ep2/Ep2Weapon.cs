using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2Weapon : LumpElement
    {
        private static readonly LumpElementHeaderInfo HeaderInfo = new LumpElementHeaderInfo
        {
            Type = LumpElementHeaderInfo.HeaderNameType.String
        };

        public override void Read(BinaryReader reader, byte version)
        {
            ReadChildren<Ep2WeaponRecord>(reader, version, HeaderInfo);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            WriteChildren(writer, version, HeaderInfo);
        }
    }
}
