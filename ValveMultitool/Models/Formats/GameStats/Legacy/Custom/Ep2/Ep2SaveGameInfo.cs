using System.IO;
using System.Linq;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2SaveGameInfo : LumpElement
    {
        public Ep2SaveGameInfoRecord CurrentRecord;
        public int CurrentSaveFileTime;
        public string CurrentSaveFile;

        public override void Read(BinaryReader reader, byte version)
        {
            CurrentSaveFile = reader.ReadNullTerminatedString();
            CurrentSaveFileTime = reader.ReadInt32();

            if ((Ep2GameStats.Ep2GameStatsVersion) version >= Ep2GameStats.Ep2GameStatsVersion.FileVersion2)
            {
                ReadChildren<Ep2SaveGameInfoRecord2>(reader, version);
            } else
            {
                ReadChildren<Ep2SaveGameInfoRecord>(reader, version);
            }

            if (Children.Count > 0)
                CurrentRecord = (Ep2SaveGameInfoRecord) Children.Last();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.WriteNullTerminatedString(CurrentSaveFile);
            writer.Write(CurrentSaveFileTime);
            WriteChildren(writer, version);
        }
    }
}
