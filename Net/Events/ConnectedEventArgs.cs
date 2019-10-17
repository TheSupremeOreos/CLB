using System;
using System.Net.Sockets;
using System.Threading;

namespace PixelCLB.Net.Events
{
    public sealed class ConnectedEventArgs : EventArgs
    {
        public Session NetworkSession { get; private set; }
        public bool Connected { get; private set; }
        public string Exception { get; private set; }
        public string ip { get; private set; }
        public short port { get; private set; }

        public ConnectedEventArgs(Socket pSocket, string IP, short Port)
        {
            Connected = true;
            ip = IP;
            port = Port;
            NetworkSession = new Session(pSocket, IP, Port);
        }

        public ConnectedEventArgs(string pException, string IP, short p)
        {
            ip = "";
            port = 0;
            Connected = false;
            Exception = pException;
        }
    }
}
