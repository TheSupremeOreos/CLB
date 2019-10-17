using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PixelCLB
{
    public class NPCShop
    {
        private Dictionary<int, int> shopItems = new Dictionary<int, int>();

        public void addItem(int itemID, int price)
        {
            shopItems.Add(itemID, price);
        }

        public bool shopContains(int itemID)
        {
            if (shopItems.ContainsKey(itemID))
                return true;
            return false;
        }

        public int getPrice(int itemID)
        {
            if (shopItems.ContainsKey(itemID))
            {
                return shopItems[itemID];
            }
            return -1;
        }

        public int getItemOrderNumer(int itemID)
        {
            int num = 0;
            if (shopItems.ContainsKey(itemID))
            {
                foreach (KeyValuePair<int, int> items in shopItems)
                {
                    if (items.Key.Equals(itemID))
                    {
                        return num;
                    }
                    else
                        num++;
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }
    }
}
