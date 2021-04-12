using System;

namespace TCPLibrary
{
    public class TCPServer : TCP
    {
        public TCPServer() : base() {}
        public TCPServer(string ipAddress, int port) : base(ipAddress, port) {}

        public bool Start()
        {
            try
            {
                _socket.Bind(_ipServer);
                _socket.Listen(10);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public TCPClient NewClient()
        {
            try
            {
                var client = new TCPClient(_socket.Accept());
                return client;
            }
            catch (Exception)
            {
                throw new Exception("Ошибка соединения с клиентом");
            }
        }
    }
}