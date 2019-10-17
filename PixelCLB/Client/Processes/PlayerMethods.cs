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
        public void addPlayer(Player player)
        {
            Players.Add(player.uid, player);
            //myCharacter.Map.addChar(player.uid, player);
            Program.gui.UpdatePlayers(this);
        }

        public bool getPlayer(List<string> ign)
        {
            List<string> igns = new List<string>();
            try
            {
                foreach (KeyValuePair<int, Player> users in Players)
                {
                    igns.Add(users.Value.ign.ToLower());
                }
                foreach (string str in ign)
                {
                    if (igns.Contains(str.ToLower()))
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public Player getPlayer(int UID)
        {
            if (Players.ContainsKey(UID))
                return Players[UID];
            return null;
        }

        public bool doesPlayerExist(int UID)
        {
            if (Players.ContainsKey(UID))
                return true;
            return false;
        }

        public void characterMove(string teleCoords)
        {
            try
            {
                if (teleCoords != null)
                {
                    string[] coord = teleCoords.Split(',');
                    short x = short.Parse(coord[0]);
                    short y = short.Parse(coord[1]);
                    Portal portal = new Portal();
                    portal.X = x;
                    portal.Y = y;
                    int crc = Database.getMAPCRC(myCharacter.mapID);
                    //if (crc != -1)
                        //ses.SendPacket(PacketHandler.CharacterMove(this, crc, portal, false).ToArray());
                 }
            }
            catch (Exception e)
            {
                updateLog("Character teleport error!");
            }
        }

        public void exportUID(string ign, int UID)
        {
            try
            {
                string fileDirectory = Path.Combine(Program.uidFileDirectory, "UIDExport.txt");
                if (File.Exists(fileDirectory))
                {
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(fileDirectory);
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] str = line.Split(' ');
                        if (str[0].Equals(ign))
                        {
                            file.Close();
                            return;
                        }
                    }
                    file.Close();
                    PacketBuilder hexedUID = new PacketBuilder();
                    hexedUID.WriteInt(UID);
                    updateLog("Exporting UID for " + ign + ". UID: " + hexedUID);
                    StreamWriter streamWriter = File.AppendText(fileDirectory);
                    streamWriter.WriteLine(ign + " Hex: " + hexedUID + " Int: " + UID);
                    hexedUID.Dispose();
                    streamWriter.Close();
                }
                else
                {
                    updateLog("File not found! UIDExport.txt file created");
                    File.Create(fileDirectory);
                    Thread.Sleep(100);
                    exportUID(ign, UID);
                }
            }
            catch { }
        }

        public void useMpPot()
        {
            byte slotID = myCharacter.Inventorys[InventoryType.USE].getSlotById(MPPotID);
            if (slotID > 0)
            {
                ses.SendPacket(PacketHandler.useItem(slotID, MPPotID).ToArray());
                Thread.Sleep(1500);
            }
        }

        ///DC Hacks
        ///
        /// 
        public void dcUser(string targetUID, byte maxUsers, byte type = 0x00)
        {
            if (type == 0x00)
            {
                if (mode == 1)
                    type = 0x06;
                else if (mode == 20)
                    type = 0x03;
            }
            ses.SendPacket(PacketHandler.ChatDC(targetUID, type, maxUsers).ToArray());
            Thread.Sleep(1000);
        }

        public void makeExped(string exped)
        {
            ses.SendPacket(PacketHandler.formExped(exped).ToArray());
            Thread.Sleep(500);
        }

    }
}