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

namespace PixelCLB.Packets.Handlers
{
    class PermitRemoved : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int UID = packet.ReadInt();
                if (c.clientMode == ClientMode.FULLMAPPING_P || c.clientMode == ClientMode.FULLMAPPING_NP || c.clientMode == ClientMode.SPOTFREE)
                {
                    if (packet.Length == 7)
                    {
                        if (!c.mapleFMShopCollection.doesShopExist(UID, true)) //RPS close response = same as permit close?!?!
                            return;
                        string movement = c.getPermitXY(UID);
                        if (movement == null)
                            return;
                        c.mapleFMShopCollection.RemoveShop(UID, true);
                        c.coords = movement;
                        c.freeSpot = true;
                        return;
                    }
                    if (packet.Length > 8)
                    {
                        if (UID == c.myCharacter.uid & c.freeSpot)
                        {
                            c.updateAccountStatus("Permit set up @ CH" + c.channel + "FM" + c.RoomNum);
                            c.updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + " Permit up @ CH" + c.channel + "FM" + c.RoomNum + ".");
                            c.clientMode = ClientMode.PERMITUP;
                            if (c.mode != 19)
                            {
                                try
                                {
                                    SoundPlayer simpleSound = new SoundPlayer(@"C:\Windows\Media\tada.wav");
                                    simpleSound.Play();
                                    MessageBoxEx.Show(new Form() { TopMost = true }, "Permit Should be up on " + c.LoginID + " at CH" + c.channel + "FM" + c.RoomNum + ". Click stop to close.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 60000);
                                }
                                catch
                                {
                                    MessageBoxEx.Show(new Form() { TopMost = true }, "Permit Should be up on " + c.LoginID + " at CH" + c.channel + "FM" + c.RoomNum + ". Click stop to close.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 60000);
                                }
                            }
                            else
                            {
                                c.banAndCopyMerchant(c.merchantCopyTargetIGN, c.merchantCopyIGN);
                            }
                        }
                        if (UID != c.myCharacter.uid & c.freeSpot & c.clientMode != ClientMode.SPOTBOTTING_NP & c.clientMode != ClientMode.SPOTBOTTING_P)
                        {
                            c.updateLog(DateTime.Now.ToString("[h:mm:ss tt]") + " Failed to get spot!");
                            c.reset();
                        }
                    }
                }
                else
                {
                    if (c.clientMode == ClientMode.SPOTBOTTING_P || c.clientMode == ClientMode.SPOTBOTTING_NP)
                    {
                        if (c.storeTargetUID == UID)
                        {
                            c.storeWaitingDoneNowOpen();
                            c.storeTargetCoords = null;
                            c.storeTargetUID = 0;
                        }
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 105");
            }
        }
    }
}
