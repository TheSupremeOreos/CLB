using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using PixelCLB.Net;

namespace PixelCLB
{
    static class Program
    {
        public static PixelCLB gui = null;
        public static EndedBots terminatedBots = null;
        public static HWIDVerify verifyHWID;
        public static string serverip = "8.31.99.141", accessLevelLabel = "";
        public static string clbName = "PixelCLB", version = "176.1", proxyDirectory = "", uidFileDirectory = "", FMTimesFileDirectory = "", resetFilterIGNs = "", HWID = string.Empty, name = string.Empty, logOffPass = string.Empty, slogan = string.Empty, adLink = string.Empty;
        public static short port = 8484;
        public static bool usingMapleCrypto = false;
        public static bool usingProxy = false, usingHWID = false, whiteList = true, resetMethod = true, exportUIDs = false, debugMode = false, userDebugMode = false, exportFMTimes = false, openFMOwlWindow = true, allChannelsOwl = false, continousFMOwl = false, switchAccountsOwl = false;
        public static bool webLogin = true, logNeedsUpdating = false;
        public static int permitID = 5140000, nexonAuthRestartTime = 180, accountLoggedRestartTime = 180, mapleVersion = 176;
        public static long price = 9999999999;
        public static double accessLevel = 0;
        public static int loadPercent = 0;

        public static string GMSKey = "B35D0DAD2C61D40E9684B131656DF37F99CA998F321078C2D068535941D5DBF5";

        public static IniFile iniFile = new IniFile(@"\ProgramData\PixelCLB\PixelCLBSettings.ini");
        public static string settingFolder = @"\ProgramData\PixelCLB";
        public static string settingFile = @"\ProgramData\PixelCLB\PixelCLBSettings.ini";
        public static string FMWhiteList = @"\ProgramData\PixelCLB\FMWhitelist.txt";
        public static string FMBlackList = @"\ProgramData\PixelCLB\FMBlacklist.txt";
        public static string FMExport = @"\ProgramData\PixelCLB";

        public static string lootList = @"\ProgramData\PixelCLB\Loot.txt";
        public static string lootIgnoreList = @"\ProgramData\PixelCLB\IgnoreLoot.txt";

        public static List<String> clientsStarted = new List<String>();

        public static List<Client> keepRunning = new List<Client>();
        public static List<Client> clients = new List<Client>();
        public static List<Client> endedClients = new List<Client>();

        //Harvest
        public static List<int> harvestIDs = new List<int>();
        public static List<int> lootIDs = new List<int>();

        public static List<string> modes = new List<string>();
        public static List<string> worlds = new List<string>();
        public static List<string> shopTypes = new List<string>();
        public static List<string> commands = new List<string>();

        public static List<int> itemBags = new List<int>();
        //public static List<int> storeIDs = new List<int>();

        public static int exploitID = 2431195;

        [STAThread]
        private static void Main()
        {
            /*
            Database.loadEtcStrings();
            Database.loadEquipStrings();
            Database.loadInsStrings();
            Database.loadSkillStrings();
            Database.loadConsumeStrings();
            PacketReader x2 = new PacketReader(new byte[] { 0xC7, 0x4F, 0xFF, 0xA9 });
            int Packet = x2.ReadInt();
            MessageBox.Show(Packet.ToString());
            int y = Packets.CRC32.CalcPortalCRC(Packet, 10702070);
            MessageBox.Show("PortalCRC: " + y.ToString());
            
            */
            uint z = Packets.CRC32.CalcItemCRC(2020013);
            //MessageBox.Show("2020013 | " + z.ToString());
            uint z2 = Packets.CRC32.CalcItemCRC(2020013);
           // MessageBox.Show("2020013 | " + z2.ToString());


            uint x = Packets.CRC32.CalcItemCRC(2020015);
            MessageBox.Show("2020015 | " + x.ToString());


            //uint crc = Packets.CRC32.CalcCRC32(176, 4, 0, 0);
            //crc = Packets.CRC32.CalcCRC32(1, 5) ^ crc;
            //crc = Packets.CRC32.CalcCRC32(0, 5) ^ crc;
            //crc = Packets.CRC32.CalcCRC32(0, 5) ^ crc;
            //crc = crc ^ 240000001;
            //MessageBox.Show(crc.ToString());
            //Database.exportAllItemNames();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            checkNecessaryFolders();
            gui = new PixelCLB();
            gui.c.updateLog("[Loading " + loadPercent + "%] Packet CRC Loading...");
            GMSKeys.Initialize();
            gui.c.updateLog("[Loading " + loadPercent + "%] Packet CRC Complete");
            loadPercent = loadPercent + 10;

            gui.c.updateLog("[Loading " + loadPercent + "%] Checking necessary files...");
            checkNecessaryFiles();
            loadPercent = loadPercent + 10;
            gui.c.updateLog("[Loading " + loadPercent + "%] All files accounted for.");

            gui.c.updateLog("[Loading " + loadPercent + "%] Checking settings...");
            checkSettings();
            loadPercent = loadPercent + 10;
            gui.c.updateLog("[Loading " + loadPercent + "%] Settings are all in line...");

            Thread load = new Thread(delegate()
            {
                gui.c.updateLog("[Loading " + loadPercent + "%] Gathering required attributes");
                getAttributes(gui.c);
                loadPercent = loadPercent + 5;
                gui.c.updateLog("[Loading " + loadPercent + "%] All necessary information gathered.");
                gui.c.updateLog("[Loading " + loadPercent + "%] Retrieving the last of it!");
                Thread.Sleep(1000);
                loadPercent = loadPercent + 5;
                while (loadPercent < 100)
                {
                    Thread.Sleep(1000);
                }
                gui.c.updateLog("[" + loadPercent + "% Complete] Complete! Ready for use.");
            });
            load.Start();
            Application.Run(gui);
            load.Join();
        }

        private static void checkNecessaryFolders()
        {
            if (Directory.Exists(@"\ProgramData\RichieCLB"))
                Directory.Move(@"\ProgramData\RichieCLB", Program.settingFolder);
            if (File.Exists(@"\ProgramData\PixelCLB\RichieCLBSettings.ini"))
                File.Move(@"\ProgramData\PixelCLB\RichieCLBSettings.ini", Program.settingFile);


            if (!Directory.Exists(Program.settingFolder))
                Directory.CreateDirectory(Program.settingFolder);

            if (!File.Exists(Program.settingFile))
                File.Create(Program.settingFile).Dispose();

            if (!File.Exists(Program.FMWhiteList))
                File.Create(Program.FMWhiteList).Dispose();

            if (!File.Exists(Program.FMBlackList))
                File.Create(Program.FMBlackList).Dispose();

            if (!File.Exists(Program.lootIgnoreList))
                File.Create(Program.lootIgnoreList).Dispose();
        }
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private static void checkNecessaryFiles()
        {

            if (!Directory.Exists(@"WZFiles"))
            {
                MessageBox.Show("WZFiles directory not found. Please refer to the readme.txt");
                Environment.Exit(0);
            }
            else
            {
                if (!File.Exists(@"WZFiles/Map.wz"))
                {
                    MessageBox.Show("Map.wz file not found! Please be sure you have a Map.wz file in the WZFiles directory. \nRefer to the read me. \nNow exiting.");
                    Environment.Exit(0);
                }
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz file not found! Please be sure you have a String.wz file in the WZFiles directory. \nRefer to the read me. \nNow exiting.");
                    Environment.Exit(0);
                }
                if (!File.Exists(@"WZFiles/Item.wz"))
                {
                    MessageBox.Show("Item.wz file not found! Please be sure you have a Item.wz file in the WZFiles directory. \nRefer to the read me. \nNow exiting.");
                    Environment.Exit(0);
                }
                if (!File.Exists(@"WZFiles/Character.wz"))
                {
                    MessageBox.Show("Character.wz file not found! Please be sure you have a Character.wz file in the WZFiles directory. \nRefer to the read me. \nNow exiting.");
                    Environment.Exit(0);
                }
            }
        }

        public static void loadWorldsAndMode()
        {
            //Modes
            modes.Clear();
            modes.Add("Regular Log In");
            modes.Add("FM Spot Bot (Permit)");
            modes.Add("FM Spot Bot (Non-Permit)");
            modes.Add("FM Spot Camper (Permit)");
            modes.Add("FM Spot Camper (Non-Permit)");
            modes.Add("Full Map FM Spot Bot (Permit)");
            modes.Add("Full Map FM Spot Bot (Non-Permit)");
            modes.Add("Close Shop");
            modes.Add("Reset Shop");
            if (accessLevel <= 3)
            {
                modes.Add("Map Lag / Overload");
            }
            if (accessLevel <= 2) //JDR
            {
                modes.Add("Expedition DC");
                modes.Add("Alliance DC");
                modes.Add("IGN Stealer");
            }
            if (accessLevel <= 1) //Exploiter
            {
                if (accessLevel == 1 || accessLevel == 0)
                {
                    //modes.Add("Maple Farm - Ilya");
                    modes.Add("Login Spam - John");
                    modes.Add("Moon Spawn");
                    modes.Add("WB Meso Exploit");
                }
                if (accessLevel == .5 || accessLevel == 0)
                {
                    modes.Add("Mushy Redirect");
                }
                modes.Add("Cassandra CSS Exploit");
            }
            if (accessLevel == 0) // Admin 
            {
                modes.Add("FM Search (Owl)");
                modes.Add("Ore Bot (Mining)");
                modes.Add("Mining Bot (Herblore)");
                modes.Add("8th Anni Bot (Towns)");
            }

            //Worlds
            worlds.Clear();
            worlds.Add("Scania");
            worlds.Add("Bera");
            worlds.Add("Broa");
            worlds.Add("Windia");
            worlds.Add("Khaini");
            worlds.Add("Bellocan");
            worlds.Add("Mardia");
            worlds.Add("Kradia");
            worlds.Add("Yellonde");
            worlds.Add("Demethos");
            worlds.Add("Galicia");
            worlds.Add("El Nido");
            worlds.Add("Zenith");
            worlds.Add("Arcania");
            worlds.Add("Chaos");
            worlds.Add("Nova");
            worlds.Add("Renegades");
        }

        public static void getAccessLevel(double accessLevel)
        {
            if (accessLevel <= 7)
                Program.accessLevelLabel = "Free User";
            if (accessLevel <= 6)
                Program.accessLevelLabel = "Free User";
            if (accessLevel <= 5)
                Program.accessLevelLabel = "VIP User Access - Bronze";
            if (accessLevel <= 4)
                Program.accessLevelLabel = "VIP User Access - Silver";
            if (accessLevel <= 3)
                Program.accessLevelLabel = "VIP User Access - Gold";
            if (accessLevel <= 2)
                Program.accessLevelLabel = "Premium VIP - JDR Crew";
            if (accessLevel <= 1)
                Program.accessLevelLabel = "Exploiter";
            if (accessLevel <= 0)
                Program.accessLevelLabel = "Administrator. Full Access";
        }

        private static void getAttributes(Client c)
        {

            //Timer
            //verifyHWID = new HWIDVerify(20.4, 30.1);

            //commands
            commands.Add("[CMD] cmdlist = List of commands");
            commands.Add("[CMD] deadbots - Ended Bot Logs");
            commands.Add("[CMD] whitelist - FM Whitelist");
            commands.Add("[CMD] fmlist - FM Owl Output list");
            commands.Add("[CMD] minimap (or minimap xxxx) - opens minimap");
            commands.Add("[CMD] dc uid - UID Format Ex: 00 00 00 00");
            commands.Add("[CMD] entercs");
            commands.Add("[CMD] exitcs");
            commands.Add("[CMD] say ___ - Says text in all chat");
            //commands.Add("[CMD] crash");
            //commands.Add("[CMD] exploit xxxx - Changes itemID to exploit. X = itemID");
            //commands.Add("[CMD] generate xxxx - Generates item. X = itemID");



            //ShopTypes
            shopTypes.Add("Automatic");
            shopTypes.Add("Permit");
            shopTypes.Add("1 Day Mushy");
            shopTypes.Add("7 Day Mushy");
            shopTypes.Add("14 Day Mushy");
            shopTypes.Add("30 Day Mushy");
            shopTypes.Add("1 Day Granny");
            shopTypes.Add("7 Day Granny");
            shopTypes.Add("30 Day Granny");
            shopTypes.Add("1 Day Coffeehouse");
            shopTypes.Add("7 Day Coffeehouse");
            shopTypes.Add("1 Day Teddy");
            shopTypes.Add("7 Day Teddy");
            shopTypes.Add("1 Day Robot");
            shopTypes.Add("7 Day Robot");
            shopTypes.Add("Tiki (1 / 7 Day(s)");
            shopTypes.Add("Black Friday Shop");


            /*/StoreIDs
            storeIDs.Add(5030001); //Mushy
            storeIDs.Add(5030000);
            storeIDs.Add(5030006);
            storeIDs.Add(5030010); //Granny
            storeIDs.Add(5030011);
            storeIDs.Add(5030008); //Coffeehouse
            storeIDs.Add(5030009);
            storeIDs.Add(5030002); //Teddy Bear Clerk
            storeIDs.Add(5030003);
            storeIDs.Add(5030005); //Robot Stand
            storeIDs.Add(5030004);
            storeIDs.Add(5030012); //Tiki
            */


            //ItemBags
            itemBags.Add(4330004); //4 Slot Mineral
            itemBags.Add(4330005); //5 Slot Mineral
            itemBags.Add(4330006); //6 Slot Mineral
            itemBags.Add(4330007); //8 Slot Mineral
            itemBags.Add(4330009); //Cole's Mineral bag
            itemBags.Add(4330011); //Master's mineral bag
            itemBags.Add(4330013); //Battle Square Mineral Bag
            itemBags.Add(4330015); //12 Slot Mineral
            itemBags.Add(4330018); //Grant's Mineral bag
            itemBags.Add(4330022); //10 Slot Mineral
            itemBags.Add(4330025); //12 Slot Mineral

            itemBags.Add(4330000); //4 Slot Herb Bag
            itemBags.Add(4330001); //5 Slot Herb Bag
            itemBags.Add(4330002); //6 Slot Herb Bag
            itemBags.Add(4330003); //8 Slot Herb Bag
            itemBags.Add(4330008); //Saffron's Herb Bag
            itemBags.Add(4330010); //Master Herb Bag
            itemBags.Add(4330012); //BattleSquare Herb Bag
            itemBags.Add(4330014); //12 Slot Herb Bag
            itemBags.Add(4330017); //Grant's Herb Bag
            itemBags.Add(4330021); //10 Slot Herb Bag
            itemBags.Add(4330024); //12 Slot Herb Bag


            Program.lootIDs.Clear();
            if (!File.Exists(Program.lootList))
            {
                StreamWriter streamWriter = File.AppendText(Program.lootList);
                foreach (KeyValuePair<int, string> items in Database.ItemCRC)
                {
                    streamWriter.WriteLine(Database.getItemName(items.Key));
                }
                streamWriter.Close();
                string line;
                StreamReader file = new StreamReader(Program.lootList);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("Name not found: "))
                    {
                        string[] lines = line.Split(':');
                        Program.lootIDs.Add(Database.getItemID(line.Replace(" ", "")));
                    }
                    else
                    {
                        Program.lootIDs.Add(Database.getItemID(line));
                    }
                }
                file.Close();
            }
            else
            {
                string line;
                StreamReader file = new StreamReader(Program.lootList);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("Name not found: "))
                    {
                        string[] lines = line.Split(':');
                        Program.lootIDs.Add(int.Parse(lines[1].Replace(" ", "")));
                    }
                    else
                    {
                        Program.lootIDs.Add(Database.getItemID(line));
                    }
                }
                file.Close();
            }

            Database.loadAttributes(c);
        }

        public static int getModeID(string mode)
        {
            int modeID = 0;
            switch (mode)
            {
                case "Regular Log In":
                    {
                        modeID = 0;
                        break;
                    }
                case "Expedition DC":
                    {
                        modeID = 1;
                        break;
                    }
                case "Reset Shop":
                    {
                        modeID = 2;
                        break;
                    }
                case "Close Shop":
                    {
                        modeID = 3;
                        break;
                    }
                case "FM Spot Bot (Non-Permit)":
                    {
                        modeID = 4;
                        break;
                    }
                case "FM Spot Bot (Permit)":
                    {
                        modeID = 5;
                        break;
                    }
                case "Full Map FM Spot Bot (Permit)":
                    {
                        modeID = 6;
                        break;
                    }
                case "Full Map FM Spot Bot (Non-Permit)":
                    {
                        modeID = 7;
                        break;
                    }
                case "FM Spot Camper (Permit)":
                    {
                        modeID = 17;
                        break;
                    }
                case "FM Spot Camper (Non-Permit)":
                    {
                        modeID = 16;
                        break;
                    }
                case "FM Search (Owl)":
                    {
                        modeID = 8;
                        break;
                    }
                case "Ore Bot (Mining)":
                    {
                        modeID = 10;
                        break;
                    }
                case "Mining Bot (Herblore)":
                    {
                        modeID = 11;
                        break;
                    }
                case "8th Anni Bot (Towns)":
                    {
                        modeID = 12;
                        break;
                    }
                case "AutoChat":
                    {
                        modeID = 13;
                        break;
                    }
                case "Auto Buff":
                    {
                        modeID = 14;
                        break;
                    }
                case "Store AFK":
                    {
                        modeID = 15;
                        break;
                    }
                case "Mushy Redirect":
                    {
                        modeID = 19;
                        break;
                    }
                case "Alliance DC":
                    {
                        modeID = 20;
                        break;
                    }
                case "IGN Stealer":
                    {
                        modeID = 30;
                        break;
                   }
                case "WB Meso Exploit":
                    {
                        modeID = 94;
                        break;
                    }
                case "Cassandra CSS Exploit":
                    {
                        modeID = 95;
                        break;
                    }
                case "Moon Spawn":
                    {
                        modeID = 96;
                        break;
                    }
                case "Map Lag / Overload":
                    {
                        modeID = 99;
                        break;
                    }
                case "Login Spam - John":
                    {
                        modeID = 97;
                        break;
                    }
                case "Maple Farm - Ilya":
                    {
                        modeID = 98;
                        break;
                    }
            }
            return modeID;
        }

        public static int getShopID(string shopType)
        {
            int storeID = 5140000;
            switch (shopType)
            {
                case "Permit":
                    {
                        storeID = 5140000;
                        break;
                    }
                case "1 Day Mushy":
                    {
                        storeID = 5030001;
                        break;
                    }
                case "7 Day Mushy":
                    {
                        storeID = 5030000;
                        break;
                    }
                case "14 Day Mushy":
                    {
                        storeID = 5030006;
                        break;
                    }
                case "30 Day Mushy":
                    {
                        storeID = 5030006; //503001?
                        break;
                    }
                case "1 Day Granny":
                    {
                        storeID = 5030010;
                        break;
                    }
                case "7 Day Granny":
                    {
                        storeID = 5030010;
                        break;
                    }
                case "14 Day Granny":
                    {
                        storeID = 5030011;
                        break;
                    }
                case "30 Day Granny":
                    {
                        storeID = 5030010;
                        break;
                    }
                case "1 Day Coffeehouse":
                    {
                        storeID = 5030008;
                        break;
                    }
                case "7 Day Coffeehouse":
                    {
                        storeID = 5030009;
                        break;
                    }
                case "1 Day Teddy":
                    {
                        storeID = 5030003;
                        break;
                    }
                case "7 Day Teddy":
                    {
                        storeID = 5030002;
                        break;
                    }
                case "Tiki (1 / 7 Day(s))":
                    {
                        storeID = 5030012;
                        break;
                    }
                case "1 Day Robot":
                    {
                        storeID = 5030005;
                        break;
                    }
                case "7 Day Robot":
                    {
                        storeID = 5030004;
                        break;
                    }
                case "Black Friday Shop":
                    {
                        storeID = 5030029;
                        break;
                    }
            }
            return storeID;
        }

        private static void checkSettings()
        {
            try
            {
                Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues("CLB Settings");
                if (!sectionValues.ContainsKey("ProxyDirectory"))
                    Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", "");
                if (!sectionValues.ContainsKey("UseProxy"))
                    Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                if (!sectionValues.ContainsKey("Whitelist"))
                    Program.iniFile.WriteValue("CLB Settings", "Whitelist", "True");
                if (!sectionValues.ContainsKey("AdminPass"))
                    Program.iniFile.WriteValue("CLB Settings", "AdminPass", "password");
                if (!sectionValues.ContainsKey("ResetMethod"))
                    Program.iniFile.WriteValue("CLB Settings", "ResetMethod", "True");
                if (!sectionValues.ContainsKey("ShopPrice"))
                    Program.iniFile.WriteValue("CLB Settings", "ShopPrice", "9999999999");
                if (!sectionValues.ContainsKey("ExportUID"))
                    Program.iniFile.WriteValue("CLB Settings", "ExportUID", "False");
                if (!sectionValues.ContainsKey("ExportUIDPath"))
                    Program.iniFile.WriteValue("CLB Settings", "ExportUIDPath", "");
                if (!sectionValues.ContainsKey("ExportFMTimes"))
                    Program.iniFile.WriteValue("CLB Settings", "ExportFMTimes", "False");
                if (!sectionValues.ContainsKey("ExportFMTimesPath"))
                    Program.iniFile.WriteValue("CLB Settings", "ExportFMTimesPath", "");
                if (!sectionValues.ContainsKey("WebLogin"))
                    Program.iniFile.WriteValue("CLB Settings", "WebLogin", "True");
                if (!sectionValues.ContainsKey("resetFilterIGNs"))
                    Program.iniFile.WriteValue("CLB Settings", "resetFilterIGNs", "");
                if (!sectionValues.ContainsKey("DebugMode"))
                    Program.iniFile.WriteValue("CLB Settings", "DebugMode", "False");
                if (!sectionValues.ContainsKey("FMOwlPath"))
                    Program.iniFile.WriteValue("CLB Settings", "FMOwlPath", Program.FMExport);
                if (!sectionValues.ContainsKey("OpenFMOwlWindow"))
                    Program.iniFile.WriteValue("CLB Settings", "OpenFMOwlWindow", "True");
                if (!sectionValues.ContainsKey("ContinousFMOwl"))
                    Program.iniFile.WriteValue("CLB Settings", "ContinousFMOwl", "False");
                if (!sectionValues.ContainsKey("allChannelsOwl"))
                    Program.iniFile.WriteValue("CLB Settings", "allChannelsOwl", "False");
                if (!sectionValues.ContainsKey("switchAccountsOwl"))
                    Program.iniFile.WriteValue("CLB Settings", "switchAccountsOwl", "False");
                if (!sectionValues.ContainsKey("nexonAuthRestartTime"))
                    Program.iniFile.WriteValue("CLB Settings", "nexonAuthRestartTime", "180");
                if (!sectionValues.ContainsKey("accountLoggedRestartTime"))
                    Program.iniFile.WriteValue("CLB Settings", "accountLoggedRestartTime", "120");
                sectionValues = Program.iniFile.GetSectionValues("CLB Settings");
                if (Program.iniFile.GetSectionValues("CLB Settings").Count() >= 7)
                {
                    if (sectionValues["Whitelist"].Equals("True"))
                    {
                        Program.whiteList = true;
                    }
                    else
                    {
                        Program.whiteList = false;
                    }

                    if (sectionValues["ResetMethod"].Equals("True"))
                    {
                        Program.resetMethod = true; //Use Cash shop
                    }
                    else
                    {
                        Program.resetMethod = false; //Use CC
                    }

                    if (sectionValues["WebLogin"].Equals("True"))
                    {
                        Program.webLogin = true;
                    }
                    else
                    {
                        Program.webLogin = false;
                    }
                    if (sectionValues["OpenFMOwlWindow"].Equals("True"))
                    {
                        Program.openFMOwlWindow = true;
                    }
                    else
                    {
                        Program.openFMOwlWindow = false;
                    }
                    if (sectionValues["ContinousFMOwl"].Equals("True"))
                    {
                        Program.continousFMOwl = true;
                    }
                    else
                    {
                        Program.continousFMOwl = false;
                    }
                    if (sectionValues["switchAccountsOwl"].Equals("True"))
                    {
                        Program.switchAccountsOwl = true;
                    }
                    else
                    {
                        Program.switchAccountsOwl = false;
                    }
                    if (sectionValues["allChannelsOwl"].Equals("True"))
                    {
                        Program.allChannelsOwl = true;
                    }
                    else
                    {
                        Program.allChannelsOwl = false;
                    }
                    if (sectionValues["UseProxy"].Equals("True"))
                    {
                        Program.usingProxy = true;
                        Program.proxyDirectory = sectionValues["ProxyDirectory"];
                        if (!File.Exists(Program.proxyDirectory))
                        {
                            MessageBox.Show("Proxy settings disabled! Please reconfigure your settings.", "Proxy Settings", MessageBoxButtons.OK);
                            Program.usingProxy = false;
                            Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", "");
                            Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                            return;
                        }
                        else
                        {
                            if (!Program.checkProxyFileFormat())
                            {
                                MessageBox.Show("Proxy settings disabled! Please reconfigure your settings.", "Proxy Settings", MessageBoxButtons.OK);
                                Program.usingProxy = false;
                                Program.iniFile.WriteValue("CLB Settings", "ProxyDirectory", "");
                                Program.iniFile.WriteValue("CLB Settings", "UseProxy", "False");
                                return;
                            }
                        }

                    }
                    else
                    {
                        Program. usingProxy = false;
                    }
                    if (sectionValues["ExportUID"].Equals("True"))
                    {
                        Program.exportUIDs = true; //EXPORT UIDS
                    }
                    else
                    {
                        Program.exportUIDs = false; //DO NOT EXPORT UIDS
                    }
                    if (sectionValues["ExportFMTimes"].Equals("True"))
                    {
                        Program.exportFMTimes = true; //EXPORT TIMES
                    }
                    else
                    {
                        Program.exportFMTimes = false; //DO NOT EXPORT TIMES
                    }
                    if (sectionValues["DebugMode"].Equals("True"))
                    {
                        Program.userDebugMode = true;
                    }
                    else
                    {
                        Program.userDebugMode = false;
                    }
                    Program.FMExport = sectionValues["FMOwlPath"];
                    Program.FMTimesFileDirectory = sectionValues["ExportFMTimesPath"];
                    Program.uidFileDirectory = sectionValues["ExportUIDPath"];
                    Program.logOffPass = sectionValues["AdminPass"];
                    Program.resetFilterIGNs = sectionValues["resetFilterIGNs"];
                    Program.price = long.Parse(sectionValues["ShopPrice"]);
                    Program.nexonAuthRestartTime = int.Parse(sectionValues["nexonAuthRestartTime"]);
                    Program.accountLoggedRestartTime = int.Parse(sectionValues["accountLoggedRestartTime"]);
                }

                foreach (string section in Program.iniFile.GetSectionNames())
                {
                    if (section != "CLB Settings")
                    {
                        Dictionary<string, string> sectionValues2 = Program.iniFile.GetSectionValues(section);
                        if (!sectionValues2.ContainsKey("Username"))
                            Program.iniFile.WriteValue(section, "Username", "Username");
                        if (!sectionValues2.ContainsKey("Password"))
                            Program.iniFile.WriteValue(section, "Password", "Password");
                        if (!sectionValues2.ContainsKey("PIC"))
                            Program.iniFile.WriteValue(section, "PIC", "111111");
                        if (!sectionValues2.ContainsKey("World"))
                            Program.iniFile.WriteValue(section, "World", "Scania");
                        if (!sectionValues2.ContainsKey("Channel"))
                            Program.iniFile.WriteValue(section, "Channel", "1");
                        if (!sectionValues2.ContainsKey("CharNum"))
                            Program.iniFile.WriteValue(section, "CharNum", "1");
                        if (!sectionValues2.ContainsKey("Spawn"))
                            Program.iniFile.WriteValue(section, "Spawn", "");
                        if (!sectionValues2.ContainsKey("Title"))
                            Program.iniFile.WriteValue(section, "Title", "amazing bots!");
                        if (!sectionValues2.ContainsKey("Shop Type"))
                            Program.iniFile.WriteValue(section, "Shop Type", "Permit");
                        if (!sectionValues2.ContainsKey("Mode"))
                            Program.iniFile.WriteValue(section, "Mode", "Regular Log In");
                        if (!sectionValues2.ContainsKey("DCTarget"))
                            Program.iniFile.WriteValue(section, "DCTarget", "");
                        if (!sectionValues2.ContainsKey("CoordOverride"))
                            Program.iniFile.WriteValue(section, "CoordOverride", "False");
                        if (!sectionValues2.ContainsKey("FMRoomOverride"))
                            Program.iniFile.WriteValue(section, "FMRoomOverride", "False");
                        if (!sectionValues2.ContainsKey("FMRoomNum"))
                            Program.iniFile.WriteValue(section, "FMRoomNum", "1");
                        if (!sectionValues2.ContainsKey("KeepRunning"))
                            Program.iniFile.WriteValue(section, "KeepRunning", "False");
                        if (!sectionValues2.ContainsKey("ignSteal"))
                            Program.iniFile.WriteValue(section, "ignSteal", "IGNS");
                        if (!sectionValues2.ContainsKey("LootItems"))
                            Program.iniFile.WriteValue(section, "LootItems", "False");
                        if (!sectionValues2.ContainsKey("FMxLow"))
                            Program.iniFile.WriteValue(section, "FMxLow", "0");
                        if (!sectionValues2.ContainsKey("FMxHigh"))
                            Program.iniFile.WriteValue(section, "FMxHigh", "0");
                        if (!sectionValues2.ContainsKey("FMyLow"))
                            Program.iniFile.WriteValue(section, "FMyLow", "0");
                        if (!sectionValues2.ContainsKey("FMyHigh"))
                            Program.iniFile.WriteValue(section, "FMyHigh", "0");
                        if (!sectionValues2.ContainsKey("AccProxy"))
                            Program.iniFile.WriteValue(section, "AccProxy", "");
                        if (!sectionValues2.ContainsKey("MushyRedirect"))
                            Program.iniFile.WriteValue(section, "MushyRedirect", "");
                        if (!sectionValues2.ContainsKey("MushyRedirect2"))
                            Program.iniFile.WriteValue(section, "MushyRedirect2", "");
                    }
                }

                if (Directory.Exists(Path.Combine(Program.FMExport, "FMExport")))
                {
                    System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(Path.Combine(Program.FMExport, "FMExport"));
                    foreach(System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error writing settings. If this error persists, please report it. \nNow exiting");
                Environment.Exit(0);
            }
        }

        public static bool checkProxyFileFormat()
        {
            bool proxy = false;
            if (File.Exists(Program.proxyDirectory))
            {
                StreamReader file = null;
                try
                {
                    file = new StreamReader(Program.proxyDirectory);
                    string line;
                    int lineCount = 0;
                    while ((line = file.ReadLine()) != null)
                    {
                        lineCount++;
                        if (!line.Contains("@"))
                        {
                            string[] parts = line.Split(':', '.');
                            if (parts.Length != 5)
                            {
                                MessageBox.Show("Line " + lineCount.ToString() + "contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx", "Invalid Format!", MessageBoxButtons.OK);
                                return false;
                            }
                        }
                        else
                        {
                            string[] parts0 = line.Split('@');
                            string[] parts = parts0[1].Split(':', '.');
                            string[] userPass = parts0[0].Split(':');
                            if (userPass.Length != 2 || parts.Length != 5)
                            {
                                MessageBox.Show("Line " + lineCount.ToString() + "contains an invalid IP format! \nPlease fix the ip on the line and browse the file again.\nCorrect non-login format: x.x.x.x:xxxx(skip line)\nCorrect login format: user:pass@x.x.x.x:xxxx", "Invalid Format!", MessageBoxButtons.OK);
                                return false;
                            }
                        }
                    }
                    proxy = true;
                }
                finally
                {
                    if (file != null)
                        file.Close();
                }
            }
            else
            {
                MessageBox.Show("Proxy file does not exist! Please make sure it is a valid text file.", "Invalid File!", MessageBoxButtons.OK);
            }
            return proxy;
        }

    }
}
