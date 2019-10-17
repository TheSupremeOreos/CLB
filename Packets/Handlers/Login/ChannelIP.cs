using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets.Handlers
{
    class ChannelIP : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                packet.ReadShort();
                byte[] numArray = packet.ReadBytes(4);
                short num = packet.ReadShort();
                packet.ReadInt();
                object[] objArray = new object[] { numArray[0], ".", numArray[1], ".", numArray[2], ".", numArray[3] };
                c.currentip = string.Concat(objArray);
                c.currentport = num;
                if (Program.debugMode || Program.userDebugMode)
                    c.updateLog("[IP] " + c.currentip + ":" + c.currentport);
                c.ChannelConnect();
                return;
            }
            catch
            {
                c.updateLog("Error Code: 002");
            }
        }
    }
}
