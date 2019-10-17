using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using PixelCLB.Net;
using PixelCLB.Net.Events;
using PixelCLB.Net.Packets;
using PixelCLB;
using PixelCLB.PacketCreation;
using PixelCLB.Crypto;
using PixelCLB.CLBTools;


namespace PixelCLB
{
    public class Inventory
    {
		private InventoryType type;

		public byte SlotLimit;

		public Dictionary<byte, MapleItem> inventory = new Dictionary<byte, MapleItem>();

		public Inventory(InventoryType type, byte slotlimit)
		{
			this.type = type;
			this.SlotLimit = slotlimit;
		}

        public void addBagItems(PacketReader packet, byte Pos)
        {
            packet.Skip(3);
            MapleItem mapleEquip;
            byte num = packet.Read();
            if (num == 2)
                mapleEquip = new MapleItem(packet.ReadInt(), (byte)(inventory.Count() + 1), 1);
            else
                return;
            packet.Skip(13);
            if (mapleEquip != null)
            {
                inventory.Add(mapleEquip.position, mapleEquip);
                mapleEquip.quantity = packet.ReadShort();
                packet.ReadShort();
                packet.ReadShort();
            }
        }

        private string getPotentialLines(short type, int level)
        {
            string potLine = "";
            if (Database.potentialLines.ContainsKey(type))
            {
                return Database.potentialLines[type].potLevels[level];
            }
            return potLine;
        }

        private string getNebuliteLine(short type)
        {
            string nebLine = "";
            if (Database.nebuliteLines.ContainsKey(type))
            {
                return Database.nebuliteLines[type];
            }
            return nebLine;
        }


		public void addFromPacket(PacketReader packet, byte Pos, bool Update, Client c)
		{
			MapleItem mapleEquip;
			if (Update && inventory.ContainsKey(Pos))
			{
				inventory.Remove(Pos);
			}
			if (!Update && this.type < InventoryType.USE)
			{
				packet.Read();
			}
			byte num = packet.Read();
			byte num1 = num;
			switch (num1)
			{
				case 1:
				{
					mapleEquip = new MapleEquip(packet.ReadInt(), Pos);
					break;
				}
				case 2:
				{
					mapleEquip = new MapleItem(packet.ReadInt(), Pos, 1);
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
            //c.updateLog("Adding " + Database.getItemName(mapleEquip.ID));
			bool flag = packet.Read() > 0;
			if (flag)
			{
				if (mapleEquip != null)
				{
					mapleEquip.cashOID = packet.ReadLong();
				}
				else
				{
					packet.Skip(8);
				}
			}
			packet.Skip(8);
            packet.Skip(4);
			if (mapleEquip != null)
			{
				if (!inventory.ContainsKey(mapleEquip.position))
				{
					inventory.Add(mapleEquip.position, mapleEquip);
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
                        if (mapleEquip.potLevel > 0 & mapleEquip.potLevel <= 20)
                        {
                            if (potLine1 < 20000 &&potLine2 < 10000 && potLine2 < 10000)
                                mapleEquip.potline1 = "Rare";
                            else if (potLine1 < 30000 && potLine2 < 20000 && potLine2 < 20000)
                                mapleEquip.potline1 = "Epic";
                            else if (potLine1 < 40000 && potLine2 < 30000 && potLine2 < 30000)
                                mapleEquip.potline1 = "Unique";
                            else if (potLine1 < 50000 && potLine2 < 40000 && potLine2 < 40000)
                                mapleEquip.potline1 = "Legendary";
                            mapleEquip.potline1 = getPotentialLines((short)potLine1, mapleEquip.potLevel);
                            mapleEquip.potline1 = mapleEquip.potline1.Replace(".", "");
                            mapleEquip.potline2 = getPotentialLines((short)potLine2, mapleEquip.potLevel);
                            mapleEquip.potline2 = mapleEquip.potline2.Replace(".", "");
                            mapleEquip.potline3 = getPotentialLines((short)potLine3, mapleEquip.potLevel);
                            mapleEquip.potline3 = mapleEquip.potline3.Replace(".", "");
                        }
                        short bonuspotline1 = packet.ReadShort();
                        short bonuspotline2 = packet.ReadShort();
                        short bonuspotline3 = packet.ReadShort();
                        packet.ReadShort();
                        mapleEquip.bonuspotLevel = packet.Read();
                        packet.Read();
                        if (mapleEquip.bonuspotLevel > 0 & mapleEquip.bonuspotLevel <= 20)
                        {
                            mapleEquip.bonuspotline1 = getPotentialLines(bonuspotline1, mapleEquip.bonuspotLevel);
                            mapleEquip.bonuspotline1 = mapleEquip.bonuspotline1.Replace(".", "");
                            mapleEquip.bonuspotline2 = getPotentialLines(bonuspotline2, mapleEquip.bonuspotLevel);
                            mapleEquip.bonuspotline2 = mapleEquip.bonuspotline2.Replace(".", "");
                            mapleEquip.bonuspotline3 = getPotentialLines(bonuspotline3, mapleEquip.bonuspotLevel);
                            mapleEquip.bonuspotline3 = mapleEquip.bonuspotline3.Replace(".", "");
                        }
                        short nebulite = packet.ReadShort();
                        if (nebulite >= 0 & nebulite <= 9999)
                            mapleEquip.nebulite = getNebuliteLine(nebulite);

                        /*if ((mapleItem.potLevel > 0 & mapleItem.potLevel <= 20) || (mapleItem.bonuspotLevel > 0 & mapleItem.bonuspotLevel <= 20))
                        {
                            MessageBox.Show(
                                storeTitle + "\n" +
                                mapleItem.potLevel.ToString() + "\n" +
                                mapleItem.potline1 + "\n" +
                                mapleItem.potline2 + "\n" +
                                mapleItem.potline3 + "\n" +
                                mapleItem.bonuspotLevel.ToString() + "\n" +
                                mapleItem.bonuspotline1 + "\n" +
                                mapleItem.bonuspotline2 + "\n" +
                                mapleItem.bonuspotline3 + "\n" +
                                mapleItem.nebulite + "\n");
                        }
                        int y = 0;
                        //c.updateLog(mapleItem.ID.ToString() + ", " + mapleItem.price.ToString() + ", " + mapleItem.quantity.ToString() + ", " + "Equip");
                                            
                        while (true)
                        {
                            while (packet.Read() != 255)
                            {
                            }
                            y++;
                            if (y == 1)
                            {
                                packet.Skip(33);
                            }
                            else if (y != 1 & y < 3)
                                packet.Skip(6);
                            else
                            {
                                packet.Skip(3);
                                break;
                            }
                        }
                         * */

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
                        /*
                        packet.Skip(30);
                        while (packet.Read() != 255)
                        {
                        }
                        while (true)
                        {
                            while (packet.Read() != 0x40)
                            {
                            }
                            if (packet.Read() == 0xE0)
                            {
                                break;
                            }
                        }
                        while (packet.Read() != 255)
                        {
                        }
                        packet.Skip(3);
                        break;
                        */
                    }
                    return;
                }
                else
                {
                    mapleEquip.quantity = packet.ReadShort();
                    short num5 = packet.ReadShort();
                    if (num5 != 0)
                        packet.ReadString(num5);
                    if (packet.ReadShort() == 0x0C)
                        mapleEquip.isOpen = true;
                    if (isRechargable(mapleEquip.ID) || mapleEquip.ID / 10000 == 287)
                    {
                        packet.Skip(8);
                    }
                    return;
                }
			}
			else
			{
				packet.Skip(13);
				packet.Skip(1);
				packet.Skip(2);
				packet.Skip(1);
				packet.Skip(8);
				packet.Skip(2);
				packet.Skip(2);
				packet.Skip(4);
				packet.Skip(2);
				packet.Skip(1);
				packet.Skip(4);
				return;
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

        public bool getBags()
        {
            foreach (KeyValuePair<byte, MapleItem> item in inventory)
            {
                foreach (int itemID in Program.itemBags)
                {
                    if (item.Value.ID == itemID & item.Value.isOpen)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		public MapleItem getItem(byte pos)
		{
			if (!inventory.ContainsKey(pos))
			{
				return null;
			}
			else
			{
				return inventory[pos];
			}
		}

		public MapleItem getItemById(int id)
		{
			MapleItem value;
			Dictionary<byte, MapleItem>.Enumerator enumerator = inventory.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<byte, MapleItem> current = enumerator.Current;
					if (current.Value.ID != id)
					{
						continue;
					}
					value = current.Value;
					return value;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return value;
		}

		public int getItemCount(int id)
		{
			int value = 0;
			try
			{
				foreach (KeyValuePair<byte, MapleItem> keyValuePair in inventory)
				{
					if (keyValuePair.Value.ID != id)
					{
						continue;
					}
					value = value + keyValuePair.Value.quantity;
				}
			}
			catch (Exception exception)
			{
			}
			return value;
		}

        public void updateItemCount(byte slot, short itemCount, int itemID = 0)
        {
            try
            {
                if (inventory.ContainsKey(slot))
                {
                    MapleItem item = inventory[slot];
                    item.quantity = itemCount;
                }
                else
                {
                    MapleItem item = new MapleItem(itemID, slot, itemCount);
                    inventory.Add(slot, item);
                }
            }
            catch (Exception exception)
            {
            }
        }


		public List<MapleItem> getItemList()
		{
			List<MapleItem> mapleItems = new List<MapleItem>();
			foreach (KeyValuePair<byte, MapleItem> list in inventory.ToList<KeyValuePair<byte, MapleItem>>())
			{
				mapleItems.Add(list.Value);
			}
			return mapleItems;
		}

		public byte getSlotById(int itemID)
		{
			byte key;
			Dictionary<byte, MapleItem>.Enumerator enumerator = inventory.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<byte, MapleItem> current = enumerator.Current;
					if (current.Value.ID != itemID)
					{
						continue;
					}
					key = current.Key;
					return key;
				}
				return (byte)0;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return key;
		}

        public bool isSlotFree(byte slot)
        {
            if (!inventory.ContainsKey(slot))
                return true;
            return false;
        }

		public void removeSlot(byte slot)
		{
			if (inventory.ContainsKey(slot))
			{
				inventory.Remove(slot);
			}
		}

        public int totalItems()
        {
            return inventory.Count();
        }

        public bool isFull()
        {
            if (totalItems() == SlotLimit)
                return true;
            return false;
        }

        public byte getNextFreeSlot()
        {
            for (byte slot = 1; slot <= SlotLimit; slot++)
            {
                if (!inventory.ContainsKey(slot))
                    return slot;
            }
            return 0;
        }


        public int getFreeSlots()
        {
            int freeSlots = 0;
            byte currentSlot = 1;
            while(currentSlot <= SlotLimit){
                if (isSlotFree(currentSlot))
                    freeSlots++;
                currentSlot++;
            }
            return freeSlots;
        }

        public void dropAll(int ID, Client c)
        {
            try{
            foreach (KeyValuePair<byte, MapleItem> items in inventory)
            {
                if (items.Value.ID == ID)
                {
                    c.dropItem.Add(items.Value);
                    //c.updateLog("[Inventory] Dropping " + items.Value.quantity + " " + Database.getItemName(items.Value.ID));
                    //c.ses.SendPacket(PacketHandler.dropItem((byte)type, items.Value.position, items.Value.quantity).ToArray());
                }
            }
            }catch{}
        }

        public void consumeAllScripted(int ID, Client c)
        {
            try
            {
                foreach (KeyValuePair<byte, MapleItem> items in inventory)
                {
                    if (items.Value.ID == ID)
                    {
                        c.dropItem.Add(items.Value);
                    }
                }
            }
            catch { }
        }

        public bool itemExists(int itemID)
        {
            try
            {
                Dictionary<byte, MapleItem> backupInven = inventory;
                foreach (KeyValuePair<byte, MapleItem> items in backupInven)
                {
                    if (items.Value.ID == itemID)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        public InventoryType getInvenType(byte invenType)
        {
            InventoryType type = InventoryType.EQUIP;
            if (invenType == 0x01)
            {
                type = InventoryType.EQUIP;
            }
            else if (invenType == 0x02)
            {
                type = InventoryType.USE;
            }
            else if (invenType == 0x03)
            {
                type = InventoryType.SETUP;
            }
            else if (invenType == 0x04)
            {
                type = InventoryType.ETC;
            }
            else if (invenType == 0x05)
            {
                type = InventoryType.CASH;
            }
            return type;
        }


        public InventoryType getInvenTypeByID(int itemID)
        {
            InventoryType type = InventoryType.EQUIP;
            if (itemID >= 1000000 && itemID < 2000000)
            {
                type = InventoryType.EQUIP;
            }
            else if (itemID >= 2000000 && itemID < 3000000)
            {
                type = InventoryType.USE;
            }
            else if (itemID >= 3000000 && itemID < 4000000)
            {
                type = InventoryType.SETUP;
            }
            else if (itemID >= 4000000 && itemID < 5000000)
            {
                type = InventoryType.ETC;
            }
            else if (itemID >= 5000000 && itemID < 6000000)
            {
                type = InventoryType.CASH;
            }
            return type;
        }
	}
}