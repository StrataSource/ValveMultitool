using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValveMultitool.Models.Formats.GameStats.Legacy;

namespace ValveMultitool.Tests.Parser
{
    [TestClass]
    [DeploymentItem("Resources/TestData/GameStats/Legacy")]
    public class GameStatsTests : ParserTests
    {
        private readonly IDictionary<string, byte[]> _testBytes = new Dictionary<string, byte[]>();

        public GameStatsTests()
        {
            // load all files for testing
            foreach (var file in Directory.GetFiles("Resources/TestData/GameStats/Legacy"))
            {
                var type = file.Split(@"\").Last().Replace("_gamestats.dat", string.Empty);
                _testBytes.Add(type, File.ReadAllBytes(file));
            }
        }

        [TestMethod]
        public void Behaviour()
        {
            Assert.ThrowsException<InvalidOperationException>(() => { LegacyGameStats.Parse(new byte[0], "test123"); }); // invalid type
            Assert.ThrowsException<TargetInvocationException>(() => { LegacyGameStats.Parse(new byte[0], "ep2"); });     // empty byte array
        }

        [TestMethod]
        public override void TestParse()
        {
            foreach (var file in _testBytes)
            {
                var stats = LegacyGameStats.Parse(file.Value, file.Key);
                Assert.IsNotNull(stats);
                Assert.IsNotNull(stats.Stats);
                Assert.IsNotNull(stats.Stats.Summary);

                var inner = stats.Stats;
                var summary = inner.Summary;

                Assert.AreEqual(false, inner.CyberCafe);
                Assert.AreEqual(95, inner.DxLevel);
                Assert.AreEqual(0, inner.SecondsToCompleteGame);
                Assert.AreEqual(false, inner.Steam);

                Assert.AreEqual(0, summary.Captions);
                Assert.AreEqual(0, summary.Commentary);
                Assert.AreEqual(false, summary.CyberCafe);
                Assert.AreEqual(0, summary.Hdr);
                Assert.AreEqual(false, summary.Steam);

                switch (file.Key)
                {
                    case "ep2":
                        Assert.AreEqual(11, inner.Hl2ChapterUnlocked);
                        Assert.AreEqual(10, inner.MapTotals.Count);
                        Assert.AreEqual(39, summary.Count);
                        Assert.AreEqual(5, summary.Deaths);
                        Assert.AreEqual(4354, summary.Seconds);
                        break;
                    case "portal":
                        Assert.AreEqual(0, inner.Hl2ChapterUnlocked);
                        Assert.AreEqual(6, inner.MapTotals.Count);
                        Assert.AreEqual(3, summary.Count);
                        Assert.AreEqual(0, summary.Deaths);
                        Assert.AreEqual(1087, summary.Seconds);
                        break;
                    default:
                        break;
                }

                

                // Now try to serialise them
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                        stats.SaveToBuffer(writer);
                    var data = stream.ToArray();

                    // Ensure bytes after match exactly our original bytes
                    Assert.IsTrue(file.Value.SequenceEqual(data), $"Original bytes do not match serialised bytes for type \"{file.Key}\".");
                }
            }
        }
    }
}
