using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Drawing;

namespace PixelCLB.Packets.Handlers
{
    class PlayerMoved : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int UID = packet.ReadInt();
                packet.ReadInt();
                short x = packet.ReadShort();
                short y = packet.ReadShort();
                Foothold foothold = c.myCharacter.Map.footholds.findBelow(new Point(x, y));
                Player user = c.getPlayer(UID);
                if (user != null)
                {
                    user.x = x;
                    user.y = y;
                    user.foothold = (short)foothold.getId();
                }
            }
            catch
            {
                c.updateLog("Error Code: 301");
            }
        }
    }
}
