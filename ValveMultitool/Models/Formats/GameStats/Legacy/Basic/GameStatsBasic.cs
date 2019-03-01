using System;
using System.Collections.Generic;
using System.IO;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Basic
{
    public class GameStatsBasic
    {
        /// <summary>
        /// Seconds the player took to complete the game.
        /// 0 means the player hasn't finished playing yet.
        /// </summary>
        public int SecondsToCompleteGame;
        public GameStatsBasicRecord Summary;
        public IDictionary<string, GameStatsBasicRecord> MapTotals;
        public bool Steam;
        public bool CyberCafe;
        public int Hl2ChapterUnlocked;
        public int DxLevel;

        private LegacyGameStats.GameStatsVersion _blobVersion;

        public GameStatsBasic()
        {
            Summary = new GameStatsBasicRecord();
            MapTotals = new Dictionary<string, GameStatsBasicRecord>();
        }

        public void ParseFromBuffer(BinaryReader reader, LegacyGameStats.GameStatsVersion blobVersion)
        {
            _blobVersion = blobVersion;
            SecondsToCompleteGame = reader.ReadInt32();

            Summary.ParseFromBuffer(reader, _blobVersion);

            // Read the number of map sections
            var sections = reader.ReadInt32();
            if (sections > 1024 || sections < 0)
                throw new InvalidOperationException("Invalid number of sections.");

            // Read every map section
            for (var i = 0; i < sections; i++)
            {
                var mapName = reader.ReadNullTerminatedString();
                var record = FindOrAddRecordForMap(mapName);
                record.ParseFromBuffer(reader, blobVersion);
            }

            // Read Stats Version 2
            if (_blobVersion >= LegacyGameStats.GameStatsVersion.Old2)
            {
                Hl2ChapterUnlocked = reader.ReadChar();
                Steam = Convert.ToBoolean((int)reader.ReadChar());
            }

            // Read Stats Version 3
            if (_blobVersion >= LegacyGameStats.GameStatsVersion.Old3)
            {
                CyberCafe = Convert.ToBoolean((int)reader.ReadChar());
                DxLevel = reader.ReadInt16();
            }
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(SecondsToCompleteGame);
            Summary.SaveToBuffer(writer);

            // Write the number of map sections
            writer.Write(MapTotals.Count);
            foreach (var map in MapTotals)
            {
                writer.WriteNullTerminatedString(map.Key);
                map.Value.SaveToBuffer(writer);
            }

            // Write Versioned stats
            if (_blobVersion >= LegacyGameStats.GameStatsVersion.Old2)
            {
                writer.Write((char)Hl2ChapterUnlocked);
                writer.Write((char)(Steam ? 1 : 0));
            }

            if (_blobVersion >= LegacyGameStats.GameStatsVersion.Old3)
            {
                writer.Write((char)(CyberCafe ? 1 : 0));
                writer.Write((short)DxLevel);
            }
        }

        private GameStatsBasicRecord FindOrAddRecordForMap(string mapName)
        {
            if (!MapTotals.ContainsKey(mapName))
                MapTotals[mapName] = new GameStatsBasicRecord();

            return MapTotals[mapName];
        }
    }
}
