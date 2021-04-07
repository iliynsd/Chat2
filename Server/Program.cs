using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 8005;
            IPEndPoint ip_server = new IPEndPoint(ip, port);

            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ip_server);
            listen.Listen(10);

            while (true)
            {
                Socket client = listen.Accept();

                StringBuilder message = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];

                do
                {
                    bytes = client.Receive(data);
                    message.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (client.Available > 0);
                
                Console.WriteLine($"{DateTime.Now:u}: {message}");

                string msg = "Сообщение получено";
                byte[] data_send = Encoding.Unicode.GetBytes(msg);
                client.Send(data_send);
                
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}