using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValveMultitool.Tests.Parser.BinaryVdfTests
{
    [DeploymentItem("Resources/TestData/Vdf/Binary")]
    public class BinaryVdfTestBase
    {
        protected readonly IDictionary<string, byte[]> TestBytes = new Dictionary<string, byte[]>();
        protected BinaryVdfTestBase()
        {
            // load all files for testing
            foreach (var file in Directory.GetFiles("Resources/TestData/Vdf/Binary"))
                TestBytes.Add(Path.GetFileNameWithoutExtension(file), File.ReadAllBytes(file));
        }
    }
}
