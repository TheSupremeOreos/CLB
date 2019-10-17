using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets.Handlers
{
    class PlayerRemove : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int UID = packet.ReadInt();
                Player user = c.getPlayer(UID);
                if (user != null)
                {
                    c.Players.Remove(UID);
                    Program.gui.UpdatePlayers(c);
                }
            }
            catch
            {
                c.updateLog("Error Code: 302");
            }
        }
    }
}
