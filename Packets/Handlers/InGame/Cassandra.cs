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
    class Cassandra : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Read() == 0x7F)
                {
                    c.ses.SendPacket(PacketHandler.Cassandra(0, 0, 0).ToArray());
                    c.npcWindows++;
                }
            }
            catch { }
        }
    }
}
