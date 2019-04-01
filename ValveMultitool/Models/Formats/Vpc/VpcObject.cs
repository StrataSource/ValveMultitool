using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ValveMultitool.Models.Formats.Vpc
{
    public class VpcObject : List<VpcValue>
    {
        public string Type;
        public string Key;
        public bool IsArray = false;
        public readonly VpcConditionalCollection Conditions = new VpcConditionalCollection();

        public VpcObject(string type = null, string key = null)
        {
            Type = type;
            Key = key;
        }

        public static VpcObject Parse(Stream stream)
        {
            return new VpcParser(stream).Parse();
        }

        public void Serialise(Stream stream)
        {
            new VpcSerialiser(stream, this).Serialise();
        }

        public override string ToString()
        {
            var type = string.IsNullOrWhiteSpace(Type) ? "Root" : Type;
            var key = string.IsNullOrWhiteSpace(Key) ? " " : $" {Key} ";

            if (IsArray) type = type + "[]";

            if (Count <= 0)
            {
                return $"${type}{key}Empty!";
            }
            else if (Count == 1)
            {
                return $"${type}{key}{this.First()}";
            }
            else
            {
                return $"${type}{key}Multiple";
            }
        }
    }
}
