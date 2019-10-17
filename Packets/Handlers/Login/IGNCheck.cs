using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.PacketCreation;
using System.Threading;
using System.Windows.Forms;

namespace PixelCLB.Packets.Handlers
{
    class IGNCheck : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                string ign = packet.ReadMapleString();
                byte identifier = packet.Read();
                if (identifier == 0)
                {
                    c.clientMode = ClientMode.DISCONNECTED;
                    c.ses.SendPacket(PacketHandler.CreateCharacter(ign).ToArray());
                }
                c.ignChecked = false;
            }
            catch { }
        }
    }
}
