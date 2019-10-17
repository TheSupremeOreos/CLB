using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCLB.Net;
using System.Threading;
using PixelCLB.PacketCreation;

namespace PixelCLB.Packets.Handlers
{
    class StoreRemoved2 : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Length == 6)
                {
                    int UID;
                    UID = packet.ReadInt();
                    storeCloseMethod(c, UID, true);
                }
            }
            catch
            {
                c.updateLog("Error Code: 102");
            }
        }

        private void storeCloseMethod(Client c, int UID, bool secondPacket)
        {
            if (!secondPacket)
            {
                c.secondClosePacket = true;
            }
            else
            {
                if (c.secondClosePacket)
                {
                    c.secondClosePacket = false;
                    return;
                }
            }

            if (UID == c.myCharacter.uid)
            {
                if (c.clientMode == ClientMode.SHOPCLOSE || c.clientMode == ClientMode.SHOPRESET)
                {
                    if (c.clientMode == ClientMode.SHOPCLOSE)
                    {
                        c.updateLog("[Store Closer] Store Closed.");
                        c.updateAccountStatus("Store closed successfully");
                        c.forceDisconnect(false, 0, false, "Store closed successfully");
                        return;
                    }
                    c.resetIsOpen = false;
                    c.canReset = true;
                }
                return;
            }
            else
            {
                if (Program.exportFMTimes)
                {
                    MapleFMShop shop = c.mapleFMShopCollection.getPlayerShop(UID, false);
                    c.exportFMTime(shop.owner, shop);
                }
                if (c.clientMode != ClientMode.SHOPRESET)
                {
                    if (c.clientMode == ClientMode.FULLMAPPING_P || c.clientMode == ClientMode.FULLMAPPING_NP)
                    {
                        string movement = c.getStoreXY(UID);
                        if (movement == null)
                            return;
                        c.coords = movement;
                        c.freeSpot = true;
                        return;
                    }
                    if (UID == c.storeTargetUID)
                        if (c.clientMode == ClientMode.SHOPWAIT_P || c.clientMode == ClientMode.SHOPWAIT_P)
                            c.storeWaitingDoneNowOpen();
                    c.mapleFMShopCollection.RemoveShop(UID, false);
                }
                return;
            }
        }
    }
}
