using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Steamworks;
using ValveKeyValue;
using ValveMultitool.Models.Formats.Vdf.Binary;
using ValveMultitool.Utilities;

namespace ValveMultitool.Tests.Parser.BinaryVdfTests
{
    [TestClass]
    [DeploymentItem("Resources/TestData/Vdf/Binary")]
    public class BinaryVdfDeserialisationTests : BinaryVdfTestBase
    {
        [TestMethod]
        public void ThrowsWhenHeaderNull() => Assert.ThrowsException<ArgumentException>(() => { BinaryVdf.Parse(new byte[0], typeof(Type)); });

        [TestMethod]
        public void TestDeserialise()
        {
            foreach (var file in TestBytes)
            {
                var type = BinaryVdf.ResolveHeaderType(file.Key);
                var vdf = BinaryVdf.Parse(file.Value, type);

                // TODO: also verify version
                if (type != null)
                    Assert.AreEqual(EUniverse.k_EUniversePublic, vdf.UniverseType);
                File.WriteAllText("test.txt", JsonConvert.SerializeObject(vdf));
            }
        }

        [TestMethod]
        public void TestParseVbkv()
        {
            // TODO: use proper test data file with all data types
            var options = new KvSerializerOptions {HasVbkvHeader = true};

            var bytes = File.ReadAllBytes("Resources/TestData/Vdf/Binary/Vbkv/chat.cfg");
            using (var stream = BufferUtilities.CreateStream(bytes))
            {
                var data = KvSerializer.Create(KvSerializationFormat.KeyValues1Binary)
                    .Deserialize(stream, options);
                using (var mem = new MemoryStream())
                {
                    KvSerializer.Create(KvSerializationFormat.KeyValues1Binary)
                        .Serialize(mem, data, options);
                    var testBytes = mem.ToArray();

                    Assert.IsTrue(bytes.SequenceEqual(testBytes));
                }
            }
        }

        /**
        [TestMethod]
        public void TestParseAch()
        {
            using (var stream = File.OpenRead("Resources/TestData/GameStats/Steam/schema_220.bin"))
            {
                var data = KVSerializer.Create(KVSerializationFormat.KeyValues1Binary)
                    .Deserialize(stream);
            }
        }*/
    }
}
