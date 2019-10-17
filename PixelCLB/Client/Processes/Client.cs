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
using MaplePacketLib;


namespace PixelCLB
{
    public partial class Client
    {
        private Net.Connector connector;
        public ClientMode clientMode = ClientMode.DISCONNECTED;
        public string accountProfile;
        public System.Timers.Timer timer;
        public Values realMode = new Values("");
        public NPCShop npcShop = null;

        public MapleFMShopCollection mapleFMShopCollection = new MapleFMShopCollection();
        public Dictionary<int, Player> Players = new Dictionary<int, Player>();

        public HackShield hackShield = null;
        public ModeDetermine modeDeter = new ModeDetermine();
        public double hackShieldMin = 1.9, hackShieldMax = 2.05; //1.9/2.05
        private PacketRecv Packet;
        public CharacterInfo charInfoWindow = null;
        public Net.Session ses;
        public Thread thread;
        public Me myCharacter = new Me();
        public int RAND1, RAND2, RAND3, timesRepeated = 0, authFailures = 0;
        public short currentport;
        public string LoginID, Password, mapName, mapleVersion = "MapleStory", worldName, owlWorldName, charIGN = "", proxyServer = "";
        public string storename, dcTarget, findTarget;
        public string coords = "592,-26", ardentMineCoords = "1641,507", ardentHerbCoords = "1906,520", specialSpawnCoords = "", storeTargetCoords = null, merchantCopyIGN = "", merchantCopyTargetIGN = "";
        public short footholds1, footholds2, oldx, oldy;
        public int channel = 0, charnumber = 0, maxcharnumber = 0, storesMissed = 0, totalStoresRead = 0, roomStoresRead = 0, maxChannels = 20;
        public string pic, currentip, storeAFKIGN;
        public int mushyType, MPUnderValue, MPPotID;
        public int xLow = 0, xHigh = 0, yLow = 0, yHigh = 0;
        public bool inmushy = false, craftDone = false, regLogin = false, specialSpawn = false, detectStore = false, ignChecked = false, remoteHackPacket = false, lootItems = false;
        public long cauth = (long)0;
        public int tries, bannedstores = 0, npcWindows = 0;
        public bool sent = false, timeOutCheck = true, loginCheck = false;
        public int mode = 0, modeBak;
        public bool inCS = false, csCheck, serverCheck, waitPacket = true, afkPermit = false, webLogin = false, connectionCheck = false, serverOffline = false;
        public short waitForPacketOpCode;
        public bool storeClicked = false, reRun = false, freeSpot = false, resetIsOpen = true, canReset = false, regSpawn = true, readShop = false, chatLogs = false, inStore = false, clientWaiting = false, clientStartAble = true, storeLoaded = false, fmRoomOverride = false, secondClosePacket = false;
        public int storeTries = 45, timeBeforeCS = 60, storeTargetUID = 0;
        public DateTime execute;
        private object disconnectObject = new object();
        public string RoomNum = "0", overrideRoomNum = "0", autoChatMode = "";
        public byte slotNum = 0;
        public int roomNum = 0, worldID = 0;
        public World world;
        public List<string> autoChatText = new List<string>(), ignStealer = new List<string>();
        public Stopwatch sw = new Stopwatch();
        public List<int> shopsToRead = new List<int>();
        public List<int> shopsToReadBak = new List<int>();
        public List<string> autoBuffList = new List<string>();
        public List<String> logBox = new List<String>();
        public List<Thread> workerThreads = new List<Thread>();
        public List<MapleItem> dropItem = new List<MapleItem>();

        public string nexonCookie = string.Empty;
        public DateTime nexonCookieTimeout = DateTime.Now;

        public List<System.Timers.Timer> timerList = new List<System.Timers.Timer>();
        public bool exploitClose = false;

        //Chat Variables
        public List<int> buddyUIDS = new List<int>();
        public List<int> guildMemberUIDS = new List<int>();
        public List<string> chatCollection = new List<string>();

        public override string ToString() { return string.Concat(accountProfile + " - " + realMode.Value, " | ", getTime()); }
        public string toProfile() { return accountProfile; }

        public Client(string profileName)
        {
            try
            {
                Packet = new PacketRecv(this);
                while (Packet.complete == 0)
                    Thread.Sleep(1);
                accountProfile = profileName;
                RAND1 = new Random().Next(0, Int32.MaxValue);
                RAND2 = new Random().Next(0, Int16.MaxValue);
                //Thread t = new Thread(() => checkThreads());
                //t.Start();
            }
            catch { }
        }


        public double timeLeft()
        {
            try
            {
                TimeSpan difference = execute - DateTime.Now;
                return Math.Floor(difference.TotalSeconds);
            }
            catch { return 0; }
        }

        private void checkThreads()
        {
            while (true)
            {
                try
                {
                    List<Thread> t = workerThreads.ToList<Thread>();
                    lock (t)
                    {
                        if (t.Count > 20)
                        {
                            for (int x = 0; x < t.Count - 20; x++)
                            {
                                if (!t[x].IsAlive)
                                {
                                    workerThreads.Remove(t[x]);
                                }
                            }
                        }
                    }
                }
                catch { }
                Thread.Sleep(20000);
            }
        }

        public void initializeClient()
        {
            try
            {
                hackShield = new HackShield(hackShieldMin, hackShieldMax, this);
            }
            catch { }
        }


        private string getTime()
        {
            try
            {
                if (hackShield != null)
                    return hackShield.timeLeft().ToString() + " secs";
                else if (clientWaiting)
                    return "Starting in " + timeLeft().ToString() + " secs";
                else
                    return "Initiating HS Timer";
            }
            catch
            {
                return "null";
            }
        }

        public void Connect(string ip, short port)
        {
            try
            {
                Net.Connector connector = new Net.Connector(this);
                this.connector = connector;
                connector.addConnectedHandler();
                connector.Connect(ip, port); //Logs into server
                if (hackShield != null)
                    hackShield.resetHackShieldTimer(hackShieldMin, hackShieldMax);
                updateLog("[Connection] Connecting...");
            }
            catch
            {
                updateLog("[Error]Servers exploded!!!");
            }
        }

        public void con_OnClientConnected(object sender, ConnectedEventArgs e)
        {
            try
            {
                ses = e.NetworkSession;
                ses.OnHandshakeHandler += new EventHandler<HandshakeEventArgs>(ses_OnInitPacketHandler);
                ses.OnPacketRecvHandler += new EventHandler<PacketEventArgs>(ses_OnPacketRecvHandler);
                ses.OnPacketSendHandler += new EventHandler<PacketEventArgs>(ses_OnPacketSendHandler);
                //ses.OnDisconnectedHandler += new EventHandler<DisconnectedEventArgs>(ses_OnDisconnect);
                ses.addDisconnectHandler(new EventHandler<DisconnectedEventArgs>(ses_OnDisconnect));
                return;
            }
            catch
            {
                updateLog("[Connection] Initial response error");
                //forceDisconnect(true, 1, false, "Initial Reponse error");
            }
        }

        public void ses_OnPacketSendHandler(object sender, PacketEventArgs e)
        {
            if (Program.gui.logSendPackets)
            {
                updateLog("[Send]" + e.Reader.ToString());
            }
           //Logger.Write(Logger.LogTypes.SEND, packet);
        }

        public void ses_OnPacketRecvHandler(object sender, PacketEventArgs e)
        {
            try
            {
                short header = e.Reader.ReadShort();
                if (Packet.Ops.ContainsKey(header))
                    Packet.getPacketHandler(header).packetAction(this, e.Reader);
            }
            catch { }
            if (Program.gui.logRecvPackets)
            {
                //object[] string2s = new object[] { e.Reader.ToArray().ToString2s() };
                updateLog("[Recv]" + e.Reader.ToString());
            }
            //Logger.Write(Logger.LogTypes.RECV, "{0}", string2s);
        }

        private void ses_OnDisconnect(object sender, DisconnectedEventArgs e)
        {
            bool disconnected = ses != null;
            if (disconnected && clientMode != ClientMode.DISCONNECTED)
            {
                if (Program.debugMode || Program.userDebugMode)
                {
                    if (hackShield != null)
                        updateLog("[Hackshield Timer] Time Left: " + hackShield.timeLeft().ToString() + "secs");
                }
                updateLog("[Connection] Disconnected: " + e.Reason.ToString());
                updateLog("[Status] Restarting.");
                forceDisconnect(true, 1, false, "Disconnected by Server");
            }
        }

        private int getNexonCookieTimeout()
        {
            int num;
            if (nexonCookie != string.Empty)
            {
                TimeSpan now = nexonCookieTimeout - DateTime.Now;
                num = (now.TotalSeconds >= 0 ? (int)Math.Ceiling(now.TotalSeconds) : 0);
            }
            else
            {
                num = 0;
            }
            return num;
        }

        /* OLD LOGIN METHOD
        public void checkValidNexonCookie()
        {
            if (nexonCookie == string.Empty || getNexonCookieTimeout() == 0)
            {
                nexonCookie = string.Empty;
                nexonCookieTimeout = DateTime.Now.AddSeconds(120);
                updateLog("[Auth] Grabbing nexon auth...");
                nexonCookie = getNexonCookie(LoginID, Password);
                if (nexonCookie == string.Empty)
                {
                    if (Program.usingProxy)
                    {
                        authFailures++;
                        if (authFailures < 4)
                        {
                            updateLog("[Auth] Failed to get auth, restarting.");
                            forceDisconnect(true, 1, false, "Auth: General failure");
                        }
                        else if (authFailures >= 4 && authFailures < 6)
                        {
                            proxyServer = "";
                            Program.iniFile.WriteValue(accountProfile, "AccProxy", "");
                            updateLog("[Auth] Failed to get auth 8 times. Disconnecting.");
                            forceDisconnect(false, 1, false, "Failed to get auth 8 times");
                        }
                        else
                        {
                            proxyServer = "";
                            Program.iniFile.WriteValue(accountProfile, "AccProxy", "");
                            updateLog("[Auth] Failed to get auth 3 times, restarting with different proxy.");
                            forceDisconnect(true, 1, false, "Auth: Restarting with different proxy");
                        }

                    }
                    else
                    {
                        updateLog("[Auth] Failed to get auth, restarting in " + Program.nexonAuthRestartTime.ToString() + " secs");
                        forceDisconnect(true, Program.nexonAuthRestartTime, false, "Auth: General failure");
                    }
                }
                updateLog("[Auth] Nexon Auth grabbed");
                return;
            }
            return;
        }
        ----END OLD LOGIN*/

        private void ses_OnInitPacketHandler(object sender, HandshakeEventArgs e)
        {
                try
                {
                    Thread t = new Thread(delegate()
                    {
                        updateLog("[Connection] Established at: " + connector.ip + ":" + connector.port);
                        //if (clientMode == ClientMode.DISCONNECTED)
                        if (connector.ip == Program.serverip & connector.port == Program.port)
                        {
                            sw.Reset();
                            sw.Start();
                            clientMode = ClientMode.LOGIN;
                            pictureBoxChange();
                            Program.mapleVersion = (short)e.Version;
                            ses.SendPacket(PacketHandler.ValidateLogin(e.Locale, e.Version, e.Subversion).ToArray());
                            /* OLD LOGIN METHOD
                            Thread th = null;
                            if (nexonCookie == string.Empty || getNexonCookieTimeout() == 0)
                            {
                                th = new Thread(() => checkValidNexonCookie());
                                workerThreads.Add(th);
                                th.Start();
                            }
                            if (th != null)
                                th.Join();
                            */
                            nexonCookie = string.Empty;
                            nexonCookie = getNexonCookie(LoginID, Password);
                            while (nexonCookie == string.Empty)
                                Thread.Sleep(1);
                            if (nexonCookie != null || nexonCookie != string.Empty || nexonCookie != "Error" || nexonCookie != "Invalid" || nexonCookie != "Incorrect")
                            {
                                /*
                                while (clientMode == ClientMode.LOGIN)
                                {
                                    if (checkWorldStatus())
                                        break;
                                    else
                                        updateLog("[Status] Checking world server status");
                                    Thread.Sleep(1);
                                }
                                 */
                                if (!Program.webLogin)
                                {
                                    ses.SendPacket(PacketHandler.ClientLogin(this, nexonCookie, Password).ToArray());
                                }
                                else
                                {
                                    ses.SendPacket(PacketHandler.LoginResponse(this, nexonCookie, world, channel, Password).ToArray());
                                }
                                mapleVersion = string.Concat("MapleStory v", e.Version.ToString(), ".", e.Subversion.ToString());
                                updateLog("[Version] " + mapleVersion);
                                updateAccountStatus("Logged in");
                                Thread loginCheckThread = new Thread(delegate()
                                {
                                    DateTime startCheck = DateTime.Now;
                                    while (!loginCheck)
                                    {
                                        if ((DateTime.Now - startCheck).TotalSeconds > 10)
                                        {
                                            updateLog("[Login] No login response! Restarting!");
                                            forceDisconnect(true, 0, false, "No login Response");
                                            return;
                                        }
                                        Thread.Sleep(1);
                                    }
                                    return;
                                });
                                workerThreads.Add(loginCheckThread);
                                loginCheckThread.Start();
                            }
                            else
                            {
                                string x = string.Empty;
                                if (nexonCookie == "Error")
                                    x = "Auth Error";
                                if (nexonCookie == "Invalid")
                                    x = "Invalid Response;";
                                if (nexonCookie == "Incorrect")
                                    x = "Incorrect Info";
                                updateLog("[Nexon Auth] Failed to retrieve auth.");
                                updateLog("[Nexon Auth] " + x);
                                forceDisconnect(true, 1, false, "Failed to retrieve auth.");
                            }
                        }
                        else
                        {
                            if (!remoteHackPacket)
                            {
                                ses.SendPacket(PacketHandler.EnterChannel(myCharacter.uid, RAND1, RAND2, cauth, worldID).ToArray());
                                //Thread.Sleep(1000);
                                //ses.SendPacket(PacketHandler.Custom("2F 01 01").ToArray());
                            }
                            else
                            {
                                remoteHackPacket = false;
                            }
                        }
                    });
                    workerThreads.Add(t);
                    t.Start();
                }
                catch
                {
                    updateLog("[Error] Debug Error 0");
                }
        }

        public string getNexonCookie(string username, string password)
        {
            try
            {
                return MaplePacketLib.Auth.GetAuth(username, password);
            }
            catch
            {
                return "Error";
            }

            /*
            try
            {
                string nexonAuth = string.Empty;
                WebResponse httpWebResponse = Post("https://www.nexon.net/api/v001/account/login", string.Concat("userID=", username, "&password=", password));
                if (httpWebResponse != null)
                {
                    nexonAuth = httpWebResponse.Headers.ToString();
                    //MessageBox.Show(nexonAuth);
                    httpWebResponse.Close();
                    if (nexonAuth.Contains("NPPv2="))
                    {
                        nexonAuth = nexonAuth.Remove(0, nexonAuth.IndexOf("NPPv2=") + 6);
                        nexonAuth = nexonAuth.Remove(nexonAuth.IndexOf(";"));
                    }
                    else
                    {
                        nexonAuth = string.Empty;
                    }
                }
                else
                {
                    nexonAuth = string.Empty;
                }
                return nexonAuth;
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("401"))
                {
                    updateLog("[Error] 401: Check your LoginID / Password combination");
                }
                else if (e.ToString().Contains("429"))
                {
                    updateLog("[Error] 429: Too many auths being requested.");
                    updateLog("[Error] Please wait before attempting again.");
                }
                else 
                {
                    updateLog("[Error] Unable to retrieve auth.");
                    updateLog("[Error] Please wait before attempting again.");
                }
                return string.Empty;
            }
            */
        }

        public HttpWebResponse Post(string url, string data)
        {
            HttpWebResponse response = null;
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(data);
                HttpWebRequest webData = (HttpWebRequest)WebRequest.Create(url);
                if (Program.usingProxy)
                {
                    if (File.Exists(Program.proxyDirectory))
                    {
                        string proxy = proxyServer;
                        if (proxy == "")
                        {
                            string[] proxyList = File.ReadLines(Program.proxyDirectory).ToArray();
                            proxy = proxyList[new Random().Next(0, proxyList.Length)];
                            Program.iniFile.WriteValue(accountProfile, "AccProxy", proxy);
                        }
                        if (!proxy.ToLower().Equals("null"))
                        {
                            proxyServer = proxy;
                            if (proxy.Contains("@"))
                            {
                                updateLog("Proxy: " + proxy);
                                string[] userPass = proxy.Split(':', '@');
                                string[] realProxy = proxy.Split('@');
                                IWebProxy z = new WebProxy(realProxy[1]);
                                z.Credentials = new NetworkCredential(userPass[0], userPass[1]);
                                webData.Proxy = z;
                            }
                            else
                            {
                                updateLog("Proxy: " + proxy);
                                IWebProxy z = new WebProxy(proxy);
                                webData.Proxy = z;
                            }
                        }
                        WebRequest.DefaultWebProxy = null;  
                    }
                    else
                    {
                        MessageBox.Show("Please be sure your proxy file exists.\nNow proceeding without a proxy", "Proxy error!", MessageBoxButtons.OK);
                        Program.usingProxy = false;
                        Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                        Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", "");
                    }
                }
                webData.Method = "POST";
                webData.ContentType = "application/x-www-form-urlencoded";
                webData.ContentLength = bytes.Length;
                Stream streamData = webData.GetRequestStream();
                streamData.Write(bytes, 0, bytes.Length);
                streamData.Close();
                response = (HttpWebResponse)webData.GetResponse();
            }
            catch (Exception e)
            {
                Exception exception = e;
                //Logger.Write(Logger.LogTypes.Warning, exception.ToString());
                return response;
            }
            return response;
        }

        public bool ProxyTest(string host, string port)
        {
            var is_success = false;
            try
            {
                var connsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 50);
                var hip = IPAddress.Parse(host);
                var ipep = new IPEndPoint(hip, int.Parse(port));
                connsock.Connect(ipep);
                if (connsock.Connected)
                {
                    is_success = true;
                }
                connsock.Close();
            }
            catch (Exception)
            {
                is_success = false;
            }
            return is_success;
        }
    }
}