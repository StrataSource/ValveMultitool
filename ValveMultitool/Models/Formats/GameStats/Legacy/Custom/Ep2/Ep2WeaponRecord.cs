using System.IO;
using ValveMultitool.Models.Formats.Lump;

namespace ValveMultitool.Models.Formats.GameStats.Legacy.Custom.Ep2
{
    public class Ep2WeaponRecord : LumpElement, INamedElement
    {
        /// <summary>
        /// Number of shots the weapon has fired so far.
        /// </summary>
        public uint Shots;

        /// <summary>
        /// Number of shots this weapon has fired that have hit a target.
        /// </summary>
        public uint Hits;

        /// <summary>
        /// Total damage inflicted by this weapon.
        /// </summary>
        public double DamageInflicted;

        public string Name { get; set; }

        public override void Read(BinaryReader reader, byte version)
        {
            Shots = reader.ReadUInt32();
            Hits = reader.ReadUInt32();
            DamageInflicted = reader.ReadDouble();
        }

        public override void Write(BinaryWriter writer, byte version)
        {
            writer.Write(Shots);
            writer.Write(Hits);
            writer.Write(DamageInflicted);
        }
    }
}
