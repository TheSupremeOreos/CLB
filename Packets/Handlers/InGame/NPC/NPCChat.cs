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
    class NPCChat : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                byte identifier = packet.Read();
                byte action = 0x00;
                int npcID;
                if (identifier == 0x04)
                {
                    npcID = packet.ReadInt();
                    packet.Read();
                    action = packet.Read();
                    if (packet.Read() != 0)
                    {
                        npcID = packet.ReadInt();
                    }
                }
                else if (identifier == 0x03)
                {
                    packet.ReadInt();
                    if (packet.Read() == 0x01)
                    {
                        npcID = packet.ReadInt();
                        action = packet.Read();
                        packet.Read();
                    }
                    else
                    {
                        action = packet.Read();
                        packet.Read();
                        npcID = packet.ReadInt();
                    }
                }
                string npcText = packet.ReadMapleString();
                byte[] npcaction = new byte[] { action, 1 };
                if (c.clientMode != ClientMode.WBMESOEXPLOIT)
                    c.ses.SendPacket(PacketHandler.npcChat(npcaction).ToArray());
                if (npcText.ToLower().Contains("talk to me again when you have at least") && npcText.ToLower().Contains("empty slots"))
                {
                    c.updateLog("[NPC] Inventory is full!");
                    if (Program.switchAccountsOwl)
                    {
                        c.npcWindows = 0;
                        Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.accountProfile);
                        Program.iniFile.WriteValue(c.accountProfile, "Channel", "1");
                        int charNum = c.charnumber + 1;
                        int maxNum = c.maxcharnumber;
                        if (charNum == maxNum)
                        {
                            c.updateLog("[Acc Switch] Max Char #s reached. Disconnecting");
                            c.forceDisconnect(false, 0, false, "Max Char #s reached");
                            return;
                        }
                        charNum++;
                        Program.iniFile.WriteValue(c.accountProfile, "CharNum", charNum);
                        c.updateLog("[Acc Switch] Char num has been switched to " + charNum.ToString());
                        c.forceDisconnect(true, 1, false, "Char Num switched");
                        return;
                    }
                }
                c.npcWindows++;
            }
            catch { }
        }
    }
}
