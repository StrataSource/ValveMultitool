using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalJumpEvent : LumpElement
    {
        public Vector3<float> PlayerPositionAtJumpStart;
        public Vector3<float> PlayerVelocityAtJumpStart;

        public override void Read(BinaryReader reader, byte version)
        {
            PlayerPositionAtJumpStart = reader.ReadVector<float>();
            PlayerVelocityAtJumpStart = reader.ReadVector<float>();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(PlayerPositionAtJumpStart);
            writer.WriteVector(PlayerVelocityAtJumpStart);
        }
    }
}
