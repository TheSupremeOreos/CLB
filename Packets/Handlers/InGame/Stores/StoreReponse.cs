using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace PixelCLB.Packets.Handlers
{
    class StoreReponse : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (c.clientMode == ClientMode.AUTOCHAT || c.clientMode == ClientMode.IDLE || c.clientMode ==ClientMode.SHOPAFKER)
                    return;
                Thread newThread = new Thread(delegate()
                {
                    short packetLength = packet.Length;
                    if (packetLength < 6)
                    {
                        if (packetLength == 4)
                        {
                            byte identifier = packet.Read();
                            if (identifier == 0x12)
                            {
                                byte identiFier = packet.Read(); //Slot in which someone leaves
                                return;
                            }
                            if (identifier == 0x13) //C2 02 13 02
                            {
                                return;
                            }
                        }
                        else
                        {
                            byte identifier = packet.Read();
                            if (identifier == 0x14)
                            {
                                packet.Read();
                                byte identiFier = packet.Read();
                                if (identiFier == 0x02) // B1 02 0A 00 02 //Full store
                                {
                                    c.updateLog("You can't enter the room due to full capacity");
                                    c.sent = false;
                                }
                                else if (identiFier == 0x0D) // C2 02 0B 00 0D //Cannot open here
                                {
                                    c.sent = false;
                                    //c.updateLog("You cannot establish a miniroom here");
                                    //Disconnect in store AFK mode
                                }
                                else if (identiFier == 0x11) // B1 02 0A 00 11 //Banned
                                {
                                    c.updateLog("You have been banned from this store");
                                    //Disconnect in store AFK mode
                                    if (c.clientMode == ClientMode.SHOPAFKER)
                                    {
                                        c.forceDisconnect(false, 0, false, "Banned from the store");
                                        return;
                                    }
                                    c.bannedstores++;
                                    if (c.bannedstores == 11)
                                    {
                                        if (Program.switchAccountsOwl)
                                        {
                                            c.bannedstores = 0;
                                            Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                                            Program.iniFile.WriteValue(c.accountProfile, "Channel", "1");
                                            int num;
                                            if (int.TryParse(sectionValues["CharNum"], out num))
                                            {
                                                int charNum = int.Parse(sectionValues["CharNum"]);
                                                int maxNum = c.maxcharnumber;
                                                if (charNum == maxNum)
                                                {
                                                    charNum = 1;
                                                }
                                                else
                                                {
                                                    charNum++;
                                                }
                                                Program.iniFile.WriteValue(c.accountProfile, "CharNum", charNum);
                                                c.updateLog("[Acc Switch] Char num has been switched to " + charNum.ToString());
                                                c.forceDisconnect(true, 1, false, "Char Num switched");
                                                return;
                                            }
                                            else
                                            {
                                                c.updateLog("[Acc Switch] User not using a valid char num");
                                                c.forceDisconnect(true, 1, false, "Not using a valid char num");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            c.bannedstores = 0;
                                        }
                                    }
                                }
                                else if (identiFier == 0x12) // B1 02 0A 00 12 //Reg Maint
                                {
                                    c.updateLog("Store is undergoing maintenance");
                                    c.sent = false;
                                }

                            }
                            else if (identifier == 0x1C)
                            {
                                packet.Read();
                                byte identiFier = packet.Read();
                                if (identiFier == 0x11) // Kicked for maint B1 02 12 01 11
                                {
                                    c.updateLog("Store is now undergoing maintenance");
                                    c.sent = false;
                                }
                                else if (identifier == 0x05)
                                {
                                    c.updateLog("You have been kicked from the store.");
                                    c.npcWindows++;
                                }
                            }
                        }
                    }
                    else
                    {
                        byte num1 = packet.Read();
                        if (num1 == 0x4D) //Add item
                        {
                            if (c.mushyType != Program.permitID)
                            {
                                if (c.storeTargetCoords == null)
                                {
                                    c.ses.SendPacket(PacketHandler.Open_stage1().ToArray());
                                    Thread.Sleep(30);
                                    c.ses.SendPacket(PacketHandler.Open_stage2().ToArray());
                                    Thread.Sleep(30);
                                    c.ses.SendPacket(PacketHandler.Open_stage3().ToArray());
                                    return;
                                }
                            }
                            else
                            {
                                if (c.storeTargetCoords == null)
                                {
                                    c.ses.SendPacket(PacketHandler.On_Permit_Final().ToArray());
                                    Thread.Sleep(10);
                                    c.ses.SendPacket(PacketHandler.On_Permit_Final2().ToArray());
                                    return;
                                }
                            }
                        }
                        else if (num1 == 0x15)
                        {
                            byte num2 = packet.Read();
                            string ign = packet.ReadMapleString();
                            int tradeID = packet.ReadInt();
                            if (num2 == 0x04)
                                c.updateLog("[Trade] Request from " + ign + " | Trade ID: " + tradeID.ToString());
                            else if (num2 == 0x03)
                                c.updateLog("[RPS] Request from " + ign + " | Trade ID: " + tradeID.ToString());
                            //c.ses.SendPacket(PacketHandler.accept_Trade_Request(tradeID).ToArray());
                            //c.ses.SendPacket(PacketHandler.decline_Trade_Request(tradeID).ToArray());
                            return;
                        }
                        else if (c.clientMode == ClientMode.SHOPAFKER)
                        {
                            c.sent = true;
                            return;
                        }
                        else if (c.clientMode != ClientMode.FMOWL)
                        {
                            if (c.clientMode == ClientMode.PERMITUP && c.mode == 19)
                            {
                                c.updateLog("[Merchant Copy] Successfully copied merchant");
                                return;
                            }
                            if (c.clientMode != ClientMode.SHOPRESET & c.clientMode != ClientMode.SHOPCLOSE & c.clientMode != ClientMode.SPOTFREE)
                            {
                                c.clientMode = ClientMode.SPOTFREE;
                                return;
                            }
                            if (c.canReset & c.clientMode != ClientMode.SPOTFREE)
                            {
                                c.clientMode = ClientMode.SPOTFREE;
                                return;
                            }
                            if (c.clientMode == ClientMode.SHOPCLOSE || c.clientMode == ClientMode.SHOPRESET)
                            {
                                c.storeLoaded = true;
                                List<string> igns = new List<string>();
                                string[] str = Program.resetFilterIGNs.Split(',');
                                foreach (var x in str)
                                {
                                    igns.Add(x);
                                }
                                while (true)
                                {
                                    if (!c.getPlayer(igns))
                                    {
                                        break;
                                    }
                                    Thread.Sleep(100);
                                }
                                c.ses.SendPacket(PacketHandler.closeStore(true).ToArray());
                                Thread.Sleep(300);
                                c.ses.SendPacket(PacketHandler.closeStore(false).ToArray());
                                c.freeSpot = true;
                            }
                            return;
                        }
                        else if (num1 == 0x14)
                        {
                            try
                            {
                                byte num2 = packet.Read();
                                packet.ReadShort();
                                if (num2 == 0x05)
                                {
                                    c.storeLoaded = true;
                                    /*
                                    int x = 0;
                                    int times = 0;
                                    if (num3 == 1)
                                        times = 7;
                                    if (num3 >= 2)
                                        times = 10;
                                    while (x < times)
                                    {
                                        while (packet.Read() != 255)
                                        {
                                        }
                                        x++;
                                    }
                                    */
                                    parseUsers(c, packet, true);
                                    int storeID = packet.ReadInt();
                                    string title;
                                    try
                                    {
                                        title = packet.ReadMapleString();
                                    }
                                    catch
                                    {
                                        c.updateLog("[FM Owl] Substituted store name due to invalid characters");
                                        title = "---Unknown Store Name---";
                                    }
                                    packet.Read();
                                    MapleFMShop shopByOwner = c.mapleFMShopCollection.getShopOwner2(title);
                                    if (shopByOwner == null)
                                    {
                                        Thread.Sleep(300);
                                        shopByOwner = c.mapleFMShopCollection.getShopOwner2(title);
                                    }
                                    if (shopByOwner != null)
                                    {
                                        byte num4 = packet.Read();
                                        //c.updateLog("Reading " + ign + "'s Store." + num4.ToString() + " items total.");
                                        for (int i = 0; i < num4; i = i + 1)
                                        {
                                            parseItems(c, packet, shopByOwner, i, shopByOwner.owner);
                                        }
                                        shopByOwner.owled = true;
                                    }
                                    else
                                    {
                                        c.storesMissed++;
                                        c.updateLog("[Permit] " + title + " not found");
                                    }
                                }
                                else if (num2 == 0x06) //mushy
                                {
                                    c.storeLoaded = true;
                                    parseUsers(c, packet);
                                    if (packet.Read() > 0)
                                    {
                                        int x = 0;
                                        while (x < 3)
                                        {
                                            while (packet.Read() != 255)
                                            {
                                                Thread.Sleep(1);
                                            }
                                            x++;
                                            Thread.Sleep(1);
                                        }
                                    }
                                    string ign = packet.ReadMapleString();
                                    int storeID = packet.ReadInt();
                                    string storeTitle;
                                    try
                                    {
                                        storeTitle = packet.ReadMapleString();
                                    }
                                    catch
                                    {
                                        c.updateLog("[FM Owl] Substituted store name due to invalid characters");
                                        storeTitle = "---Unknown Store Name---";
                                    }
                                    packet.Skip(9);
                                    MapleFMShop shopByOwner = c.mapleFMShopCollection.getShopOwner2(ign);
                                    if (shopByOwner == null)
                                    {
                                        Thread.Sleep(300);
                                        shopByOwner = c.mapleFMShopCollection.getShopOwner2(ign);
                                    }
                                    if (shopByOwner != null)
                                    {
                                        byte num4 = packet.Read();
                                        //c.updateLog("Reading " + ign + "'s Store." + num4.ToString() + " items total.");
                                        for (int i = 0; i < num4; i = i + 1)
                                        {
                                            parseItems(c, packet, shopByOwner, i, ign);
                                        }
                                        shopByOwner.owled = true;
                                    }
                                    else
                                    {
                                        c.storesMissed++;
                                        c.updateLog("[Store] " + ign + "'s store not found");
                                    }
                                }
                            }
                            catch (Exception es)
                            {
                                c.updateLog("[FM Owl] Processing error");
                                c.storesMissed++;
                            }
                        }
                    }
                });
                c.workerThreads.Add(newThread);
                newThread.Start();
            }
            catch (Exception e)
            {
                c.storesMissed++;
                //MessageBox.Show(packet.ToString() + "\n\n\n\n" + e.ToString());
                //c.updateLog("Store missed! Total Missed: " + c.storesMissed.ToString()); //Code 106
            }
        }


        private void parseUsers(Client c, PacketReader packet, bool permit = false)
        {
            try
            {
                short anum = 0;
                while (true)
                {
                    while (true)
                    {
                        if (packet.Read() == 0) //Runs twice?
                        {
                            if (packet.Read() == 0) // Breaks once?!
                            {
                                if (packet.Read() == 0)
                                    if (packet.Read() == 0)
                                        if (packet.Read() == 0)
                                            break;
                            }
                        }
                    }
                    packet.Skip(4);
                    anum = packet.ReadShort();
                    if (anum == 0)
                    {
                        packet.Read();
                        packet.ReadShort();
                        anum = packet.ReadShort();
                        if (anum < 3 || anum > 12)
                        {
                            anum = packet.ReadShort();
                            if (anum < 3 || anum > 12)
                            {
                                anum = packet.ReadShort();
                                if (anum == 0)
                                {
                                    if (packet.ReadInt() == 0)
                                    {
                                        packet.Read();
                                        anum = packet.ReadShort();
                                        if (anum == 0)
                                        {
                                            anum = packet.ReadShort();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (anum >= 3 & anum <= 12)
                    {
                        string str2 = packet.ReadString(anum);
                        packet.ReadShort();
                        if (permit)
                        {
                            if (packet.Read() == 0xFF)
                                break;
                        }
                        else
                        {
                            if (packet.ReadShort() == 0xFF)
                                break;
                        }
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 107");
            }
        }


        private void parseItems(Client c, PacketReader packet, MapleFMShop mapleShop, int i, string ign)
        {
            try
            {
                MapleItem mapleItem = new MapleItem();
                mapleItem.owner = ign;
                mapleItem.quantity = packet.ReadShort();
                mapleItem.bundle = packet.ReadShort();
                if (mapleItem.bundle == 0)
                {
                    packet.ReadInt();
                    mapleItem.quantity = packet.ReadShort();
                    mapleItem.bundle = packet.ReadShort();
                }
                if (mapleItem.quantity < -1 || mapleItem.quantity > 20000)
                {
                    packet.Skip(4);
                    mapleItem.quantity = packet.ReadShort();
                    mapleItem.bundle = packet.ReadShort();
                }
                mapleItem.price = packet.ReadLong();
                mapleItem.position = (byte)(i + 1);
                packet.Skip(1);
                mapleItem.ID = packet.ReadInt();
                mapleShop.addItem(mapleItem);
                if (mapleItem.ID < 2000000)
                {
                    bool flag = packet.Read() > 0;
                    if (flag)
                    {
                        mapleItem.cashOID = packet.ReadLong();
                    }
                    packet.Skip(8);
                    packet.Skip(4);
                    packet.ReadInt();


                    int Level = 1;
                    if (Database.items.ContainsKey(mapleItem.ID))
                    {
                        if (Database.items[mapleItem.ID].itemLevel == 0)
                        {
                            //Database.items[mapleItem.ID].itemLevel = Database.getItemLevel(mapleItem.ID);
                            Database.items[mapleItem.ID].itemLevel = Database.getItemLevel(mapleItem.ID);
                            if (Database.items[mapleItem.ID].itemLevel == 0)
                                Database.items[mapleItem.ID].itemLevel = 10;
                        }
                        Level = Database.items[mapleItem.ID].itemLevel / 10;
                    }

                    while (true)
                    {
                        if (packet.Read() == 0x4)
                        {
                            int x = 0;
                            byte bytes = 0;
                            bool complete = false;
                            while (true)
                            {
                                bytes = packet.Read();
                                if (bytes == 0)
                                {
                                    x++;
                                }
                                else if (bytes == 4)
                                {
                                    x = 0;
                                }
                                else
                                    break;
                                if (x >= 3)
                                {
                                    complete = true;
                                    break;
                                }
                            }
                            if (complete)
                            {
                                packet.Read();
                                break;
                            }
                        }
                        Thread.Sleep(1);
                    }
                    mapleItem.craftedBy = packet.ReadMapleString();
                    mapleItem.potLevel = packet.Read();
                    mapleItem.enhancements = packet.Read();
                    short potLine1 = packet.ReadShort();
                    short potLine2 = packet.ReadShort();
                    short potLine3 = packet.ReadShort();
                    int potline1 = potLine1;
                    if (potline1 < 0)
                        potline1 = 32767 - potline1;
                    int potline2 = potLine2;
                    if (potline2 < 0)
                        potline2 = 32767 - potline2;
                    int potline3 = potLine3;
                    if (potline3 < 0)
                        potline3 = 32767 - potline3;
                    if (mapleItem.potLevel > 0 & mapleItem.potLevel <= 20 && potLine1 > 0)
                    {
                        if (potLine1 < 20000 && potLine2 < 10000 && potLine3 < 10000)
                            mapleItem.potline0 = "Rare";
                        else if (potLine1 < 30000 && potLine2 < 20000 && potLine3 < 20000)
                            mapleItem.potline0 = "Epic";
                        else if (potline1 < 40000 && potLine2 < 30000 && potLine3 < 30000)
                            mapleItem.potline0 = "Unique";
                        else if (potline1 < 50000 && potline2 < 40000 && potline3 < 40000)
                            mapleItem.potline0 = "Legendary";
                        mapleItem.potline1 = c.getPotentialLines(potLine1, Level);
                        mapleItem.potline1 = mapleItem.potline1.Replace(".", "");
                        mapleItem.potline2 = c.getPotentialLines(potLine2, Level);
                        mapleItem.potline2 = mapleItem.potline2.Replace(".", "");
                        mapleItem.potline3 = c.getPotentialLines(potLine3, Level);
                        mapleItem.potline3 = mapleItem.potline3.Replace(".", "");
                    }
                    short bonuspotline1 = packet.ReadShort();
                    short bonuspotline2 = packet.ReadShort();
                    short bonuspotline3 = packet.ReadShort();
                    packet.ReadShort();
                    mapleItem.bonuspotLevel = packet.Read();
                    packet.Read();
                    int BPL1 = bonuspotline1;
                    if (BPL1 < 0)
                        BPL1 = 32767 - BPL1;
                    int BPL2 = bonuspotline2;
                    if (BPL2 < 0)
                        BPL2 = 32767 - BPL2;
                    int BPL3 = bonuspotline3;
                    if (BPL3 < 0)
                        BPL3 = 32767 - BPL3;
                    if (mapleItem.bonuspotLevel > 0 & mapleItem.bonuspotLevel <= 20)
                    {
                        if (BPL1 < 20000 && BPL2 < 10000 && BPL3 < 10000)
                            mapleItem.bonuspotline0 = "Rare";
                        else if (BPL1 < 30000 && BPL2 < 20000 && BPL3 < 20000)
                            mapleItem.bonuspotline0 = "Epic";
                        else if (BPL1 < 40000 && BPL2 < 30000 && BPL3 < 30000)
                            mapleItem.bonuspotline0 = "Unique";
                        else if (BPL1 < 50000 && BPL2 < 40000 && BPL3 < 40000)
                            mapleItem.bonuspotline0 = "Legendary";
                        mapleItem.bonuspotline1 = c.getPotentialLines(bonuspotline1, Level);
                        mapleItem.bonuspotline1 = mapleItem.bonuspotline1.Replace(".", "");
                        mapleItem.bonuspotline2 = c.getPotentialLines(bonuspotline2, Level);
                        mapleItem.bonuspotline2 = mapleItem.bonuspotline2.Replace(".", "");
                        mapleItem.bonuspotline3 = c.getPotentialLines(bonuspotline3, Level);
                        mapleItem.bonuspotline3 = mapleItem.bonuspotline3.Replace(".", "");
                    }
                    short nebulite = packet.ReadShort();
                    if (nebulite >= 0 & nebulite <= 9999)
                    {
                        if (nebulite == 0)
                        {
                            if (packet.Read() == 0xFF)
                                mapleItem.nebulite = c.getNebuliteLine(nebulite);
                        }
                        else
                        {
                            mapleItem.nebulite = c.getNebuliteLine(nebulite);
                        }
                    }

                    /*if ((mapleItem.potLevel > 0 & mapleItem.potLevel <= 20) || (mapleItem.bonuspotLevel > 0 & mapleItem.bonuspotLevel <= 20))
                    {
                        MessageBox.Show(
                            storeTitle + "\n" +
                            mapleItem.potLevel.ToString() + "\n" +
                            mapleItem.potline1 + "\n" +
                            mapleItem.potline2 + "\n" +
                            mapleItem.potline3 + "\n" +
                            mapleItem.bonuspotLevel.ToString() + "\n" +
                            mapleItem.bonuspotline1 + "\n" +
                            mapleItem.bonuspotline2 + "\n" +
                            mapleItem.bonuspotline3 + "\n" +
                            mapleItem.nebulite + "\n");
                    }
                    int y = 0;
                    //c.updateLog(mapleItem.ID.ToString() + ", " + mapleItem.price.ToString() + ", " + mapleItem.quantity.ToString() + ", " + "Equip");
                                            
                    while (true)
                    {
                        while (packet.Read() != 255)
                        {
                        }
                        y++;
                        if (y == 1)
                        {
                            packet.Skip(33);
                        }
                        else if (y != 1 & y < 3)
                            packet.Skip(6);
                        else
                        {
                            packet.Skip(3);
                            break;
                        }
                    }
                     * */

                    while (true)
                    {
                        while (packet.Read() != 0x40)
                        {
                            Thread.Sleep(1);
                        }
                        if (packet.Read() == 0xE0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }
                    while (packet.Read() != 0xFF)
                    {
                        Thread.Sleep(1);
                    }
                    packet.Skip(3);
                    while (true)
                    {
                        while (packet.Read() != 0x40)
                        {
                            Thread.Sleep(1);
                        }
                        if (packet.Read() == 0xE0)
                        {
                            break;
                        }
                        Thread.Sleep(1);
                    }
                    while (packet.Read() != 0x01)
                    {
                        Thread.Sleep(1);
                    }
                    packet.Skip(16);


                }
                else
                {
                    //c.updateLog(mapleItem.ID.ToString() + ", " + mapleItem.price.ToString() + ", " + mapleItem.quantity.ToString() + ", " + "nEquip");
                    packet.Skip(8);
                    while (true)
                    {
                        if (packet.Read() == 255)
                        {
                            packet.Skip(9);
                            if (isRechargableFamiliar(mapleItem.ID))
                                packet.Skip(8);
                            break;
                        }
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString() + "\n\n\n\n" + packet.ToString());
                c.updateLog("Error Code: 108");

            }
        }
        public static bool isRechargableFamiliar(int itemId)
        {
            if (itemId / 10000 == 233)
            {
                return true;
            }
            else if (itemId / 10000 == 207)
            {
                return true;
            }
            else
            {
                return itemId / 10000 == 287;
            }
        }
    }
}
