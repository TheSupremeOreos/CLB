using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using PixelCLB.PacketCreation;
using System.Windows.Forms;

namespace PixelCLB.Packets.Handlers
{
    class LoginReponse : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                c.loginCheck = true;
                byte identiFier = packet.Read();
                if (identiFier == 0x0)
                {
                    if (!Program.webLogin & !c.webLogin)
                    {
                        c.ses.SendPacket(PacketHandler.SelectWorldChannel_Client(c, c.world, c.channel).ToArray());
                        c.webLogin = true;
                        packet.Reset(packet.Length - 8);
                        c.cauth = packet.ReadLong();
                        return;
                    }
                    else
                    {
                        if (Program.webLogin)
                        {
                            packet.Reset(packet.Length - 35);
                            c.cauth = packet.ReadLong();
                        }
                    }
                    c.updateLog("[Connection] Login successful.");
                    return;
                }
                else if (identiFier == 0x01)
                {
                    //??
                    c.updateLog("[Login] Unknown login error.");
                    c.forceDisconnect(true, 30, false, "Unknown Login Error");
                    return;
                }
                else if (identiFier == 0x02)
                {
                    c.updateLog("[Login] This account has been perma banned");
                    c.updateLog("Now exiting");
                    c.forceDisconnect(false, 0, false, "Account Perma Banned");
                    return;
                }
                else if (identiFier == 0x07)
                {
                    c.updateLog("[Login] The LoginID " + c.LoginID + " is already logged in.");
                    c.updateLog("Restarting in " + Program.accountLoggedRestartTime + " seconds");
                    c.forceDisconnect(true, Program.accountLoggedRestartTime, false, "Account already logged in");
                    return;
                }
                else if (identiFier == 780)
                {
                    c.nexonCookie = string.Empty;
                    c.updateLog("[Login] New auth required. Now restarting.");
                    c.forceDisconnect(true, 1, false, "Require new auth");
                    return;
                }
                else if (identiFier == 0x0C) // 00 00 0C 03 00 00 00 00
                {
                    c.updateLog("[Nexon Auth] Auth Expired, regrabbing auth.");
                    c.nexonCookie = string.Empty;
                    c.forceDisconnect(true, 0, false, "Auth expired");
                    return;
                }
                else
                {
                    c.updateLog("[Login] Unknown login error.");
                    c.forceDisconnect(true, 30, false, "Unknown login error");
                    return;
                }
            }
            catch
            {
                c.updateLog("Error Code: 005");
            }
        }
    }
}
