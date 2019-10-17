using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace PixelCLB.Packets.Handlers
{
    class StoreSpawned : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int UID = packet.ReadInt();
                if (UID != c.myCharacter.uid)
                {
                    if (c.freeSpot & c.clientMode != ClientMode.SPOTBOTTING_NP & c.clientMode != ClientMode.SPOTBOTTING_P & c.clientMode != ClientMode.SPOTFREE)
                    {
                        c.updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + " Failed to get spot!");
                        c.reset();
                    }
                }
                int storeID = packet.ReadInt();
                short x = packet.ReadShort();
                short y = packet.ReadShort();
                short fh = packet.ReadShort();
                string ign = packet.ReadMapleString();
                packet.Read();
                int resetID = packet.ReadInt();
                string storeName = packet.ReadMapleString();
                MapleFMShop mapleShop = new MapleFMShop(resetID, c.myCharacter.mapID);
                mapleShop.owner = ign;
                mapleShop.description = storeName;
                mapleShop.channel = c.channel;
                mapleShop.fmRoom = (c.myCharacter.mapID - 910000000).ToString();
                mapleShop.playerUID = UID;
                mapleShop.permit = false;
                mapleShop.storeID = storeID;
                mapleShop.x = x;
                mapleShop.y = y;
                mapleShop.fh = fh;
                if (Program.userDebugMode || Program.debugMode)
                {
                    c.updateLog("[Debug]" + ign + "'s store has been recorded");
                }
                c.addShop(mapleShop, mapleShop.permit);
                if (UID == c.myCharacter.uid)
                {
                    if (c.freeSpot & c.clientMode != ClientMode.SPOTBOTTING_P & c.clientMode != ClientMode.FULLMAPPING_P)
                    {
                        if (c.mode == 2)
                        {
                            c.updateLog("[Store Reset] Reset successfully!");
                        }
                        c.updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + " Store up @ CH" + c.channel + "FM" + c.RoomNum + " @ " + mapleShop.x.ToString() + "," + mapleShop.y.ToString());
                        c.updateAccountStatus("Store set up @ CH" + c.channel + "FM" + c.RoomNum);
                        c.forceDisconnect(false, 0, false, "Store set up");
                        try
                        {
                            if (File.Exists(@"\Windows\Media\tada.wav"))
                            {
                                SoundPlayer simpleSound = new SoundPlayer(@"\Windows\Media\tada.wav");
                                simpleSound.Play();
                            }
                            MessageBox.Show(new Form() { TopMost = true }, "Store should be up on " + c.LoginID + " at CH" + c.channel + "FM" + c.RoomNum + " @ " + mapleShop.x.ToString() + "," + mapleShop.y.ToString() + ".", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch
                        {
                            MessageBox.Show(new Form() { TopMost = true }, "Store should be up on " + c.LoginID + " at CH" + c.channel + "FM" + c.RoomNum + " @ " + mapleShop.x.ToString() + "," + mapleShop.y.ToString() + ".", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 104");
            }
        }
    }
}
