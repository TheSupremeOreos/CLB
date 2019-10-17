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
        public void dropCassndraItems()
        {
            try
            {
                if (myCharacter.Inventorys[InventoryType.EQUIP] != null)
                    myCharacter.Inventorys[InventoryType.EQUIP].dropAll(1122221, this);
                if (myCharacter.Inventorys[InventoryType.USE] != null)
                {
                    myCharacter.Inventorys[InventoryType.USE].dropAll(2049402, this);
                    myCharacter.Inventorys[InventoryType.USE].dropAll(2431042, this);
                }
                foreach (MapleItem item in dropItem)
                {
                    updateLog("[Inventory] Dropping " + item.quantity + " " + Database.getItemName(item.ID));
                    ses.SendPacket(PacketHandler.dropItem(item.type, item.position, item.quantity).ToArray());
                    //ses.SendPacket(PacketHandler.deleteItem(item.ID).ToArray());
                    Thread.Sleep(10);
                }
                dropItem.Clear();
            }
            catch { }
        }


        private void openMesoExploitBoxes(int ID)
        {
            try
            {
                if (myCharacter.Inventorys[InventoryType.USE] != null)
                    myCharacter.Inventorys[InventoryType.USE].consumeAllScripted(ID, this);
                if (dropItem.Count > 0)
                {
                    foreach (MapleItem item in dropItem)
                    {
                        Thread.Sleep(8);
                        updateLog("[Inventory] Consuming " + item.quantity + " " + Database.getItemName(item.ID));
                        ses.SendPacket(PacketHandler.consumeScripted(item.position, item.ID, item.quantity).ToArray());
                    }
                    Thread.Sleep(8);
                    dropItem.Clear();
                }
            }
            catch { }
        }

        /// <summary>
        /// chatID 
        /// 1 = ALL,
        /// 2 = Whisper,
        /// 3 = Buddy,
        /// 4 = Guild,
        /// 5 = Alliance
        /// 6 = /find
        /// 7 = /guildinvite
        /// </summary>
        /// <param name="chatID"></param>
        /// <param name="text"></param>
        public void chat(int chatID, string text, string ign)
        {
            try
            {
                if (chatID > 0)
                {
                    if (text.Equals(string.Empty) || text.Equals(null) || text.Equals(""))
                    {
                        updateLog("Please enter valid text to say");
                        return;
                    }
                    if (chatID == 2 & (ign.Count() < 4 || ign.Count() > 13 || ign.Equals("")))
                    {
                        updateLog("Please enter a valid ign to whisper");
                        return;
                    }
                    if (ign.Equals(""))
                        ign = myCharacter.ign;
                    string format = ign.Replace("\0", "") + " : " + text;
                    switch (chatID)
                    {
                        case 1:
                            {
                                format = string.Concat("[All] ", format);
                                ses.SendPacket(PacketHandler.allChat(text).ToArray());
                                break;
                            }
                        case 2:
                            {
                                format = string.Concat("[Whisper] ", ign, " << ", text);
                                ses.SendPacket(PacketHandler.whisper(ign, text).ToArray());
                                break;
                            }
                        case 3:
                            {
                                format = string.Concat("[Buddy] ", format);
                                ses.SendPacket(PacketHandler.buddyChat(this, text).ToArray());
                                break;
                            }
                        case 4:
                            {
                                format = string.Concat("[Guild] ", format);
                                ses.SendPacket(PacketHandler.guildChat(this, text).ToArray());
                                break;
                            }
                        case 6:
                            {
                                format = text;
                                string[] str = text.Split(' ');
                                ses.SendPacket(PacketHandler.findPlayer(str[1]).ToArray());
                                break;
                            }
                        case 7:
                            {
                                format = text;
                                string[] str = text.Split(' ');
                                findTarget = str[1];
                                ses.SendPacket(PacketHandler.addToGuild(str[1]).ToArray());
                                break;
                            }
                    }
                    refreshChatBox(format);
                }
            }
            catch
            {
                updateLog("Error with chatting!");
            }
        }


        public void moveMap(int mapID)
        {
            timeOutCheck = true;
            serverCheck = true;
            while (serverCheck == true && clientMode != ClientMode.DISCONNECTED)
            {
                if (timeOutCheck == true)
                {
                    //ses.SendPacket(PacketHandler.mapTele(mapID).ToArray()); //OWL TELEPORT
                    timeOut(3, 5);
                }
                Thread.Sleep(1);
            }
        }


        public void refreshChatBox(string textToAdd)
        {
            if (chatLogs)
            {
                if (chatCollection.Count > 600)
                {
                    chatCollection.RemoveAt(0);
                }
                if (textToAdd != "")
                    chatCollection.Add(textToAdd);
                if (charInfoWindow != null)
                    charInfoWindow.refreshChatBox();
            }
        }

        private bool fileDeleteAndMove(string deleteFile, string moveFile)
        {
            try
            {
                if (File.Exists(deleteFile))
                    File.Delete(deleteFile);
                if (File.Exists(moveFile))
                    File.Move(moveFile, deleteFile);
                return true;
            }
            catch
            {
                Thread.Sleep(500);
                return false;
            }
        }


        public void makeAccountIdle()
        {
            try
            {
                List<Thread> threadlist = workerThreads.ToList<Thread>();
                foreach (Thread t in threadlist)
                {
                    if (t.IsAlive)
                    {
                        try
                        {
                            t.Join(500);
                        }
                        catch (ThreadAbortException e) { }
                    }
                }
            }
            catch { }
            mode = 0;
            modeDeter.getMode(this, true);
            clientMode = ClientMode.DISCONNECTED;
            clientMode = ClientMode.IDLE;
        }

        private void whisperCommands()
        {
            try
            {
                updateLog("----- Whisper Commands ------");
                updateLog("#cc x");
                updateLog("#room x");
                updateLog("#logoff " + Program.logOffPass);
                updateLog("#skill xxxxxx");
                updateLog("#skill hs");
            }
            catch (Exception e)
            {
            }

        }


        /// <summary>
        /// MISC
        /// </summary>


        public void pictureBoxChange()
        {
            try
            {
                Program.gui.GUIInvokeMethod(() =>
                    {
                        if (clientMode == ClientMode.DISCONNECTED)
                            Program.gui.pictureBox1.Image = Properties.Resources.Stop16;
                        else if (clientMode == ClientMode.LOGIN || clientMode == ClientMode.WAITFORONLINE || clientMode == ClientMode.WAITFORONLINE)
                            Program.gui.pictureBox1.Image = Properties.Resources.Loading16;
                        else if (clientMode == ClientMode.LOGGEDIN)
                            Program.gui.pictureBox1.Image = Properties.Resources.Tick16;
                        else
                            Program.gui.pictureBox1.Image = Properties.Resources.Tick16;
                    });
            }
            catch { }
        }
        
        ///Status Info
        ///
        public void updateAccountStatus(string status)
        {
            try
            {
                int attempts = 0;
                while (realMode.Value != status)
                {
                    if (attempts >= 10)
                        break;
                    realMode.Value = status;
                    attempts++;
                }
                Program.gui.updateCharInfo(this);

                /*
                int tries = 0;
                while (Program.gui.activeAccounts.FindString(accountProfile + " - " + status) < 1)
                {
                    Thread.Sleep(100);
                    realMode.Value = status;
                    Program.gui.updateActiveAccounts(true);
                    Program.gui.updateCharInfo(this);
                    tries++;
                    if (tries > 10)
                        break;
                }
                 * */
            }
            catch { }
        }
        ///waitforpacket
        ///OpCode / Timeout(secs)
        public void waitForPacket(int opCode, int timeout)
        {
            waitForPacketOpCode = (short)opCode;
            waitPacket = false;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (waitPacket & clientMode != ClientMode.DISCONNECTED)
            {
                if (stopWatch.Elapsed.Seconds >= timeout)
                    break;
                else
                    Thread.Sleep(10);
            }
        }

        //Harvest Processes
        public void addHarvest(Harvest harvest)
        {
            if (!myCharacter.Map.containsHarvest(harvest.harvestID))
            {
                if (clientMode == ClientMode.HARVESTBOTMINE)
                {
                    if (harvest.Type >= HarvestType.Vien1 & harvest.Type <= HarvestType.VienHeart2)
                    {
                        myCharacter.Map.addHarvest(harvest);
                    }
                }
                else if (clientMode == ClientMode.HARVESTBOTHERB)
                {
                    if (harvest.Type >= HarvestType.Herb1 & harvest.Type <= HarvestType.HerbGold2)
                    {
                        myCharacter.Map.addHarvest(harvest);
                    }
                }
                else
                {
                    myCharacter.Map.addHarvest(harvest);
                }
            }
        }


        public void harvest(int harvestID)
        {
            ses.SendPacket(PacketHandler.harvestObject(harvestID).ToArray());
        }

        public void buyCraftingItem()
        {

        }

        public void buyEnergyDrink()
        {
            if (npcShop == null)
            {
                foreach (NPC npc in myCharacter.Map.getNPCList())
                {
                    if (npc.ID != 9031007)
                    {
                        continue;
                    }
                    ses.SendPacket(PacketHandler.NPC_Click(npc.ObjectID, npc.x, npc.y).ToArray());
                }
            }
            else
            {
                ses.SendPacket(PacketHandler.buyItemFromNPC((short)npcShop.getItemOrderNumer(2340220), 2340220, 24, npcShop.getPrice(2340220)).ToArray());
                Thread.Sleep(100);
                ses.SendPacket(PacketHandler.closeNPCShop(this).ToArray());
            }
                
        }

        public void buyCraftingItem(int itemID, int amount)
        {
            if (npcShop == null)
            {
                foreach (NPC npc in myCharacter.Map.getNPCList())
                {
                    if (npc.ID != 9031007)
                    {
                        continue;
                    }
                    ses.SendPacket(PacketHandler.NPC_Click(npc.ObjectID, npc.x, npc.y).ToArray());
                }
            }
            else
            {
                ses.SendPacket(PacketHandler.buyItemFromNPC((short)npcShop.getItemOrderNumer(itemID), itemID, (short)amount, npcShop.getPrice(itemID)).ToArray());
                Thread.Sleep(100);
                ses.SendPacket(PacketHandler.closeNPCShop(this).ToArray());
            }

        }


        public void useEnergyDrink()
        {

        }

        public void ardentMillProcess()
        {
            if (clientMode == ClientMode.HARVESTBOTMINE)
                characterMove(ardentMineCoords);
            else if (clientMode == ClientMode.HARVESTBOTHERB)
                characterMove(ardentHerbCoords);
            while (!craftDone & clientMode != ClientMode.DISCONNECTED)
            {
                foreach (int itemID in Program.lootIDs)
                {
                    bool craftItemStatus = false;
                    while (craftItemStatus & clientMode != ClientMode.DISCONNECTED)
                    {
                        if (myCharacter.Inventorys[InventoryType.ETC].getItemCount(itemID) + myCharacter.Inventorys[InventoryType.EtcBags].getItemCount(itemID) < Computations.getRequiredAmount(itemID))
                            craftItemStatus = true;

                        if (myCharacter.Fatigue > 180)
                        {
                            if (myCharacter.Inventorys[InventoryType.USE].getItemCount(2340220) > 0) //Super Moo
                                useEnergyDrink();
                            else if (myCharacter.Inventorys[InventoryType.USE].getFreeSlots() > 0)
                                buyEnergyDrink();
                            else if (myCharacter.Inventorys[InventoryType.USE].getFreeSlots() == 0)
                                updateLog("No use space! Now Disconnecting");
                                //disconnect here
                        }
                        else
                        {
                            if (myCharacter.Inventorys[InventoryType.ETC].getItemCount(Computations.getRequiredCraftingItem(itemID)) == 0)
                            {

                                //buyCraftingItem(itemID, 100);
                            }
                            if (myCharacter.Inventorys[InventoryType.ETC].getItemCount(Computations.getRequiredCraftingItem(itemID)) > 0)
                            {

                                //CraftItem
                            }

                        }
                    }
                    
                }
                craftDone = true;
            }
        }

        public void updateLog(string Text)
        {
            try
            {
                Program.gui.GUIInvokeMethod(() =>
                {
                    if (Text.Equals(""))
                    {
                        Program.gui.logBox.BeginUpdate();
                        if (Program.logNeedsUpdating)
                        {
                            Program.gui.logBox.Items.Clear();
                            if (logBox.Count > 3000)
                            {
                                logBox.RemoveAt(0);
                            }
                            logBox.Add(Text);
                            foreach (string s in logBox)
                            {
                                if (!string.IsNullOrEmpty(s))
                                    Program.gui.logBox.Items.Add(s);
                            }
                            Program.logNeedsUpdating = false;
                        }
                        Program.gui.logBox.SelectedIndex = Program.gui.logBox.Items.Count - 1;
                        Program.gui.logBox.SelectedIndex = -1;
                        Program.gui.logBox.EndUpdate();
                    }
                    else
                    {
                        if (logBox.Count > 3000)
                        {
                            logBox.RemoveAt(0);
                        }
                        logBox.Add(Text);
                        if (this == Program.gui.c)
                        {
                            Program.gui.logBox.Items.Add(Text);
                            if (Program.gui.logBox.Items.Count > 3000)
                                Program.gui.logBox.Items.RemoveAt(0);
                        }
                        if (Program.gui.logBox.SelectedIndex == -1)
                        {
                            Program.gui.logBox.SelectedIndex = Program.gui.logBox.Items.Count - 1;
                            Program.gui.logBox.SelectedIndex = -1;
                        }
                    }
                });
            }
            catch
            {
            }
        }



        //AB Crash
        public void abCrash(int skillID, byte level)
        {
            ses.SendPacket(PacketHandler.abXForm(true).ToArray());
            Thread.Sleep(100);
            ses.SendPacket(PacketHandler.abSkill(false, skillID, level).ToArray());
            Thread.Sleep(100);
            ses.SendPacket(PacketHandler.abSkill(true, skillID).ToArray());
            Thread.Sleep(100);
            ses.SendPacket(PacketHandler.abXForm(false).ToArray());
        }

        //Exploit Process
        public void spamThis()
        {
            updateLog("Spamming some packet o_O");
            updateAccountStatus("Spamming packet");
            Thread opps = new Thread(delegate()
            {
                while (true & clientMode != ClientMode.DISCONNECTED)
                {
                    Thread.Sleep(200);
                    ses.SendPacket(PacketHandler.spamThis(this).ToArray());
                }
            });
            opps.Start();
        }

    }
}
