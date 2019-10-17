using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.CLBTools;

namespace PixelCLB.Packets.Handlers
{
    class CashShopReponse : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            c.csCheck = false;
        }
    }
}