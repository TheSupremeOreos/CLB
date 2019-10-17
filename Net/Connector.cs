using System;
using System.Net;
using System.Net.Sockets;
using PixelCLB.Net.Events;
using System.Threading;

namespace PixelCLB.Net
{
    public class Connector
    {
        public event EventHandler<ConnectedEventArgs> OnClientConnected;
        private Client client;
        public System.Timers.Timer connectionTimeout;
        private int timeout = 12;
        private readonly Socket _conSock;
        public string ip = "";
        public short port = 0;
        public Connector(Client c)
        {
            client = c;
            _conSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public delegate void ClientConnectedHandler(Session session);
        public event Connector.ClientConnectedHandler con_OnClientConnected;

        public void Connect(string pIP, short pPort)
        {
            //_conSock.BeginConnect(pIP, pPort, ConnectCallback, null);
            connectionTimeout = new System.Timers.Timer();
            connectionTimeout.Interval = timeout * 1000;
            connectionTimeout.Elapsed += new System.Timers.ElapsedEventHandler(connectionTimeout_Timeout);
            connectionTimeout.Start();
            ip = pIP;
            port = pPort;
            _conSock.BeginConnect(pIP, pPort, ConnectCallback, null);
        }
        public void Connect(IPAddress pIP, short pPort)
        {
            _conSock.BeginConnect(pIP, pPort, ConnectCallback, null);
        }
        public void Connect(IPEndPoint pEndPoint)
        {
            _conSock.BeginConnect(pEndPoint, ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult pIAR)
        {
            try
            {
                _conSock.EndConnect(pIAR);
                //client.updateLog("[Connection] Connection Established");
                ConnectedEventArgs eventArgs = new ConnectedEventArgs(_conSock, ip, port);
                if (OnClientConnected != null)
                {
                    OnClientConnected(this, eventArgs);
                    if (connectionTimeout != null)
                        connectionTimeout.Dispose();
                }
                eventArgs.NetworkSession.WaitForInit();
            }
            catch (Exception ex)
            {
                if (OnClientConnected != null)
                {
                    ConnectedEventArgs eventArgs = new ConnectedEventArgs(ex.Message, ip, port);
                    OnClientConnected(this, eventArgs);
                }
                if (connectionTimeout != null)
                    connectionTimeout.Dispose();
                client.updateLog("[Connection] Connection Failed");
                client.forceDisconnect(true, 1, false, "Connection Failed");
            }
        }

        private void connectionTimeout_Timeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (connectionTimeout != null)
                connectionTimeout.Dispose();
            _conSock.Close();
        }

        public void addConnectedHandler()
        {
            int attempts = 0;
            while (OnClientConnected == null)
            {
                OnClientConnected += new EventHandler<ConnectedEventArgs>(client.con_OnClientConnected);
                if (OnClientConnected != null)
                    return;
                else
                    attempts++;
                if (attempts >= 10)
                {
                    client.updateLog("[Connection] Connection handler failed");
                    client.forceDisconnect(true, 3, false, "Connection handler failed");
                }
            }
        }
    }
}
