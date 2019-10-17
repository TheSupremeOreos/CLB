using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;

namespace PixelCLB.Packets.Handlers
{
    class GuildReponse : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            string format = "";
            try
            {
                byte bytes = packet.Read();
                if (bytes == 0x30) // Cannot find when added
                {
                    format = "The character cannot be found in the current channel.";
                    c.chat(6, "/find " + c.findTarget, "");
                    c.refreshChatBox(format);
                }
                else if (bytes == 0x2E) // Already joined another guild
                {
                    format = "Already joined a guild.";
                    c.refreshChatBox(format);
                }
                else if (bytes == 0x2D) // Joined
                {
                    packet.ReadInt();
                    c.guildMemberUIDS.Add(packet.ReadInt());
                    format = packet.ReadString(13).Replace("\0", "") + " has joined the guild.";
                    c.refreshChatBox(format);
                }
                else if (bytes == 0x32) // Leave
                {
                    packet.ReadInt();
                    c.guildMemberUIDS.Remove(packet.ReadInt());
                    format = packet.ReadMapleString() + " has left the guild.";
                    c.refreshChatBox(format);
                }
                else if (bytes == 0x35) // Kicked
                {
                    packet.ReadInt();
                    c.guildMemberUIDS.Remove(packet.ReadInt());
                    format = packet.ReadMapleString() + " has been kicked from the guild.";
                    c.refreshChatBox(format);
                }
                else if (bytes == 0x20) //Loadout
                {
                    if (packet.Length > 4)
                    {
                        packet.ReadInt();
                        packet.Read();
                        c.myCharacter.guild = packet.ReadMapleString();
                        packet.ReadMapleString(); //Guild Master Rank Name
                        packet.ReadMapleString(); //Guild Jr.Master Rank Name
                        packet.ReadMapleString(); //Guild Member3 Rank Name
                        packet.ReadMapleString(); //Guild Member2 Rank Name
                        packet.ReadMapleString(); //Guild Member1 Rank Name
                        Program.gui.updateCharInfo(c);
                        byte guildMembers = packet.Read();
                        c.guildMemberUIDS.Clear();
                        while (guildMembers != 0)
                        {
                            c.guildMemberUIDS.Add(packet.ReadInt());
                            guildMembers--;
                        }
                        packet.Dispose();
                        return;
                    }
                    else
                    {
                        c.myCharacter.guild = "N/A";
                        return;
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 204");
            }
        }
    }
}
