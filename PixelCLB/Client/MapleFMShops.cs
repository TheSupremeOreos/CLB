using System;
using System.Collections.Generic;

namespace PixelCLB
{
    public class MapleFMShop : IComparable
    {
        public int shopID;
        public int mapID;
        public int playerUID;
        public string playerGuild;
        public string owner;
        public string description;
        public int channel;
        public string fmRoom;
        public bool permit;
        public short x;
        public short y;
        public short fh;
        public int storeID;
        public bool owled = false;
        public int tries = 0;

        private List<MapleItem> items = new List<MapleItem>();

        public MapleFMShop(int shopID, int mapID)
        {
            this.shopID = shopID;
            this.mapID = mapID;
        }

        public void addItem(MapleItem item)
        {
            items.Add(item);
        }

        public int CompareTo(object othera)
        {
            MapleFMShop mapleShop = (MapleFMShop)othera;
            if (mapID >= mapleShop.mapID)
            {
                if (mapID <= mapleShop.mapID)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public List<MapleItem> getItemList()
        {
            return items;
        }
    }
}