using System.IO;
using ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2PlayerDeaths : Ep2PlayerDeaths
    {
        /// <summary>
        /// Weapon that killed the player.
        /// </summary>
        public short Weapon;

        /// <summary>
        /// Distance that the attacker was from the player.
        /// </summary>
        public ushort Distance;

        /// <summary>
        /// Class that killed the player.
        /// </summary>
        public byte AttackClass;

        /// <summary>
        /// Class of the player killed.
        /// </summary>
        public byte TargetClass;

        public override void Read(BinaryReader reader, byte version)
        {
            base.Read(reader, version);
            Weapon = reader.ReadInt16();
            Distance = reader.ReadUInt16();
            AttackClass = reader.ReadByte();
            TargetClass = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            base.Write(writer, version);
            writer.Write(Weapon);
            writer.Write(Distance);
            writer.Write(AttackClass);
            writer.Write(TargetClass);
        }
    }
}
