using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using PixelCLB.Net.Packets;
using System.Linq;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.InteropServices;


namespace PixelCLB
{
    public class Database
    {
        private static Dictionary<int, MapleMap> mapLibrary = new Dictionary<int, MapleMap>();
        private static Dictionary<int, MapleEquip> equipLibrary = new Dictionary<int, MapleEquip>();
        public static Dictionary<int, MonsterStats> monsterstats = new Dictionary<int, MonsterStats>();
        private static List<int> mpPots = new List<int>();
        private static bool mpPotsloaded = false;
        public static Dictionary<int, ItemStrings> items = new Dictionary<int, ItemStrings>();
        public static Dictionary<int, MSItems> itemCRC = new Dictionary<int, MSItems>();
        public static Dictionary<int, string> skills = new Dictionary<int, string>();
        public static Dictionary<int, string> maps = new Dictionary<int, string>();
        public static Dictionary<int, string> mobs = new Dictionary<int, string>();
        private static Dictionary<int, string> MapCRC = new Dictionary<int, string>();
        public static Dictionary<int, string> ItemCRC = new Dictionary<int, string>();
        private static object CSlock = new object();
        private static Dictionary<int, int> familiarDmgs = new Dictionary<int, int>();
        private static Dictionary<int, int> familiarSkills = new Dictionary<int, int>();
        private static Dictionary<int, string> storeTypes = new Dictionary<int, string>();
        private static object wzLocker = new object();
        public static int crcID = 0;

        public static Dictionary<short, PotentialLines> potentialLines = new Dictionary<short, PotentialLines>();
        public static Dictionary<short, string> nebuliteLines = new Dictionary<short, string>();

        private static Dictionary<int, int> unParseditems = new Dictionary<int, int>();

        public static void loadAttributes(Client c)
        {
            //Storenames
            Database.loadStoreTypes();

            //MapCRc
            Database.loadItemCRC();
            Database.loadMapCRC();
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading MAP names & common maps..");
            Database.loadMapStrings();
            Database.loadMap(910000001);
            Database.loadMap(910000002);
            Database.loadMap(910000003);
            Database.loadMap(910000004);
            Database.loadMap(910000005);
            Database.loadMap(910000006);
            Database.loadMap(910000007);
            Database.loadMap(910000008);
            Database.loadMap(910000009);
            Database.loadMap(910000010);
            Database.loadMap(910000011);
            Database.loadMap(910000012);
            Database.loadMap(910000013);
            Database.loadMap(910000014);
            Database.loadMap(910000015);
            Database.loadMap(910000016);
            Program.loadPercent = Program.loadPercent + 10;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading ETC item names..");
            Database.loadEtcStrings();
            Program.loadPercent = Program.loadPercent + 10;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading EQUIP item names..");
            Database.loadEquipStrings();
            Program.loadPercent = Program.loadPercent + 5;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading USE item names..");
            Database.loadConsumeStrings();
            Program.loadPercent = Program.loadPercent + 10;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading SETUP item names..");
            Database.loadInsStrings();
            Program.loadPercent = Program.loadPercent + 10;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading SKILL names..");
            Database.loadSkillStrings();
            Program.loadPercent = Program.loadPercent + 10;
            c.updateLog("[Loading " + Program.loadPercent + "%] Loading MISC info..");
            //MPPots
            Database.loadMPPots();

            //Database.exportAllItemNames();
            if (Program.name.ToLower().Contains("administrator vps"))
            {

                DialogResult dialogResult2 = MessageBox.Show("Do you want to load Equip Images and Item Names?", "Load item names / item images?", MessageBoxButtons.YesNo);
                if (dialogResult2 == DialogResult.Yes)
                {
                    Database.exportAllItemNames();
                    c.updateLog("Saving equip images");
                    saveEquipImages(c);
                    c.updateLog("Saving item images");
                    saveItemImages(c);
                    c.updateLog("Copying source images");
                    copySourceImages(c);
                    c.updateLog("Imaging complete");
                }

                DialogResult dialogResult = MessageBox.Show("Do you want to load Equip Levels?", "Load equip levels?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    c.updateLog("[Loading " + Program.loadPercent + "%] Loading EQUIP levels..");
                    Thread t = new Thread(delegate ()
                    {
                        Database.loadItemLevel();
                        Program.loadPercent = Program.loadPercent + 5;
                        c.updateLog("[Loading " + Program.loadPercent + "%] Loading EQUIP levels complete");
                        //PotentialLines
                        Database.loadPotentialLines();
                    });
                    t.Start();
                    //do something
                }
                else
                {
                    Program.loadPercent = Program.loadPercent + 5;
                }
            }
            else
                Program.loadPercent = Program.loadPercent + 5;

        }


        public static void exportAllItemNames()
        {

            /*Original
try
{
List<string> strs = new List<string>();
foreach (KeyValuePair<int, ItemStrings> x in items)
{
    if (!strs.Contains(x.Value.Name))
    {
        strs.Add(x.Value.Name);
    }
}
strs.Sort();
string fileDirectory = Path.Combine("itemNames.txt");
if (!File.Exists(fileDirectory))
{
    File.Create(fileDirectory);
    Thread.Sleep(100);
}
while (!File.Exists(fileDirectory))
    Thread.Sleep(1000);
StreamWriter streamWriter = File.AppendText(fileDirectory);
foreach (string str in strs)
{
    streamWriter.WriteLine(str);
}
streamWriter.Close();
}
catch
{ }
*/
            try
            {
                string fileDirectory = Path.Combine("itemNames2.txt");
                if (!File.Exists(fileDirectory))
                {
                    File.Create(fileDirectory);
                    Thread.Sleep(100);
                }
                while (!File.Exists(fileDirectory))
                    Thread.Sleep(1000);
                StreamWriter streamWriter = File.AppendText(fileDirectory);
                foreach (KeyValuePair<int, ItemStrings> x in items)
                {
                    string str = x.Key + " : " + x.Value.Name;
                    streamWriter.WriteLine(str);
                }
                streamWriter.Close();
            }
            catch
            { }
        }

        public static void getMapNPCS()
        {
            try
            {

                WzFile wzFile = new WzFile(@"WZFiles/Map.wz", Constants.WzType);
                wzFile.ParseWzFile();
                int id = 0;
                WzDirectory directoryByName = wzFile.WzDirectory.GetDirectoryByName("Map").GetDirectoryByName(string.Concat("Map", id));
                Dictionary<int, int> mapIDs = new Dictionary<int, int>();
                while (directoryByName != null)
                {
                    foreach (WzImage x in directoryByName.GetChildImages())
                    {
                        WzImageProperty item = x["life"];
                        if (item != null)
                        {
                            if (item.WzProperties.Count() > 0)
                            {
                                string names = x.Name.Replace(".img", "");
                                //MessageBox.Show(item.WzProperties.Count().ToString() + " - " + names.ToString());
                                mapIDs.Add(int.Parse(names), item.WzProperties.Count());
                            }
                            /*
                            if (item["forcedReturn"] == null)
                            {
                                MessageBox.Show(x.Name + "\nNULL\n" + x.Name);
                            }
                            else
                            {
                                try
                                {
                                    int number = int.Parse(item["forcedReturn"].WzValue.ToString());
                                    if (number == 950000100)
                                        MessageBox.Show(number.ToString());
                                }
                                catch
                                {
                                    MessageBox.Show(x.Name);
                                }
                            }
                             * */
                        }
                    }
                    id++;
                    try
                    {
                        directoryByName = wzFile.WzDirectory.GetDirectoryByName("Map").GetDirectoryByName(string.Concat("Map", id));
                    }
                    catch
                    {
                        directoryByName = null;
                    }
                }
                MessageBox.Show(mapIDs.Count().ToString() + " - ");
                string fileDirectory = Path.Combine("MapsIDS.txt");
                if (!File.Exists(fileDirectory))
                {
                    File.Create(fileDirectory);
                    Thread.Sleep(100);
                }
                StreamWriter streamWriter = File.AppendText(fileDirectory);
                foreach (KeyValuePair<int, int> ints in mapIDs)
                {
                    string MapName = Database.getMapName(ints.Key);
                    if (MapName == null)
                        MapName = "Unknown";
                    streamWriter.WriteLine(ints.Key.ToString() + " - " + ints.Value + " - " + MapName);
                }
                streamWriter.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }


        public static MapleMap loadMap(int mapID, Client c = null)
        {
            MapleMap mapleMap;
            WzFile wzFile = null;
            try
            {
                if (!Database.mapLibrary.ContainsKey(mapID))
                {
                    lock (Database.wzLocker)
                    {
                        if (Database.mapLibrary.ContainsKey(mapID))
                            return Database.mapLibrary[mapID].cloneMap();
                        MapleMap mapleMap1 = new MapleMap(mapID);
                        if (!File.Exists(@"WZFiles/Map.wz"))
                        {
                            MessageBox.Show("WZFiles/Map.wz missing.");
                            Environment.Exit(0);
                        }
                        int num = 0;
                        while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Map.wz")))
                        {
                            Thread.Sleep(300);
                            num++;
                            if (num <= 20)
                            {
                                continue;
                            }
                            MessageBox.Show("Map.Wz is in use by another program");
                        }
                        wzFile = new WzFile(@"WZFiles/Map.wz", Constants.WzType);
                        wzFile.ParseWzFile();
                        int id = mapID / 100000000;
                        string mapImageName = mapID.ToString();
                        if (mapID.ToString().Length < 9)
                        {
                            int difference = 9 - mapImageName.Length;
                            for (int x = 0; x < difference; x++)
                            {
                                mapImageName = string.Concat("0", mapImageName);
                            }

                        }
                        List<WzDirectory> wzDirectories = wzFile.WzDirectory.WzDirectories;
                        WzImage imageByName = wzFile.WzDirectory.GetDirectoryByName("Map").GetDirectoryByName(string.Concat("Map", id)).GetImageByName(string.Concat(mapImageName, ".img"));
                        if (imageByName == null)
                        {
                            if (c != null)
                                c.updateLog("[Map] Missing map Data for ID: " + mapID);
                            else
                                MessageBox.Show(string.Concat("Missing Map Data: ", mapID, "\nPlease make sure you have updated wzfiles."));
                            return null;
                        }
                        WzImageProperty item = imageByName["portal"];
                        if (item != null)
                        {
                            foreach (WzImageProperty wzProperty in item.WzProperties)
                            {
                                Portal portal = new Portal();
                                portal.ID = int.Parse(wzProperty.Name);
                                portal.name = wzProperty["pn"].WzValue.ToString();
                                portal.X = (short)int.Parse(wzProperty["x"].WzValue.ToString());
                                portal.Y = (short)int.Parse(wzProperty["y"].WzValue.ToString());
                                portal.dest = int.Parse(wzProperty["tm"].WzValue.ToString());
                                mapleMap1.addPortal(portal);
                            }
                        }
                        mapleMap1.footholds = Database.loadFootholds(imageByName["foothold"]);
                        Database.mapLibrary.Add(mapID, mapleMap1);
                        wzFile.Dispose();
                        mapleMap = mapleMap1.cloneMap();
                    }
                }
                else
                {
                    mapleMap = Database.mapLibrary[mapID].cloneMap();
                }
            }
            catch
            {
                if (wzFile != null)
                {
                    wzFile.Dispose();
                }
                if (c != null)
                    c.updateLog("[Map] Missing map Data for ID: " + mapID);
                else
                    MessageBox.Show(string.Concat("Missing Map Data: ", mapID, "\nPlease make sure you have updated wzfiles."));
                mapleMap = null;
            }
            return mapleMap;
        }

        public static FootholdTree loadFootholds(WzImageProperty mapFootProp)
        {
            List<Foothold> footholds = new List<Foothold>();
            Point point = new Point(10000, 10000);
            Point point1 = new Point(-10000, -10000);
            if (mapFootProp != null)
            {
                foreach (WzImageProperty wzProperty in mapFootProp.WzProperties)
                {
                    foreach (WzImageProperty wzImageProperty in wzProperty.WzProperties)
                    {
                        foreach (WzImageProperty wzProperty1 in wzImageProperty.WzProperties)
                        {
                            int num = int.Parse(wzProperty1.Name);
                            int num1 = int.Parse(wzProperty1["x1"].WzValue.ToString());
                            int num2 = int.Parse(wzProperty1["y1"].WzValue.ToString());
                            int num3 = int.Parse(wzProperty1["x2"].WzValue.ToString());
                            int num4 = int.Parse(wzProperty1["y2"].WzValue.ToString());
                            Foothold foothold = new Foothold(new Point(num1, num2), new Point(num3, num4), num);
                            foothold.setNext(int.Parse(wzProperty1["next"].WzValue.ToString()));
                            foothold.setPrev(int.Parse(wzProperty1["prev"].WzValue.ToString()));
                            if (num1 < point.X)
                            {
                                point.X = num1;
                            }
                            if (num3 > point1.X)
                            {
                                point1.X = num3;
                            }
                            if (num2 < point.Y)
                            {
                                point.Y = num2;
                            }
                            if (num4 > point1.Y)
                            {
                                point1.Y = num4;
                            }
                            footholds.Add(foothold);
                        }
                    }
                }
            }
            FootholdTree footholdTree = new FootholdTree(point, point1);
            foreach (Foothold foothold1 in footholds)
            {
                footholdTree.Insert(foothold1);
            }
            return footholdTree;
        }

        public static void saveItemImages(Client c)
        {
            if (!Directory.Exists(@"C:\images"))
            {
                Directory.CreateDirectory(@"C:\images");
            }
            lock (Database.wzLocker)
            {
                if (!Directory.Exists(@"C:\images"))
                {
                    Directory.CreateDirectory(@"C:\images");
                }
                if (!File.Exists(@"WZFiles/Item.wz"))
                {
                    MessageBox.Show("Item.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Item.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/Item.wz", Constants.WzType);
                wzFile.ParseWzFile();
                foreach (WzDirectory x in wzFile.WzDirectory.subDirs)
                {
                    if (x.Name.ToLower() != "pet")
                    {
                        foreach (WzImage wzImage in x.images)
                        {
                            Thread.Sleep(5);
                            try
                            {
                                foreach (WzImageProperty wzProperty in wzImage.WzProperties)
                                {
                                    Thread.Sleep(3);
                                    int itemID = int.Parse(wzProperty.Name.Replace(".img", ""));
                                    WzImageProperty wzImageProperty = wzProperty["info"];
                                    if (wzImageProperty == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        WzImageProperty item = wzImageProperty["icon"];
                                        if (item != null)
                                        {
                                            try
                                            {

                                                if (item["source"] != null)
                                                {
                                                    try
                                                    {
                                                        WzImageProperty itemSource = item["source"];
                                                        string[] strs = null;
                                                        if (itemSource.WzValue.ToString() != null)
                                                        {
                                                            strs = itemSource.WzValue.ToString().Split('/');
                                                        }
                                                        foreach (string str in strs)
                                                        {
                                                            int num;
                                                            //MessageBox.Show("0 - " + itemID.ToString() + " - " + str);
                                                            //MessageBox.Show("1 - " + itemID.ToString() + " - " + num.ToString());
                                                            bool res = int.TryParse(str, out num);
                                                            if (res)
                                                            {
                                                                //c.updateLog(num.ToString() + " -> " + itemID.ToString());
                                                                if (unParseditems.ContainsKey(itemID))
                                                                    unParseditems.Remove(itemID);
                                                                unParseditems.Add(itemID, num);
                                                                break;
                                                            }
                                                        }
                                                        continue;
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        MessageBox.Show(e.ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    WzCanvasProperty wzCanvasProperty = (WzCanvasProperty)item;
                                                    Bitmap pNG = wzCanvasProperty.PngProperty.GetPNG(true);
                                                    pNG.Save(@"C:\images\" + itemID.ToString() + ".png", ImageFormat.Png);
                                                    pNG.Dispose();
                                                }
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    string[] strs = null;
                                                    WzUOLProperty itemSource = (WzUOLProperty)item;
                                                    if (itemSource.ToString() != null)
                                                    {
                                                        strs = itemSource.ToString().Split('/');
                                                    }
                                                    foreach (string str in strs)
                                                    {
                                                        int num;
                                                        //MessageBox.Show("0 - " + itemID.ToString() + " - " + str);
                                                        //MessageBox.Show("1 - " + itemID.ToString() + " - " + num.ToString());
                                                        bool res = int.TryParse(str, out num);
                                                        if (res)
                                                        {
                                                            if (num > 1000000)
                                                            {
                                                                //c.updateLog(num.ToString() + " -> " + itemID.ToString());
                                                                if (unParseditems.ContainsKey(itemID))
                                                                    unParseditems.Remove(itemID);
                                                                unParseditems.Add(itemID, num);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    MessageBox.Show(itemID.ToString() + "\n" + e.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(x.Name + "\n" + wzImage.Name + "\n" + e.ToString());
                                continue;
                            }
                            wzImage.Dispose();
                        }
                    }
                    else
                    {
                        foreach (WzImage wzImage in x.images)
                        {
                            int itemID = int.Parse(wzImage.Name.Replace(".img", ""));
                            WzImageProperty wzImageProperty = wzImage["info"];
                            if (wzImageProperty == null)
                            {
                                continue;
                            }
                            else
                            {
                                WzImageProperty item = wzImageProperty["icon"];
                                if (item != null)
                                {
                                    try
                                    {
                                        if (item["source"] != null)
                                        {
                                            try
                                            {
                                                WzImageProperty itemSource = item["source"];
                                                string[] strs = null;
                                                if (itemSource.WzValue.ToString() != null)
                                                {
                                                    strs = itemSource.WzValue.ToString().Split('/');
                                                }
                                                itemSource.Dispose();
                                                foreach (string str in strs)
                                                {
                                                    string realSTR = str.Replace(".img", "");
                                                    int num;
                                                    //MessageBox.Show("0 - " + itemID.ToString() + " - " + str);
                                                    //MessageBox.Show("1 - " + itemID.ToString() + " - " + num.ToString());
                                                    bool res = int.TryParse(realSTR, out num);
                                                    if (res)
                                                    {
                                                        if (num > 1000000)
                                                        {
                                                            //c.updateLog(num.ToString() + " -> " + itemID.ToString());
                                                            unParseditems.Add(itemID, num);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                MessageBox.Show(itemID.ToString() + "\n" + e.ToString());
                                            }
                                            continue;
                                        }
                                        else
                                        {
                                            WzCanvasProperty wzCanvasProperty = (WzCanvasProperty)item;
                                            Bitmap pNG = wzCanvasProperty.PngProperty.GetPNG(true);
                                            pNG.Save(@"C:\images\" + itemID.ToString() + ".png", ImageFormat.Png);
                                            pNG.Dispose();
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show("Pet - " + itemID.ToString() + "\n" + e.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                wzFile.Dispose();
            }
        }

        

        public static byte[] ImageToByte2(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public static byte[][] Convert1DArray2D(byte[] Input, int height, int width)
        {
            byte[][] multi = new byte[height][];
            for (int y = 0; y < height; ++y)
            {
                multi[y] = new byte[width];
                // Do optional translation of the byte into your own format here
                // For purpose of illustration, here is a straight copy
                Array.Copy(Input, width * y, multi[y], 0, width);
            }
            return multi;
        }


        public static string getItemType (int itemID)
        {
            string type = "UNK";
            int num = itemID / 1000000;
            if (num == 1)
                type = "Equip";
            else if (num == 2)
                type = "Consume";
            else if (num == 3)
                type = "Install";
            else if (num == 4)
                type = "Etc";
            else if (num == 5)
                type = "Cash";
            else if (num == 9)
                type = "Special";
            return type;
        }



        public static WzCanvasProperty getItemBitMap(WzDirectory dir, WzFile wzfile, int itemID, bool isRaw)
        {
            WzCanvasProperty itemImage = null;
            string itemType = Database.getItemType(itemID);
            string subImage = "0" + itemID.ToString().Substring(0, 3);
            WzImageProperty wzImageProperty = dir.GetImageByName(subImage + ".img")["0" + itemID.ToString()]["info"];
            if (wzImageProperty == null)
                return itemImage;
            MSItems msItem = new MSItems();
            WzImageProperty item;
            if (isRaw)
                item = wzImageProperty["iconRaw"];
            else
                item = wzImageProperty["icon"];
            if (item == null)
                return itemImage;

            msItem.itemID = itemID;
            if (wzImageProperty["price"] != null)
                msItem.itemPrice = int.Parse(wzImageProperty["price"].WzValue.ToString());
            if (wzImageProperty["reqLevel"] != null)
                msItem.itemReqLev = int.Parse(wzImageProperty["reqLevel"].WzValue.ToString());
            if (wzImageProperty["unitPrice"] != null)
                msItem.itemUnitPrice = int.Parse(wzImageProperty["unitPrice"].WzValue.ToString());
            if (wzImageProperty["slotMax"] != null)
                msItem.itemSlotMax = int.Parse(wzImageProperty["slotMax"].WzValue.ToString());
            if (wzImageProperty["max"] != null)
                msItem.itemMax = int.Parse(wzImageProperty["max"].WzValue.ToString());
            if (wzImageProperty["noCancelMouse"] != null)
                msItem.noCancelMouse = int.Parse(wzImageProperty["noCancelMouse"].WzValue.ToString());
            if (wzImageProperty["expireOnLogout"] != null)
                msItem.expireOnLogout = int.Parse(wzImageProperty["expireOnLogout"].WzValue.ToString());
            if (wzImageProperty["notSale"] != null)
                msItem.notSale = int.Parse(wzImageProperty["notSale"].WzValue.ToString());
            if (wzImageProperty["tradeAvailable"] != null)
                msItem.tradeAvailable = int.Parse(wzImageProperty["tradeAvailable"].WzValue.ToString());
            if (wzImageProperty["tradeBlock"] != null)
                msItem.tradeBlock = int.Parse(wzImageProperty["tradeBlock"].WzValue.ToString());
            if (wzImageProperty["timeLimited"] != null)
                msItem.timeLimited = int.Parse(wzImageProperty["timeLimited"].WzValue.ToString());
            if (wzImageProperty["pquest"] != null)
                msItem.pquest = int.Parse(wzImageProperty["pquest"].WzValue.ToString());
            if (wzImageProperty["quest"] != null)
                msItem.quest = int.Parse(wzImageProperty["quest"].WzValue.ToString());
            
            //IfEquip
            if(itemType == "Equip" || itemType == "Etc" || itemType == "Install")
            {

                if (wzImageProperty["reqSTR"] != null)
                    msItem.reqSTR = int.Parse(wzImageProperty["reqSTR"].WzValue.ToString());
                if (wzImageProperty["reqDEX"] != null)
                    msItem.reqDEX = int.Parse(wzImageProperty["reqDEX"].WzValue.ToString());
                if (wzImageProperty["reqINT"] != null)
                    msItem.reqINT = int.Parse(wzImageProperty["reqINT"].WzValue.ToString());
                if (wzImageProperty["reqLUK"] != null)
                    msItem.reqLUK = int.Parse(wzImageProperty["reqLUK"].WzValue.ToString());
                if (wzImageProperty["reqJob"] != null)
                    msItem.reqJob = int.Parse(wzImageProperty["reqJob"].WzValue.ToString());
                if (wzImageProperty["reqPOP"] != null)
                    msItem.reqPOP = int.Parse(wzImageProperty["reqPOP"].WzValue.ToString());
                //price & reqLevel already obtained
                if (wzImageProperty["recovery"] != null)
                    msItem.recovery = long.Parse(wzImageProperty["recovery"].WzValue.ToString());
                if (wzImageProperty["fs"] != null)
                    msItem.fs = long.Parse(wzImageProperty["fs"].WzValue.ToString());
                if (wzImageProperty["knockback"] != null)
                    msItem.knockback = int.Parse(wzImageProperty["knockback"].WzValue.ToString());


                if (wzImageProperty["epicItem"] != null)
                    msItem.epicItem = int.Parse(wzImageProperty["epicItem"].WzValue.ToString());
                if (wzImageProperty["notExtend"] != null)
                    msItem.notExtend = int.Parse(wzImageProperty["notExtend"].WzValue.ToString());
                //expireOnLogout & notSale already obtained
                if (wzImageProperty["accountSharable"] != null)
                    msItem.accountSharable = int.Parse(wzImageProperty["accountSharable"].WzValue.ToString());
                //tradeAvailable & tradeBlock already obtained
                if (wzImageProperty["onlyEquip"] != null)
                    msItem.onlyEquip = int.Parse(wzImageProperty["onlyEquip"].WzValue.ToString());
                if (wzImageProperty["only"] != null)
                    msItem.only = int.Parse(wzImageProperty["only"].WzValue.ToString());
                //timeLimited already Obtained
            }




            if (!Database.itemCRC.ContainsKey(itemID))
                Database.itemCRC.Add(itemID, msItem);

            try
            {
                WzImageProperty itemSource = item["source"];
                if (itemSource != null)
                {
                    string[] strs = null;
                    if (itemSource.WzValue.ToString() != null)
                    {
                        strs = itemSource.WzValue.ToString().Split('/');
                    }
                    itemSource.Dispose();
                    foreach (string str in strs)
                    {
                        string realSTR = str.Replace(".img", "");
                        int x;
                        //MessageBox.Show("0 - " + itemID.ToString() + " - " + str);
                        //MessageBox.Show("1 - " + itemID.ToString() + " - " + num.ToString());
                        bool res = int.TryParse(realSTR, out x);
                        if (res)
                        {
                            if (x > 1000000)
                            {
                                //c.updateLog(num.ToString() + " -> " + itemID.ToString());
                                //getItemBitMap(dir, x);
                                wzfile.Dispose();
                                crcID = x;
                                return null;
                            }
                        }
                    }
                }
            }
            catch { }
            WzCanvasProperty wzCanvasProperty = (WzCanvasProperty)item;
            itemImage = wzCanvasProperty;
            //itemImage = wzCanvasProperty.PngProperty.GetPNG(false);
            return itemImage;
        }
        public static void saveEquipImages(Client c)
        {
            if (!Directory.Exists(@"C:\images"))
            {
                Directory.CreateDirectory(@"C:\images");
            }
            lock (Database.wzLocker)
            {
                if (!Directory.Exists(@"C:\images"))
                {
                    Directory.CreateDirectory(@"C:\images");
                }
                if (!File.Exists(@"WZFiles/Character.wz"))
                {
                    MessageBox.Show("Character.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Character.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/Character.wz", Constants.WzType);
                wzFile.ParseWzFile();
                foreach (WzDirectory x in wzFile.WzDirectory.subDirs)
                {
                    if (x.Name.ToLower() != "afterimage")
                    {
                        foreach (WzImage wzImage in x.images)
                        {
                            Thread.Sleep(1);
                            try
                            {
                                int itemID = int.Parse(wzImage.Name.Replace(".img", ""));
                                WzImageProperty wzImageProperty = wzImage["info"];
                                if (wzImageProperty == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    WzImageProperty item = wzImageProperty["icon"];
                                    if (item != null)
                                    {
                                        try
                                        {
                                            if (item["source"] != null)
                                            {
                                                try
                                                {
                                                    WzImageProperty itemSource = item["source"];
                                                    string[] strs = null;
                                                    if (itemSource.WzValue.ToString() != null)
                                                    {
                                                        strs = itemSource.WzValue.ToString().Split('/');
                                                    }
                                                    itemSource.Dispose();
                                                    foreach (string str in strs)
                                                    {
                                                        string realSTR = str.Replace(".img", "");
                                                        int num;
                                                        //MessageBox.Show("0 - " + itemID.ToString() + " - " + str);
                                                        //MessageBox.Show("1 - " + itemID.ToString() + " - " + num.ToString());
                                                        bool res = int.TryParse(realSTR, out num);
                                                        if (res)
                                                        {
                                                            if (num > 1000000)
                                                            {
                                                                //c.updateLog(num.ToString() + " -> " + itemID.ToString());
                                                                unParseditems.Add(itemID, num);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    MessageBox.Show(itemID.ToString() + "\n" + e.ToString());
                                                }
                                                continue;
                                            }
                                            else
                                            {
                                                WzCanvasProperty wzCanvasProperty = (WzCanvasProperty)item;
                                                Bitmap pNG = wzCanvasProperty.PngProperty.GetPNG(true);
                                                pNG.Save(@"C:\images\" + itemID.ToString() + ".png", ImageFormat.Png);
                                                pNG.Dispose();
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show("Equip - " + itemID.ToString() + "\n" + e.ToString());
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(x.Name + "\n" + wzImage.Name + "\n" + e.ToString());
                                continue;
                            }
                            wzImage.Dispose();
                        }
                    }
                }
                wzFile.Dispose();
            }
        }

        public static void copySourceImages(Client c)
        {
            Thread.Sleep(5000);
            int total = unParseditems.Count;
            List<int> IDS = new List<int>();
            while (unParseditems.Count != IDS.Count)
            {
                try
                {
                    int remaining = unParseditems.Count - IDS.Count;
                    c.updateLog(remaining + " images remaining to copy");
                    foreach (KeyValuePair<int, int> x in unParseditems)
                    {
                        Thread.Sleep(1);
                        if (!IDS.Contains(x.Key))
                        {
                            if (copyImage(@"C:\images\", x.Value + ".png", @"C:\images\", x.Key + ".png"))
                            {
                                IDS.Add(x.Key);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
        }

        public static bool copyImage(string oldPath, string oldFileName, string newPath, string newFileName)
        {
            try
            {
                if (File.Exists(oldPath + oldFileName))
                {
                    File.Copy(oldPath + oldFileName, newPath + newFileName, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static MiniMapInfo loadMiniMap(int Id)
        {
            MiniMapInfo miniMapInfo;
            lock (Database.wzLocker)
            {
                WzFile wzFile = null;
                try
                {
                    if (!File.Exists(@"WZFiles/Map.wz"))
                    {
                        MessageBox.Show("WZFiles/Map.wz missing.");
                        Environment.Exit(0);
                    }
                    int num = 0;
                    while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Map.wz")))
                    {
                        Thread.Sleep(2000);
                        num++;
                        if (num <= 3)
                        {
                            continue;
                        }
                        MessageBox.Show("Map.Wz is in use by another program");
                    }
                    wzFile = new WzFile(@"WZFiles/Map.wz", Constants.WzType);
                    wzFile.ParseWzFile();
                    int id = Id / 100000000;
                    List<WzDirectory> wzDirectories = wzFile.WzDirectory.WzDirectories;
                    WzImage imageByName = wzFile.WzDirectory.GetDirectoryByName("Map").GetDirectoryByName(string.Concat("Map", id)).GetImageByName(string.Concat(Id, ".img"));
                    if (imageByName != null)
                    {
                        WzImageProperty wzImageProperty = imageByName["miniMap"].DeepClone();
                        if (wzImageProperty != null)
                        {
                            WzImageProperty item = wzImageProperty["canvas"];
                            if (item != null)
                            {
                                WzCanvasProperty wzCanvasProperty = (WzCanvasProperty)item;
                                Bitmap pNG = wzCanvasProperty.PngProperty.GetPNG(true);
                                WzImageProperty item1 = wzImageProperty["centerX"];
                                WzImageProperty wzImageProperty1 = wzImageProperty["centerY"];
                                WzImageProperty item2 = wzImageProperty["height"];
                                WzImageProperty wzImageProperty2 = wzImageProperty["width"];
                                if (item1 == null || wzImageProperty1 == null || item2 == null || wzImageProperty2 == null)
                                {
                                    miniMapInfo = null;
                                }
                                else
                                {
                                    MiniMapInfo miniMapInfo1 = new MiniMapInfo(Id, pNG, short.Parse(item1.WzValue.ToString()), short.Parse(wzImageProperty1.WzValue.ToString()), short.Parse(item2.WzValue.ToString()), short.Parse(wzImageProperty2.WzValue.ToString()));
                                    wzFile.Dispose();
                                    miniMapInfo = miniMapInfo1;
                                }
                            }
                            else
                            {
                                miniMapInfo = null;
                            }
                        }
                        else
                        {
                            miniMapInfo = null;
                        }
                    }
                    else
                    {
                        miniMapInfo = null;
                    }
                }
                catch 
                {
                    if (wzFile != null)
                    {
                        wzFile.Dispose();
                    }
                    miniMapInfo = null;
                }
            }
            return miniMapInfo;
        }

        public static void loadMPPots()
        {
            lock (Database.wzLocker)
            {
                if (!Database.mpPotsloaded)
                {
                    Database.mpPotsloaded = true;
                    if (!File.Exists(@"WZFiles/Item.wz"))
                    {
                        MessageBox.Show("Item.wz missing.");
                        Environment.Exit(0);
                    }
                    while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Item.wz")))
                    {
                        Thread.Sleep(2000);
                    }
                    WzFile wzFile = new WzFile(@"WZFiles/Item.wz", Constants.WzType);
                    wzFile.ParseWzFile();
                    WzImage imageByName = wzFile.WzDirectory.GetDirectoryByName("Consume").GetImageByName("0200.img");
                    foreach (WzImageProperty wzProperty in imageByName.WzProperties)
                    {
                        int num = int.Parse(wzProperty.Name);
                        WzImageProperty item = wzProperty["spec"]["mp"];
                        if (item == null)
                        {
                            item = wzProperty["spec"]["mpR"];
                        }
                        if (item == null)
                        {
                            continue;
                        }
                        Database.mpPots.Add(num);
                    }
                    wzFile.Dispose();
                }
            }
        }


        public static void loadPotentialLines()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/Item.wz"))
                {
                    MessageBox.Show("Item.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Item.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/Item.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["ItemOption.img"];
                foreach (WzSubProperty wzProperty in item.WzProperties)
                {
                    try
                    {
                        int num = int.Parse(wzProperty.Name);
                        PotentialLines potInfo = new PotentialLines();
                        potInfo.potInfo = wzProperty["info"]["string"].WzValue.ToString();
                        foreach (WzSubProperty wzProperty1 in wzProperty.WzProperties)
                        {
                            if (wzProperty1.Name.Equals("level"))
                            {
                                try
                                {
                                    string str = potInfo.potInfo;
                                    foreach (WzSubProperty wzProperty2 in wzProperty1.WzProperties)
                                    {
                                        str = potInfo.potInfo;
                                        int level = int.Parse(wzProperty2.Name);
                                        foreach (WzImageProperty wzProperty3 in wzProperty2.WzProperties)
                                        {
                                            if (str.Contains("#" + wzProperty3.Name))
                                                str = str.Replace("#" + wzProperty3.Name, wzProperty3.WzValue.ToString());
                                        }
                                        potInfo.potLevels.Add(level, str);
                                    }
                                }
                                catch { continue; }
                                Database.potentialLines.Add((short)num, potInfo);
                            }
                            else
                                continue;
                        }
                    }
                    catch {
                        MessageBox.Show(wzProperty.Name);
                    }
                }
                wzFile.Dispose();
            }
        }

        public static int getMpConsumePot(List<MapleItem> items)
        {
            int d;
            List<MapleItem>.Enumerator enumerator = items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    MapleItem current = enumerator.Current;
                    if (!Database.mpPots.Contains(current.ID))
                    {
                        continue;
                    }
                    d = current.ID;
                    return d;
                }
                return 0;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            return d;
        }


        public static List<int> getMpConsumePotList(List<MapleItem> items)
        {
            List<int> mpPots = new List<int>();
            List<MapleItem>.Enumerator enumerator = items.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    MapleItem current = enumerator.Current;
                    if (!Database.mpPots.Contains(current.ID))
                    {
                        continue;
                    }
                    mpPots.Add(current.ID);
                }
                return mpPots;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            return mpPots;
        }


        public static void loadMapStrings()
        {
            if (Database.maps.Count <= 0)
            {
                lock (Database.wzLocker)
                {
                    if (!File.Exists(@"WZFiles/String.wz"))
                    {
                        MessageBox.Show("String.wz missing.");
                        Environment.Exit(0);
                    }
                    while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                    {
                        Thread.Sleep(2000);
                    }
                    WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                    wzFile.ParseWzFile();
                    WzImage item = (WzImage)wzFile.WzDirectory["Map.img"];
                    foreach (WzSubProperty wzProperty in item.WzProperties)
                    {
                        foreach (WzSubProperty wzSubProperty in wzProperty.WzProperties)
                        {
                            try
                            {
                                int num = int.Parse(wzSubProperty.Name);
                                string str = wzSubProperty["streetName"].WzValue.ToString() + " : "+ wzSubProperty["mapName"].WzValue.ToString();
                                if (!Database.maps.ContainsKey(num))
                                {
                                    Database.maps.Add(num, str);
                                }
                            }
                            catch { }
                        }
                    }
                    wzFile.Dispose();
                }
            }
        }




        public static void loadEquipStrings()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["Eqp.img"];
                foreach (WzSubProperty wzProperty in item["Eqp"].WzProperties)
                {
                    foreach (WzSubProperty wzSubProperty in wzProperty.WzProperties)
                    {
                        int num = int.Parse(wzSubProperty.Name);
                        ItemStrings itemStrings = new ItemStrings();
                        itemStrings.Name = wzSubProperty["name"].WzValue.ToString();
                        if (!Database.items.ContainsKey(num))
                        {
                            Database.items.Add(num, itemStrings);
                        }
                    }
                }
                wzFile.Dispose();
            }
        }

        public static void loadEtcStrings()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["Etc.img"];
                foreach (WzSubProperty wzProperty in item["Etc"].WzProperties)
                {
                    int num = int.Parse(wzProperty.Name);
                    ItemStrings itemStrings = new ItemStrings();
                    itemStrings.Name = "No name associated.";
                    if (wzProperty["name"] != null)
                    {
                        itemStrings.Name = wzProperty["name"].WzValue.ToString();
                    }
                    if (wzProperty["desc"] != null)
                    {
                        itemStrings.Description = wzProperty["desc"].WzValue.ToString();
                        string[] stringArray = { Environment.NewLine, "\\r\\n", "\\r", "\\n", "#c", "#" };
                        foreach (string str1 in stringArray)
                        {
                            itemStrings.Description = itemStrings.Description.Replace(str1, " ");
                        }
                        Thread.Sleep(1);
                    }
                    if (!Database.items.ContainsKey(num))
                    {
                        Database.items.Add(num, itemStrings);
                    }
                }
                wzFile.Dispose();
            }
        }

        public static void loadConsumeStrings()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["Consume.img"];
                foreach (WzSubProperty wzProperty in item.WzProperties)
                {
                    int num = int.Parse(wzProperty.Name);
                    ItemStrings itemStrings = new ItemStrings();
                    itemStrings.Name = "No name associated";
                    if (wzProperty["name"] != null)
                    {
                        try
                        {
                            itemStrings.Name = wzProperty["name"].WzValue.ToString();
                            string[] stringArray = { Environment.NewLine, "\\r\\n", "\\r", "\\n", "#c", "#" };
                            foreach (string str1 in stringArray)
                            {
                                itemStrings.Name = itemStrings.Name.Replace(str1, " ");
                            }
                            Thread.Sleep(1);
                        }
                        catch
                        {
                            itemStrings.Name = "No name associated";
                        }
                    }
                    if (wzProperty["desc"] != null)
                    {
                        try
                        {
                            itemStrings.Description = wzProperty["desc"].WzValue.ToString();
                            string[] stringArray = { Environment.NewLine, "\\r\\n", "\\r", "\\n", "#c", "#", "\n" };
                            foreach (string str1 in stringArray)
                            {
                                itemStrings.Description = itemStrings.Description.Replace(str1, " ");
                            }
                            //mapleItem.itemDesc = wzProperty["desc"].WzValue.ToString();
                        }
                        catch
                        {
                            itemStrings.Description = "";
                        }
                    }
                    if (!Database.items.ContainsKey(num))
                    {
                        Database.items.Add(num, itemStrings);
                    }
                }
                wzFile.Dispose();
            }
        }

        public static void loadInsStrings()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["Ins.img"];
                foreach (WzSubProperty wzProperty in item.WzProperties)
                {
                    int num = int.Parse(wzProperty.Name);
                    ItemStrings itemStrings = new ItemStrings();
                    itemStrings.Name = "No name associated.";
                    if (wzProperty["name"] != null)
                    {
                        itemStrings.Name = wzProperty["name"].WzValue.ToString();
                    }
                    if (wzProperty["desc"] != null)
                    {
                        itemStrings.Description = wzProperty["desc"].WzValue.ToString();
                        string[] stringArray = { Environment.NewLine, "\\r\\n", "\\r", "\\n", "#c", "#" };
                        foreach (string str1 in stringArray)
                        {
                            itemStrings.Description = itemStrings.Description.Replace(str1, " ");
                        }
                        Thread.Sleep(1);
                    }
                    if (!Database.items.ContainsKey(num))
                    {
                        Database.items.Add(num, itemStrings);
                    }
                    if (num >= 3060000 & num <= 3069999)
                    {
                        string[] rank = itemStrings.Name.Split(' '); //rank[0] is [X]
                        string description = wzProperty["desc"].WzValue.ToString();
                        while (description.Contains("*"))
                            description = description.Substring(description.IndexOf('*') + 1);
                        description = rank[0] + description.Replace("#", "");
                        Database.nebuliteLines.Add((short)(num - 3060000), description);
                    }

                }
                wzFile.Dispose();
            }
        }

        public static void loadSkillStrings()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/String.wz"))
                {
                    MessageBox.Show("String.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/String.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/String.wz", Constants.WzType);
                wzFile.ParseWzFile();
                WzImage item = (WzImage)wzFile.WzDirectory["Skill.img"];
                foreach (WzSubProperty wzProperty in item.WzProperties)
                {
                    try
                    {
                        int num = int.Parse(wzProperty.Name);
                        string str = wzProperty["name"].WzValue.ToString();
                        if (!Database.skills.ContainsKey(num))
                        {
                            Database.skills.Add(num, str);
                        }
                    }
                    catch { }

                }
                wzFile.Dispose();
            }
        }

        public static void loadItemLevel()
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/Character.wz"))
                {
                    MessageBox.Show("Character.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Character.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/Character.wz", Constants.WzType);
                wzFile.ParseWzFile();
                List<Thread> threadz = new List<Thread>();
                foreach (WzDirectory x in wzFile.WzDirectory.subDirs)
                {
                    foreach (WzImage wzImage in x.images)
                    {
                        try
                        {
                            int itemID = int.Parse(wzImage.Name.Replace(".img", ""));
                            WzImageProperty wzImageProperty = wzImage["info"];
                            if (wzImageProperty == null)
                            {
                                continue;
                            }
                            else
                            {
                                foreach (WzImageProperty wzProperty in wzImageProperty.WzProperties)
                                {
                                    if (wzProperty.Name.ToLower() == "reqlevel")
                                    {
                                        if (Database.items.ContainsKey(itemID))
                                        {
                                            int itemLevel = int.Parse(wzProperty.WzValue.ToString());
                                            if (itemLevel < 10)
                                            {
                                                itemLevel = 10;
                                            }
                                            Database.items[itemID].itemLevel = itemLevel;
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        wzImage.Dispose();
                    }
                }
                wzFile.Dispose();
            }
        }



        public static int getItemLevel(int itemID)
        {
            lock (Database.wzLocker)
            {
                if (!File.Exists(@"WZFiles/Character.wz"))
                {
                    MessageBox.Show("Character.wz missing.");
                    Environment.Exit(0);
                }
                while (Utilities.IsFileLocked(new FileInfo(@"WZFiles/Character.wz")))
                {
                    Thread.Sleep(2000);
                }
                WzFile wzFile = new WzFile(@"WZFiles/Character.wz", Constants.WzType);
                wzFile.ParseWzFile();
                foreach (WzDirectory x in wzFile.WzDirectory.subDirs)
                {
                    string str = string.Concat("0", itemID.ToString(), ".img");
                    WzImage wzImage = x.GetImageByName(str);
                    if (wzImage == null)
                    {
                        continue;
                    }
                    else
                    {
                        WzImageProperty wzImageProperty = wzImage["info"];
                        if (wzImageProperty == null)
                        {
                            wzFile.Dispose();
                            return 10;
                        }
                        else
                        {
                            foreach (WzImageProperty wzProperty in wzImageProperty.WzProperties)
                            {
                                if (wzProperty.Name.ToLower() == "reqlevel")
                                {

                                    int itemLevel = int.Parse(wzProperty.WzValue.ToString());
                                    if (itemLevel < 10)
                                    {
                                        itemLevel = 10;
                                    }
                                    wzFile.Dispose();
                                    return itemLevel;
                                }
                            }
                        }
                    }
                }
                wzFile.Dispose();
                return 10;
            }
        }



        public static string getItemName(int id)
        {
            if (!Database.items.ContainsKey(id))
            {
                return string.Concat("Unknown Item: ", id);
            }
            else
            {
                return Database.items[id].Name;
            }
        }

        public static string getItemDescription(int id)
        {
            if (!Database.items.ContainsKey(id))
            {
                return string.Concat("Unknown Item: ", id);
            }
            else
            {
                return Database.items[id].Description;
            }
        }

        public static int getItemID(string name)
        {
            foreach (KeyValuePair<int, ItemStrings> item in items)
            {
                if (item.Value.Name.Equals(name))
                    return item.Key;
            }
            return 0;
        }


        public static string getMapName(int id)
        {
            if (!Database.maps.ContainsKey(id))
            {
                return string.Concat("Unknown Item: ", id);
            }
            else
            {
                return Database.maps[id];
            }
        }



        public static int getMAPCRC(int map)
        {
            if (!Database.MapCRC.ContainsKey(map))
            {
                return -1;
            }
            else
            {
                return BitConverter.ToInt32(HexEncoding.GetBytes(Database.MapCRC[map]), 0);
            }
        }

        public static int getItemCRC(int itemID)
        {
            if (!Database.ItemCRC.ContainsKey(itemID))
            {
                return 0;
            }
            else
            {
                return BitConverter.ToInt32(HexEncoding.GetBytes(Database.ItemCRC[itemID]), 0);
            }
        }


        public static string getStoreType(int shopID)
        {
            if (!Database.storeTypes.ContainsKey(shopID))
            {
                return "Unknown Store";
            }
            else
            {
                return Database.storeTypes[shopID];
            }
        }

        public static void loadStoreTypes()
        {
            lock (Database.CSlock)
            {
                if (Database.storeTypes.Count <= 0)
                {
                    Database.storeTypes.Add(5140000, "Store Permit");
                    Database.storeTypes.Add(5030000, "Mushroom Elf House");
                    Database.storeTypes.Add(5030001, "Mushroom Elf House");
                    Database.storeTypes.Add(5030006, "Mushroom Elf House");
                    Database.storeTypes.Add(5030010, "Granny's Food Stand");
                    Database.storeTypes.Add(5030011, "Granny's Food Stand");
                    Database.storeTypes.Add(5030008, "Homely Coffeehouse");
                    Database.storeTypes.Add(5030009, "Homely Coffeehouse");
                    Database.storeTypes.Add(5030003, "Cashier: Teddy Bear Clerk");
                    Database.storeTypes.Add(5030002, "Cashier: Teddy Bear Clerk");
                    Database.storeTypes.Add(5030012, "Tiki Torch Store");
                    Database.storeTypes.Add(5030004, "The Robot Stand");
                    Database.storeTypes.Add(5030005, "The Robot Stand");
                    Database.storeTypes.Add(5030029, "Black Friday Maid");
                }
            }
        }


        public static void loadItemCRC()
        {
            lock (Database.CSlock)
            {
                if (Database.ItemCRC.Count <= 0)
                {
                    //Database.ItemCRC.Add(2431551, "A1 78 6E 5C"); //Candy??
                    /*
                    Database.ItemCRC.Add(4000654, "37 CA 56 5D"); //Night Pendant
                    Database.ItemCRC.Add(4000655, "53 E0 CB C4"); //Blaze Pendant
                    Database.ItemCRC.Add(4000656, "8A C8 D1 78"); //Thunder Pendant
                    Database.ItemCRC.Add(4000657, "E1 36 53 64"); //Wind Pendant
                    Database.ItemCRC.Add(4000658, "5C 34 D4 41"); //Dawn Pendant
                    //Pick up packets ( Last 4 Bytes )
                    //Ores
                    Database.ItemCRC.Add(4010002, "87 D1 92 AD"); //Mithril Ore
                    Database.ItemCRC.Add(4010006, "28 13 44 67"); //Gold Ore
                    Database.ItemCRC.Add(4010007, "03 D2 60 2A"); //Lidium Ore
                    Database.ItemCRC.Add(4020000, "00 16 1F 24"); //Garnet Ore
                    Database.ItemCRC.Add(4020002, "D1 04 BA 29"); //Aquamarine Ore
                    Database.ItemCRC.Add(4020003, "D6 A9 AC A8"); //Emerald Ore
                    Database.ItemCRC.Add(4020006, "6A 77 63 B7"); //Topaz Ore
                    Database.ItemCRC.Add(4020007, "00 52 CD 1C"); //Diamond Ore
                    Database.ItemCRC.Add(4020008, "BE 2F AB FC"); //Black Crystal Ore
                    Database.ItemCRC.Add(4004000, "12 F0 38 90"); //Power Crystal Ore
                    Database.ItemCRC.Add(4004001, "BE 5C D3 31"); //Wisdom Crystal Ore
                    Database.ItemCRC.Add(4004002, "CB EA 67 BB"); //Dex Crystal Ore
                    Database.ItemCRC.Add(4004003, "22 09 AF 64"); //Luk Crystal Ore
                    Database.ItemCRC.Add(4004004, "91 B3 5E 83"); //Dark Crystal Ore

                    //Herbs 
                    //Flowers
                    Database.ItemCRC.Add(4022009, "37 DC E5 04"); //Lemon Balm Flower
                    Database.ItemCRC.Add(4022010, "09 1F 50 89"); //Peppermint Flower
                    Database.ItemCRC.Add(4022012, "85 0A 7D A4"); //Jasmine Flower
                    Database.ItemCRC.Add(4022014, "6B EB 6A E8"); //Tea Tree Flower
                    Database.ItemCRC.Add(4022016, "20 7D BF BA"); //Chamomile Flower
                    Database.ItemCRC.Add(4022018, "F9 55 A5 06"); //Patchouli Flower
                    Database.ItemCRC.Add(4022020, "16 40 D0 97"); //Juniper Berry Flower
                    Database.ItemCRC.Add(4022021, "E9 A0 7A 19"); //Hyssop Flower
                    //Seeds
                    Database.ItemCRC.Add(4022008, "E8 74 5B 36"); //Lemon Balm Seed
                    Database.ItemCRC.Add(4022011, "43 23 BB 88"); //Jasmine Seed
                    Database.ItemCRC.Add(4022013, "45 A4 86 A2"); //Tea Tree Seed
                    Database.ItemCRC.Add(4022015, "24 1D 23 87"); //Chamomile Seed
                    Database.ItemCRC.Add(4022017, "C0 35 C7 70"); //Patchouli Seed
                    Database.ItemCRC.Add(4022019, "CB 96 0C E9"); //Juniper Berry Seed


                    //8th Anniversary Event
                    Database.ItemCRC.Add(2430737, "50 E6 DE 47"); //8th Anniversary Box
                    Database.ItemCRC.Add(4001695, "6F 9E 32 78"); //8th Anniversary Leaf
                    */
                }
            }
        }

        public static void loadMapCRC()
        {
            lock (Database.CSlock)
            {
                if (Database.MapCRC.Count <= 0)
                {
                    /*
                    Database.MapCRC.Add(180000003, "18 B5 D7 7A"); //GM Mining Map
                    Database.MapCRC.Add(100000000, "E3 47 1E 5E"); //Henesys
                    Database.MapCRC.Add(680100000, "63 32 A8 29"); //Maple 7th Day Market (FM 0)
                    Database.MapCRC.Add(104000003, "07 AC 26 63"); //Lith Harbor Wep Store
                    */
                    Database.MapCRC.Add(910000000, "16 44 54 E0"); //FM Entrance
                    Database.MapCRC.Add(910000001, "7B F4 70 3A"); //FM 1
                    Database.MapCRC.Add(910000002, "B2 8D D5 2C"); //FM 2
                    Database.MapCRC.Add(910000003, "A0 5C C8 A5"); //FM 3
                    Database.MapCRC.Add(910000004, "0A 0B 3B 8D"); //FM 4
                    Database.MapCRC.Add(910000005, "C2 41 39 B2"); //FM 5
                    Database.MapCRC.Add(910000006, "D7 DC 81 D9"); //FM 6
                    Database.MapCRC.Add(910000007, "61 CB 94 EB"); //FM 7
                    Database.MapCRC.Add(910000008, "08 22 A3 2D"); //FM 8
                    Database.MapCRC.Add(910000009, "D8 AF 66 6C"); //FM 9
                    Database.MapCRC.Add(910000010, "65 F0 A6 41"); //FM 10
                    Database.MapCRC.Add(910000011, "88 7A 9D 6A"); //FM 11
                    Database.MapCRC.Add(910000012, "6B F0 4F 90"); //FM 12
                    Database.MapCRC.Add(910000013, "27 9B DD 79"); //FM 13
                    Database.MapCRC.Add(910000014, "33 3B D8 80"); //FM 14
                    Database.MapCRC.Add(910000015, "E3 B6 1D C1"); //FM 15
                    Database.MapCRC.Add(910000016, "6A 77 87 A8"); //FM 16
                    Database.MapCRC.Add(910000017, "BA FA 42 E9"); //FM 17
                    Database.MapCRC.Add(910000018, "7A E6 E1 82"); //FM 18
                    Database.MapCRC.Add(910000019, "A4 1D 62 09"); //FM 19
                    Database.MapCRC.Add(910000020, "50 4F D6 D0"); //FM 20
                    Database.MapCRC.Add(910000021, "8E B4 55 5B"); //FM 21
                    Database.MapCRC.Add(910000022, "5B A5 10 C3"); //FM 22

                    //Database.MapCRC.Add(271030600, "7D 96 A0 54");//HOH
                    //Database.MapCRC.Add(240000000, "CD AC 22 B8"); //Leafre
                    //Database.MapCRC.Add(200000000, "99 AA EE DE"); //Orbis
                    //Database.MapCRC.Add(105000000, "EA C9 03 0A"); //Sleepywood
                    //Database.MapCRC.Add(910001000, "B6 36 FE 7F"); //ArdentMill
                }
            }
        }

        public static World getWorld(string world)
        {
            World worldType = World.Scania;
            switch (world)
            {
                case "Scania":
                    {
                        worldType = World.Scania;
                        break;
                    }
                case "Bera":
                    {
                        worldType = World.Bera;
                        break;
                    }
                case "Broa":
                    {
                        worldType = World.Broa;
                        break;
                    }
                case "Windia":
                    {
                        worldType = World.Windia;
                        break;
                    }
                case "Khaini":
                    {
                        worldType = World.Khaini;
                        break;
                    }
                case "Bellocan":
                    {
                        worldType = World.Bellocan;
                        break;
                    }
                case "Mardia":
                    {
                        worldType = World.Mardia;
                        break;
                    }
                case "Kradia":
                    {
                        worldType = World.Kradia;
                        break;
                    }
                case "Yellonde":
                    {
                        worldType = World.Yellonde;
                        break;
                    }
                case "Demethos":
                    {
                        worldType = World.Demethos;
                        break;
                    }
                case "Galicia":
                    {
                        worldType = World.Galicia;
                        break;
                    }
                case "El Nido":
                    {
                        worldType = World.ElNido;
                        break;
                    }
                case "Zenith":
                    {
                        worldType = World.Zenith;
                        break;
                    }
                case "Arcania":
                    {
                        worldType = World.Arcania;
                        break;
                    }
                case "Chaos":
                    {
                        worldType = World.Chaos;
                        break;
                    }
                case "Nova":
                    {
                        worldType = World.Nova;
                        break;
                    }
                case "Renegades":
                    {
                        worldType = World.Renegades;
                        break;
                    }
            }
            return worldType;
        }

        public static string getServerIP(World world)
        {
            switch (world)
            {
                case World.Scania:
                    {
                        return "8.31.99.145";
                    }
                case World.Bera:
                    {
                        return "8.31.99.150";
                    }
                case World.Broa:
                    {
                        return "8.31.99.155";
                    }
                case World.Windia:
                    {
                        return "8.31.99.160";
                    }
                case World.Khaini:
                    {
                        return "8.31.99.165";
                    }
                case World.Bellocan:
                case World.Nova:
                    {
                        return "8.31.99.170";
                    }
                case World.Yellonde:
                case World.Mardia:
                case World.Chaos:
                case World.Kradia:
                    {
                        return "8.31.99.175";
                    }
                case World.Galicia:
                case World.Arcania:
                case World.Zenith:
                case World.ElNido:
                case World.Demethos:
                    {
                        return "8.31.99.180";
                    }
                case World.Renegades:
                    {
                        return "8.31.99.185";
                    }
            }
            return Program.serverip;
        }



        public static string getWorldOwlString(World world)
        {
            switch (world)
            {
                case World.Scania:
                    {
                        return "Scania";
                    }
                case World.Bera:
                    {
                        return "Bera";
                    }
                case World.Broa:
                    {
                        return "Broa";
                    }
                case World.Windia:
                    {
                        return "Windia";
                    }
                case World.Khaini:
                    {
                        return "Khaini";
                    }
                case World.Bellocan:
                    {
                        return "BelloNova";
                    }
                case World.Yellonde:
                case World.Mardia:
                case World.Chaos:
                case World.Kradia:
                    {
                        return "YMCK";
                    }
                case World.Galicia:
                case World.Arcania:
                case World.Zenith:
                case World.ElNido:
                case World.Demethos:
                    {
                        return "GAZED";
                    }
                case World.Renegades:
                    {
                        return "Renegades";
                    }
            }
            return "Scania";
        }

        public static int getWorldID(World world)
        {
            int ID = 0;
            switch (world)
            {
                case World.Scania:
                    {
                        ID = 0;
                        break;
                    }
                case World.Bera:
                    {
                        ID = 1;
                        break;
                    }
                case World.Broa:
                    {
                        ID = 2;
                        break;
                    }
                case World.Windia:
                    {
                        ID = 3;
                        break;
                    }
                case World.Khaini:
                    {
                        ID = 4;
                        break;
                    }
                case World.Bellocan:
                case World.Nova:
                    {
                        ID = 5;
                        break;
                    }
                case World.Yellonde:
                case World.Mardia:
                case World.Chaos:
                case World.Kradia:
                    {
                        ID = 6;
                        break;
                    }
                case World.Galicia:
                case World.Arcania:
                case World.Zenith:
                case World.ElNido:
                case World.Demethos:
                    {
                        ID = 9;
                        break;
                    }
                case World.Renegades:
                    {
                        ID = 16;
                        break;
                    }
            }
            return ID;
        }


        public static int getMaxChannels(World world)
        {
            switch (world)
            {
                case World.Scania:
                    {
                        return 20;
                    }
                case World.Bera:
                    {
                        return 20;
                    }
                case World.Broa:
                    {
                        return 20;
                    }
                case World.Windia:
                    {
                        return 20;
                    }
                case World.Khaini:
                    {
                        return 20;
                    }
                case World.Bellocan:
                case World.Nova:
                    {
                        return 20;
                    }
                case World.Yellonde:
                case World.Mardia:
                case World.Chaos:
                case World.Kradia:
                    {
                        return 20;
                    }
                case World.Galicia:
                case World.Arcania:
                case World.Zenith:
                case World.ElNido:
                case World.Demethos:
                    {
                        return 20;
                    }
                case World.Renegades:
                    {
                        return 20;
                    }
            }
            return 20;
        }
    }
}
