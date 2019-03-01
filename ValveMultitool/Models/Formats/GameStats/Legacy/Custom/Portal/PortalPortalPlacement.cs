using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalPortalPlacement : LumpElement
    {
        public Vector3<float> PlayerFiredFrom;
        public Vector3<float> PlacementPosition;
        public char SuccessCode;

        public override void Read(BinaryReader reader, byte version)
        {
            PlayerFiredFrom = reader.ReadVector<float>();
            PlacementPosition = reader.ReadVector<float>();
            SuccessCode = reader.ReadChar();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(PlayerFiredFrom);
            writer.WriteVector(PlacementPosition);
            writer.Write(SuccessCode);
        }
    }
}
