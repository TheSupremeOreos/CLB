using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Windows.Forms;

namespace PixelCLB.Packets.Handlers
{
    class IncorrectPIC : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                byte identifier = packet.Read();
                if (identifier == 0x14)
                {
                    c.updateLog("PIC is incorrect, please update your PIC Code");
                    c.forceDisconnect(false, 0, false, "Incorrect PIC Code");
                    MessageBox.Show("PIC is incorrect, please update your PIC Code and restart the bot.");
                    if (Program.gui.profile == null)
                    {
                        Profiles openProfiles = new Profiles(c.accountProfile, true);
                        openProfiles.Show();
                    }
                    else
                    {
                        Profiles profile = Program.gui.profile;
                        profile.updateAccountInfo(c.accountProfile, true);
                        profile.Focus();
                    }
                }
            }
            catch { }
        }
    }
}
