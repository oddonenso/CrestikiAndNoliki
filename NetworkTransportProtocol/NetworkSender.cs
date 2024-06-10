using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLib;

namespace NetworkTransportProtocol
{
    public class NetworkSender : ISend
    {

        private StreamWriter _writer;
        public NetworkSender(StreamWriter writer)
        {
            ArgumentNullException.ThrowIfNull(writer);
            _writer = writer;
        }
        public void Send(CommandType type)
        {
            Send(type.ToString());  
        }

        public void Send(string message)
        {
            _writer.WriteLine(message);
            _writer.Flush(); //дальше сообщения не будэ. пауза моежду передачей сообщения
        }
    }
}
