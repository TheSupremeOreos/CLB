using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets.Handlers
{
    class PlayerSpawned : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                short x;
                short y;
                short fh;
                int uid;
                bool flag = false;
                uid = packet.ReadInt();
                packet.Read();
                string ign = packet.ReadMapleString();
                string title;
                short num = packet.ReadShort();
                string guild = "";
                if (num == 0)
                    guild = packet.ReadMapleString();
                else
                    guild = packet.ReadString(num);
                int storeID = 0;
                if (!c.myCharacter.uid.Equals(uid))
                {
                    packet.ReadShort();
                    packet.Skip(203);
                    while (packet.Read() != 255)
                    {
                    }
                    while (packet.Read() != 255)
                    {
                    }
                    while (packet.Read() != 255)
                    {
                    }
                    int counter = 0;
                    while (true) //Packet.skip(85);
                    {
                        if (packet.Read() == 255)
                            counter = 0;
                        else
                            counter++;
                        if (counter == 85)
                            break;
                    }
                    packet.ReadInt(); //v148
                    x = packet.ReadShort();
                    y = packet.ReadShort();
                    if (x == 0 & y == 0)
                    {
                        x = packet.ReadShort();
                        y = packet.ReadShort();
                    }
                    packet.Read();
                    fh = packet.ReadShort();

                    if (packet.Read() == 225)
                    {
                        packet.Skip(6);
                    }
                    packet.Skip(16);
                    if (packet.Read() == 5)
                    {
                        storeID = packet.ReadInt();
                        title = packet.ReadMapleString();
                        MapleFMShop mapleShop = new MapleFMShop(storeID, c.myCharacter.mapID);
                        mapleShop.owner = ign;
                        mapleShop.description = title;
                        mapleShop.channel = c.channel;
                        mapleShop.fmRoom = (c.myCharacter.mapID - 910000000).ToString();
                        mapleShop.mapID = c.myCharacter.mapID;
                        mapleShop.shopID = storeID;
                        mapleShop.playerUID = uid;
                        mapleShop.permit = true;
                        mapleShop.storeID = 5140000;
                        mapleShop.x = x;
                        mapleShop.y = y;
                        mapleShop.fh = fh;
                        mapleShop.playerGuild = guild;
                        c.addShop(mapleShop, mapleShop.permit);
                        if (Program.userDebugMode || Program.debugMode)
                        {
                            c.updateLog("[Debug]" + ign + "'s permit has been recorded");
                        }
                    }
                    Player user = c.getPlayer(uid);
                    if (user != null)
                        flag = true;
                    if (!flag)
                    {
                        if (Program.exportUIDs)
                        {
                            c.exportUID(ign, uid);
                        }
                        Player p = new Player(ign, uid, x, y, fh);
                        c.addPlayer(p);
                    }
                }
            }
            catch 
            {
                c.updateLog("Error Code: 300");
            }
        }
    }
}
