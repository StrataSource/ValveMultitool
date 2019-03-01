using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using SteamKit2.GC.Dota.Internal;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Common.Ipc
{
    public class ValveIpcManager
    {
        private readonly MemoryMappedFile _ipcFile;
        private readonly Mutex _mutex = new Mutex(false, $"{ProtocolName}_MTX_{ProtocolVersion}");

        internal const int ProtocolVersion = 1;
        internal const long ProtocolMemory = 256 * 1024; // 256 KB
        internal const string ProtocolName = "VALVE_IPC_MGR_";

        public ValveIpcManager()
        {
            _ipcFile = MemoryMappedFile.CreateOrOpen($"{ProtocolName}_MEM_{ProtocolVersion}", ProtocolMemory);
        }

        public ValveIpcServerEntry DiscoverServer(string serverName)
        {
            return DiscoverServers().FirstOrDefault(s => s.Name == serverName);
        }

        /// <summary>
        /// Reads the memory-mapped file to discover servers.
        /// </summary>
        public IEnumerable<ValveIpcServerEntry> DiscoverServers()
        {
            _mutex.WaitOne();
            var list = new List<ValveIpcServerEntry>();
            using (var stream = _ipcFile.CreateViewStream())
            {
                do
                {
                    var entry = ValveIpcServerEntry.Parse(stream);
                    if (entry == null) break;
                    list.Add(entry);
                } while (stream.Position < stream.Length);
            }

            _mutex.ReleaseMutex();
            return list;
        }
    }
}
