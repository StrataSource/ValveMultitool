using System;
using System.IO;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Basic
{
    public class GameStatsBasicRecord
    {
        // section lengths
        public int Count;
        public int Seconds;
        public int Commentary;
        public int Hdr;
        public int Captions;
        public int[] Skill = new int[3];
        public bool Steam;
        public bool CyberCafe;
        public int Deaths;

        private LegacyGameStats.GameStatsVersion _blobVersion;

        public void ParseFromBuffer(BinaryReader reader, LegacyGameStats.GameStatsVersion blobVersion)
        {
            _blobVersion = blobVersion;

            Count = reader.ReadInt32();
            Seconds = reader.ReadInt32();
            Seconds = Math.Max(Seconds, 0);
            Commentary = reader.ReadInt32();
            Hdr = reader.ReadInt32();
            Captions = reader.ReadInt32();

            // Read the skill sections
            for (var i = 0; i < 3; i++)
            {
                Skill[i] = reader.ReadInt32();
                if (Skill[i] > 100000 || Skill[i] < 0)
                    throw new InvalidOperationException("Invalid number of sections.");
            }

            // Check section lengths
            if (Count > 100000 || Count < 0 ||
                Seconds > 100000 || Seconds < 0 ||
                Commentary > 100000 || Commentary < 0 ||
                Hdr > 100000 || Hdr < 0 ||
                Captions > 100000 || Captions < 0)
                throw new InvalidOperationException("Invalid number of sections.");

            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old)
                Steam = Convert.ToBoolean((int)reader.ReadChar());

            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old2)
                CyberCafe = Convert.ToBoolean((int)reader.ReadChar());

            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old5)
                Deaths = reader.ReadInt32();
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(Count);
            writer.Write(Seconds);
            writer.Write(Commentary);
            writer.Write(Hdr);
            writer.Write(Captions);

            // Write the skill sections
            for (var i = 0; i < 3; i++)
            {
                writer.Write(Skill[i]);
            }

            // Write Versioned stats
            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old)
                writer.Write((char)(Steam ? 1 : 0));
            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old2)
                writer.Write((char)(CyberCafe ? 1 : 0));
            if (_blobVersion > LegacyGameStats.GameStatsVersion.Old5)
                writer.Write(Deaths);
        }
    }
}
