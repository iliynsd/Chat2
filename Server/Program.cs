using System;
using System.Collections.Generic;
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

            int count = 0;
            var clients = new Dictionary<int, Socket>();

            while (true)
            {
                var client = listen.Accept();
                count++;
                clients.Add(count, client);
                var count1 = count;
                var task = Task.Run(() => NewClient(client, count1, ref clients));
            }
        }

        static void NewClient(Socket client, int id, ref Dictionary<int, Socket> clients)
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

                var temp = $"#{id} {DateTime.Now:u}: {message}";
                Console.WriteLine(temp);
                
                var dataSend = Encoding.Unicode.GetBytes(temp);

                foreach (var item in clients)
                {
                    item.Value.Send(dataSend);
                }
            }
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}