

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCLB
{
    public static class Computations
    {
        public static int getRequiredCraftingItem(int id)
        {
            if (id >= 4022008 & id <= 4022012)
                return 4024001;
            else if (id >= 4022013 & id <= 4022018)
                return 4024002;
            else if (id >= 4022019 & id <= 4022021)
                return 4024003;

            switch (id)
            {
                case 4010002:
                    return 04024005;
                case 4010006:
                    return 04024005;
                case 4020003:
                    return 04024005;
                case 4020006:
                    return 04024005;
                case 4004002:
                    return 04024005;
                case 4020000:
                    return 04024006;
                case 4020002:
                    return 04024006;
                case 4020007:
                    return 04024006;
                case 4020008:
                    return 04024006;
                case 4004004:
                    return 04024006;
                case 4994000:
                    return 04024006;
                case 4004001:
                    return 04024007;
                case 4004003:
                    return 04024007;
                case 4010007:
                    return 04024007;
                default:
                    return 0;
            }
            return 0;
        }

        public static int getRequiredAmount(int itemID)
        {
            if (itemID >= 4022008 & itemID <= 4022012)
                return 4;
            else if (itemID >= 4022013 & itemID <= 4022016)
                return 5;
            else if (itemID >= 4022017 & itemID <= 4022021)
                return 6;

            switch (itemID)
            {
                case 4010002:
                    return 4;
                case 4010006:
                    return 4;
                case 4020003:
                    return 4;
                case 4020006:
                    return 4;
                case 4004002:
                    return 4;
                case 4020000:
                    return 5;
                case 4020002:
                    return 5;
                case 4020007:
                    return 6;
                case 4020008:
                    return 6;
                case 4004004:
                    return 6;
                case 4994000:
                    return 5;
                case 4004001:
                    return 5;
                case 4004003:
                    return 6;
                case 4010007:
                    return 6;
                default:
                    return 0;
            }
            return 0;
        }

        public static int buyAmount(int itemID, int itemCount, int freeSlots)
        {
            int totalAmount = itemCount / Computations.getRequiredAmount(itemID); // make x
            if (totalAmount / 100 > freeSlots)
            {
                return freeSlots * 100; //return maximum buy
            }
            else
            {
                return totalAmount; //return total amount
            }
        }

    }
}

