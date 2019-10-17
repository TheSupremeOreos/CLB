using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using PixelCLB;
using PixelCLB.CLBTools;
using PixelCLB.Tools;
using PixelCLB.Net;
using PixelCLB.Net.Packets;
using PixelCLB.Crypto;
using PixelCLB.Net.Events;
using PixelCLB.PacketCreation;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO.Pipes;
using System.Management;
using System.Net.NetworkInformation;

namespace PixelCLB
{

    internal partial class PixelCLB : Form
    {
        public bool read = false;
        private int startTries = 0, mushyUpdatingTimes = 0, playerUpdatingTimes = 0;
        public int oldIndex = -1;
        private bool showLog = false, mushyUpdating = false, playerUpdating = false;
        public string box;
        public bool logSendPackets = false, logRecvPackets = false;
        private GroupBox groupBox1;
        public Profiles profile = null;
        public Client c, c2;
        private static System.Drawing.Size minSize = new System.Drawing.Size(292, 122);
        private static System.Drawing.Size openSize = new System.Drawing.Size(788, 518);
        private object startLock = new object();
        public List<CharacterInfo> charWindows = new List<CharacterInfo>();
        public List<ModeSelector> modeWindows = new List<ModeSelector>();
        public List<AutoChat> autoChatWindows = new List<AutoChat>();
        public List<AutoBuff> autoBuffWindows = new List<AutoBuff>();
        public List<StoreAFKer> storeAFKWindows = new List<StoreAFKer>();
        public List<FMRoomChange> FMRoomChangeWindows = new List<FMRoomChange>();
        public List<CrashOptions> crashWindows = new List<CrashOptions>();

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);


        public PixelCLB()
        {
            InitializeComponent();
            if (Program.usingHWID)
                checkHWID();
            Program.loadWorldsAndMode();
            startUp();
            updateProgramTexts(true);
            Program.verifyHWID = new HWIDVerify();
        }


        private void startUp()
        {
            comboBox1.MouseWheel += new MouseEventHandler(comboBox_MouseWheel); //Prevent Scrolling
            startTimeComboBox.MouseWheel += new MouseEventHandler(comboBox_MouseWheel); //Prevent Scrolling
            comboBox1_SelectedIndexChanged(null, null); //Load Profiles
            tabControl1.SelectedIndexChanged += new EventHandler(tabs_selectedIndexChange); //??
            //Size = minSize; //Set Min Size
            Size = openSize;
            if(c == null)
                c = new Client(Program.clbName); //New Client
            c.worldName = "Scania";
            c.owlWorldName = "Scania";
            c.myCharacter.mapID = 910000000;
            countdownTimer();
            startTimeComboBox.SelectedIndex = 0;
            if (Program.whiteList)
                tabPage3.Text = "WhiteList";
            else
                tabPage3.Text = "BlackList";
            updateWhiteList(false);
            logOffPasswordLabel.Text = Program.logOffPass;
            foreach (ToolStripMenuItem x in menuStrip1.Items)
                ((ToolStripDropDownMenu)x.DropDown).ShowImageMargin = false;
        }

        private void countdownTimer()
        {
            //Thread countDown = new Thread(new ThreadStart(countDownTimer));
            //countDown.Start();
        }

        private void checkRunningAccounts()
        {
            //Thread checkRunningAccs = new Thread(new ThreadStart(checkRunningAccount));
            //checkRunningAccs.Start();
        }

        public void updateProgramTexts(bool success)
        {
            GUIInvokeMethod(() =>
            {
                Text = Program.clbName + " " + Program.version + " - User: " + Program.name + " : " + Program.slogan; //Change Text
                userLabel.Text = Program.clbName + " " + Program.version + " - User: " + Program.name; // Change User Label
                accessLevelLabel.Text = Program.accessLevelLabel;
                lastUpdatedLabel.Text = "HWID Last Checked: " + DateTime.Now.ToString("MMMM dd, yyyy") + " at " + DateTime.Now.ToString("h:mm:ss tt");
                if (success)
                {
                    hwidCheckLabel.ForeColor = System.Drawing.Color.Green;
                    hwidCheckLabel.Text = "HWID Status: Verified";
                }
                else
                {
                    hwidCheckLabel.ForeColor = System.Drawing.Color.Red;
                    hwidCheckLabel.Text = "HWID Status: Unverified #" + Program.verifyHWID.failed.ToString();
                }
                //Advertisement
            });
        }

        private void countDownTimer()
        {
            while (true)
            {
                try
                {
                    updateActiveAccounts(true);
                    Thread.Sleep(1100);
                }
                catch { }
            }
        }

        private void checkRunningAccount()
        {
            while (true)
            {
                try
                {
                    foreach (Client clients in Program.keepRunning)
                    {
                        if (!Program.clients.Contains(clients))
                        {
                            clients.forceDisconnect(true, 1, false, "Keep Running");
                        }
                    }
                }
                catch { }
                Thread.Sleep(600000);
            }
        }

        public void checkHWID()
        {
            if (IsConnected())
            {
                try
                {
                    if (string.IsNullOrEmpty(Program.HWID))
                    {
                        Program.HWID = FingerPrint.Value();
                    }
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] data = wc.DownloadData("http://clb.pixelcha.com/clb/HWIDVerification/getHWID.php?HWID=" + Program.HWID + "&Version=" + Program.version);
                    string[] HWID = System.Text.Encoding.UTF8.GetString(data).Split(',');
                    if (HWID.Length > 6)
                    {
                        if (HWID[4].Equals(Program.HWID))
                        {
                            if (double.Parse(HWID[0]) > double.Parse(Program.version))
                            {
                                MessageBox.Show("New Update Available for download! Don't forget to read the read me!\nCurrent Version: v" + Program.version + " \nVersion Available: v" + HWID[0]);
                                HWID[8] = HWID[8].Replace(Environment.NewLine, "");
                                if (HWID[8].ToLower().Contains("true"))
                                {
                                    MessageBox.Show("Older versions of this bot has been disabled. Please re-download the new version. Now exiting.");
                                    Environment.Exit(0);
                                }
                            }
                            Program.clbName = HWID[1];
                            Program.slogan = HWID[2];
                            Program.adLink = HWID[3];
                            Program.name = HWID[5];
                            Program.accessLevel = double.Parse(HWID[6]);
                            Program.getAccessLevel(Program.accessLevel);
                            if (Program.slogan.ToLower().Equals("debug mode"))
                                Program.debugMode = true;
                            else
                                Program.debugMode = false;
                            if (HWID[7].ToLower().Contains("true"))
                            {
                                MessageBox.Show("This licenses has been disabled. Please contact the administrator.");
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            MessageBox.Show(HWID[1]);
                            Environment.Exit(-1);
                        }
                    }
                    else
                    {
                        MessageBox.Show(HWID[0]);
                        Environment.Exit(-1);
                    }
                    wc.Dispose();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error with HWID verification. Failed to connect to external server. \nAction: Now Exiting.");
                    Environment.Exit(-1);
                }
            }
            else
            {
                MessageBox.Show("Unable to verify HWID. \nReason: Not connected to the internet.\nNow exiting.");
                Environment.Exit(-1);
            }
        }

        public static bool IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }
        public void button2_Click(object sender, EventArgs e) //Start Bot Button
        {
            if (startTimeComboBox.SelectedItem.ToString().Equals("Start Now"))
            {
                startBot(box, true, false, true);
            }
            else if (startTimeComboBox.SelectedItem.ToString().Equals("Post Server Check"))
            {
                startBot(box, false, false, true);
            }
            else if (startTimeComboBox.SelectedItem.ToString().Equals("Regular Log-In (Post SC)"))
            {
                startBot(box, false, true, true);
            }
        }
        public void button1_Click(object sender, EventArgs e) //Reg Login Button
        {
            startBot(box, true, true, true);
        }

        public void startBot(string Login, bool startNow, bool regLogin, bool userClick, int startDelay = 0)
        {
            if (Program.loadPercent < 100)
            {
                MessageBox.Show("Settings have yet to finish loading. \nPlease wait until you see the complete message in the log box.");
                return;
            }
            else if (Program.loadPercent == 150)
            {
                MessageBox.Show("You cannot start a bot with authentication state. Please retry after restarting the bot.");
                return;
            }
            if (Login != null)
            {
                showLog = true;
                GUIInvokeMethod(() => Size = openSize);
                startTries = 0;
                try
                {
                    lock (startLock)
                    {
                        if (Program.clientsStarted.Exists(str => str == Login))
                        {
                            if (userClick)
                            {
                                MessageBox.Show(new Form() { TopMost = true }, "An instance has already been started. Please press stop and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            return;
                        }
                        Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(Login);
                        if (!Program.modes.Contains(sectionValues["Mode"]))
                        {
                            MessageBox.Show("This mode is no longer valid due to one of the following reasons: \n-You do not have the proper access level\n-Removed due to being patched \n-Removed due to abuse \n-Removed for ADMIN USE ONLY");
                            return;
                        }
                        Client client = null;
                        foreach (Client deadClient in Program.endedClients)
                        {
                            if (deadClient.toProfile() == Login)
                            {
                                client = deadClient;
                                continue;
                            }
                        }
                        if (client == null)
                            client = new Client(Login);
                        else
                            GUIInvokeMethod(() => Program.endedClients.Remove(client));

                        GUIInvokeMethod(() => Program.clientsStarted.Add(Login));
                        GUIInvokeMethod(() => Program.clients.Add(client));
                        Thread threadz = new Thread(delegate()
                        {
                            try
                            {
                                int num;
                                //client.remoteHackPacket = true;
                                client.bannedstores = 0;
                                client.Password = sectionValues["Password"];
                                client.LoginID = sectionValues["Username"];
                                client.pic = sectionValues["PIC"];
                                client.worldName = sectionValues["World"];
                                string ignSteals = sectionValues["ignSteal"];
                                client.ignStealer.Clear();
                                foreach (string x in ignSteals.Split(','))
                                {
                                    client.ignStealer.Add(x);
                                }
                                client.world = Database.getWorld(client.worldName);
                                client.worldID = Database.getWorldID(client.world);
                                client.owlWorldName = Database.getWorldOwlString(client.world);
                                client.maxChannels = Database.getMaxChannels(client.world);
                                client.channel = int.Parse(sectionValues["Channel"]);
                                bool charNum = int.TryParse(sectionValues["CharNum"], out num);
                                if (!charNum)
                                {
                                    client.charIGN = sectionValues["CharNum"];
                                    client.charnumber = -1;
                                }
                                else
                                {
                                    client.charIGN = "";
                                    client.charnumber = int.Parse(sectionValues["CharNum"]) - 1;
                                }
                                client.coords = sectionValues["Spawn"];
                                client.storename = sectionValues["Title"];
                                client.realMode.Value = sectionValues["Mode"];
                                client.dcTarget = sectionValues["DCTarget"];
                                client.regLogin = regLogin;
                                client.proxyServer = sectionValues["AccProxy"];
                                client.merchantCopyTargetIGN = sectionValues["MushyRedirect"];
                                client.merchantCopyIGN = sectionValues["MushyRedirect2"];
                                if (Program.usingProxy)
                                {
                                    if (client.proxyServer != "")
                                    {
                                        client.updateLog("[Proxy] Checking Proxy Status");
                                        string[] userPassProxy = client.proxyServer.Split(':', '@');
                                        if (client.proxyServer.Contains("@"))
                                        {
                                            if (!client.ProxyTest(userPassProxy[2], userPassProxy[3]))
                                            {
                                                client.updateLog("Proxy: " + client.proxyServer + "is offline. Switching proxies");
                                                client.proxyServer = "";
                                            }
                                            else
                                            {
                                                client.updateLog("[Proxy] Proxy is online!");
                                            }
                                        }
                                        else
                                        {
                                            if (!client.ProxyTest(userPassProxy[0], userPassProxy[1]))
                                            {
                                                client.updateLog("Proxy: " + client.proxyServer + "is offline. Switching proxies");
                                                client.proxyServer = "";
                                            }
                                            else
                                            {
                                                client.updateLog("[Proxy] Proxy is online!");
                                            }
                                        }
                                    }
                                }
                                if (!Program.webLogin)
                                {
                                    client.webLogin = false;
                                }
                                if (userClick)
                                {
                                    if (!client.regLogin)
                                    {
                                        client.mode = Program.getModeID(sectionValues["Mode"]);
                                        if (client.mode == 5 || client.mode == 6 || client.mode == 19)
                                            client.slotNum = 2;
                                        else
                                            client.slotNum = 1;
                                    }
                                    else
                                    {
                                        client.mode = 0;
                                        client.realMode.Value = "Regular Log In";
                                    }
                                }
                                else
                                {
                                    if (!userClick & client.mode != 13 & client.mode != 14 & client.mode != 15)
                                    {
                                        if (!client.regLogin)
                                        {
                                            client.mode = Program.getModeID(sectionValues["Mode"]);
                                            if (client.mode == 5 || client.mode == 6 || client.mode == 19)
                                                client.slotNum = 2;
                                            else
                                                client.slotNum = 1;
                                        }

                                        else
                                        {
                                            client.mode = 0;
                                            client.realMode.Value = "Regular Log In";
                                        }
                                    }
                                }
                                if (sectionValues["Shop Type"] == "Automatic")
                                {
                                    client.mushyType = 0;
                                    client.slotNum = 0;
                                    client.detectStore = true;
                                }
                                else
                                {
                                    client.mushyType = Program.getShopID(sectionValues["Shop Type"]);
                                    client.detectStore = false;
                                }
                                if (sectionValues["CoordOverride"].Equals("True"))
                                {
                                    client.specialSpawn = true;
                                    client.specialSpawnCoords = sectionValues["Spawn"];
                                }
                                else
                                {
                                    client.specialSpawn = false;
                                }
                                if (sectionValues["FMRoomOverride"].Equals("True"))
                                {
                                    client.fmRoomOverride = true;
                                    client.overrideRoomNum = sectionValues["FMRoomNum"];
                                }
                                else
                                {
                                    client.fmRoomOverride = false;
                                }
                                if (sectionValues["LootItems"].Equals("True"))
                                {
                                    client.lootItems = true;
                                }
                                else
                                {
                                    client.lootItems = false;
                                }
                                client.xLow = int.Parse(sectionValues["FMxLow"]);
                                client.xHigh = int.Parse(sectionValues["FMxHigh"]);
                                client.yLow = int.Parse(sectionValues["FMyLow"]);
                                client.yHigh = int.Parse(sectionValues["FMyHigh"]);
                                int buffCounter = 0;
                                buffCounter = 0;
                                client.autoBuffList.Clear();
                                while (true)
                                {
                                    if (sectionValues.ContainsKey("Buff" + buffCounter.ToString()))
                                    {
                                        client.autoBuffList.Add(sectionValues["Buff" + buffCounter.ToString()]);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    buffCounter++;
                                }
                                if (sectionValues.ContainsKey("PotWhenMPUnder"))
                                {
                                    client.MPUnderValue = int.Parse(sectionValues["PotWhenMPUnder"]);
                                }
                                if (sectionValues.ContainsKey("useMPPot"))
                                {
                                    client.MPPotID = Database.getItemID(sectionValues["useMPPot"]);
                                }
                                if (sectionValues["KeepRunning"].ToLower().Equals("true"))
                                {
                                    Program.keepRunning.Add(client);
                                }
                                client.modeBak = client.mode;
                                client.myCharacter.Map = null;
                                client.updateLog("[Started] " + DateTime.Now.ToString("MMMM dd, yyyy h:mm:ss tt"));
                                updateActiveAccounts(false);
                                client.mapleFMShopCollection.owledShops.Clear();
                                client.mapleFMShopCollection.shops.Clear();
                                client.loginCheck = false;
                                client.clientMode = ClientMode.DISCONNECTED;
                                activeAccounts.Invoke(new MethodInvoker(delegate
                                {
                                    if (activeAccounts.SelectedIndex < 0)
                                    {
                                        activeAccounts.SelectedIndex = activeAccounts.Items.Count - 1;
                                    }
                                }));
                                //modeLabel.Invoke(new MethodInvoker(delegate { modeLabel.Text = "Mode: " + client.realMode.Value; }));
                                client.execute = DateTime.Now;
                                if (startDelay > 0)
                                {
                                    client.clientWaiting = true;
                                    client.execute = DateTime.Now.AddMilliseconds(startDelay);
                                }
                                client.clientStartAble = true;
                                while (client.timeLeft() > 0 & client.clientStartAble)
                                {
                                    Thread.Sleep(1000);
                                }
                                if (client.clientStartAble)
                                {
                                    client.clientWaiting = false;
                                    if (!startNow)
                                    {
                                        client.waitForOffline(Program.serverip, Program.port);
                                    }
                                    client.checkMapleStatus();
                                }
                                else
                                    return;
                            }
                            catch (Exception e)
                            {
                            }
                        });
                        client.workerThreads.Add(threadz);
                        threadz.Start();
                    }
                }
                catch (Exception e)
                {
                    startTries++;
                    if (startTries > 3)
                        MessageBox.Show("Error! Please re-update the profile.");
                    else
                        startBot(Login, startNow, regLogin, false, startDelay);
                }
            }
            else
            {
                MessageBox.Show("Please select a profile before beginning.");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (showLog)
            {
                //Size = minSize;
                Size = openSize;
                showLog = false;
                return;
            }
            if (!showLog)
            {
                Size = openSize;
                showLog = true;
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Select Profile
            try
            {
                if (File.Exists(Program.settingFile))
                {
                    if (read == false)
                    {
                        GUIInvokeMethod(() =>
                        {
                            comboBox1.BeginUpdate();
                            comboBox1.Items.Clear();
                            comboBox1.Items.Add("");
                            foreach (string section in Program.iniFile.GetSectionNames())
                            {
                                if (section != "CLB Settings")
                                    comboBox1.Items.Add(section);
                            }
                            comboBox1.EndUpdate();
                            read = true;
                        });
                    }
                }
                else
                {
                    read = false;
                    File.Create(Program.settingFile).Dispose();
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show("Error: " + ex.Message + " A settings file will now be created. Please add profiles by clicking the + button on the side.");
                    File.Create(Program.settingFile).Dispose();
            }
        }

        public void openProfilesEdit(string accountProfile)
        {
            Profiles openProfiles = new Profiles(accountProfile, false);
            openProfiles.Show();
        }

        private void openProfiles_Click(object sender, EventArgs e)
        {
            try
            {
                GUIInvokeMethod(() =>
                {
                    string userProfile = "";
                    if (!comboBox1.Text.Equals(""))
                    {
                        userProfile = comboBox1.Text;
                    }
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is Profiles)
                        {
                            if (profile != null)
                                Program.gui.profile.updateAccountInfo(userProfile, false);
                            f.Focus();
                            return;
                        }
                    }
                    Profiles openProfiles = new Profiles(userProfile, false);
                    openProfiles.Show();
                });
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string deleteRecord = comboBox1.Text;
            DialogResult DR = MessageBox.Show("Delete Record of " + deleteRecord + "?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DR == DialogResult.Yes)
            {
                Program.iniFile.DeleteSection(deleteRecord);
                read = false;
                comboBox1.SelectedIndex = 0;
            }
        }
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception en) { System.Windows.Forms.MessageBox.Show(en.ToString()); }
        }

        private delegate void GUIInvokeMethodDelegate(Action @delegate);
        public void GUIInvokeMethod(Action @delegate)
        {
            bool invokeRequired = !base.InvokeRequired;
            if (!invokeRequired)
            {
                try
                {
                    object[] objArray = new object[] { @delegate };
                    base.Invoke(new PixelCLB.GUIInvokeMethodDelegate(GUIInvokeMethod), objArray);
                    return;
                }
                catch
                {
                }
            }
            @delegate();
        }
        public void UpdatePlayers(Client client)
        {
            playerUpdatingTimes++;
            if (c == client)
            {
                if (!playerUpdating & playerUpdatingTimes > 0)
                {
                    GUIInvokeMethod(() => Player_Box.BeginUpdate());
                    while (playerUpdatingTimes > 0)
                    {
                        Thread.Sleep(10);
                        playerUpdatingTimes = 0;
                        playerUpdating = true;
                        GUIInvokeMethod(() => Player_Box.Items.Clear());
                        List<Player> users = new List<Player>(c.Players.Values);
                        while (users.Count > 0 & c.clientMode != ClientMode.DISCONNECTED)
                        {
                            try
                            {
                                Player current = users[0];
                                PacketBuilder hexedUID = new PacketBuilder();
                                hexedUID.WriteInt(current.uid);
                                if (client.mapleFMShopCollection.shops.ContainsKey(client.mapleFMShopCollection.getSortingID(false, current.uid)))
                                    this.GUIInvokeMethod(() => Player_Box.Items.Add("UID: " + hexedUID + " IGN: " + current.ign + " *WATCH*"));
                                else
                                    this.GUIInvokeMethod(() => Player_Box.Items.Add("UID: " + hexedUID + " IGN: " + current.ign));
                                hexedUID.Dispose();
                                users.Remove(current);
                            }
                            catch { }
                            Thread.Sleep(1);
                        }
                    }
                    GUIInvokeMethod(() => Player_Box.EndUpdate());
                    playerUpdating = false;
                }
            }
        }

        public void clearPlayerBox()
        {
            GUIInvokeMethod(() => Player_Box.Items.Clear());
        }

        public void clearMushyBox()
        {
            GUIInvokeMethod(() => mushy_Box.Items.Clear());
        }

        public void updateMushy(Client client)
        {
            mushyUpdatingTimes++;
            if (client == c)
            {
                if (!mushyUpdating & mushyUpdatingTimes > 0)
                {
                    while (mushyUpdatingTimes > 0)
                    {
                        Thread.Sleep(10);
                        mushyUpdatingTimes = 0;
                        mushyUpdating = true;
                        GUIInvokeMethod(() => mushy_Box.BeginUpdate());
                        GUIInvokeMethod(() => mushy_Box.Items.Clear());
                        List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = c.mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                        try
                        {
                            this.GUIInvokeMethod(() => mushy_Box.Items.Add("-------- Stores (Recorded once) --------"));
                            while (c.clientMode != ClientMode.DISCONNECTED)
                            {
                                bool flag = enumerator.MoveNext();
                                if (!flag)
                                {
                                    break;
                                }
                                KeyValuePair<int, MapleFMShop> s = enumerator.Current;
                                if (!s.Value.permit)
                                    this.GUIInvokeMethod(() => mushy_Box.Items.Add(s.Value.owner + "'s store @ " + s.Value.x + "," + s.Value.y + "," + s.Value.fh));
                                else
                                    this.GUIInvokeMethod(() => mushy_Box.Items.Add(s.Value.owner + "'s permit @ " + s.Value.x + "," + s.Value.y + "," + s.Value.fh));
                            }

                        }
                        finally
                        {
                            ((IDisposable)enumerator).Dispose();
                        }
                        GUIInvokeMethod(() => mushy_Box.EndUpdate());
                    }
                    mushyUpdating = false;
                }
            }
        }



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void comboBox_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
        private void tabs_selectedIndexChange(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage3)
            {
                updateWhiteList(true);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (activeAccounts.SelectedIndex > -1)
                {
                    Client selectedItem = (Client)activeAccounts.SelectedItem;
                    if (selectedItem != null)
                        selectedItem.forceDisconnect(false, 0, false, "User termination", true);
                    GUIInvokeMethod(() => activeAccounts.SelectedIndex = -1);
                }
                else
                {
                    MessageBox.Show("Please highlight a profile from the active accounts box to stop.");
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void updateCharInfo(Client c)
        {
            try
            {
                if (c == this.c)
                {
                    GUIInvokeMethod(() =>
                    {
                        if (mapleVersionLabel.Text != c.mapleVersion)
                            mapleVersionLabel.Text = c.mapleVersion;
                        if (c.clientMode == ClientMode.DISCONNECTED)
                            ignLabel.Text = "IGN: - Not logged in -";
                        else
                            ignLabel.Text = "IGN: " + c.myCharacter.ign;
                        if (c.clientMode == ClientMode.DISCONNECTED || c.myCharacter.mapID == 0)
                            Map.Text = "Map: N/A";
                        else
                            Map.Text = "Map: " + c.mapName;
                        if (c.clientMode == ClientMode.DISCONNECTED)
                            modeLabel.Text = "Mode: Not Started";
                        else
                            modeLabel.Text = "Mode: " + c.realMode.Value;
                        if (c.clientMode == ClientMode.DISCONNECTED)
                            guildLabel.Text = "Guild: N/A";
                        else
                        {
                            if (c.myCharacter.guild == "" || c.myCharacter.guild == null)
                                guildLabel.Text = "Guild: N/A";
                            else
                                guildLabel.Text = "Guild: " + c.myCharacter.guild;
                        }
                        updateMesoAmount(c);
                    });
                }
            }
            catch { }
        }

        public void updateMesoAmount(Client c)
        {
            try
            {
                if (c == this.c)
                {
                    GUIInvokeMethod(() =>
                    {
                        if (c.clientMode == ClientMode.DISCONNECTED)
                            mesoLabel.Text = "Meso: N/A";
                        else
                        {
                            if (c.myCharacter.Meso != null)
                            {
                                if (c.myCharacter.guild == "" || c.myCharacter.guild == null)
                                    mesoLabel.Text = "Meso: N/A";
                                else
                                    mesoLabel.Text = "Meso: " + c.myCharacter.Meso.ToString("N0");
                            }
                        }
                    });
                }
            }
            catch { }
        }

        public void changeAdminPass(string password)
        {
            Program.logOffPass = password;
            GUIInvokeMethod(() => logOffPasswordLabel.Text = password);
        }

        public void updateWhiteList(bool special)
        {
            if (special)
                return;
            read = false;
            if (read == false)
            {
                try
                {
                    GUIInvokeMethod(() => whiteListBox.Items.Clear());
                    if (Program.whiteList)
                    {
                        if (File.Exists(Program.FMWhiteList))
                        {
                            foreach (var line in System.IO.File.ReadAllLines(Program.FMWhiteList))
                            {
                                GUIInvokeMethod(() => whiteListBox.Items.Add(line));
                            }
                        }
                    }
                    else
                    {
                        if (File.Exists(Program.FMBlackList))
                        {
                            foreach (var line in System.IO.File.ReadAllLines(Program.FMBlackList))
                            {
                                GUIInvokeMethod(() => whiteListBox.Items.Add(line));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (Program.whiteList)
                        MessageBox.Show("There has been an error with FM Whitelist updates.");
                    else
                        MessageBox.Show("There has been an error with FM Blacklist updates.");
                }
                read = true;
            }
        }

        private void MovementPacketTest_Click(object sender, EventArgs e)
        {
            bool flag;
            char[] chrArray = new char[] { ',' };
            bool length = (int)textBoxGetPlayer.Text.Split(chrArray).Length >= 2;
            if (length)
            {
                flag = (c == null || c.ses == null ? true : !c.ses.Socket.Connected);
                length = flag;
                if (!length)
                {
                    c.updateLog("[Teleport] Moving to " + textBoxGetPlayer.Text);
                    c.characterMove(textBoxGetPlayer.Text);
                }
            }
            else
            {
                MessageBox.Show("Invalid coords in the textbox! Use the proper format.\n X,Y,FH*");
            }
        }


        private void Player_Box_Click(object sender, EventArgs e)
        {
            bool selectedItem = Player_Box.SelectedItem == null;
            if (!selectedItem)
            {
                char[] chrArray = new char[] { ' ' };
                string str = Player_Box.Text.Split(chrArray)[6]; 
                List<Player> users = new List<Player>(c.Players.Values);
                while (users.Count > 0 & c.clientMode != ClientMode.DISCONNECTED)
                {
                    try
                    {
                        Player current = users[0];
                        if (current.ign == str)
                        {
                            try
                            {
                                if (current.foothold <= 0)
                                {
                                    Foothold foothold = c.myCharacter.Map.footholds.findBelow(new Point(current.x, current.y));
                                    object[] objArray = new object[] { current.x, ",", foothold.getY1(), ",", foothold.getId() };
                                    textBoxGetPlayer.Text = string.Concat(objArray);
                                    //textBoxGetPlayer.Text = "Unable to display " + current.ign + "'s coords";
                                }
                                else
                                {
                                    object[] objArray = new object[] { current.x, ",", current.y, ",", current.foothold };
                                    textBoxGetPlayer.Text = string.Concat(objArray);
                                }
                            }
                            catch { }
                            return;
                        }
                        users.Remove(current);
                    }
                    catch { }
                }
            }
        }
        public static class TextTool
        {
            /// <summary>
            /// Count occurrences of strings.
            /// </summary>
            public static int CountStringOccurrences(string text, string pattern)
            {
                // Loop through all instances of the string 'text'.
                int count = 0;
                int i = 0;
                while ((i = text.IndexOf(pattern, i)) != -1)
                {
                    i += pattern.Length;
                    count++;
                }
                return count;
            }
        }
        public static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
        public static string replacePacketVars(string packet)
        {
            int count = TextTool.CountStringOccurrences(packet, "**");
            int counter = 0;
            while (count != counter)
            {
                packet = ReplaceFirst(packet, "**", HexEncoding.ToHex((byte)HexTools.getNewTimeStamp()));
                Thread.Sleep(5);
                counter++;
            }
            return packet;
        }
        private void SendPacket_Click(object sender, EventArgs e)
        {
            string packet = replacePacketVars(textBox1.Text);
            c.updateLog("[Send]" + textBox1.Text);
            c.ses.SendPacket(PacketHandler.Custom(packet).ToArray());
        }

        private void ReceivePacket_Click(object sender, EventArgs e) //Is working?
        {
            //c.updateLog("[Recv]" + textBox1.Text);
            //PacketReader packetreader = new PacketReader(HexEncoding.GetBytes(textBox1.Text));
            //PacketEventArgs eventArgs = new PacketEventArgs(HexEncoding.GetBytes(textBox1.Text));
            //c.ses_OnPacketRecvHandler(c.ses, eventArgs);
            //c.ses.ReceivePacket(PacketHandler.Custom(replacePacketVars(textBox1.Text)).ToArray());
        }
        public void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            box = comboBox1.SelectedItem.ToString();
        }

        private void PixelCLB_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(-1);
        }

        private void LogSendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LogSendCheckBox.Checked)
                logSendPackets = true;
            if (!LogSendCheckBox.Checked)
                logSendPackets = false;
        }

        private void LogRecvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LogRecvCheckBox.Checked)
                logRecvPackets = true;
            if (!LogRecvCheckBox.Checked)
                logRecvPackets = false;
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void mushy_Box_MouseClick(object sender, MouseEventArgs e)
        {
            bool selectedItem = mushy_Box.SelectedItem == null;
            if (!selectedItem)
            {
                char[] chrArray = new char[] { '\'' };
                string str = mushy_Box.Text.Split(chrArray)[0];
                List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = c.mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                try
                {
                    while (c.clientMode != ClientMode.DISCONNECTED)
                    {
                        selectedItem = enumerator.MoveNext();
                        if (!selectedItem)
                        {
                            break;
                        }
                        KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                        selectedItem = !(current.Value.owner == str);
                        if (!selectedItem)
                        {
                            selectedItem = current.Value.fh <= 0;
                            if (selectedItem)
                            {
                                textBoxGetPlayer.Text = "Try again. Format: X,Y,FH*";
                            }
                            else
                            {
                                object[] objArray = new object[] { current.Value.x, ",", current.Value.y, ",", current.Value.fh };
                                textBoxGetPlayer.Text = string.Concat(objArray);

                            }
                        }
                    }
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
        }
        private void exitCS()
        {
            c.exitCS(false, false, 0, 0, 1);
        }

        public void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                int num;
                string[] words = commandTextBox.Text.Split(' ');
                if (words[0].ToLower().Equals(""))
                    c.updateLog("No command entered");
                else if (words[0].ToLower().Equals("cmdlist"))
                {
                    foreach (string str in Program.commands)
                    {
                        c.updateLog(str);
                    }
                }
                else if (words[0].ToLower().Equals("room"))
                {
                    c.changeFreeMarketRoom(words[1], (byte)c.channel);
                }
                else if (words[0].ToLower().Equals("entercs"))
                {
                    Thread thread = new Thread(c.enterCS);
                    c.workerThreads.Add(thread);
                    thread.Start();
                }
                else if (words[0].ToLower().Equals("exitcs"))
                {
                    Thread thread = new Thread(exitCS);
                    c.workerThreads.Add(thread);
                    thread.Start();
                }
                    /*
                else if (words[0].ToLower().Equals("pot"))
                {
                    int itemLevel = Database.getItemLevel(int.Parse(words[1]));
                    itemLevel = (byte)Math.Floor(Database.items[int.Parse(words[1])].itemLevel / 10.0);
                    int types = int.Parse(words[2]);
                    Int16 type = (Int16)types;
                    c.updateLog(c.getPotentialLines(type, (byte)itemLevel));
                }
                     */

                else if (words[0].ToLower().Equals("admin -disablehwid"))
                {
                    Program.usingHWID = false;
                }
                else if (words[0].ToLower().Equals("whitelist"))
                {
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is Whitelist)
                        {
                            f.Focus();
                            return;
                        }
                    }
                    new Whitelist(c).Show();
                    c.updateLog("[CMD] White/Black list");
                }
                else if (words[0].ToLower().Equals("fmlist"))
                {
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is FMExportList)
                        {
                            f.Focus();
                            FMExportList form = (FMExportList)f;
                            form.FMExportList_Load(null, null);
                            return;
                        }
                    }
                    new FMExportList(c).Show();
                    c.updateLog("[CMD] FM List");
                }
                else if (words[0].ToLower().Equals("exploit"))
                {
                    if (words.Length > 1)
                    {
                        if (int.TryParse(words[1], out num))
                        {
                            int ID = int.Parse(words[1]);
                            //Program.exploitID = ID;
                        }
                    }
                }
                else if (words[0].ToLower().Equals("generate"))
                {
                    if (words.Length > 1)
                    {
                        if (int.TryParse(words[1], out num))
                        {
                            int ID = int.Parse(words[1]);
                            //c.ses.SendPacket(PacketHandler.EXPLOIT_OPEN(c, ID).ToArray());
                        }
                    }
                }
                else if (words[0].ToLower().Equals("minimap"))
                {
                    int mapID = 0;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is MiniMap)
                        {
                            f.Focus();
                            return;
                        }
                    }
                    if (words.Length > 1)
                    {
                        if (int.TryParse(words[1], out num))
                        {
                            mapID = int.Parse(words[1]);
                        }
                        else
                        {
                            c.updateLog("Invalid map ID entered: " + words[1]);
                        }
                    }
                    else
                        mapID = c.myCharacter.mapID;
                    new MiniMap(Database.loadMiniMap(mapID)).Show();
                    c.updateLog("[CMD] Minimap" + mapID.ToString());
                }
                else if (words[0].ToLower().Equals("deadbots"))
                {
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is EndedBots)
                        {
                            f.Focus();
                            return;
                        }
                    }
                    new EndedBots().Show();
                    c.updateLog("[CMD] Deadbots");
                }
                else if (words[0].ToLower().Equals("dc"))
                {
                    Thread dcUser = new Thread(delegate()
                    {
                        if (words[1] != null & words[1].Length == 2 & words[2] != null & words[2].Length == 2 & words[3] != null & words[3].Length == 2 & words[4] != null & words[4].Length == 2)
                        {
                            string UID = string.Concat(words[1], " ", words[2], " ", words[3], " ", words[4]);
                            if (c.myCharacter.Level < 45)
                            {
                                c.updateLog("Character level does not meet requirements for DC (45+)");
                                return;
                            }
                            GUIInvokeMethod(() => modeLabel.Text = "Mode: Disconnecting UID - " + UID);
                            c.updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + "Disconnect started. UID: " + UID);
                            c.makeExped("4C D0 07 00");
                            while (true && c.clientMode != ClientMode.DISCONNECTED)
                            {
                                c.dcUser(UID, 6, 0x06);
                                if (c.hackShield.timeLeft() < c.timeBeforeCS) // 3mins
                                {
                                    c.cashShopManagement(true, false, 20, 30);
                                }
                            }
                        }
                        else
                        {
                            c.updateLog("Invalid dc command format.");
                            c.updateLog("Ex: dc 00 00 00 00");
                        }
                    });
                    c.workerThreads.Add(dcUser);
                    dcUser.Start();
                }

                else if (words[0].ToLower().Equals("say"))
                {
                    string str = ReplaceFirst(commandTextBox.Text, "say ", "");
                    c.ses.SendPacket(PacketHandler.allChat(str).ToArray());
                }
                else
                {
                    c.updateLog("Invalid Command");
                }
            }
            catch (Exception es)
            {
                c.updateLog("Invalid command. Error triggered." + es.ToString());
            }
        }
        private void logBox_MouseDown(object sender, MouseEventArgs e)
        {
            logBox.SelectedIndex = logBox.IndexFromPoint(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(logBox, e.X, e.Y);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logBox.Items.Count > 0)
            {
                StringBuilder SB = new StringBuilder();
                foreach (string itemValue in logBox.SelectedItems)
                {
                    SB.AppendLine(itemValue);
                }
                string result = SB.ToString().TrimEnd('\n');
                Clipboard.SetText(result);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c.logBox.Remove(logBox.SelectedItem.ToString());
            logBox.Items.Remove(logBox.SelectedItem);
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c.logBox.Clear();
            GUIInvokeMethod(() => logBox.BeginUpdate());
            logBox.Items.Clear();
            GUIInvokeMethod(() => logBox.EndUpdate());
        }

        private void removeWhiteList_Click(object sender, EventArgs e)
        {
            string line;
            int lineCount = 0;
            if (whiteListBox.SelectedItem != null)
            {
                if (Program.whiteList)
                {
                    using (StreamReader file = new StreamReader(Program.FMWhiteList))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Equals(whiteListBox.SelectedItem.ToString()))
                            {
                                lineCount++;
                                break;
                            }
                            lineCount++;
                        }
                    }
                    string newstring = RemoveLine(Program.FMWhiteList, lineCount);
                    WriteToFile(Program.FMWhiteList, newstring);
                }
                else
                {
                    using (StreamReader file = new StreamReader(Program.FMBlackList))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.Equals(whiteListBox.SelectedItem.ToString()))
                            {
                                lineCount++;
                                break;
                            }
                            lineCount++;
                        }
                    }
                    string newstring = RemoveLine(Program.FMBlackList, lineCount);
                    WriteToFile(Program.FMBlackList, newstring);
                }
                updateWhiteList(false);
            }
            else
                c.updateLog("[WhiteList] Please select someone to delete.");
        }

        private void addWhiteList_Click(object sender, EventArgs e)
        {
            if (Program.whiteList)
            {
                if (File.Exists(Program.FMWhiteList))
                {
                    string content = File.ReadAllText(Program.FMWhiteList);
                }
                string newContent = whiteListTextBox.Text;
                File.AppendAllText(Program.FMWhiteList, newContent + Environment.NewLine);
            }
            else
            {
                if (File.Exists(Program.FMBlackList))
                {
                    string content = File.ReadAllText(Program.FMBlackList);
                }
                string newContent = whiteListTextBox.Text;
                File.AppendAllText(Program.FMBlackList, newContent + Environment.NewLine);
            }
            whiteListTextBox.Text = "";
            updateWhiteList(false);
        }

        public string RemoveLine(string FilePath, int Position)
        {
            string OutputString = "";
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (counter != Position - 1)
                {
                    OutputString += line + Environment.NewLine;
                }
                counter++;
            }
            file.Close();
            return OutputString;
        }

        private void WriteToFile(string filepath, string contents)
        {
            StreamWriter objStreamWriter = new StreamWriter(filepath);
            objStreamWriter.Write(contents);
            objStreamWriter.Close();
        }

        public void updateActiveAccounts(bool refresh)
        {
            GUIInvokeMethod(() =>
                {
                    try
                    {
                        activeAccounts.BeginUpdate();
                        if (refresh)
                        {
                            foreach (var clientItem in activeAccounts.Items)
                            {
                                activeAccounts.DisplayMember = clientItem.ToString();
                            }
                            activeAccounts.EndUpdate();
                            return;
                        }
                        else
                        {
                            int selected = activeAccounts.SelectedIndex;
                            activeAccounts.Items.Clear();
                            foreach (Client c in Program.clients)
                            {
                                activeAccounts.Items.Add(c);
                            }
                            if (activeAccounts.Items.Count >= selected + 1)
                                activeAccounts.SelectedIndex = selected;
                            else
                                activeAccounts.SelectedIndex = -1;
                        }
                        activeAccountsGroupBox.Text = "Active Accounts: " + activeAccounts.Items.Count.ToString() + " Bots";
                        activeAccounts.EndUpdate();
                    }
                    catch
                    {
                        Thread.Sleep(2000);
                        updateActiveAccounts(false);
                    }
                });
        }

        private void activeAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (activeAccounts.SelectedIndex > -1 & activeAccounts.SelectedIndex != oldIndex)
                {
                    oldIndex = activeAccounts.SelectedIndex;
                    c = (Client)activeAccounts.SelectedItem;
                    mushyUpdating = false;
                    playerUpdating = false;
                    changeAccounts();
                }
            }
            catch
            {
            }
        }

        private void changeAccounts()
        {
            Program.logNeedsUpdating = true;
            if (c == null)
                c = c2;
            c.pictureBoxChange();
            c.updateLog("");
            UpdatePlayers(c);
            updateMushy(c);
            updateCharInfo(c);
        }


        private void activeAccounts_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (activeAccounts.SelectedIndex != -1)
                {
                    activeAccounts.SelectedIndex = activeAccounts.IndexFromPoint(e.X, e.Y);
                    Client client = (Client)activeAccounts.SelectedItem;
                    contextMenuStrip2.Items.Clear();
                    bool exists = false;
                    if (client.clientMode == ClientMode.IDLE)
                    {
                        foreach (AutoChat cinfo in autoChatWindows)
                        {
                            if (cinfo.ToString() == client.toProfile())
                                exists = true;
                        }
                        if (!exists)
                            contextMenuStrip2.Items.Add("Auto Chat", null, autoChatClicked);
                        exists = false;
                        if (client.myCharacter.mapID > 910000000 & client.myCharacter.mapID < 910000023)
                        {

                            foreach (FMRoomChange cinfo in FMRoomChangeWindows)
                            {
                                if (cinfo.ToString() == client.toProfile())
                                    exists = true;
                            }
                            if (!exists)
                                contextMenuStrip2.Items.Add("Change FM Location", null, changeFMLocationClicked);
                        }
                        exists = false;
                        foreach (ModeSelector cinfo in modeWindows)
                        {
                            if (cinfo.ToString() == client.toProfile())
                                exists = true;
                        }
                        if (!exists)
                            contextMenuStrip2.Items.Add("Change Mode", null, setModeClicked);
                        exists = false;
                        foreach (AutoBuff cinfo in autoBuffWindows)
                        {
                            if (cinfo.ToString() == client.toProfile())
                                exists = true;
                        }
                        if (!exists)
                            contextMenuStrip2.Items.Add("Auto Buff", null, autoBuffClicked);
                        exists = false;
                        foreach (StoreAFKer cinfo in storeAFKWindows)
                        {
                            if (cinfo.ToString() == client.toProfile())
                                exists = true;
                        }
                        if (!exists)
                            contextMenuStrip2.Items.Add("Store AFK-er", null, storeAFKClicked);
                    }
                    else
                    {
                        contextMenuStrip2.Items.Add("Stop / Make Idle", null, makeAccountIdle);
                    }
                    exists = false;
                    if (client.myCharacter.ign != null)
                    {
                        foreach (CharacterInfo cinfo in charWindows)
                        {
                            if (cinfo.ToString() == client.toProfile() & client.clientMode != ClientMode.DISCONNECTED)
                                exists = true;
                        }
                        if (!exists)
                            contextMenuStrip2.Items.Add("Bot Functions", null, botFunctionsClicked);
                    }
                    contextMenuStrip2.Items.Add("Terminate Bot", null, terminateClicked);
                    contextMenuStrip2.Items.Add("Cancel", null, cancelClicked);
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        contextMenuStrip2.Show(activeAccounts, e.X, e.Y);
                    }
                }
            }
            catch { }
        }

        private void crashOptionClicked(object sender, EventArgs e)
        {
            Client selectedItem = (Client)activeAccounts.SelectedItem;
            CrashOptions crashWindow = new CrashOptions(selectedItem);
            foreach (CrashOptions form in crashWindows)
            {
                if (form.client == selectedItem)
                {
                    form.Focus();
                    return;
                }
            }
            crashWindows.Add(crashWindow);
            crashWindow.Show();
        }

        private void closeShopClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close your shop?", "Close shop?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (activeAccounts.SelectedIndex > -1)
                {
                    Client client = (Client)activeAccounts.SelectedItem;
                    client.mode = 3;
                    client.modeBak = 3; 
                    Thread threadz = new Thread(() => client.onServerConnected(false));
                    client.workerThreads.Add(threadz);
                    threadz.Start();
                }
                else 
                {
                    MessageBox.Show("Please highlight a bot.");
                }
            }
        }

        private void resetShopClicked(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset your shop?", "Reset shop?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (activeAccounts.SelectedIndex > -1)
                {
                    Client client = (Client)activeAccounts.SelectedItem;
                    client.mode = 2;
                    client.modeBak = 2;
                    Thread threadz = new Thread(() => client.onServerConnected(false));
                    client.workerThreads.Add(threadz);
                    threadz.Start();
                }
                else
                {
                    MessageBox.Show("Please highlight a bot.");
                }
            }
        }

        private void makeAccountIdle(object sender, EventArgs e)
        {
            if (activeAccounts.SelectedIndex > -1)
            {
                Client client = (Client)activeAccounts.SelectedItem;
                client.makeAccountIdle();
                client.updateLog("[Bot] Status: Idle");
                client.updateAccountStatus("Idle");
            }
            else
            {
                MessageBox.Show("Please highlight a bot.");
            }
        }

        private void changeFMLocationClicked(object sender, EventArgs e)
        {
            Client selectedItem = (Client)activeAccounts.SelectedItem;
            FMRoomChange locationChangewindow = new FMRoomChange(selectedItem);
            FMRoomChangeWindows.Add(locationChangewindow);
            locationChangewindow.Show();
        }


        private void storeAFKClicked(object sender, EventArgs e)
        {
            Client selectedItem = (Client)activeAccounts.SelectedItem;
            StoreAFKer storeAFK = new StoreAFKer(selectedItem);
            storeAFKWindows.Add(storeAFK);
            storeAFK.Show();
        }


        private void autoBuffClicked(object sender, EventArgs e)
        {
            if (activeAccounts.SelectedIndex > -1)
            {
                Client selectedItem = (Client)activeAccounts.SelectedItem;
                AutoBuff autoBuff = new AutoBuff(selectedItem);
                autoBuffWindows.Add(autoBuff);
                autoBuff.Show();
            }
            else
            {
                MessageBox.Show("Please highlight a bot.");
            }
        }

        private void terminateClicked(object sender, EventArgs e)
        {
            try
            {
                if (activeAccounts.SelectedIndex > -1)
                {
                    Client selectedItem = (Client)activeAccounts.SelectedItem;
                    selectedItem.forceDisconnect(false, 0, false, "User termination", true);
                }
                else
                {
                    MessageBox.Show("Please highlight a profile from the active accounts box to terminate.");
                }
            }
            catch { }
        }

        private void cancelClicked(object sender, EventArgs e)
        {
            contextMenuStrip1.Dispose();
        }

        private void autoChatClicked(object sender, EventArgs e)
        {
            Client selectedItem = (Client)activeAccounts.SelectedItem;
            AutoChat autoChat = new AutoChat(selectedItem);
            autoChatWindows.Add(autoChat);
            autoChat.Show();
        }
        private void setModeClicked(object sender, EventArgs e)
        {
            Client selectedItem = (Client)activeAccounts.SelectedItem;
            ModeSelector mode = new ModeSelector(selectedItem);
            modeWindows.Add(mode);
            mode.Show();
        }
        private void botFunctionsClicked(object sender, EventArgs e)
        {
            CharacterInfo charinfo = new CharacterInfo(c);
            charWindows.Add(charinfo);
            charinfo.Show();
        }
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }



        private void commandTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == (char)13)
                {
                    button5_Click_1(null, null);
                    GUIInvokeMethod(() => commandTextBox.Text = "");
                }
            }
        }

        private void Player_Box_MouseDown(object sender, MouseEventArgs e)
        {
            Player_Box.SelectedIndex = Player_Box.IndexFromPoint(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                player_Box_Menu.Show(Player_Box, e.X, e.Y);
            }
        }

        private void copyPlayerUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Player_Box.SelectedIndex > -1)
            {
                string[] str = Player_Box.SelectedItem.ToString().Split(':', ' ');
                Clipboard.SetText(str[2] + " " + str[3] + " " + str[4] + " " + str[5]);
            }
        }

        private void copyPlayerCoordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool selectedItem = Player_Box.SelectedItem == null;
            if (!selectedItem)
            {
                char[] chrArray = new char[] { ' ' };
                string str = Player_Box.Text.Split(chrArray)[6];

                List<Player> users = new List<Player>(c.Players.Values);
                while (users.Count > 0 & c.clientMode != ClientMode.DISCONNECTED)
                {
                    try
                    {
                        Player current = users[0];
                        if (current.ign == str)
                        {
                            Foothold foothold = c.myCharacter.Map.footholds.findBelow(new Point(current.x, current.y));
                            object[] objArray = new object[] { current.x, ",", foothold.getY1(), ",", foothold.getId() };
                            Clipboard.SetText(string.Concat(objArray));
                            return;
                        }
                        users.Remove(current);
                    }
                    catch { }
                }
            }
        }

        private void cancelToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            player_Box_Menu.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void openSettingsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.settingFolder);
        }

        private void textBoxGetPlayer_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == (char)13)
                {
                    MovementPacketTest_Click(null, null);
                    GUIInvokeMethod(() => textBoxGetPlayer.Text = "");
                }
            }
        }

        private void proxySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ProxySettings)
                {
                    f.Focus();
                    return;
                }
            }
            new ProxySettings().Show();
        }

        private void customizeableBotSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is Settings)
                {
                    f.Focus();
                    return;
                }
            }
            new Settings().Show();
        }

        private void allProfileSettingsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is AllProfiles)
                {
                    f.Focus();
                    return;
                }
            }
            new AllProfiles().Show();
        }

        private void profToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ProfileOrganizer)
                {
                    f.Focus();
                    return;
                }
            }
            new ProfileOrganizer().Show();
        }

        private void proxySettingsToolStripMenu_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ProxySettings)
                {
                    f.Focus();
                    return;
                }
            }
            new ProxySettings().Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                updateActiveAccounts(true);
            }
            catch { }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (Client terminatedClient in Program.endedClients)
                {
                    if (Program.clients.Contains(terminatedClient))
                    {
                        terminatedClient.forceDisconnect(false, 1, false, "Terminated client", true);
                    }
                }
                /*
                foreach (Client terminatedClient in Program.keepRunning)
                {
                    if (Program.endedClients.Contains(terminatedClient))
                    {
                        terminatedClient.forceDisconnect(true, 1, false);
                    }
                }
                */
            }
            catch { }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (Client clients in Program.clients)
                {
                    try
                    {
                        List<Thread> t = clients.workerThreads.ToList<Thread>();
                        lock (t)
                        {
                            if (t.Count > 20)
                            {
                                for (int x = 0; x < t.Count - 20; x++)
                                {
                                    if (!t[x].IsAlive)
                                    {
                                        clients.workerThreads.Remove(t[x]);
                                    }
                                }
                            }
                        }
                        if (clients.timeLeft() < -10 && clients.clientMode == ClientMode.DISCONNECTED)
                        {
                            clients.forceDisconnect(true, 0, false, "Unknown Startup Error");
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }


    }
}
