using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.CLBTools;
using System.Threading;

namespace PixelCLB.Packets.Handlers
{
    class ServerReponseLogin : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                c.csCheck = false;
                if (packet.Length > 100)
                {
                    c.myCharacter.FInventorys.Clear();
                    if (c.hackShield != null)
                        c.hackShield.resetHackShieldTimer(c.hackShieldMin, c.hackShieldMax);
                    packet.Skip(27);
                    packet.Read();
                    packet.ReadInt();
                    if (packet.Read() != 0)
                    {
                        c.myCharacter.UsedPortals = 1;
                        packet.Skip(42);
                        c.myCharacter.uid = packet.ReadInt();
                        c.myCharacter.ign = packet.ReadString(13).Replace("\0", "");
                        c.myCharacter.Gender = packet.Read();
                        c.myCharacter.SkinColor = packet.Read();
                        c.myCharacter.Face = packet.ReadInt();
                        c.myCharacter.Hair = packet.ReadInt();
                        packet.Skip(24);
                        c.myCharacter.Level = packet.Read();
                        c.myCharacter.Job = packet.ReadShort();
                        c.myCharacter.Str = packet.ReadShort();
                        c.myCharacter.Dex = packet.ReadShort();
                        c.myCharacter.Int = packet.ReadShort();
                        c.myCharacter.Luk = packet.ReadShort();
                        c.myCharacter.HP = packet.ReadInt();
                        c.myCharacter.MaxHP = packet.ReadInt();
                        c.myCharacter.MP = packet.ReadInt();
                        c.myCharacter.MaxMP = packet.ReadInt();
                        c.myCharacter.AP = packet.ReadShort();
                        /*
                        c.myCharacter.SP = 0;
                        int num = packet.Read();
                        int count = 0;
                        while (count != num)
                        {
                            count = packet.Read();
                            int sp = packet.ReadInt();
                            c.myCharacter.SP = (short)(c.myCharacter.SP + sp);
                        }
                        if (IsExtendedSPJob(job))
                        {
                            c.updateLog("[Debug] Extended SP Job");
                        }
                        /*
                        //packet.ReadShort(); //v143
                        if (!IsExtendedSPJob(job))
                        {
                            c.myCharacter.SP = packet.ReadShort();
                        }
                        else
                        {
                            if (Program.debugMode || Program.userDebugMode)
                            {
                                c.updateLog("[Debug] Extended SP Job");
                            }
                            c.myCharacter.ExtendedSP = readExtendedSP(packet);
                        }
                         */
                        if (IsExtendedSPJob(c.myCharacter.Job))
                            c.myCharacter.ExtendedSP = readExtendedSP(packet);
                        else
                            c.myCharacter.SP = packet.ReadShort();
                        c.myCharacter.Exp = packet.ReadLong();
                        c.myCharacter.Fame = packet.ReadInt();
                        packet.Skip(4);
                        packet.ReadInt();
                        int MapIDRead = packet.ReadInt();
                        byte spawnPointRead = packet.Read();
                        if (MapIDRead > 0)
                        {
                            c.myCharacter.mapID = MapIDRead;
                            c.myCharacter.Spawnpoint = spawnPointRead;
                        }
                        getMap(c);
                        c.myCharacter.loadMap(c);
                        c.serverCheck = false;
                        if (c.regSpawn)
                        {
                            //if (c.clientMode == ClientMode.FMOWL || c.clientMode == ClientMode.AUTOCHAT)
                            if (c.clientMode == ClientMode.AUTOCHAT)
                            {
                                string coord = c.myCharacter.Map.footholds.getCenter().X + "," + c.myCharacter.Map.footholds.getCenter().Y;
                                c.characterMove(coord);
                            }
                            else
                            {
                                if (!c.specialSpawn)
                                {
                                    Portal portal = c.myCharacter.Map.getPortal((int)c.myCharacter.Spawnpoint);
                                    string coord = string.Concat(portal.X.ToString(), ",", portal.Y.ToString());
                                    c.characterMove(coord);
                                }
                                else
                                {
                                    if (c.specialSpawn & (c.clientMode == ClientMode.FULLMAPPING_P || c.clientMode == ClientMode.FULLMAPPING_NP))
                                    {
                                        c.characterMove(c.specialSpawnCoords);
                                    }
                                    else if (c.specialSpawn)
                                    {
                                        Portal portal = c.myCharacter.Map.getPortal((int)c.myCharacter.Spawnpoint);
                                        string coord = string.Concat(portal.X.ToString(), ",", portal.Y.ToString());
                                        c.characterMove(coord);
                                    }
                                }
                            }
                        }
                        packet.Skip(6);
                        if (isHiddenFaceJob(c.myCharacter.Job))
                            packet.ReadInt();
                        c.myCharacter.Fatigue = packet.Read();
                        packet.ReadInt(); //WHAT'S THIS?!?
                        packet.ReadInt(); //Ambition
                        packet.ReadInt(); //Insight
                        c.myCharacter.WillpowerExp = packet.ReadInt();
                        c.myCharacter.DilligenceExp = packet.ReadInt();
                        c.myCharacter.EmpathyExp = packet.ReadInt();
                        packet.ReadInt(); //Charm
                        packet.ReadInt();
                        packet.ReadShort();
                        packet.ReadShort();
                        packet.ReadShort();
                        packet.ReadShort();
                        packet.ReadShort();
                        packet.ReadShort();
                        //End Char Slots
                        packet.Skip(25);
                        packet.Read();
                        packet.Skip(40);
                        packet.ReadShort();
                        packet.ReadInt();
                        packet.Read();
                        c.myCharacter.BattlePoints = packet.ReadInt();
                        packet.Read();
                        packet.ReadInt();
                        packet.ReadInt();
                        packet.ReadInt();
                        packet.Read();
                        packet.Skip(32);
                        if (c.myCharacter.Job >= 3100 & c.myCharacter.Job <= 3199)
                        {
                            packet.ReadInt();
                        }
                        if (packet.Read() == 0)
                            packet.Skip(4);
                        if (packet.Read() == 1)
                        {
                            packet.ReadMapleString();
                        }
                        if (packet.Read() == 1)
                        {
                            packet.Skip(packet.ReadShort());
                        }
                        packet.Read();

                        //READINVENTORY HERE TEDIOUS SHIT
                        c.myCharacter.Meso = packet.ReadLong();
                        packet.ReadLong();
                        packet.Read();
                        /*
                        packet.ReadLong();
                        packet.ReadInt();
                        if (packet.ReadInt() > 0)
                        {
                            packet.Skip(36);
                        }
                        */
                        if (packet.ReadInt() > 0)
                            packet.Skip(24);
                        else
                            packet.ReadInt();
                        /*
                        */
                        Thread t = new Thread(delegate()
                        {
                            byte invenSlot = packet.Read();
                            while (invenSlot == 0 & c.clientMode != ClientMode.DISCONNECTED)
                            {
                                invenSlot = packet.Read();
                            }
                            c.myCharacter.InitInventory(InventoryType.EQUIPPED, 100);
                            c.myCharacter.InitInventory(InventoryType.EQUIP, invenSlot);
                            c.myCharacter.InitInventory(InventoryType.USE, packet.Read());
                            c.myCharacter.InitInventory(InventoryType.SETUP, packet.Read());
                            c.myCharacter.InitInventory(InventoryType.ETC, packet.Read());
                            c.myCharacter.InitInventory(InventoryType.CASH, packet.Read());
                            c.myCharacter.InitInventory(InventoryType.AndEqp, 100);
                            c.myCharacter.InitInventory(InventoryType.MechEqp, 100);
                            c.myCharacter.InitInventory(InventoryType.TotamEqp, 100);
                            c.myCharacter.InitInventory(InventoryType.UnkInv, 100);
                            c.myCharacter.InitInventory(InventoryType.UnkInv2, 100);
                            c.myCharacter.InitInventory(InventoryType.UnkInv3, 100);
                            c.myCharacter.InitInventory(InventoryType.UnkInv4, 100);
                            c.myCharacter.InitInventory(InventoryType.SetUpBags, 200);
                            c.myCharacter.InitInventory(InventoryType.EtcBags, 200);
                            packet.Skip(8);
                            packet.Read(); //Added in v148?
                            processInven(packet, c, c.myCharacter, InventoryType.EQUIPPED, 0);
                            packet.Read();
                            processInven(packet, c, c.myCharacter, InventoryType.EQUIPPED, 100);
                            packet.Read();
                            processInven(packet, c, c.myCharacter, InventoryType.EQUIP, 0);
                            packet.Skip(3);
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv2, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.MechEqp, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.AndEqp, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv3, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.TotamEqp, 0);
                            packet.Skip(3);
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv4, 0);
                            packet.Skip(1);
                            /*
                            */
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv4, 0);
                            packet.Skip(1);
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv4, 0);
                            packet.Skip(1);

                            //v143 x2
                            processInven(packet, c, c.myCharacter, InventoryType.UnkInv4, 0);
                            packet.Skip(1);

                            /*
                            */
                            processInven(packet, c, c.myCharacter, InventoryType.USE, 0);
                            processInven(packet, c, c.myCharacter, InventoryType.SETUP, 0);
                            processInven(packet, c, c.myCharacter, InventoryType.ETC, 0);
                            processInven(packet, c, c.myCharacter, InventoryType.CASH, 0);
                            processInven(packet, c, c.myCharacter, InventoryType.SetUpBags, 0);
                            processInven(packet, c, c.myCharacter, InventoryType.EtcBags, 0);
                            packet.Skip(4);
                            //END READ INVEN
                            c.RAND3 = new Random().Next(0, Int32.MaxValue);
                            Program.gui.updateCharInfo(c);
                            c.updateAccountStatus("Spawned in map " + c.myCharacter.mapID.ToString());
                            if (Program.debugMode || Program.userDebugMode)
                            {
                                c.updateLog("[DEBUG MODE - CHARACTER STATS]");
                                c.updateLog("Job: " + c.myCharacter.Job.ToString());
                                c.updateLog("Str: " + c.myCharacter.Str.ToString());
                                c.updateLog("Dex: " + c.myCharacter.Dex.ToString());
                                c.updateLog("Int: " + c.myCharacter.Int.ToString());
                                c.updateLog("Luk: " + c.myCharacter.Luk.ToString());
                                c.updateLog("HP: " + c.myCharacter.HP.ToString());
                                c.updateLog("MP: " + c.myCharacter.MP.ToString());
                                c.updateLog("AP: " + c.myCharacter.AP.ToString());
                                c.updateLog("EXP: " + c.myCharacter.Exp.ToString());
                                c.updateLog("Mesos: " + c.myCharacter.Meso.ToString());
                                c.updateLog("Willpower: " + c.myCharacter.WillpowerExp.ToString());
                                c.updateLog("Dilligence: " + c.myCharacter.DilligenceExp.ToString());
                                c.updateLog("Empathy: " + c.myCharacter.EmpathyExp.ToString());
                                c.updateLog("Empty Equip Slots: " + c.myCharacter.Inventorys[InventoryType.EQUIP].getFreeSlots().ToString() + "/" + c.myCharacter.Inventorys[InventoryType.EQUIP].SlotLimit.ToString());
                                /*
                                List<MapleItem> items = c.myCharacter.Inventorys[InventoryType.CASH].getItemList();
                                foreach (MapleItem i in items)
                                {
                                    c.updateLog(Database.getItemName(i.ID).ToString());
                                }
                                */
                                c.updateLog("Empty Use Slots: " + c.myCharacter.Inventorys[InventoryType.USE].getFreeSlots().ToString() + "/" + c.myCharacter.Inventorys[InventoryType.USE].SlotLimit.ToString());
                                c.updateLog("Empty ETC Slots: " + c.myCharacter.Inventorys[InventoryType.ETC].getFreeSlots().ToString() + "/" + c.myCharacter.Inventorys[InventoryType.ETC].SlotLimit.ToString());
                                c.updateLog("Empty SetUP Slots: " + c.myCharacter.Inventorys[InventoryType.SETUP].getFreeSlots().ToString() + "/" + c.myCharacter.Inventorys[InventoryType.SETUP].SlotLimit.ToString());
                                c.updateLog("Empty CASH Slots: " + c.myCharacter.Inventorys[InventoryType.CASH].getFreeSlots().ToString() + "/" + c.myCharacter.Inventorys[InventoryType.CASH].SlotLimit.ToString());
                                c.updateLog("Items in setup Bags: " + c.myCharacter.Inventorys[InventoryType.SetUpBags].totalItems().ToString());
                                c.updateLog("Items in etc Bags: " + c.myCharacter.Inventorys[InventoryType.EtcBags].totalItems().ToString());
                            }
                            if (((c.mode >= 2 & c.mode <= 7) || (c.mode == 16 || c.mode == 17 || c.mode == 19)) & c.detectStore)
                            {
                                c.getStoreandSlot();
                            }
                            if (c.lootItems)
                            {
                                string lootBotText = "[Loot] Looting mesos & the following items: ";
                                int count = 0;
                                foreach (KeyValuePair<int, string> x in Database.ItemCRC)
                                {
                                    lootBotText = lootBotText + Database.getItemName(x.Key);
                                    count++;
                                    if (count == Database.ItemCRC.Count)
                                        lootBotText = lootBotText + ".";
                                    else
                                        lootBotText = lootBotText + ", ";
                                }
                                c.updateLog(lootBotText);
                            }
                        });
                        c.workerThreads.Add(t);
                        t.Start();
                    }
                }
                else
                {
                    c.serverCheck = false;
                    packet.Skip(36);
                    if (c.myCharacter.UsedPortals == 255)
                        c.myCharacter.UsedPortals = 0;
                    c.myCharacter.UsedPortals++;
                    c.myCharacter.mapID = packet.ReadInt();
                    c.myCharacter.Spawnpoint = packet.Read();
                    getMap(c);
                    c.myCharacter.loadMap(c);
                    Program.gui.updateCharInfo(c);
                    c.updateAccountStatus("Spawned in map " + c.myCharacter.mapID.ToString());
                    if (c.clientMode == ClientMode.FMOWL)
                    {
                        //string coord = c.myCharacter.Map.footholds.getCenter().X + "," + c.myCharacter.Map.footholds.getCenter().Y;
                        Portal portal = c.myCharacter.Map.getPortal((int)c.myCharacter.Spawnpoint);
                        string coord = string.Concat(portal.X.ToString(), ",", portal.Y.ToString());
                        c.characterMove(coord);
                        return;
                    }
                    else if (c.clientMode == ClientMode.HARVESTBOTHERB)
                    {
                        if (c.myCharacter.mapID == 180000003)
                            c.characterMove("-247,-1046");
                    }
                    else if (c.clientMode == ClientMode.HARVESTBOTMINE)
                    {
                        if (c.myCharacter.mapID == 180000003)
                            c.characterMove("89,-1046");
                    }
                    else if (c.regSpawn)
                    {
                        Portal portal = c.myCharacter.Map.getPortal((int)c.myCharacter.Spawnpoint);
                        string coord = string.Concat(portal.X.ToString(), ",", portal.Y.ToString());
                        c.characterMove(coord);
                    }
                }
            }
            catch (Exception e)
            {
                c.updateLog("Error Code: 000");
            }
        }

        private void getTeleportAction(Client c)
        {
        }

        private void getMap(Client c)
        {
            try
            {
                c.mapName = Database.getMapName(c.myCharacter.mapID);
                string mapID = c.myCharacter.mapID.ToString();
                if (mapID.Contains("9100000"))
                {
                    c.RoomNum = (c.myCharacter.mapID - 910000000).ToString();
                }
            }
            catch { }
        }

        private static void processInven(PacketReader packet, Client client, Me c, InventoryType t, byte modifier = 0)
        {
            try
            {
                if (t == InventoryType.SetUpBags || t == InventoryType.EtcBags) //Bag Processing
                {
                    int bagCount = packet.ReadInt();
                    int num = 0;
                    if (bagCount > 0)
                        packet.Skip(8);
                    while (bagCount > num & client.clientMode != ClientMode.DISCONNECTED)
                    {
                        byte num1 = packet.Read();
                        if (num1 == 0xFF)
                        {
                            num++;
                            packet.Skip(11);
                            num1 = packet.Read();
                        }
                        c.Inventorys[t].addBagItems(packet, num1);
                    }
                }
                else //Reg Processing
                {
                    while (true & client.clientMode != ClientMode.DISCONNECTED)
                    {
                        byte num = packet.Read();
                        if (num == 0)
                        {
                            break;
                        }
                        c.Inventorys[t].addFromPacket(packet, (byte)(num + modifier), false, client);
                    }
                }
            }
            catch
            {

            }
        }


        public bool IsExtendedSPJob(int Job)
        {
            //11200 - 11212 Beast Tamer
            //1100/1110/1111/1112 - Dawn Warrior 
            //1200/1210/1211/1212 - Blaze Wizard 
            //1300/1310/1311/1312 - Wind Archer 
            //1400/1410/1411/1412 - Night Walker 
            //1500/1510/1511/1512 - Thunder Breaker
            //3600/3610/3611/3612 - Xenon
            if ((Job >= 11200 & Job <= 11212) || (Job >= 1100 & Job <= 1112) || (Job >= 1200 & Job <= 1212) || (Job >= 1300 & Job <= 1312) || (Job >= 1400 & Job <= 1412) || (Job >= 1500 & Job <= 1512))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private bool isHiddenFaceJob(int job)
        {
            if (job > 3600 && job < 3612)
                return true;
            return false;
        }

        private static int[] readExtendedSP(PacketReader p)
        {
            byte num = p.Read();
            if (num != 0)
            {
                int[] numArray = new int[1];
                for (int i = 0; i < num; i++)
                {
                    byte num1 = p.Read();
                    if (num1 > (int)numArray.Length - 1)
                    {
                        Array.Resize<int>(ref numArray, num1 + 1);
                    }
                    numArray[num1] = p.ReadInt();
                }
                return numArray;
            }
            else
            {
                return null;
            }
        }



    }
}
