using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;

namespace PixelCLB.Packets.Handlers
{
    class ItemReceivedFromNPC : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Read() == 0x05)
                {
                    int num = packet.Read();
                    while (num > 0)
                    {
                        int itemID = packet.ReadInt();
                        short count = packet.ReadShort();
                        packet.ReadShort();
                        num = num - 1;
                        if (c.clientMode == ClientMode.CASSANDRA)
                        {
                            if (itemID == 2431042 || itemID == 2049402 || itemID == 1122221)
                                continue;
                        }
                        //c.updateLog("[NPC] Gained " + count + " " + Database.getItemName(itemID));
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 201");
            }
        }
    }
}
