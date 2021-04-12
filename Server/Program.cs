using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal static class Program
    {
        private static void Main()
        {
            var ip = IPAddress.Parse("192.168.0.53");
            const int PORT = 8005;
            var ipServer = new IPEndPoint(ip, PORT);

            var listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ipServer);
            listen.Listen(10);

            while (true)
            {
                var client = listen.Accept();

                var task = Task.Run(() => NewClient(client));
            }
        }

        static void NewClient(Socket client)
        {
            var message = new StringBuilder();
            var data = new byte[256];
            while (true)
            {
                message.Clear();
                do
                {
                    var bytes = client.Receive(data);
                    message.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (client.Available > 0);
                Console.WriteLine($"{DateTime.Now:u}: {message}");

                const string MSG = "Сообщение получено";
                var dataSend = Encoding.Unicode.GetBytes(MSG);
                client.Send(dataSend);
            }
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}