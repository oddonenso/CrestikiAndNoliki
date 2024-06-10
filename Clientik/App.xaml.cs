using Clientik.View;
using Clientik.ViewModel;
using NetworkTransportProtocol;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using TransportLib;

namespace Clientik
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            TcpClient tcp = new TcpClient();
            tcp.Connect("127.0.0.1", 405);
            NetworkStream stream = tcp.GetStream();
            ISend send = new NetworkSender(new StreamWriter(stream));
            IReceive receive = new NetworkReceiver(new StreamReader(stream));

            MainViewModel mainViewModel = new MainViewModel(send, receive); 
            MainWindow window = new MainWindow(mainViewModel);
            window.ShowDialog();
        }
    }

}
