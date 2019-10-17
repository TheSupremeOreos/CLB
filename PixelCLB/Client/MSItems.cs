using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
	public class MSItems
    {
        public uint itemCRC = 0;
        public int itemID = 0;
        public string itemName = "";
        public string itemDesc = "";
        public int itemPrice = 0;
        public int itemUnitPrice = 0;
        public int itemSlotMax = 0;
        public int itemMax = 0;
        public int itemReqLev = 0;
        public int quest = 0;
        public int bNoPickupByPet = 0; //WHAT IS THIS?!
        public int noCancelMouse = 0;
        public int expireOnLogout = 0;
        public int notSale = 0;
        public int tradeAvailable = 0;
        //public int nAppliableKarmaType = 0;  //WHAT IS THIS?! is this tradeavailable???
        public int tradeBlock = 0;
        public int timeLimited = 0;
        public int pquest = 0;
        public int reqSTR = 0;
        public int reqINT = 0;
        public int reqDEX = 0;
        public int reqLUK = 0;
        public int reqJob = 0;
        public int reqPOP = 0;
        public long recovery = 0;
        public long fs = 0;
        public int knockback = 0;
        public int epicItem = 0;
        public int notExtend = 0;
        public int accountSharable = 0;
        public int onlyEquip = 0;
        public int only = 0;


        public MSItems()
		{
		}
	}
}
