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
    class StoreRemoved : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                int UID = packet.ReadInt();
                if (packet.Length == 7)
                {
                    storeCloseMethod(c, UID, false);
                }
                else
                {
                    if (packet.Read() == 0x06) //Name Updated
                    {
                        MapleFMShop shop = c.mapleFMShopCollection.getPlayerShop(UID, false);
                        if (shop != null)
                        {
                            shop.shopID = packet.ReadInt();
                            shop.description = packet.ReadMapleString();
                        }
                        return;
                        /*
                        List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = c.mapleFMShopCollection.shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                        try
                        {
                            while (true & c.clientMode != ClientMode.DISCONNECTED)
                            {
                                if (!enumerator.MoveNext())
                                {
                                    break;
                                }
                                KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                                if (current.Value.playerUID == UID)
                                {
                                    current.Value.shopID = packet.ReadInt();
                                    current.Value.description = packet.ReadMapleString();
                                    break;
                                }
                            }
                        }
                        finally
                        {
                            ((IDisposable)enumerator).Dispose();
                        }
                        */
                    }
                }
            }
            catch
            {
                c.updateLog("Error Code: 101");
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
                    if (shop != null)
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
                }
                c.mapleFMShopCollection.RemoveShop(UID, false);
                return;
            }
        }
    }
}
