using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets.Handlers
{
    class GuildBLAliChat : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (c.chatLogs)
                {
                    byte type = packet.Read();
                    string ign = packet.ReadMapleString();
                    string text = packet.ReadMapleString();
                    string collectionFormat = string.Empty;
                    string format;
                    format = string.Concat(ign, " : ", text);
                    if (!c.myCharacter.ign.Replace("\0", "").Equals(ign))
                    {
                        if (type >= 0 && type <= 6)
                        {
                            if (type == 0)
                            {
                                collectionFormat = string.Concat("[Buddy] ", format);
                            }
                            else if (type == 2)
                            {
                                collectionFormat = string.Concat("[Guild] ", format);
                            }
                            else if (type == 3)
                            {
                                collectionFormat = string.Concat("[Alliance] ", format);
                            }
                            /*
                            else if (type == 4)
                            {
                                collectionFormat = string.Concat("[Alliance] ", format);
                            }
                            else if (type == 5)
                            {
                                collectionFormat = string.Concat("[Alliance] ", format);
                            }
                             */
                            else if (type == 6)
                            {
                                collectionFormat = string.Concat("[Expedition] ", format);
                            }
                            c.refreshChatBox(collectionFormat);
                        }
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 203");
            }
        }
    }
}
