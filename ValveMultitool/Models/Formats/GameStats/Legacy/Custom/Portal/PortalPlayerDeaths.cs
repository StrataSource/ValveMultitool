using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    class PortalPlayerDeaths : LumpElement
    {
        public Vector3<float> Position;
        public int DamageType;
        public string AttackerClassName;

        public override void Read(BinaryReader reader, byte version)
        {
            Position = reader.ReadVector<float>();
            DamageType = reader.ReadInt32();
            AttackerClassName = reader.ReadPaddedString(32);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(Position);
            writer.Write(DamageType);
            writer.WritePaddedString(AttackerClassName, 32);
        }
    }
}
