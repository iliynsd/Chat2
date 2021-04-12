using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal static class Program
    {
        private static void Main()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            const int PORT = 8005;
            var ipServer = new IPEndPoint(ip, PORT);

            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipServer);

            while (true)
            {
                var message = Console.ReadLine();
                var data = Encoding.Unicode.GetBytes(message);
                server.Send(data);
            
                var msg = new StringBuilder();
                var dataSend = new byte[256];
                do
                {
                    var bytes = server.Receive(data);
                    msg.Append(Encoding.Unicode.GetString(dataSend, 0, bytes));
                } while (server.Available > 0);
                Console.WriteLine($"{DateTime.Now:u}: {msg}");
            }
            
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}