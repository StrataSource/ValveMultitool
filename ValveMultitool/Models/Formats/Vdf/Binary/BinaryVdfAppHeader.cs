using System.IO;
using ValveKeyValue;
using ValveMultitool.Models.Formats.Steam.Distribution.Apps;

namespace ValveMultitool.Models.Formats.Vdf.Binary
{
    public class BinaryVdfAppHeader : BinaryVdfHeader<AppInfo>
    {
        /// <summary>
        /// Represents the Steamworks AppID.
        /// </summary>
        public uint AppId;

        /// <summary>
        /// Represents how many bytes in length the app's data is.
        /// </summary>
        public uint Size;

        /// <summary>
        /// Unknown what this represents.
        /// TODO: create enum for this...
        /// </summary>
        public uint State;

        /// <summary>
        /// Unix timestamp of when the app was last updated.
        /// </summary>
        public uint LastUpdate;

        /// <summary>
        /// Unknown what purpose this serves.
        /// For most apps it is 0.
        /// </summary>
        public ulong AccessToken;

        /// <summary>
        /// 20-byte array, used to verify the app in offline mode.
        /// </summary>
        public byte[] CheckSum;

        /// <summary>
        /// Represents the app's Steamworks change number.
        /// </summary>
        public uint ChangeNumber;

        public const uint Terminator = 0;

        public override void ParseFromBuffer(BinaryReader reader)
        {
            AppId = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            State = reader.ReadUInt32();
            LastUpdate = reader.ReadUInt32();
            AccessToken = reader.ReadUInt64();
            CheckSum = reader.ReadBytes(20);
            ChangeNumber = reader.ReadUInt32();

            // TEST!!!!!!!
            //var pos = reader.BaseStream.Position;
            //var testobj = KvSerializer.Create(KvSerializationFormat.KeyValues1Binary).Deserialize(reader.BaseStream);
            //reader.BaseStream.Seek(pos, SeekOrigin.Begin);

            //using (var stream = File.Open("serialisation.vdf", FileMode.Append))
                //KvSerializer.Create(KvSerializationFormat.KeyValues1Text).Serialize(stream, testobj);

            base.ParseFromBuffer(reader);
        }

        public override void SaveToBuffer(BinaryWriter writer)
        {
            writer.Write(AppId);
            writer.Write(Size);
            writer.Write(State);
            writer.Write(LastUpdate);
            writer.Write(AccessToken);
            writer.Write(CheckSum);
            writer.Write(ChangeNumber);
            base.SaveToBuffer(writer);
        }
    }
}
