using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB.Net.Packets;
using PixelCLB;
using PixelCLB.CLBTools;
using PixelCLB.PacketCreation;
using System.Media;
using System.Drawing;

namespace PixelCLB.PacketCreation
{
    public class Handler
    {
        public Handler()
        {
        }
        public void popUpNotice(Client c, PacketReader packet)
        {
            packet.Read();
            packet.ReadInt();
            packet.ReadShort();
            packet.Read();
            int ID = packet.ReadInt();
            if (ID == 9010000)
            {
                //c.ses.SendPacket(PacketHandler.closePopUp().ToArray());
            }
        }

        public void cannotPreformAction(Client c, PacketReader packet)
        {
            if (packet.Length == 3)
            {
                byte ID = packet.Read();
                if (ID == 0)
                    return;
                //c.ses.SendPacket(PacketHandler.closePopUp().ToArray());
                else if (ID == 0x01)
                {
                    c.serverOffline = true;
                    c.updateLog("[Connection] Channel is offline");
                    //Cannot connect to channel
                }
            }
        }

        public bool IsExtendedSPJob(int Job)
        {
            if ((Job < 2200 || Job > 2999) && Job / 1000 != 3 && (Job < 540 || Job > 572) && Job != 2002 && Job != 2003 && Job != 2004 && Job != 508 && Job != 3001)
            {
                return Job >= 4000;
            }
            else
            {
                return true;
            }
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


        public void Mushy_Enter(Client c, PacketReader packet)
        {
            c.inmushy = true;
        }

        public void itemReponse(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Length >= 11 & packet.Length < 13)
                {
                    packet.Read();
                    byte byte1 = packet.Read();
                    if (byte1 == 1)
                    {//Mesos
                        packet.Read();
                        short lootamount = packet.ReadShort(); //Meso amount looted
                        //update label amount
                    }
                    else
                    {
                        int itemID = packet.ReadInt(); //Item ID Picked up
                        //Find item name, echo to listbox
                    }
                }
            }
            catch
            {
                c.updateLog("Inventory is full! Failed to loot!");
            }
        }

        public void giftAvailable(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Read() == 0x09)
                {
                }
            }
            catch { }
        }

        /// <summary>
        /// Harvest Settings
        /// </summary>
        /// <param name="c"></param>
        /// <param name="packet"></param>
        

        public void harvestSpawned(Client c, PacketReader packet)
        {
            try
            {
                int identifier = packet.ReadInt();
                int harvestID = packet.ReadInt();
                packet.Read();
                Point p = new Point(packet.ReadShort(), packet.ReadShort());
                if ((harvestID >= 200000 && harvestID <= 200011) || (harvestID >= 100000 && harvestID <= 100011) || (harvestID == 9702005))
                {
                    Harvest harvest = new Harvest(identifier, (HarvestType)harvestID, p);
                    c.addHarvest(harvest);
                }
            }
            catch
            {
                c.updateLog("Error Code: 400");
            }
        }
        public void harvestRemove(Client c, PacketReader packet)
        {
            try
            {
                int harvestID = packet.ReadInt();
                c.myCharacter.Map.removeHarvest(harvestID);
            }
            catch
            {
                c.updateLog("Error Code: 401");
            }
        }
        public void harvestStatus(Client c, PacketReader packet)
        {
            try
            {
                int harvestID = packet.ReadInt();
                c.harvest(harvestID);
                c.harvest(harvestID);
                c.harvest(harvestID);
            }
            catch
            {
                c.updateLog("Error Code: 402");
            }
        }


        //NPC Related
        public void NPCShopResponse(Client c, PacketReader packet)
        {
            try
            {
                c.npcShop = new NPCShop();
                NPCShop shop = c.npcShop;
                packet.ReadInt();
                packet.ReadInt();
                packet.Read();
                short itemCount = packet.ReadShort();
                int num = 1;
                while (itemCount > num)
                {
                    int itemID = packet.ReadInt();
                    int price = packet.ReadInt();
                    shop.addItem(itemID, price);
                    packet.Skip(99);
                    num++;
                }
            }
            catch
            {
                c.updateLog("Error Code: xxxx");
            }
        }


        public void NPC_Spawned(Client c, PacketReader packet)
        {
            int num = packet.ReadInt();
            int npcID = packet.ReadInt();
            short x = packet.ReadShort();
            short y = packet.ReadShort();
            c.myCharacter.Map.addNPC(num, npcID, x, y);
        }

        public void NPC_Talk(Client c, PacketReader packet)
        {
            int npcTalkID = packet.ReadInt();
            packet.Read();
            byte indentifier = packet.Read();
            packet.Read();
            string npcText = packet.ReadMapleString();

        }

    }
}