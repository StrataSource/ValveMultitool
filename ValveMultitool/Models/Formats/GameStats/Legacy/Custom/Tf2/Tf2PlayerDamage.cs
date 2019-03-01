using System.IO;
using ValveMultitool.Models.Formats.Lump;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Tf2
{
    public class Tf2PlayerDamage : LumpElement
    {
        /// <summary>
        /// Time of the damage event
        /// </summary>
        public float Time;
        /// <summary>
        /// Position of target
        /// </summary>
        public Vector3<short> TargetPosition;
        /// <summary>
        /// Position of attacker
        /// </summary>
        public Vector3<short> AttackerPosition;
        /// <summary>
        /// Total damage
        /// </summary>
        public short Damage;
        /// <summary>
        /// Weapon used
        /// </summary>
        public short Weapon;
        /// <summary>
        /// Class of the attacker
        /// </summary>
        public byte AttackClass;
        /// <summary>
        /// Class of the target
        /// </summary>
        public byte TargetClass;
        /// <summary>
        /// Was the shot a crit?
        /// </summary>
        public bool Crit;
        /// <summary>
        /// Did the shot kill the target?
        /// </summary>
        public bool Kill;

        public override void Read(BinaryReader reader, byte version)
        {
            Time = reader.ReadSingle();
            TargetPosition = reader.ReadVector<short>();
            AttackerPosition = reader.ReadVector<short>();
            Damage = reader.ReadInt16();
            Weapon = reader.ReadInt16();
            AttackClass = reader.ReadByte();
            TargetClass = reader.ReadByte();
            Crit = reader.ReadBoolean();
            Kill = reader.ReadBoolean();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Time);
            writer.WriteVector(TargetPosition);
            writer.WriteVector(AttackerPosition);
            writer.Write(Damage);
            writer.Write(Weapon);
            writer.Write(AttackClass);
            writer.Write(TargetClass);
            writer.Write(Crit);
            writer.Write(Kill);
        }
    }
}
