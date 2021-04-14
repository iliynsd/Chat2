using System;
using System.Threading.Tasks;
using TCPLibrary;

namespace Server
{
    internal static class Program
    {
        private static void Main()
        {
            ShowInfo("Сервер запущен");
            var server = new TCPServer("192.168.0.168", 8005);
            server.Start();
            ShowInfo("Ожидаю подключение...");
            while (true)
            {
                var client = server.NewClient();

                var task = Task.Run( () => TaskClient(client) );
            }
        }

        static void TaskClient(TCPClient client)
        {
            ShowInfo("Клиент подключился");
            while (true)
            {
                var messageReceive = client.GetMessage();
                if (messageReceive == @"\stop")
                {
                    ShowInfo("Клиент отключился...");
                    break;
                }
                Console.WriteLine(messageReceive);

                client.SendMessage("Сообщение получено");
            }
            client.Close();
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}