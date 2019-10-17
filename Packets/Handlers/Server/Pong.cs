using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.PacketCreation;

namespace PixelCLB.Packets.Handlers
{
    class Pong : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                c.ses.SendPacket(PacketHandler.Pong().ToArray());
            }
            catch
            {
                c.updateLog("Error Code: 006");
            }
        }
    }
}
