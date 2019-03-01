using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValveMultitool.Models.Formats.Inf;

namespace ValveMultitool.Tests.Parser
{
    [TestClass]
    [DeploymentItem("Resources/TestData/Inf")]
    public class SteamInfParserTests : ParserTests
    {
        [TestMethod]
        public void Behaviour()
        {
            for (var i = 1; i < 4; i++)
            {
                var fileName = $"Resources/TestData/Inf/invalid{i}.inf";
                var data = File.ReadAllLines(fileName);
                Assert.ThrowsException<InvalidOperationException>(() => { SteamInf.Parse(data); });
            }
        }

        [TestMethod]
        public override void TestParse()
        {
            var inf = SteamInf.Parse(File.ReadAllLines("Resources/TestData/Inf/steam.inf"));

            Assert.AreEqual(inf["PatchVersion"], "3.3.1");
            Assert.AreEqual(inf["ProductName"], "INFRA");
            Assert.AreEqual(inf["AppID"], "251110");
        }
    }
}
