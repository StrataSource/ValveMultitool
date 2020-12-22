using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValveMultitool.Models.Formats.Steam.Distribution.Apps;
using ValveMultitool.Models.Formats.Vdf.Binary;

namespace ValveMultitool.Tests.Parser.BinaryVdfTests
{
    [TestClass]
    [DeploymentItem("Resources/TestData/Vdf/Binary")]
    public class BinaryVdfSerialisationTests : BinaryVdfTestBase
    {
        private BinaryVdf _testAppInfo = new BinaryVdf
        {
            new BinaryVdfAppHeader
            {
                AccessToken = 0,
                AppId = 220,
                ChangeNumber = 5046652,
                CheckSum = new byte[]
                {
                    0x21, 0xfd, 0xa6, 0x7c, 0x8a,
                    0x96, 0x05, 0x9a, 0x76, 0xd2,
                    0x1f, 0xd8, 0x06, 0x7b, 0x9b,
                    0xef, 0x3c, 0x2f, 0xa5, 0x6b
                },
                LastUpdate = 1542807848,
                Size = 4680,
                State = 2,

                Body = new AppInfo { AppId = 220 }
            }
        };

        [TestMethod]
        public void TestSerialise()
        {
            foreach (var file in TestBytes)
            {

            }
        }
    }
}
