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
    class InventoryUpdate : PacketReceiver
    {
        public void packetAction(Client c, PacketReader packet)
        {
            try
            {
                if (packet.Length > 7)
                {
                    byte identifier = packet.Read();
                    if (identifier == 0x00) //Gained item
                    {
                        MapleItem mapleEquip;
                        short items = packet.ReadShort();
                        for (short y = 0; y < items; y++)
                        {
                            byte miniID = packet.Read();
                            if (miniID == 0x01)
                            {
                                byte invent = packet.Read();
                                short slotz = packet.ReadShort();
                                short amtt = packet.ReadShort();
                                if (amtt > 0)
                                {
                                    c.myCharacter.Inventorys[c.myCharacter.Inventorys[InventoryType.EQUIP].getInvenType(invent)].updateItemCount((byte)slotz, amtt);
                                    MapleItem item = c.myCharacter.Inventorys[c.myCharacter.Inventorys[InventoryType.EQUIP].getInvenType(invent)].getItem((byte)slotz);
                                    if (item != null)
                                        c.updateLog("[Inventory] Slot " + slotz.ToString() + ": " + item.quantity + " " + Database.getItemName(item.ID) + "s");
                                }
                                continue;
                            }
                            else if (miniID == 0x03)
                            {
                                byte invent = packet.Read();
                                short slotz = packet.ReadShort();
                                c.myCharacter.Inventorys[c.myCharacter.Inventorys[InventoryType.EQUIP].getInvenType(invent)].removeSlot((byte)slotz);
                                if (c.clientMode != ClientMode.WBMESOEXPLOIT)
                                    c.updateLog("[Inventory] Slot " + slotz.ToString() + " freed.");
                            }
                            else // miniID == 0x00
                            {
                                byte itemType = packet.Read();
                                byte slot = packet.Read();
                                packet.Read();
                                packet.Read(); //??????
                                switch (itemType)
                                {
                                    case 1:
                                        {
                                            mapleEquip = new MapleEquip(packet.ReadInt(), slot);
                                            break;
                                        }
                                    case 2:
                                        {
                                            mapleEquip = new MapleItem(packet.ReadInt(), slot, 1);
                                            break;
                                        }
                                    case 3:
                                        {
                                            mapleEquip = null;
                                            packet.ReadInt();
                                            break;
                                        }
                                    default:
                                        {
                                            return;
                                        }
                                }
                                InventoryType type = c.myCharacter.Inventorys[InventoryType.EQUIP].getInvenTypeByID(mapleEquip.ID);
                                if (mapleEquip != null)
                                {
                                    if (!c.myCharacter.Inventorys[type].inventory.ContainsKey(mapleEquip.position))
                                    {
                                        c.myCharacter.Inventorys[type].inventory.Add(mapleEquip.position, mapleEquip);
                                    }
                                    if (mapleEquip.type == 1)
                                    {
                                        while (true)
                                        {
                                            while (true)
                                            {
                                                if (packet.Read() == 0x4)
                                                {
                                                    int x = 0;
                                                    byte bytes = 0;
                                                    bool complete = false;
                                                    while (true)
                                                    {
                                                        bytes = packet.Read();
                                                        if (bytes == 0)
                                                        {
                                                            x++;
                                                        }
                                                        else if (bytes == 4)
                                                        {
                                                            x = 0;
                                                        }
                                                        else
                                                            break;
                                                        if (x >= 3)
                                                        {
                                                            complete = true;
                                                            break;
                                                        }
                                                    }
                                                    if (complete)
                                                    {
                                                        packet.Read();
                                                        break;
                                                    }
                                                }
                                                Thread.Sleep(1);
                                            }
                                            mapleEquip.craftedBy = packet.ReadMapleString();
                                            mapleEquip.potLevel = packet.Read();
                                            mapleEquip.enhancements = packet.Read();
                                            int potLine1 = packet.ReadShort();
                                            int potLine2 = packet.ReadShort();
                                            int potLine3 = packet.ReadShort();
                                            short bonuspotline1 = packet.ReadShort();
                                            short bonuspotline2 = packet.ReadShort();
                                            short bonuspotline3 = packet.ReadShort();
                                            packet.ReadShort();
                                            mapleEquip.bonuspotLevel = packet.Read();
                                            packet.Read();
                                            short nebulite = packet.ReadShort();


                                            while (true)
                                            {
                                                while (packet.Read() != 0x40)
                                                {
                                                }
                                                if (packet.Read() == 0xE0)
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1);
                                            }
                                            while (packet.Read() != 0xFF)
                                            {
                                                Thread.Sleep(1);
                                            }
                                            packet.Skip(3);
                                            while (true)
                                            {
                                                while (packet.Read() != 0x40)
                                                {
                                                    Thread.Sleep(1);
                                                }
                                                if (packet.Read() == 0xE0)
                                                {
                                                    break;
                                                }
                                                Thread.Sleep(1);
                                            }
                                            while (packet.Read() != 0x01)
                                            {
                                                Thread.Sleep(1);
                                            }
                                            packet.Skip(16);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        packet.Skip(13);
                                        mapleEquip.quantity = packet.ReadShort();
                                        packet.ReadInt();
                                    }
                                    if (c.clientMode == ClientMode.CASSANDRA)
                                    {
                                        if (mapleEquip.ID == 2431042 || mapleEquip.ID == 2049402 || mapleEquip.ID == 1122221)
                                        {
                                            continue;
                                        }
                                    }
                                    c.updateLog("[Inventory] Gained " + mapleEquip.quantity + " " + Database.getItemName(mapleEquip.ID));
                                }
                            }
                        }
                    }
                    else if (identifier == 0x01) //Drop
                    {
                        packet.ReadShort();
                        byte action = packet.Read();
                        byte invenType = packet.Read();
                        byte slotNum = packet.Read();
                        packet.Read();
                        InventoryType type = c.myCharacter.Inventorys[InventoryType.EQUIP].getInvenType(invenType);
                        if (action == 0x01) //D
                        {
                            short remaining = packet.ReadShort();
                            c.myCharacter.Inventorys[type].updateItemCount(slotNum, remaining);
                        }
                        else if (action == 0x03) 
                        {
                            c.myCharacter.Inventorys[type].removeSlot(slotNum);
                        }
                    }





                    






                    /*

                    byte inventoryType = packet.Read();
                    if (inventoryType != 0)
                    {
                        short slot = packet.ReadShort();
                        try
                        {
                            packet.Read();
                            int itemID = packet.ReadInt();
                            short itemCount = packet.ReadShort();
                            string itemName = "";
                            switch (inventoryType)
                            {
                                case 1:
                                    c.myCharacter.Inventorys[InventoryType.EQUIP].updateItemCount((byte)slot, itemCount, itemID);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.EQUIP].getItem((byte)slot).ID);
                                    break;
                                case 2:
                                    c.myCharacter.Inventorys[InventoryType.USE].updateItemCount((byte)slot, itemCount, itemID);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.USE].getItem((byte)slot).ID);
                                    break;
                                case 3:
                                    c.myCharacter.Inventorys[InventoryType.SETUP].updateItemCount((byte)slot, itemCount, itemID);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.SETUP].getItem((byte)slot).ID);
                                    break;
                                case 4:
                                    c.myCharacter.Inventorys[InventoryType.ETC].updateItemCount((byte)slot, itemCount, itemID);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.ETC].getItem((byte)slot).ID);
                                    break;
                                case 5:
                                    c.myCharacter.Inventorys[InventoryType.CASH].updateItemCount((byte)slot, itemCount, itemID);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.CASH].getItem((byte)slot).ID);
                                    break;
                            }
                            c.updateAccountStatus(itemCount.ToString() + " " + itemName + "s");
                            c.updateLog("[Inventory] " + itemCount.ToString() + " " + itemName + "s");
                        }
                        catch
                        {
                            string itemName = "";
                            switch (inventoryType)
                            {
                                case 1:
                                    c.myCharacter.Inventorys[InventoryType.EQUIP].removeSlot((byte)slot);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.EQUIP].getItem((byte)slot).ID);
                                    break;
                                case 2:
                                    c.myCharacter.Inventorys[InventoryType.USE].removeSlot((byte)slot);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.USE].getItem((byte)slot).ID);
                                    break;
                                case 3:
                                    c.myCharacter.Inventorys[InventoryType.SETUP].removeSlot((byte)slot);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.SETUP].getItem((byte)slot).ID);
                                    break;
                                case 4:
                                    c.myCharacter.Inventorys[InventoryType.ETC].removeSlot((byte)slot);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.ETC].getItem((byte)slot).ID);
                                    break;
                                case 5:
                                    c.myCharacter.Inventorys[InventoryType.CASH].removeSlot((byte)slot);
                                    itemName = Database.getItemName(c.myCharacter.Inventorys[InventoryType.CASH].getItem((byte)slot).ID);
                                    break;
                            }
                            c.updateLog("[Inventory] " + "You now have 0 " + itemName + "s");
                        }
                    }
                    else //Item Gained
                    {
                    }
                     * 
                     * 
                     */
                }
            }
            catch (Exception e)
            {
                c.updateLog("INVEN UPDATE ERROR " + e.ToString());
            }
        }

        public static bool isRechargable(int itemId)
        {
            if (itemId / 10000 == 233)
            {
                return true;
            }
            else
            {
                return itemId / 10000 == 207;
            }
        }
    }
}
