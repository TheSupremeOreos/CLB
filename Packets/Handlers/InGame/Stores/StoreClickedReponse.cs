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
    class StoreClickedReponse : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                byte num = packet.Read();
                if (num == 0x07)
                {
                    if (c.clientMode == ClientMode.FULLMAPPING_NP || c.clientMode == ClientMode.SPOTBOTTING_NP || c.clientMode == ClientMode.SHOPRESET)
                    {
                        if (c.storeTargetCoords == null)
                        {
                            c.tries++;
                            c.ses.SendPacket(PacketHandler.Open_Store(c.storename, c.mushyType, c.slotNum).ToArray());
                        }
                    }
                    else if (c.clientMode == ClientMode.SHOPWAIT_NP & c.storeTargetUID == 0)
                        c.storeTargetUID = c.mapleFMShopCollection.getStoreOnTopUID(c.coords);
                }
                else if (num == 8)
                {
                    string mapName = Database.getMapName(packet.ReadInt());
                    int channel = (int)packet.Read() + 1;
                    c.updateLog("Your store is already open in CH" + channel.ToString() + " at");
                    c.updateLog(mapName);
                    c.updateLog("Please use this after closing the store");
                    c.updateAccountStatus("Disconnected - Store already open elsewhere");
                    c.forceDisconnect(false, 0, true, "Store already open");
                }
                else if (num == 9)
                {
                    c.updateLog("Please retrieve your items from frederick");
                    c.updateAccountStatus("Disconnected - Items in frederick");
                    c.forceDisconnect(false, 0, true, "Items in frederick");
                }
                else if (num == 15)
                {
                    c.updateLog("Please retrieve your items from frederick");
                    c.updateAccountStatus("Disconnected - Items in frederick");
                    c.forceDisconnect(false, 0, true, "Items in frederick");
                }
            }
            catch
            {
                c.updateLog("Error Code: 100");
            }
        }
    }
}
