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
    class CharacterCreated : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                packet.Read();
                int characterUID = packet.ReadInt();
                string ign = packet.ReadString(13).Replace("\0", "");
                if (c.ignStealer.Contains(ign))
                {
                    c.updateLog("[IGN Stealer] " + ign + " has been successfully stolen");
                    c.ignStealer.Remove(ign);
                    if (c.ignStealer.Count < 1)
                        c.forceDisconnect(false, 0, false, "IGN successfully stolen");
                    else
                    {
                        Dictionary<string, string> sectionValues = Program.iniFile.GetSectionValues(c.toProfile());
                        string ignSteals = sectionValues["ignSteal"];
                        string ignPossibility1 = "," + ign;
                        string ignPossibility2 = ign + ",";
                        if (ignSteals.Contains(ignPossibility1))
                            ignSteals = ignSteals.Replace(ignPossibility1, "");
                        if (ignSteals.Contains(ignPossibility2))
                            ignSteals = ignSteals.Replace(ignPossibility2, "");

                        Program.iniFile.WriteValue(c.toProfile(), "ignSteal", ignSteals);
                        sectionValues = Program.iniFile.GetSectionValues(c.toProfile());
                        ignSteals = sectionValues["ignSteal"];

                        c.ignStealer.Clear();
                        foreach (string x in ignSteals.Split(','))
                        {
                            c.ignStealer.Add(x);
                        }
                        Thread threadz = new Thread(() => c.onServerConnected(false));
                        c.workerThreads.Add(threadz);
                        threadz.Start();
                    }
                }
            }
            catch { }
        }
    }
}
