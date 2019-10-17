using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets
{
    interface PacketReceiver
    {
        void packetAction(Client client, PacketReader packet);
    }
}
