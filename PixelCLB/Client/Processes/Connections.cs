using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB.Net.Packets;
using PixelCLB;
using PixelCLB.PacketCreation;
using PixelCLB.Crypto;
using PixelCLB.CLBTools;
using System.Xml;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;


namespace PixelCLB
{
    public partial class Client
    {
        public void waitForOffline(string IP, int port)
        {
            int secs = 3600;
            ClientMode modeClient = clientMode;
            clientMode = ClientMode.WAITFOROFFLINE;
            pictureBoxChange();
            while (clientMode == ClientMode.WAITFOROFFLINE)
            {
                try
                {
                    updateAccountStatus("Waiting for server to go offline");
                    TcpClient tcpClient = new TcpClient(IP, port);
                    if (tcpClient.Connected)
                    {
                        tcpClient.Close();
                        Thread.Sleep(secs * 1000);
                    }
                    else
                    {
                        updateLog("Servers have went offline at " + DateTime.Now);
                        updateAccountStatus("Servers have went offline");
                        clientMode = modeClient;
                        break;
                    }
                }
                catch
                {
                    updateLog("Servers have went offline at " + DateTime.Now);
                    updateAccountStatus("Servers have went offline");
                    clientMode = modeClient;
                    break;
                }
            }
        }

        /*
        public void checkMapleStatus()
        {
            try
            {
                Thread t = new Thread(delegate()
                {
                    try
                    {
                        int tries = 0;
                        int success = 0;
                        clientMode = ClientMode.WAITFORONLINE;
                        pictureBoxChange();
                        updateAccountStatus("Checking MapleStory Login Servers");
                        while (clientMode == ClientMode.WAITFORONLINE || clientMode == ClientMode.DISCONNECTED)
                        {
                            try
                            {
                                TcpClient tcpClient = new TcpClient(Program.serverip, Program.port);
                                if (!tcpClient.Connected)
                                {
                                }
                                else
                                {
                                    tcpClient.Close();
                                    success++;
                                    if (success == 1 & tries == 0)
                                    {
                                        updateAccountStatus("Logging in...");
                                        clientMode = ClientMode.DISCONNECTED;
                                        Connect(Program.serverip, Program.port);
                                        return;
                                    }
                                    else if (success >= 3)
                                    {
                                        if (tries >= 1)
                                        {
                                            updateAccountStatus("MapleStory is online! Logging in...");
                                            updateLog("MapleStory is online!");
                                            clientMode = ClientMode.DISCONNECTED;
                                        }
                                        else
                                        {
                                            updateAccountStatus("Logging in...");
                                            clientMode = ClientMode.DISCONNECTED;
                                        }
                                        Connect(Program.serverip, Program.port);
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                success = 0;
                                tries++;
                                if (tries == 1)
                                {
                                    realMode.Value = "Offline";
                                    updateAccountStatus("MapleStory is currently offline.");
                                    updateLog("MapleStory is currently offline.");
                                    nexonCookie = string.Empty;
                                }
                                Thread.Sleep(1);
                            }
                        }
                    }
                    catch
                    {
                    }
                });
                workerThreads.Add(t);
                t.Start();
            }
            catch { }
        }
        */


        public void checkMapleStatus()
        {
            Thread t = new Thread(delegate()
            {
                try
                {
                    int tries = 0;
                    int success = 0;
                    clientMode = ClientMode.WAITFORONLINE;
                    pictureBoxChange();
                    updateAccountStatus("Checking MapleStory Login Servers");
                    while (clientMode == ClientMode.WAITFORONLINE || clientMode == ClientMode.DISCONNECTED)
                    {
                        try
                        {
                            TcpClient tcpClient = new TcpClient(Program.serverip, Program.port);
                            if (!tcpClient.Connected)
                            {
                            }
                            else
                            {
                                tcpClient.Close();
                                success++;
                                if (success == 1 & tries == 0)
                                {
                                    updateAccountStatus("Logging in...");
                                    clientMode = ClientMode.DISCONNECTED;
                                    getCookieAndConnect();
                                    return;
                                }
                                else if (success >= 2)
                                {
                                    if (tries >= 1)
                                    {
                                        updateAccountStatus("MapleStory is online! Logging in...");
                                        updateLog("MapleStory is online!");
                                        clientMode = ClientMode.DISCONNECTED;
                                    }
                                    else
                                    {
                                        updateAccountStatus("Logging in...");
                                        clientMode = ClientMode.DISCONNECTED;
                                    }
                                    getCookieAndConnect();
                                    return;
                                }
                            }
                        }
                        catch
                        {
                            success = 0;
                            tries++;
                            if (tries == 1)
                            {
                                realMode.Value = "Offline";
                                updateAccountStatus("MapleStory is currently offline.");
                                updateLog("MapleStory is currently offline.");
                                nexonCookie = string.Empty;
                            }
                            Thread.Sleep(1);
                        }
                    }
                }
                catch
                {
                }
            });
            workerThreads.Add(t);
            t.Start();
        }

        private void getCookieAndConnect()
        {
            Connect(Program.serverip, Program.port);
        }

        public bool checkWorldStatus()
        {
            int tries = 0;
            while (clientMode != ClientMode.DISCONNECTED)
            {
                try
                {
                    string IP = Database.getServerIP(world);
                    TcpClient tcpClient = new TcpClient(IP, 8585);
                    if (!tcpClient.Connected)
                    {
                    }
                    else
                    {
                        tcpClient.Close();
                        tries++;
                    }
                }
                catch
                {
                    return false;
                }
                if (tries >= 2)
                    return true;
            }
            return false;
        }

        public void ChannelConnect()
        {
            if (clientMode == ClientMode.LOGIN)
            {
                try
                {

                    //ses.OnDisconnectedHandler -= new EventHandler<DisconnectedEventArgs>(ses_OnDisconnect);
                    /*
                    timeOutCheck = true;
                    while (ses.disconnectHandlerActive)
                    {
                        if (timeOutCheck)
                        {
                            timeOut(1, 2);
                            ses.removeDisconnectHandlers();
                        }
                        Thread.Sleep(1);
                    }
                     */
                    ses.removeDisconnectHandlers();
                    ses.Socket.Disconnect(true);
                    Connect(currentip, currentport);
                    /*
                    timeOut(8, 10);
                    while (ses._sendAES == null || !ses.Socket.Connected)
                    {
                        if (clientMode == ClientMode.DISCONNECTED)
                            break;
                        if (timeOutCheck)
                        {
                            updateLog("[Connection] Connection timed out");
                            forceDisconnect(true, 1, false);
                            return;
                        }
                        Thread.Sleep(1);
                    }
                    serverCheck = true;
                    //ses.SendPacket(PacketHandler.EnterChannel(myCharacter.uid, RAND1, RAND2, cauth).ToArray());
                    while (serverCheck)
                    {
                        Thread.Sleep(1);
                        if (clientMode == ClientMode.DISCONNECTED)
                            return;
                    }
                    */
                    modeDeter.getMode(this, false);
                    Thread threadz = new Thread(() => onServerConnected(false));
                    workerThreads.Add(threadz);
                    threadz.Start();
                    Thread t = new Thread(delegate()
                    {
                        sw.Stop();
                        updateLog("Time to log in: " + sw.Elapsed.TotalMilliseconds.ToString() + "ms");
                        pictureBoxChange();
                        initializeClient();
                    });
                    t.Start();
                }
                catch (Exception e)
                {
                    updateLog("[Error]Failed to connect to channel server.");
                    forceDisconnect(true, 1, false, "Failed to connect to channel server");
                }
            }
        }

        public void csChannel()
        {
            try
            {
                if (!inCS)
                {
                    inCS = true;
                }
                else
                {
                    inCS = false;
                    sent = false;
                }
                //ses.OnDisconnectedHandler -= new EventHandler<DisconnectedEventArgs>(ses_OnDisconnect);
                timeOutCheck = true;
                while (ses.disconnectHandlerActive)
                {
                    if (clientMode == ClientMode.DISCONNECTED)
                        break;
                    if (timeOutCheck)
                    {
                        timeOut(1, 2);
                        ses.removeDisconnectHandlers();
                    }
                    Thread.Sleep(1);
                }
                ses.Socket.Disconnect(true);
                timeOut(8, 10);
                Connect(currentip, currentport);
                while (!ses.Socket.Connected || ses._sendAES == null)
                {
                    Thread.Sleep(1); 
                    if (clientMode == ClientMode.DISCONNECTED)
                        break;
                    if (timeOutCheck)
                    {
                        updateLog("[Connection] Connection timed out");
                        forceDisconnect(true, 1, false, "Connection timed out");
                        return;
                    }
                }
                //ses.SendPacket(PacketHandler.EnterChannel(myCharacter.uid, RAND1, RAND2, cauth).ToArray());
            }
            catch
            {
                updateLog("[Error]Failed to connect to channel server.");
            }
        }

        public void reset()
        {
            foreach (Thread t in workerThreads)
            {
                if (t.IsAlive)
                {
                    t.Join(100);
                    if (t.IsAlive)
                    {
                        try
                        {
                            t.Abort();
                        }
                        catch { }
                    }
                }
            }
            clientMode = ClientMode.IDLE;
            try
            {
                freeSpot = false;
                Thread t = new Thread(delegate()
                {
                    reRun = true;
                    cashShopManagement(false, false, 0, 0);
                    reRun = false;
                    onServerConnected(false);
                });
                workerThreads.Add(t);
                t.Start();
            }
            catch { }
        }

        /// <summary>
        /// Disconnect / Restart
        /// </summary>
        public void forceDisconnect(bool restart, int secs, bool ignoreMessage, string reason, bool userTerminate = false)
        {
            if (clientMode != ClientMode.DISCONNECTED && clientMode != ClientMode.WAITFOROFFLINE && clientMode != ClientMode.WAITFORONLINE && clientMode != ClientMode.LOGIN && clientMode != ClientMode.LOGGEDIN && !userTerminate)
            {
                disconnectProcess(restart, secs, ignoreMessage, reason);
            }
            else if (clientMode == ClientMode.DISCONNECTED && !userTerminate)
            {
                return;
            }
            else if ((clientMode == ClientMode.WAITFOROFFLINE || clientMode == ClientMode.WAITFORONLINE || clientMode == ClientMode.LOGIN || clientMode == ClientMode.LOGGEDIN) && !userTerminate)
            {
                ClientMode Compare = clientMode;
                Thread.Sleep(1500);
                if ((clientMode == Compare) && !userTerminate)
                {
                    disconnectProcess(restart, secs, ignoreMessage, reason);
                }
            }
            else if (userTerminate)
            {
                disconnectProcess(restart, secs, ignoreMessage, reason);
            }
        }

        private void disconnectProcess(bool restart, int secs, bool ignoreMessage, string reason)
        {
            clientMode = ClientMode.DISCONNECTED;
            pictureBoxChange();
            Players.Clear();
            mapleFMShopCollection.shops.Clear();
            updateLog("Disconnect Reason: " + reason);
            try
            {
                Program.gui.oldIndex = -1;
                if (connector != null)
                    if (connector.connectionTimeout != null)
                        connector.connectionTimeout.Dispose();
            }
            catch
            {
                updateLog("Termination Error 0");
            }

            try
            {
                if (Program.clientsStarted.Contains(accountProfile))
                    Program.clientsStarted.Remove(accountProfile);
                if (!ignoreMessage)
                {
                    if (Program.clients.Contains(this))
                        Program.clients.Remove(this);
                    if (!Program.endedClients.Contains(this))
                        Program.endedClients.Add(this);
                    updateAccountStatus("Disconnected");
                }
                Program.gui.updateActiveAccounts(false);
                if (Program.terminatedBots != null)
                    Program.terminatedBots.loadBotList();
            }
            catch
            {
                updateLog("Termination Error 1");
            }

            try
            {
                if (hackShield != null)
                {
                    hackShield.stopHackShieldTimer();
                    hackShield = null;
                }
            }
            catch
            {
                updateLog("Termination Error 2");
            }

            try
            {
                //ses.OnDisconnectedHandler -= new EventHandler<DisconnectedEventArgs>(ses_OnDisconnect);
                clientStartAble = false;
                execute = DateTime.Now;
                timeOutCheck = true;
                if (ses != null)
                {
                    while (ses.disconnectHandlerActive)
                    {
                        if (timeOutCheck)
                        {
                            timeOut(1, 2);
                            ses.removeDisconnectHandlers();
                        }
                        Thread.Sleep(1);
                    }
                    ses.ShutDown();
                }
            }
            catch { }

            try
            {
                updateLog("[Disconnected] " + DateTime.Now.ToString("MMMM dd, yyyy h:mm:ss tt"));
                try
                {
                    List<Thread> threadlist = workerThreads.ToList<Thread>();
                    lock (threadlist)
                    {
                        foreach (Thread t in threadlist)
                        {
                            if (t.IsAlive)
                            {
                                try
                                {
                                    t.Abort();
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }
                if (restart)
                {
                    if (secs > 1)
                    {
                        updateLog("[Status] Bot will resume in " + secs + " seconds");
                    }
                    secs = secs + 1;
                    Program.gui.startBot(accountProfile, true, regLogin, false, secs * 1000);
                    return;
                }
                else
                {
                    updateLog("[Status] Bot has fully terminated.");
                }
            }
            catch (Exception e)
            {
                //if (clientMode != ClientMode.SHOPRESET & clientMode != ClientMode.SHOPCLOSE & clientMode != ClientMode.FMOWL)
                //  updateLog("Error terminating bot!");
                if (restart)
                {
                    if (secs > 1)
                    {
                        updateLog("[Status] Bot will resume in " + secs + " seconds");
                    }
                    secs = secs + 1;
                    Program.gui.startBot(accountProfile, true, regLogin, false, secs * 1000);
                    return;
                }
            }
        }


        public void cashShopManagement(bool restartSW, bool resetStores, int min, int max)
        {
            try
            {
                if (clientMode != ClientMode.DISCONNECTED)
                {
                    Players.Clear();
                    mapleFMShopCollection.shops.Clear();
                    int orginalChannel = channel;
                    if (Program.resetMethod)
                    {
                        enterCS();
                    }
                    else
                    {
                        changeChannel(true, 0);
                        updateAccountStatus("Spawned in channel " + channel++.ToString());
                    }
                    if (reRun)
                        Thread.Sleep(3000);
                    timeOutCheck = true;
                    exitCS(restartSW, resetStores, min, max, orginalChannel);
                }
            }
            catch (Exception e)
            {
                if (Program.resetMethod)
                    updateLog("Malfunction with entering cashshop!");
                else
                    updateLog("Malfunction with changing channels");
            }
        }

        public void enterCS()
        {
            int tries = 0;
            try
            {
                csCheck = true;
                timeOutCheck = true;
                connectionCheck = false;
                while (csCheck & clientMode != ClientMode.DISCONNECTED)
                {
                    if (timeOutCheck & !connectionCheck)
                    {
                        tries++;
                        if (tries >= 6)
                        {
                            forceDisconnect(true, 1, false, "Enter CS Failure");
                        }
                        updateAccountStatus("Entering cash shop");
                        if (!connectionCheck)
                            ses.SendPacket(PacketHandler.EnterCS().ToArray());
                        timeOut(4, 5);
                    }
                    Thread.Sleep(1);
                }
                updateAccountStatus("Entered cash shop");
            }
            catch (Exception e)
            {
                updateLog("[Cash Shop] Malfunction entering cashshop!");
                forceDisconnect(true, 1, false, "Malfunction entering CS");
                return;
            }
        }

        public void EnterFarm(int UID)
        {
            int tries = 0;
            try
            {
                csCheck = true;
                timeOutCheck = true;
                connectionCheck = false;
                while (csCheck & clientMode != ClientMode.DISCONNECTED)
                {
                    if (timeOutCheck & !connectionCheck)
                    {
                        tries++;
                        if (tries >= 6)
                        {
                            forceDisconnect(true, 1, false, "Malfunction entering monster farm");
                        }
                        updateAccountStatus("Entering Monster Farm");
                        if (!connectionCheck)
                            ses.SendPacket(PacketHandler.EnterFarm(UID).ToArray());
                        timeOut(4, 5);
                    }
                    Thread.Sleep(1);
                }
                updateAccountStatus("Entered Monster Farm");
            }
            catch (Exception e)
            {
                updateLog("[Cash Shop] Malfunction entering monster farm!");
                forceDisconnect(true, 1, false, "Malfunction entering monster farm");
                return;
            }
        }

        public void ExitFarm()
        {
            int tries = 0;
            try
            {
                serverCheck = true;
                timeOutCheck = true;
                connectionCheck = false;
                while (serverCheck & clientMode != ClientMode.DISCONNECTED)
                {
                    if (timeOutCheck & !connectionCheck)
                    {
                        if (tries >= 10)
                            forceDisconnect(true, 1, false, "Error exiting monster farm");
                        timeOut(2, 3);
                        if (Program.resetMethod)
                        {
                            updateAccountStatus("Exiting monster farm");
                            if (!connectionCheck)
                                ses.SendPacket(PacketHandler.ExitFarm().ToArray());
                        }
                        tries++;
                    }
                    Thread.Sleep(1);
                }
            }
            catch (Exception e)
            {
                updateLog("[Monster Farm] Malfunction exiting monster farm!");
                forceDisconnect(true, 1, false, "Malfunction exiting monster farm!");
                return;
            }
        }




        public void exitCS(bool restartSW, bool resetStores, int min, int max, int orginalChannel)
        {
            try
            {
                int tries = 0;
                if (resetStores)
                {
                    storeTries = new Random().Next(min, max);
                }
                if (restartSW)
                {
                    timeBeforeCS = new Random().Next(min, max);
                }
                serverCheck = true;
                timeOutCheck = true;
                connectionCheck = false;
                while (serverCheck & clientMode != ClientMode.DISCONNECTED)
                {
                    if (timeOutCheck & !connectionCheck)
                    {
                        if (tries >= 10)
                            forceDisconnect(true, 1, false, "Exit CS Fail");
                        timeOut(2, 3);
                        if (Program.resetMethod)
                        {
                            updateAccountStatus("Exiting cash shop");
                            if (!connectionCheck)
                                ses.SendPacket(PacketHandler.ExitCS().ToArray());
                        }
                        else
                        {
                            updateAccountStatus("Changing back to original channel (" + orginalChannel.ToString() + ")");
                            changeChannel(false, orginalChannel);
                        }
                        tries++;
                    }
                    Thread.Sleep(1);
                }
            }
            catch
            {
                if (Program.resetMethod)
                    updateLog("Malfunction with exiting cash shop!");
                else
                    updateLog("Malfunction with changing channels");
            }
        }


        public void changeChannel(bool addChannel, int channelNum = 0)
        {
            try
            {
                int addChannels = 1;
                Players.Clear();
                mapleFMShopCollection.shops.Clear();
                serverCheck = true;
                connectionCheck = false;
                timeOutCheck = true;
                while (serverCheck & clientMode != ClientMode.DISCONNECTED)
                {
                    if (timeOutCheck & !connectionCheck)
                    {
                        timeOut(6, 8);
                        if (channelNum == 0)
                        {
                            if (channel != maxChannels)
                                channelNum = channel + 1;
                            else
                                channelNum = 1;
                        }
                        if (channelNum > 20)
                        {
                            channelNum = 20;
                            if (channel == 20)
                                channelNum = 1;
                        }
                        updateAccountStatus("Changing channels to " + channelNum.ToString());
                        updateLog("Changing channels to " + channelNum.ToString());
                        if (!connectionCheck)
                            ses.SendPacket(PacketHandler.changeChannel(channelNum - 1).ToArray());
                    }
                    if (serverOffline)
                    {
                        if (channelNum != maxChannels)
                            channelNum = channelNum + 1;
                        else
                            channelNum = 1;
                        addChannels++;
                        serverOffline = false;
                        if (clientMode == ClientMode.FMOWL)
                        {
                            //break;
                        }
                    }
                    Thread.Sleep(1);
                }
                Program.iniFile.WriteValue(accountProfile, "Channel", channelNum.ToString());
                if (addChannel)
                {
                    channel = channel + addChannels;
                    if (channel > maxChannels)
                    {
                        if (channelNum == 1)
                            channel = 1;
                        else
                            channel = channelNum;
                    }
                }
                else
                {
                    channel = channelNum;
                }
                if (Program.userDebugMode || Program.debugMode)
                {
                    updateLog("Channel File Update: " + channelNum.ToString());
                    updateLog("Channel Update: " + channel.ToString());
                }
            }
            catch { }
        }
        public void changeFreeMarketRoom(string roomNumber, byte channel)
        {
            try
            {
                int mapID = myCharacter.mapID;
                int tries = 0;
                bool cleared = false;
                serverCheck = true;
                while (mapID == myCharacter.mapID & clientMode != ClientMode.DISCONNECTED & serverCheck)
                {
                    if (!cleared)
                    {
                        Players.Clear();
                        mapleFMShopCollection.shops.Clear();
                        cleared = true;
                    }
                    timeOut(3, 4);
                    ses.SendPacket(PacketHandler.changeFMRoom(roomNumber, channel).ToArray());
                    while (serverCheck & !timeOutCheck & clientMode != ClientMode.DISCONNECTED)
                        Thread.Sleep(1);
                    if (serverCheck)
                    {
                        tries++;
                        if (clientMode == ClientMode.FMOWL)
                        {
                            //ses.SendPacket(PacketHandler.Close_Store().ToArray());
                            //ses.SendPacket(PacketHandler.Close_Store().ToArray());
                        }
                        if (tries >= 10)
                        {
                            cashShopManagement(false, false, 0, 0);
                            tries = 0;
                            cleared = false;
                            Thread.Sleep(1000);
                        }
                    }
                    Thread.Sleep(1);
                    if ((byte)this.channel != channel)
                    {
                        this.channel = (int)channel;
                        Program.iniFile.WriteValue(accountProfile, "Channel", this.channel.ToString());
                    }
                }
            }
            catch { }
        }

        public void moveFMRoomsOwlMethod(int channel, int mapid)
        {
            try
            {
                int tries = 0;
                bool cleared = false;
                serverCheck = true;
                int currentChannel = channel;
                if (channel >= maxChannels)
                {
                    channel = 1;
                }
                while (clientMode != ClientMode.DISCONNECTED & serverCheck)
                {
                    if (!cleared)
                    {
                        Players.Clear();
                        mapleFMShopCollection.shops.Clear();
                        cleared = true;
                    }
                    timeOut(3, 4);
                    ses.SendPacket(PacketHandler.owlTeleport((byte)(channel), mapid).ToArray());
                    while (serverCheck & !timeOutCheck & clientMode != ClientMode.DISCONNECTED)
                        Thread.Sleep(1);
                    if (serverCheck)
                    {
                        tries++;
                        if (clientMode == ClientMode.FMOWL)
                        {
                            //ses.SendPacket(PacketHandler.Close_Store().ToArray());
                            //ses.SendPacket(PacketHandler.Close_Store().ToArray());
                        }
                        if (tries >= 10)
                        {
                            cashShopManagement(false, false, 0, 0);
                            tries = 0;
                            cleared = false;
                            Thread.Sleep(1000);
                        }
                    }
                    Thread.Sleep(1);
                }
                changeChannel(false, currentChannel);
            }
            catch { }
            //owlTeleport
        }
    }
}