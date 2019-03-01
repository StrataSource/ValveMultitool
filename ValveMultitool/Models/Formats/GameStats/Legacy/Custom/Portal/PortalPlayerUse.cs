using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Portal
{
    public class PortalPlayerUse : LumpElement
    {
        public Vector3<float> TraceStart;
        public Vector3<float> TraceDelta;
        public string UseEntityClassName;

        public override void Read(BinaryReader reader, byte version)
        {
            TraceStart = reader.ReadVector<float>();
            TraceDelta = reader.ReadVector<float>();
            UseEntityClassName = reader.ReadPaddedString(32);
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteVector(TraceStart);
            writer.WriteVector(TraceDelta);
            writer.WritePaddedString(UseEntityClassName, 32);
        }
    }
}
