using PixelCLB.Crypto;
using PixelCLB.Tools;
using PixelCLB.Net.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using PixelCLB;
using System.Threading;

namespace PixelCLB.Net
{
    public class Session
    {
        public event EventHandler<HandshakeEventArgs> OnHandshakeHandler;
        public event EventHandler<PacketEventArgs> OnPacketRecvHandler;
        public event EventHandler<PacketEventArgs> OnPacketSendHandler;
        public event EventHandler<DisconnectedEventArgs> OnDisconnectedHandler;

        private List<EventHandler<DisconnectedEventArgs>> delegates = new List<EventHandler<DisconnectedEventArgs>>();
        public bool disconnectHandlerActive = false;
        
        private readonly Socket _sock;
        public MapleCrypt _sendAES;
        public MapleCrypt _recvAES;

        public Socket Socket { get { return _sock; } }
        public string ip { get; private set; }
        public short port { get; private set; }

        public Session(Socket pSocket, string IP, short p)
        {
            ip = IP;
            port = p;
            _sock = pSocket;
        }

        public void WaitForInit()
        {
            RecBuffer recBuffer = new RecBuffer(2, false);
            Thread th = new Thread(delegate() { ReceivePacket(recBuffer); });
            th.Start(); 
            //ReceivePacket(recBuffer);
        }
        public void WaitForPacket()
        {
            RecBuffer recBuffer = new RecBuffer(4, true);
            ReceivePacket(recBuffer);
        }
        private void ReceivePacket(RecBuffer pBuffer)
        {
            _sock.BeginReceive(pBuffer.Data, pBuffer.Index, pBuffer.Position, SocketFlags.None, new AsyncCallback(PacketCallback), pBuffer);
            //_sock.BeginReceive(pRawPacket, 0, pRawPacket.Length, SocketFlags.None, SendCallback, pAsyncState); //DO NOT USE
        }

        public void addDisconnectHandler(EventHandler<DisconnectedEventArgs> d)
        {
            OnDisconnectedHandler += d;
            delegates.Add(d);
            disconnectHandlerActive = true;
        }

        public void removeDisconnectHandlers()
        {
            foreach (var d in delegates)
            {
                OnDisconnectedHandler -= d;
            }
            delegates.Clear();
            disconnectHandlerActive = false;
        }

        private void PacketCallback(IAsyncResult pIAR)
        {
            try
            {
                int rec = _sock.EndReceive(pIAR);
                if (rec == 0 && OnDisconnectedHandler != null)
                {
                    DisconnectedEventArgs eventArgs = new DisconnectedEventArgs(DisconnectType.Server);
                    OnDisconnectedHandler(this, eventArgs);
                    return;
                }

                RecBuffer recBuffer = (RecBuffer)pIAR.AsyncState;
                recBuffer.Index += rec;

                if (recBuffer.IsReady)
                    HandleData(recBuffer);
                else
                    ReceivePacket(recBuffer);
            }
            #region Error handler
            /*
        catch (SocketException se)
        {
                
            if (se.SocketErrorCode == SocketError.ConnectionAborted || se.SocketErrorCode == SocketError.ConnectionReset)
            {
                if (OnDisconnected != null)
                {
                    OnDisconnected(this);
                }
            }
            else
            {
                Extensions.Logger.LogMessage("Error - PacketCallback - " + se.ToString());
            }
        }
        catch (ObjectDisposedException)
        {
            Extensions.Logger.LogMessage("Error - PacketCallback - Socket closed.");
        }
        catch (NullReferenceException)
        {
            Extensions.Logger.LogMessage("Error - PacketCallback - NullReferenceException.");
        }
             */
            #endregion
            catch (Exception ex)
            {
                if (OnDisconnectedHandler != null)
                {
                    DisconnectedEventArgs eventArgs = new DisconnectedEventArgs(ex.ToString());
                    OnDisconnectedHandler(this, eventArgs);
                }
            }
        }
        private void HandleData(RecBuffer pBuffer)
        {
            if (pBuffer.IsHeader)
            {
                byte[] headerData = pBuffer.Data;

                int packetSize = pBuffer.IsEncrypted ?
                    MapleCrypt.GetPacketLength(headerData) :
                    BitConverter.ToInt16(headerData, 0);

                pBuffer.Reset(false, packetSize);
                ReceivePacket(pBuffer);
            }
            else
            {
                if (pBuffer.IsEncrypted)
                {
                    byte[] packetData = pBuffer.Data;

                    lock (_recvAES)
                    {
                        _recvAES.Transform(packetData);
                    }
                    if (Program.usingMapleCrypto)
                        MapleCustom.Decrypt(packetData);

                    if (OnPacketRecvHandler != null)
                    {
                        PacketEventArgs eventArgs = new PacketEventArgs(packetData);
                        OnPacketRecvHandler(this, eventArgs);
                    }

                    pBuffer.Reset(true, 4);
                    ReceivePacket(pBuffer);
                }
                else
                {
                    using (PacketReader pPacket = new PacketReader(pBuffer.Data))
                    {

                        ushort Version = pPacket.ReadUShort();
                        string Subversion = pPacket.ReadString();

                        byte[] SIV = pPacket.ReadBytes(4);
                        byte[] RIV = pPacket.ReadBytes(4);

                        _sendAES = new MapleCrypt(Version, SIV, false);
                        _recvAES = new MapleCrypt(Version, RIV, false);

                        byte Locale = pPacket.Read();

                        if (OnHandshakeHandler != null)
                        {
                            HandshakeEventArgs eventArgs = new HandshakeEventArgs(Version, Subversion, SIV, RIV, Locale);
                            OnHandshakeHandler(this, eventArgs);
                        }

                        pBuffer.Dispose();
                        WaitForPacket();
                    }
                }
            }
        }

        public void SendPacket(byte[] pPacket)
        {
            int packetLength = pPacket.Length;
            byte[] safeGaurd = pPacket.QuickClone();
            byte[] sendData = new byte[packetLength + 4];

            if (Program.usingMapleCrypto)
                MapleCustom.Encrypt(pPacket);

            lock (_sendAES)
            {
                byte[] header = _sendAES.GetHeaderToServer(packetLength);
                _sendAES.Transform(pPacket);

                System.Buffer.BlockCopy(header, 0, sendData, 0, 4);
            }

            System.Buffer.BlockCopy(pPacket, 0, sendData, 4, packetLength);
            SendRawPacket(sendData, safeGaurd);
        }

        public void SendRawPacket(byte[] pRawPacket, object pAsyncState)
        {
            try
            {
                _sock.BeginSend(pRawPacket, 0, pRawPacket.Length, SocketFlags.None, SendCallback, pAsyncState);
            }
            catch
            {
            }
        }

        public void ReceivePacket(byte[] pPacket)
        {
            int packetLength = pPacket.Length;
            byte[] safeGaurd = pPacket.QuickClone();
            byte[] sendData = new byte[packetLength + 4];

            if (Program.usingMapleCrypto)
                MapleCustom.Encrypt(pPacket);

            lock (_recvAES)
            {
                byte[] header = _recvAES.GetHeaderToServer(packetLength);
                _recvAES.Transform(pPacket);

                System.Buffer.BlockCopy(header, 0, sendData, 0, 4);
            }

            System.Buffer.BlockCopy(pPacket, 0, sendData, 4, packetLength);
            ReceiveRawPacket(sendData, safeGaurd);
        }

        public void ReceiveRawPacket(byte[] pRawPacket, object pAsyncState)
        {
            try
            {

                //_sock.BeginReceive(pBuffer.Data, pBuffer.Index, pBuffer.Position, SocketFlags.None, new AsyncCallback(PacketCallback), pBuffer);
                _sock.BeginReceive(pRawPacket, 0, pRawPacket.Length, SocketFlags.None, SendCallback, pAsyncState);
            }
            catch
            {
            }
        }

        private void SendCallback(IAsyncResult pIAR)
        {
            try
            {
                _sock.EndSend(pIAR);

                if (OnPacketSendHandler != null)
                {
                    byte[] packet = pIAR.AsyncState as byte[];
                    PacketEventArgs eventArgs = new PacketEventArgs(packet);
                    OnPacketSendHandler(this, eventArgs);
                }
            }
            catch (System.Exception ex)
            {
                if (OnDisconnectedHandler != null)
                {
                    DisconnectedEventArgs eventArgs = new DisconnectedEventArgs(ex.Message);
                    OnDisconnectedHandler(this, eventArgs);
                }
            }
        }

        public void ShutDown()
        {
            try
            {
                _sock.Shutdown(SocketShutdown.Both);
            }
            catch { }
           _sock.Close();
        }
    }
}
