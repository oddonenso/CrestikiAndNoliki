using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLib;

namespace NetworkTransportProtocol
{
    public class NetworkReceiver : IReceive
    {

        private StreamReader _reader;
        
     public NetworkReceiver(StreamReader reader)
        {
            ArgumentNullException.ThrowIfNull(reader);
            _reader = reader;   
        }
        public CommandType GetMessage()
        {
            string? message = _reader.ReadLine();
            Enum.TryParse(message, true, out CommandType commandType); //true проверка для регистера(I,i)
            return commandType;
        }

        public string GetMessageToString()
        {
            return _reader.ReadLine();
        }
    }
}
