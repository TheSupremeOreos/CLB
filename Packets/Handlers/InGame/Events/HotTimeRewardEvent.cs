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
    class HotTimeRewardEvent : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Length < 10)
                {
                    int itemID = packet.ReadInt();
                    c.updateLog("[Hottime] Gained " + Database.getItemName(itemID));
                }
                else
                {
                    byte identifier = packet.Read();
                    if (identifier == 0x09)
                    {
                        int num = packet.ReadInt();
                        while (num != 0)
                        {
                            int rewardID = packet.ReadInt();
                            while (true)
                            {
                                while (packet.Read() != 0x40)
                                {
                                    Thread.Sleep(1);
                                }
                                if (packet.Read() == 0xE0)
                                {
                                    if (packet.Read() == 0xFD)
                                        break;
                                }
                                Thread.Sleep(1);
                            }
                            packet.Skip(12);
                            byte[] bytes = packet.ReadBytes(54);
                            c.ses.SendPacket(PacketHandler.claimReward(bytes, 1, rewardID).ToArray());
                            packet.ReadMapleString(); //String
                        }
                    }
                }
            }
            catch { }
        }
    }
}
