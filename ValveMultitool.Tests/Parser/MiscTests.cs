using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ValveKeyValue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValveMultitool.Common.Ipc;
using ValveMultitool.Models.Formats.Steam.Distribution;
using ValveMultitool.Models.Formats.Steam.Network;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Tests.Parser
{
    [TestClass]
    public class MiscTests
    {
        [TestMethod]
        public void Test()
        {
            /**
            using (var stream = File.OpenRead(@"C:\Program Files (x86)\Steam\steam\cached\CellMap.vdf"))
            {
                var data = KvSerializer.Create(KvSerializationFormat.KeyValues1Text).Deserialize<CellMap>(stream);
            }*/

            //using (var client = new ValveIpcClient("HAMMER_IPC_SERVER"))
            //{
            //    if (!client.Connected) return;
            //    var result = client.Invoke("echo hi");
            //}
        }
    }
}
