using System;
using TCPLibrary;

namespace Client
{
    internal static class Program
    {
        private static void Main()
        {
            var client = new TCPClient("192.168.0.168", 8005);
            client.Connect();
            
            Console.Write("Введите имя: ");
            var name = Console.ReadLine();

            while (true)
            {
                Console.Write("Сообщение: ");
                var messageSend = Console.ReadLine();
                client.SendMessage($"{name}: {messageSend}");

                if (messageSend == @"\stop")
                {
                    Console.WriteLine("Вы отключились...");
                    break;
                }

                var messageReceive = client.GetMessage();
                Console.WriteLine(messageReceive);
            }
            
            client.Close();
        }
    }
}