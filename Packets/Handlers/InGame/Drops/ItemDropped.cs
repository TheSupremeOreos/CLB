using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;
using System.Media;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace PixelCLB.Packets.Handlers
{
    class ItemDropped : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            if (c.lootItems)
            {
                byte identifier = packet.Read();
                if (identifier != 0) // 0 = Air 1 = Floor 2 = Spawned already on floor?
                {
                    int lootID = packet.ReadInt();
                    bool meso = false;
                    byte x = packet.Read();
                    if (x == 1)
                        meso = true;
                    int itemID = packet.ReadInt();
                    if (itemID > 0)
                    {
                        packet.Skip(5);
                        Point point = new Point(packet.ReadShort(), packet.ReadShort());
                        if (!meso)
                        {
                            if (Database.getItemCRC(itemID) != 0)
                            {
                                c.ses.SendPacket(PacketHandler.lootItem(c, lootID, point, Database.getItemCRC(itemID)).ToArray()); //Non-Meso
                                Thread.Sleep(80);
                            }
                        }
                        else
                        {
                            c.ses.SendPacket(PacketHandler.lootItem(c, lootID, point, 0).ToArray()); //Meso
                            Thread.Sleep(80);
                        }
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
}
