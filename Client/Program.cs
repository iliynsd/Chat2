using System;
using System.Text.Json;
using MessageModel;
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
            client.SendMessage(MessageTypeName(name));

            while (true)
            {
                Console.Write("Сообщение: ");
                var messageSend = Console.ReadLine();
                if (messageSend == @"\stop")
                {
                    client.SendMessage(MessageTypeStop());
                    Console.WriteLine("Вы отключились...");
                    break;
                }
                client.SendMessage(MessageTypeMessage(messageSend));

                var msg_temp = JsonSerializer.Deserialize<Message>(client.GetMessage());
                if (msg_temp.Type == TypeMessage.Message)
                {
                    Console.WriteLine($"Сообщение от сервера: {msg_temp.Msg}");
                }
            }
            
            client.Close();
        }

        static string MessageTypeName(string name)
        {
            var msg = new Message
            {
                Type = TypeMessage.Name,
                Msg = name
            };
            var msg_send = JsonSerializer.Serialize(msg);
            return msg_send;
        }
        
        static string MessageTypeMessage(string message)
        {
            var msg = new Message
            {
                Type = TypeMessage.Message,
                Msg = message
            };
            var msg_send = JsonSerializer.Serialize(msg);
            return msg_send;
        }
        
        static string MessageTypeStop()
        {
            var msg = new Message
            {
                Type = TypeMessage.Stop,
                Msg = ""
            };
            var msg_send = JsonSerializer.Serialize(msg);
            return msg_send;
        }
    }
}