using System.Net.Sockets;
using System.Net;
using TransportLib;
using NetworkTransportProtocol;
using SuperKrestikiNoliki;

class Program
{
    static void Main(string[] args)
    {
        TcpListener tcpListener = new TcpListener(IPAddress.Any, 405);
        tcpListener.Start();
        while (true)
        {
            var client = tcpListener.AcceptTcpClient();
            Task.Run(() => Start(client));
        }
    }

    
    private static Game _game = new Game();
    private static void Start(TcpClient client)
    {
        using (NetworkStream network = client.GetStream())
        {
            ISend send = new NetworkSender(new StreamWriter(network));
            IReceive receive = new NetworkReceiver(new StreamReader(network));
            User user = new User(send, receive);
            while (true)
            {
                CommandType type = receive.GetMessage();

                switch(type)
                {
                    case CommandType.StartGame:
                        {
                            _game.Join(user);
                            while (true)
                            {
                                if (user.MyStepNow == true)
                                {
                                    user.Step(_game._field);
                                }
                            }
                        }
                        break;

                }
            }
        }
    }
}
