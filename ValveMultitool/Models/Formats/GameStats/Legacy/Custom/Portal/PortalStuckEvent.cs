using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalStuckEvent : LumpElement
    {
        public Vector3<float> PlayerPosition;
        public Vector3<float> PlayerAngles;
        public bool NearPortal;
        public bool Ducking;

        public override void Read(BinaryReader reader, byte version)
        {
            PlayerPosition = reader.ReadVector<float>();
            PlayerAngles = reader.ReadVector<float>();
            NearPortal = reader.ReadBoolean();
            Ducking = reader.ReadBoolean();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(PlayerPosition);
            writer.WriteVector(PlayerAngles);
            writer.Write(NearPortal);
            writer.Write(Ducking);
        }
    }
}
