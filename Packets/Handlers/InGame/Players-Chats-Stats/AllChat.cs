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
    class AllChat : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int uid = packet.ReadInt();
                if (c.chatLogs & uid != c.myCharacter.uid)
                {
                    byte type = packet.Read();
                    string text = packet.ReadMapleString();
                    string ign = "";
                    Player user = c.getPlayer(uid);
                    if (user != null)
                    {
                        ign = user.ign;
                        if (!ign.Equals(""))
                        {
                            text = string.Concat("[All] ", ign, " : ", text);
                            c.refreshChatBox(text);
                        }
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 200");
            }
        }
    }
}
