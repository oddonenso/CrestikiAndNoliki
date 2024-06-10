using System.Net.Sockets;
using System.Net;
using NetworkTransportProtocol;
using TransportLib;
using ServerTwo;

class Program
{
    static void Main(string[] args)
    {
        TcpListener tcpListener = new TcpListener(IPAddress.Any, 405);
        tcpListener.Start();
        while (true)
        {
            var client = tcpListener.AcceptTcpClient(); //приходит новый клиент
            Task.Run(() => Start(client));
        }
    }
    private static PlayingField field = new PlayingField();
    private static Game game = new Game(); 
    private static void Start(TcpClient client)
    {
        using (NetworkStream network = client.GetStream())
        {
            ISend send = new NetworkSender(new StreamWriter(network));
            IReceive receive = new NetworkReceiver(new StreamReader(network));
            User user = new User(send, receive);
            CommandType commandType = receive.GetMessage();

            if (commandType==CommandType.StartGame)
            {
                if (game.Join(user))
                {
                    user.ClientWait();
                    while (true)
                    {
                        if (user.Wait==false)
                        {
                            string isWin = field.Win();
                            if (isWin!=string.Empty)
                            {
                                CommandType value = isWin == "x" ? CommandType.X : CommandType.O;
                                send.Send(value);
                                game.Next();
                                break;
                            }
                            user.Step(field);
                            game.Next();
                        }
                    }
                }
                else
                {
                    send.Send(CommandType.Exit);
                }
            }
        }
    }

}