using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using ValveMultitool.Utilities;
using ValveMultitool.Utilities.Extensions;

namespace ValveMultitool.Common.Ipc
{
    public class ValveIpcClient : IDisposable
    {
        private readonly ValveIpcManager _manager = new ValveIpcManager();
        private readonly NamedPipeClientStream _stream;
        public bool Connected => _stream?.IsConnected ?? false;

        public ValveIpcClient(string serverName)
        {
            var server = _manager.DiscoverServer(serverName);
            if (server == null) return;

            // Test if the server is still alive
            if (!Mutex.TryOpenExisting($"{server.Guid}_ALIVE_{ValveIpcManager.ProtocolVersion}", out var result)) return;
            result.Close();

            _stream = new NamedPipeClientStream($"{server.Guid}_PIPE_{ValveIpcManager.ProtocolVersion}");
            _stream.Connect();
        }

        public string Invoke(string command)
        {
            var str = Encoding.UTF8.GetBytes(command + '\0');
            _stream.Write(str, 0, str.Length);
            _stream.WaitForPipeDrain();
            using (var reader = new BinaryReader(_stream, Encoding.UTF8, true))
                return reader.ReadNullTerminatedString();
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}
