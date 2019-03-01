using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValveMultitool.Tests.Parser
{
    [TestClass]
    public abstract class ParserTests
    {
        /// <summary>
        /// Tests how the parser handles
        /// parsing an object to data structures, and
        /// tests the consistency of the data the parser saves back to disk.
        /// </summary>
        [TestMethod]
        public abstract void TestParse();
    }
}
