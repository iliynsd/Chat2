using System;
using System.Net.Sockets;

namespace TCPLibrary
{
    public class TCPClient : TCP
    {
        public TCPClient() : base() {}

        public TCPClient(string ipAddress, int port) : base(ipAddress, port) {}

        public TCPClient(Socket socket)
        {
            _socket = socket ?? throw new Exception("Ошибка создания клиента");
        }
        
        public bool Connect()
                {
                    try
                    {
                        _socket.Connect(_ipServer);
                    }
                    catch (SocketException)
                    {
                        return false;
                    }
        
                    return true;
                }
    }
}