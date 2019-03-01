using System.IO;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2SaveGameInfoRecord2 : Ep2SaveGameInfoRecord
    {
        public enum SaveType
        {
            Unknown,
            AutoSave,
            UserSave
        }

        public SaveType Type;

        public override void Read(BinaryReader reader, byte version)
        {
            base.Read(reader, version);
            Type = (SaveType)reader.ReadChar();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            base.Write(writer, version);
            writer.Write((char)Type);
        }
    }
}
