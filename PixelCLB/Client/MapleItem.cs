using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
	public class MapleItem
	{
		public byte isEquip = 1;
        public byte isItem = 2;
        public byte isPet = 3;
        public bool isOpen = false;
		public int ID;
		public long price;
		public byte position;
        public byte secondPos;
		public short quantity;
		public short bundle;
		public byte type;
		public string owner;
		public bool isAEquip;
		public short str;
		public short dex;
		public short _int;
		public short luk;
		public short hp;
		public short mp;
		public short watk;
		public short matk;
		public short wdef;
		public short mdef;
		public short acc;
		public short avoid;
		public short hands;
		public short speed;
		public short jump;
        public short hammers;

        public byte potLevel;
        public string potline0 = "";
        public string potline1 = "";
        public string potline2 = "";
        public string potline3 = "";
        public byte bonuspotLevel;
        public string bonuspotline0 = "";
        public string bonuspotline1 = "";
        public string bonuspotline2 = "";
        public string bonuspotline3 = "";
        public string craftedBy = "";
        public string itemDescription = "";

		public short socket;
		public string nebulite = "";
		public byte level;
		public byte upgradeSlots;

		public byte enhancements;
		public int attackType;
		public long cashOID;

		public MapleItem()
		{
		}

		public MapleItem(int id, byte position, short quantity)
		{
			this.ID = id;
			this.position = position;
			this.quantity = quantity;
			this.type = this.isItem;
		}

		public MapleItem(int id)
		{
			this.ID = id;
		}

		public string statstoString()
		{
			object[] objArray = new object[] { this.level, " ", this.upgradeSlots, " ", this.potLevel, " ", this.enhancements, " ", this.str, " ", this.dex, " ", this._int, " ", this.luk, " ", this.hp, " ", this.mp, " ", this.watk, " ", this.matk, " ", this.wdef, " ", this.mdef, " ", this.acc, " ", this.avoid, " ", this.hands, " ", this.speed, " ", this.jump, " ", this.hammers, " ", this.potline1, " ", this.potline2, " ", this.potline3, " ", this.nebulite };
			return string.Concat(objArray);
		}
	}
}
