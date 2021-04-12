using System;
using TCPLibrary;

namespace Server
{
    internal static class Program
    {
        private static void Main()
        {
            var server = new TCPServer();
            server.Start();
            while (true)
            {
                var client = server.NewClient();
                Console.WriteLine(client.GetMessage());
                client.Close();
            }
        }
    }
}