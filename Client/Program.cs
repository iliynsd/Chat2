using TCPLibrary;

namespace Client
{
    internal static class Program
    {
        private static void Main()
        {
            var client = new TCPClient();
            client.Connect();
            client.SendMessage("Hello");
            client.Close();
        }
    }
}