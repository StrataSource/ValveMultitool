using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ValveMultitool.Utilities;

namespace ValveMultitool.Models.Formats.Misc
{
    public class BannedWords : IBinaryParseable
    {
        private byte[] _buffer;

        public virtual void ParseFromBuffer(BinaryReader reader)
        {
            var str = new string(reader.ReadChars(4));
            if (str != "BDR1") throw new Exception("Dictionary header is invalid.");
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            // Load buffer in
            _buffer = reader.BaseStream.ReadToEnd();
        }

        public void SaveToBuffer(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
