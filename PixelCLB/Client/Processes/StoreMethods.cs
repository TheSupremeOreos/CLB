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


        public void addShop(MapleFMShop shop, bool Permit)
        {
            mapleFMShopCollection.addShop(shop, this, Permit);
            if (!shopsToRead.Contains(shop.shopID) && shop.playerUID != myCharacter.uid && clientMode == ClientMode.FMOWL)
            {
                shopsToRead.Add(shop.shopID);
            }
        }
        /// <summary>
        /// OPEN STORE SETTINGS
        /// </summary>
        public void storeWaitingDoneNowOpen()
        {
            if (mushyType == Program.permitID)
                ses.SendPacket(PacketHandler.Open_Permit(storename, mushyType, slotNum).ToArray());
            else
                ses.SendPacket(PacketHandler.Open_Store(storename, mushyType, slotNum).ToArray());
            tries++;
            storeTargetCoords = null;
            storeTargetUID = 0;
        }

        private void permitAC()
        {
            while (slotNum == 0)
                Thread.Sleep(1);
            if (tries >= storeTries)
            {
                cashShopManagement(false, true, 46, 50);
                Thread.Sleep(new Random().Next(300, 400));
                characterMove(coords);
                tries = 0;
                updateAccountStatus("[Permit AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
            }
            if (timeOutCheck)
                sent = false;
            if (!sent)
            {
                tries++;
                ses.SendPacket(PacketHandler.Open_Permit(storename, mushyType, slotNum).ToArray());
                sent = true;
                timeOut(1, 2);
            }
        }

        private void permitOpenStage2()
        {
            ses.SendPacket(PacketHandler.Permit_AddItem(Program.price).ToArray());
        }

        private void storeAC()
        {
            while (slotNum == 0)
                Thread.Sleep(1);
            if (tries >= storeTries)
            {
                cashShopManagement(false, true, 45, 50);
                characterMove(coords);
                tries = 0;
                updateAccountStatus("[Store AC] AC @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
            }
            if (tries == 0 || tries == 1 || tries == 15 || tries == 30 || tries == 45)
                updateAccountStatus("[Store AC] ACing @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
            if (clientMode != ClientMode.DISCONNECTED)
            {
                ses.SendPacket(PacketHandler.ClickInven(slotNum).ToArray());
                Thread.Sleep(150);
            }
            Thread.Sleep(1);
        }

        /*
        if (!sent)
        {
            if (tries >= storeTries)
            {
                cashShopManagement(false, true, 45, 50);
                characterMove(coords);
                Thread.Sleep(new Random().Next(200, 300));
                tries = 0;
            }
            updateAccountStatus("Store AC @ " + channel.ToString() + "/" + RoomNum + " @ " + coords);
            sent = true;
            storeClicked = false;
            timeOutCheck = true;
        }
        else
        {
            if (!storeClicked)
            {
                if (timeOutCheck)
                {
                    timeOut(1, 2);
                    timeOutCheck = false;
                    ses.SendPacket(PacketHandler.ClickInven(slotNum).ToArray());
                }
            }
            else
            {
                if (timeOutCheck)
                {
                    tries++;
                    timeOut(2, 3);
                    timeOutCheck = false;
                    storeClicked = false;
                    ses.SendPacket(PacketHandler.Open_Store(storename, mushyType, slotNum).ToArray());
                }
            }
        }
         * 
         * */

        private void storeOpenStage2()
        {
            ses.SendPacket(PacketHandler.AddItem(Program.price).ToArray());
            /* Moved to handler
            Thread.Sleep(200);
            ses.SendPacket(PacketHandler.Open_stage1().ToArray());
            Thread.Sleep(200);
            ses.SendPacket(PacketHandler.Open_stage2().ToArray());
            Thread.Sleep(200);
            ses.SendPacket(PacketHandler.Open_stage3().ToArray());
            if (clientMode == ClientMode.SHOPRESET)
                updateLog("Reset successful!");
             * 
             * */
        }

        private void storeClose()
        {
            ses.SendPacket(PacketHandler.editStore(pic, getStoreUID()).ToArray());
            Thread.Sleep(50);
            ses.SendPacket(PacketHandler.editStore2(getStoreUID()).ToArray());
            Thread.Sleep(50);
        }

        private void storeAFK(bool twoStores, bool afkPermit)
        {
            if (!twoStores)
            {
                if (!sent)
                {
                    MapleFMShop shopByOwner = mapleFMShopCollection.getShopOwner(storeAFKIGN);
                    if (shopByOwner == null)
                    {
                        updateLog("[Store AFKer] Error finding " + storeAFKIGN + "'s store");
                        forceDisconnect(false, 0, false, "Error finding " + storeAFKIGN + "'s store"); ;
                        return;
                    }
                    ses.SendPacket(PacketHandler.Enter_Store(shopByOwner.shopID).ToArray());
                }
            }
            else
            {
                if (!sent)
                {
                    MapleFMShop shopByOwner = mapleFMShopCollection.getShopOwnerIdentifier(storeAFKIGN, afkPermit);
                    if (shopByOwner == null)
                    {
                        updateLog("[Store AFKer] Error finding " + storeAFKIGN + "'s store");
                        forceDisconnect(false, 0, false, "Error finding " + storeAFKIGN + "'s store");
                        return;
                    }
                    ses.SendPacket(PacketHandler.Enter_Store(shopByOwner.shopID).ToArray());
                }

            }
        }

        private void overrideRoomCheck()
        {
            if (myCharacter.mapID >= 910000000 & myCharacter.mapID < 910000023)
            {
                if (myCharacter.mapID == 910000000)
                    moveFMRoomsOwlMethod(channel, 910000001);
            }
            else
            {
                updateLog("[Map Check] Character not in FM!");
                updateLog("[Map Check] Disconnecting bot.");
                forceDisconnect(false, 0, false, "Map Check: Character not in FM");
                return;
            }
            if (fmRoomOverride)
            {
                if (myCharacter.mapID > 910000000 & myCharacter.mapID < 910000023)
                {
                    int room = 910000000 + int.Parse(overrideRoomNum);
                    if (myCharacter.mapID != room)
                    {
                        updateLog("[FM Room Override] Incorrect FM Room!");
                        updateLog("[FM Room Override] Moving to FM " + overrideRoomNum);
                        changeFreeMarketRoom(overrideRoomNum, (byte)channel);
                        return;
                    }
                }
            }
        }


        public string getStoreXY(int UID)
        {
            MapleFMShop shop = mapleFMShopCollection.getPlayerShop(UID, false);
            if (shop != null)
            {
                if (clientMode != ClientMode.SHOPRESET)
                {
                    if (isCoordFiltered(shop.x, shop.y, xLow, xHigh, yLow, yHigh))
                    {
                        updateLog("[Filter] Spot filtered at " + shop.x + "," + shop.y);
                        return null;
                    }
                }
                Foothold foothold = myCharacter.Map.footholds.findBelow(new Point(shop.x, shop.y));
                if (clientMode == ClientMode.SHOPRESET)
                    return string.Concat(shop.x, ",", foothold.getY1(), ",", foothold.getId());
                if (Program.whiteList)
                {
                    string contents = File.ReadAllText(Program.FMWhiteList);
                    if (clientMode != ClientMode.SHOPRESET & clientMode != ClientMode.SHOPCLOSE & contents.ToLower().Contains(shop.owner.ToLower()))
                    {
                        return null;
                    }
                }
                else
                {
                    string contents = File.ReadAllText(Program.FMBlackList);
                    if (clientMode != ClientMode.SHOPRESET & clientMode != ClientMode.SHOPCLOSE & contents.ToLower().Contains(shop.owner.ToLower()))
                    {
                        return string.Concat(shop.x, ",", foothold.getY1(), ",", foothold.getId());
                    }
                    return null;
                }
                return string.Concat(shop.x, ",", foothold.getY1(), ",", foothold.getId());
            }
            else
            {
                updateLog("[Error] Could not find your store!");
                updateLog("[Error] Is your char in the right FM room?");
                forceDisconnect(false, 0, false, "Character not in right FM room?");
                return null;
            }

            /*
            List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (current.Value.playerUID == UID)
                    {
                        Foothold foothold = myCharacter.Map.footholds.findBelow(new Point(current.Value.x, current.Value.y));
                        if (clientMode == ClientMode.SHOPRESET)
                            return string.Concat(current.Value.x, ",", foothold.getY1(), ",", foothold.getId());
                        if (Program.whiteList)
                        {
                            string contents = File.ReadAllText(Program.FMWhiteList);
                            if (clientMode != ClientMode.SHOPRESET & clientMode != ClientMode.SHOPCLOSE & contents.ToLower().Contains(current.Value.owner.ToLower()))
                            {
                                return null;
                            }
                        }
                        else
                        {
                            string contents = File.ReadAllText(Program.FMBlackList);
                            if (clientMode != ClientMode.SHOPRESET & clientMode != ClientMode.SHOPCLOSE & contents.ToLower().Contains(current.Value.owner.ToLower()))
                            {
                                return string.Concat(current.Value.x, ",", foothold.getY1(), ",", foothold.getId());
                            }
                            return null;
                        }
                        return string.Concat(current.Value.x, ",", foothold.getY1(), ",", foothold.getId());
                    }
                }
                updateLog("[Error] Could not find your store!");
                updateLog("[Error] Is your char in the right FM room?");
                forceDisconnect(false, 0, false);
                return null;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            return null;
            */
        }

        public string getPermitXY(int UID)
        {
            Player user = getPlayer(UID);
            if (user != null)
            {
                if (isCoordFiltered(user.x, user.y, xLow, xHigh, yLow, yHigh))
                {
                    updateLog("[Filter] Spot filtered at " + user.x + "," + user.y);
                    return null;
                }
                Foothold foothold = myCharacter.Map.footholds.findBelow(new Point(user.x, user.y));
                if (Program.whiteList)
                {
                    string contents = File.ReadAllText(Program.FMWhiteList);
                    if (contents.ToLower().Contains(user.ign.ToLower()))
                    {
                        return null;
                    }
                }
                else
                {
                    string contents = File.ReadAllText(Program.FMBlackList);
                    if (contents.ToLower().Contains(user.ign.ToLower()))
                    {
                        return string.Concat(user.x, ",", foothold.getY1(), ",", foothold.getId());
                    }
                    return null;
                }
                return string.Concat(user.x, ",", foothold.getY1(), ",", foothold.getId());
            }
            return null;
        }

        public MapleFMShop getShopViaIGN(string IGN)
        {
            bool selectedItem;
            List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
            try
            {
                while (true & clientMode != ClientMode.DISCONNECTED)
                {
                    selectedItem = enumerator.MoveNext();
                    if (!selectedItem)
                    {
                        return null;
                    }
                    KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (IGN.ToLower().Equals(current.Value.owner.ToLower()))
                    {
                        return current.Value;
                    }
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            return null;
        }


        /// <summary>
        /// Retrieving store INFOS
        /// </summary>

        public int findBlackListIGNs(string targetIGN)
        {
            int validStores = 0;
            bool selectedItem;
            List<string> blackListContents = new List<string>();
            if (targetIGN.Equals(""))
            {
                using (StreamReader streamReader = new StreamReader(Program.FMBlackList))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        line = line.Replace(Environment.NewLine, "");
                        line = line.Replace("\r", "");
                        line = line.Replace("\n", "");
                        blackListContents.Add(line);
                    }
                }
            }
            else
            {
                blackListContents.Add(targetIGN);
            }
            while (clientMode != ClientMode.DISCONNECTED)
            {
                List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                try
                {
                    while (true & clientMode != ClientMode.DISCONNECTED)
                    {
                        selectedItem = enumerator.MoveNext();
                        if (!selectedItem)
                        {
                            break;
                        }
                        KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                        foreach (string str in blackListContents)
                        {
                            if (str.ToLower().Equals(current.Value.owner.ToLower()))
                            {
                                validStores++;
                            }
                        }
                    }
                    break;
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
            return validStores;
        }

        private bool isCoordFiltered(short x_coord, short y_coord, int x_Low, int x_High, int y_Low, int y_High)
        {
            return x_Low <= x_coord && x_coord <= x_High && y_Low <= y_coord && y_coord <= y_High;
        }


        private int getStoreID()
        {
            int num = 0;
            while (num < 16)
            {
                MapleItem store = myCharacter.Inventorys[InventoryType.CASH].getItemById(5030000 + num);
                if (store == null)
                {
                    num++;
                }
                else
                {
                    return store.ID;
                }
            }
            return 0;
        }

        public void getStoreandSlot()
        {
            int storeID = 0;
            if (mode == 5 || mode == 6 || mode == 17 || mode == 19)
                storeID = 5140000;
            else
                storeID = getStoreID();
            if (storeID == 0)
            {
                updateLog("There was no store found in your inventory");
                forceDisconnect(false, 0, false, "No store in inventory");
                return;
            }
            byte slot = myCharacter.Inventorys[InventoryType.CASH].getSlotById(storeID);
            if (slot == 0)
            {
                updateLog("Error detecting slot number");
                forceDisconnect(false, 0, false, "Error detecting slot #");
                return;
            }
            mushyType = storeID;
            slotNum = slot;
            if (Program.userDebugMode || Program.debugMode)
            {
                updateLog("[Debug] Store Type: " + mushyType + " / Slot : " + slotNum);
            }
        }

        public int getStoreUID()
        {
            MapleFMShop myShop = mapleFMShopCollection.getPlayerShop(myCharacter.uid, false);
            if (myShop != null)
            {
                return myShop.shopID;
            }
            return 0;
            /*
            bool selectedItem = false;
            if (!selectedItem)
            {
                List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                try
                {
                    while (true & clientMode != ClientMode.DISCONNECTED)
                    {
                        selectedItem = enumerator.MoveNext();
                        if (!selectedItem)
                        {
                            updateLog("[Error] Could not find the store!");
                            updateLog("[Error] Is your char in the right FM room?");
                            forceDisconnect(false, 0, false);
                            return 0;
                        }
                        KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                        selectedItem = !(current.Value.playerUID == myCharacter.uid);
                        if (!selectedItem)
                        {
                            return current.Value.shopID;
                        }
                    }
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
            return 0;
            */
        }

        void FTPLogin(string server, string username, string password, string uploadDir, bool upperChannel, bool first = true)
        {
            try
            {
                using (FtpLib.FtpConnection ftp = new FtpLib.FtpConnection(server, username, password))
                {

                    ftp.Open(); /* Open the FTP connection */
                    ftp.Login(); /* Login using previously provided credentials */
                    DirSearch(uploadDir, ftp, "/", upperChannel, first);
                    ftp.Close();
                }
            }
            catch {  }
        }

        void DirSearch(string sDir, FtpLib.FtpConnection ftp, string currentDirectory, bool createWorldFolder = false, bool first = false)
        {
            try
            {
                ftp.SetCurrentDirectory(currentDirectory);
                if (createWorldFolder)
                {
                    if (!ftp.DirectoryExists(owlWorldName))
                    {
                        ftp.CreateDirectory(owlWorldName);
                    }
                    ftp.SetCurrentDirectory(owlWorldName);
                    currentDirectory = owlWorldName;
                }
                foreach (string f in Directory.GetFiles(sDir))
                {
                    Uri uri = new Uri(f);
                    string str = f.ToLower();
                    if (first)
                    {
                        if (str.Contains("channel1.txt") || str.Contains("channel2.txt") || str.Contains("channel3.txt") || str.Contains("channel4.txt") || str.Contains("channel5.txt"))
                            ftp.PutFile(f, System.IO.Path.GetFileName(uri.LocalPath));
                        else
                            continue;
                    }
                    else
                    {
                        if (!str.Contains("channel1.txt") && !str.Contains("channel2.txt") && !str.Contains("channel3.txt") && !str.Contains("channel4.txt") && !str.Contains("channel5.txt"))
                            ftp.PutFile(f, System.IO.Path.GetFileName(uri.LocalPath));
                    }
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    ftp.SetCurrentDirectory(currentDirectory);
                    string dirname = new DirectoryInfo(d).Name;
                    if (!ftp.DirectoryExists(dirname))
                    {
                        ftp.CreateDirectory(dirname);
                    }
                    string newCurrentDir = currentDirectory + "/" + dirname;
                    DirSearch(d, ftp, newCurrentDir);
                }
            }
            catch (System.Exception e)
            {
            }
        }

        private bool checkFMFileTime()
        {
            try
            {
                string path = Path.Combine(Program.FMExport, "FMExport", owlWorldName, "Channel" + channel + ".txt");
                if (File.Exists(path))
                {
                    string line;
                    string[] firstLine = { "", "" };
                    using (StreamReader streamReader = new StreamReader(File.OpenRead(path)))
                    {
                        line = streamReader.ReadLine();
                        streamReader.Close();
                        if (!String.IsNullOrWhiteSpace(line))
                        {
                            if (line.Contains("%&"))
                            {
                                firstLine = Regex.Split(line, "%&");
                                DateTime dateTime;
                                if (DateTime.TryParseExact(firstLine[0], "MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                                {
                                    DateTime date1 = DateTime.ParseExact(firstLine[0], "MMMM dd, yyyy h:mm:ss tt", null);
                                    DateTime date2 = DateTime.Now;
                                    System.TimeSpan difference = date2.Subtract(date1);
                                    if (difference.TotalMinutes > 2)
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public string getPotentialLines(short type, int level)
        {
            string potLine = "";
            if (Database.potentialLines.ContainsKey(type))
            {
                return Database.potentialLines[type].potLevels[level];
            }
            return potLine;
        }

        public string getNebuliteLine(short type)
        {
            string nebLine = "";
            if (Database.nebuliteLines.ContainsKey(type))
            {
                return Database.nebuliteLines[type];
            }
            return nebLine;
        }

        private void exportFM(bool deleteFile, bool launchWindow, int num, bool doubleCheck = false, bool moveFiles = false)
        {
            try
            {
                if (clientMode == ClientMode.FMOWL)
                    updateLog("[FM Owl] Exporting stores to file...");
                string directory = Path.Combine(Program.FMExport, "FMExport", owlWorldName);
                string tempDirectory = Path.Combine(Program.FMExport, "FMExport", owlWorldName, "temp");
                string tempPath = Path.Combine(directory, "temp", "Channel" + num + ".txt");
                string path = Path.Combine(directory, "Channel" + num + ".txt");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                if (!Directory.Exists(tempDirectory))
                    Directory.CreateDirectory(tempDirectory);
                if (File.Exists(tempPath) & deleteFile)
                {
                    File.Delete(tempPath);
                }
                Thread thread = new Thread(delegate()
                {
                    int tries = 0;
                    try
                    {
                        StreamWriter streamWriter = File.AppendText(tempPath);
                        if (deleteFile)
                            streamWriter.WriteLine(string.Concat(DateTime.Now.ToString("MMMM dd, yyyy h:mm:ss tt"), "%&"));
                        foreach (MapleFMShop shopList in mapleFMShopCollection.getShopListOwl())
                        {
                            if (shopList.owled)
                            {
                                string[] stringArray = { Environment.NewLine, "\r\n", "\r", "\n" };
                                while (stringArray.All(s => shopList.description.Contains(s)))
                                {
                                    foreach (string str in stringArray)
                                    {
                                        shopList.description = shopList.description.Replace(str, "");
                                    }
                                    Thread.Sleep(1);
                                }
                                object[] objArray = new object[] { shopList.owner, "%&", shopList.playerGuild, "%&", shopList.description, "%&", Database.getStoreType(shopList.storeID), "%&", shopList.channel, "%&", shopList.fmRoom, "%&" };
                                foreach (MapleItem itemList in shopList.getItemList())
                                {
                                    if (itemList.bundle > 0 & itemList.ID > 0)
                                    {
                                        string str = string.Concat(objArray);
                                        str = string.Concat(str, itemList.ID, "%&");
                                        str = string.Concat(str, Database.getItemName(itemList.ID), "%&");
                                        str = string.Concat(str, Database.getItemDescription(itemList.ID), "%&");
                                        if (itemList.quantity > 0)
                                            str = string.Concat(str, itemList.quantity, "%&");
                                        else
                                            str = string.Concat(str, "SOLD", "%&");
                                        str = string.Concat(str, itemList.bundle, "%&");
                                        str = string.Concat(str, itemList.price);
                                        if (itemList.ID < 2000000)
                                        {
                                            str = string.Concat(str, "%&");
                                            int enhancements = itemList.enhancements;
                                            int enhanceCounter = 0;
                                            while (enhancements != 0)
                                            {
                                                str = string.Concat(str, "*");
                                                enhancements = enhancements - 1;
                                                enhanceCounter++;
                                                if (enhanceCounter == 5)
                                                {
                                                    str = string.Concat(str, " ");
                                                    enhanceCounter = 0;
                                                }
                                            }
                                            str = string.Concat(str, "%&<br>");
                                            if (itemList.potLevel >= 1)
                                            {
                                                if (itemList.potline1 == "" & itemList.potline2 == "" & itemList.potline3 == "")
                                                    str = string.Concat(str, "-----Hidden Potential----<br>");

                                                if (itemList.potline1 != "")
                                                    str = string.Concat(str, "(" + itemList.potline0 + " Item)<br>", itemList.potline1);
                                                if (itemList.potline2 != "")
                                                    str = string.Concat(str, "<br>", itemList.potline2);
                                                if (itemList.potline3 != "")
                                                    str = string.Concat(str, "<br>", itemList.potline3, "<br>");
                                            }
                                            if (itemList.bonuspotLevel >= 1)
                                            {
                                                if (itemList.potline1 != "")
                                                    str = string.Concat(str, "<br>");

                                                if (itemList.bonuspotline1 == "" & itemList.bonuspotline2 == "" & itemList.bonuspotline3 == "")
                                                    str = string.Concat(str, "<br>----Hidden Bonus Potential----");

                                                if (itemList.potline1 != "")
                                                    if (itemList.bonuspotline1 != "")
                                                    {
                                                        str = string.Concat(str, "<br>(" + itemList.bonuspotline0 + "Bonus Potential)<br>", itemList.bonuspotline1);
                                                        if (itemList.bonuspotline2 != "")
                                                            str = string.Concat(str, ", ", itemList.bonuspotline2);
                                                        if (itemList.bonuspotline3 != "")
                                                            str = string.Concat(str, ", ", itemList.bonuspotline3);
                                                    }
                                            }

                                            if (itemList.nebulite != "")
                                            {
                                                if (itemList.potline1 != "" || itemList.bonuspotline1 != "")
                                                    str = string.Concat(str, "<br>");
                                                str = string.Concat(str, "<br><br> - ", itemList.nebulite);
                                            }

                                            if (itemList.craftedBy != "")
                                            {
                                                if (itemList.potline1 != "" || itemList.bonuspotline1 != "" || itemList.nebulite != "")
                                                    str = string.Concat(str, "<br>");
                                                str = string.Concat(str, "Tagged By: ", itemList.craftedBy);
                                            }
                                        }
                                        streamWriter.WriteLine(str);
                                    }
                                    Thread.Sleep(1);
                                }
                                mapleFMShopCollection.owledShops.Remove(mapleFMShopCollection.getSortingID(shopList.permit, shopList.playerUID));
                                totalStoresRead++;
                            }
                            else
                            {
                                if (shopList.tries >= 2)
                                    mapleFMShopCollection.owledShops.Remove(mapleFMShopCollection.getSortingID(shopList.permit, shopList.playerUID));
                                else
                                    shopList.tries++;
                            }
                        }
                        updateLog("[FM Owl] Exported: Total(" + totalStoresRead.ToString() + ") / Missed(" + storesMissed.ToString() + ")");
                        streamWriter.Close();
                        if (launchWindow)
                        {
                            if (mapleFMShopCollection.owlListChannels(num) > 0 & doubleCheck)
                            {
                                updateLog("[FM Owl] " + mapleFMShopCollection.owlListChannels(num) + " stores still not exported!");
                                updateLog("[FM Owl] Waiting for stores to complete processing");
                                if (mapleFMShopCollection.owlListChannels(num) > 0)
                                {
                                    Thread.Sleep(2000);
                                    exportFM(deleteFile, launchWindow, num, doubleCheck, moveFiles);
                                }
                                return;
                            }
                            if (moveFiles)
                            {
                                for (int x = 0; x <= maxChannels; x++)
                                {
                                    if (mapleFMShopCollection.owlListChannels(x) == 0)
                                    {
                                        string tempPath2 = Path.Combine(directory, "temp", "Channel" + x + ".txt");
                                        string path2 = Path.Combine(directory, "Channel" + x + ".txt");
                                        if (File.Exists(tempPath2))
                                        {
                                            try
                                            {
                                                int attempts = 0;
                                                while (!fileDeleteAndMove(path2, tempPath2))
                                                {
                                                    attempts++;
                                                    Thread.Sleep(300);
                                                    if (attempts > 10)
                                                        break;
                                                }
                                                updateLog("[FM Owl] Channel " + x.ToString() + "'s export complete");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                            if (Program.openFMOwlWindow)
                            {
                                Program.gui.c2 = Program.gui.c;
                                Program.gui.c = this;
                                string text = Program.gui.commandTextBox.Text;
                                Program.gui.GUIInvokeMethod(() => Program.gui.commandTextBox.Text = "fmlist");
                                //Program.gui.GUIInvokeMethod(() => Program.gui.button5_Click_1(null, null));
                                Program.gui.GUIInvokeMethod(() => Program.gui.commandTextBox.Text = text);
                                Program.gui.c = Program.gui.c2;
                            }
                            return;
                        }
                        else
                        {
                            if (moveFiles)
                            {
                                for (int x = 0; x <= maxChannels; x++)
                                {
                                    if (mapleFMShopCollection.owlListChannels(x) == 0)
                                    {
                                        string tempPath2 = Path.Combine(directory, "temp", "Channel" + x + ".txt");
                                        string path2 = Path.Combine(directory, "Channel" + x + ".txt");
                                        if (File.Exists(tempPath2))
                                        {
                                            try
                                            {
                                                int attempts = 0;
                                                while (!fileDeleteAndMove(path2, tempPath2))
                                                {
                                                    attempts++;
                                                    Thread.Sleep(300);
                                                    if (attempts == 10)
                                                        break;
                                                }
                                                updateLog("[FM Owl] Channel " + x.ToString() + "'s export complete");
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (tries >= 3)
                        {
                            exportFM(deleteFile, launchWindow, num, doubleCheck, moveFiles);
                            tries++;
                        }
                        else
                            updateLog("Error writing FM file!");
                    }
                });
                thread.Start();
            }
            catch { }
        }

        public void exportFMTime(string ign, MapleFMShop shop)
        {
            try
            {
                string fileDirectory = Path.Combine(Program.FMTimesFileDirectory, "FMShopTimes.txt");
                if (File.Exists(fileDirectory))
                {
                    updateLog("[Shop Closed] " + ign + " :" + DateTime.Now.ToString("MMMM dd, yyyy h:mm:ss tt"));
                    StreamWriter streamWriter = File.AppendText(fileDirectory);
                    streamWriter.WriteLine("IGN: " + shop.owner + " Coords: " + shop.x + "," + shop.y + " Closed: " + DateTime.Now.ToString("MMMM dd, yyyy h:mm:ss tt") + "Map ID: " + myCharacter.mapID.ToString() + "Channel: " + channel.ToString());
                    streamWriter.Close();
                }
                else
                {
                    updateLog("File not found! FMShopTimes.txt file created");
                    File.Create(fileDirectory);
                    Thread.Sleep(100);
                    exportFMTime(ign, shop);
                }
            }
            catch { }
        }


        private void moveToFM1()
        {
            Thread.Sleep(2000);
            while (myCharacter.mapID != 910000001 & clientMode == ClientMode.FMOWL)
            {
                updateLog("Chracter not in FM1, now moving to FM1");
                shopsToRead.Clear();
                mapleFMShopCollection.shops.Clear();
                mapleFMShopCollection.owledShops.Clear();
                changeFreeMarketRoom("1", (byte)channel);
            }
        }
        private void blackListCheck()
        {
            try
            {
                if (!Program.whiteList)
                {
                    Thread.Sleep(1000);
                    updateLog("FullMapping using BLACKLIST");
                    int ignCount = findBlackListIGNs("");
                    if (ignCount >= 1)
                    {
                        updateLog("Possible target count: " + ignCount.ToString());
                    }
                    else
                    {
                        updateLog("There are no possible targets in this map listed in your blacklist. ");
                        updateLog("Please switch to whitelist or be sure to add proper igns to the blacklist.");
                        updateLog("Now Exiting");
                        MessageBox.Show("There are no possible targets in this map listed in your blacklist. \nPlease switch to whitelist or be sure to add proper igns to the blacklist.\nCurrent Action: Now Exiting");
                        forceDisconnect(false, 0, false, "No possible blacklist targets");
                    }
                }
                else
                    return;
            }
            catch { }
        }

        public void banAndCopyMerchant(string targetIGN, string redirectIGN)
        {
            npcWindows = 0;
            timeOut(2,3);
            ses.SendPacket(PacketHandler.banUserFromPermit(0, "a", true).ToArray());
            MapleFMShop shopTarget = getShopViaIGN(targetIGN);
            MapleFMShop shopCopy = getShopViaIGN(redirectIGN);
            while (npcWindows == 1 && !timeOutCheck)
                Thread.Sleep(200);
            characterMove(shopTarget.x.ToString() + "," + shopTarget.y.ToString());
            Thread.Sleep(300);
            ses.SendPacket(PacketHandler.Enter_Store(shopCopy.shopID).ToArray());
        }

        private bool checkUserStoreSpawned(int UID)
        {
            MapleFMShop shop = mapleFMShopCollection.getPlayerShop(UID, false);
            if (shop != null)
            {
                return true;
            }
            return false;
            /*
            List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
            try
            {
                while (clientMode != ClientMode.DISCONNECTED)
                {
                    if (!enumerator.MoveNext())
                    {
                        return false;
                    }
                    KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (current.Value.playerUID == UID)
                    {
                        return true;
                    }
                }
                return false;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            return false;
            */
        }

    }
}