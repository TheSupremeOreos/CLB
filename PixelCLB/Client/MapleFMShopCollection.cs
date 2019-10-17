using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
    public class MapleFMShopCollection
    {
        public Dictionary<int, MapleFMShop> shops = new Dictionary<int, MapleFMShop>();
        public Dictionary<int, MapleFMShop> owledShops = new Dictionary<int, MapleFMShop>();

        public MapleFMShopCollection()
		{
		}

		public void addShop(MapleFMShop shop, Client c, bool Permit)
        {
            try
            {
                if (shops.ContainsKey(getSortingID(Permit, shop.playerUID)))
                {
                    shops.Remove(getSortingID(Permit, shop.playerUID));
                }
                shops.Add(getSortingID(Permit, shop.playerUID), shop);
                if (owledShops.ContainsKey(getSortingID(Permit, shop.playerUID)))
                {
                    owledShops.Remove(getSortingID(Permit, shop.playerUID));
                }
                owledShops.Add(getSortingID(Permit, shop.playerUID), shop);
                Program.gui.updateMushy(c);
            }
            catch { }
		}

        public int getSortingID(bool Permit, int UID)
        {
            if (Permit)
                return UID + 268435456;
            else
                return UID;
            //return mapID * 10000000 + shopID;
        }

        public MapleFMShop getPlayerShop(int UID, bool Permit)
        {
            if (shops.ContainsKey(getSortingID(Permit, UID)))
                return shops[getSortingID(Permit, UID)];
            return null;
        }

        public bool doesShopExist(int UID, bool Permit)
        {
            if (shops.ContainsKey(getSortingID(Permit, UID)))
                return true;
            return false;
        }

        public void RemoveShop(int UID, bool Permit)
        {
            MapleFMShop shop = getPlayerShop(UID, Permit);
            if (shop != null)
                shops.Remove(getSortingID(Permit, UID));
            /*
            List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (current.Value.playerUID != UID)
                    {
                        continue;
                    }
                    shops.Remove(current.Key);
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            */
        }


        /*
		public string getOwnerFromSortingID(int sid)
		{
			string value;
			List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, MapleFMShop> current = enumerator.Current;
					if (current.Value.getSortingID() != sid)
					{
						continue;
					}
					value = current.Value.owner;
					return value;
				}
				return "Owner not Found";
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}
        */

		public MapleFMShop getShopOwner(string str)
		{
			MapleFMShop value = null;
			List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (current.Value.description.Equals(str) || current.Value.owner.Equals(str))
                    {
                        value = current.Value;
                        return value;
                    }
                }
                return value;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
		}

        public MapleFMShop getShopOwner2(string str)
        {
            try
            {
                MapleFMShop value = null;
                List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = owledShops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                        if (current.Value.description.Equals(str) || current.Value.owner.Equals(str))
                        {
                            value = current.Value;
                            return value;
                        }
                    }
                    return value;
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
            catch
            {
                return null;
            }
        }


        public MapleFMShop getShopOwnerIdentifier(string str, bool permit)
        {
            MapleFMShop value = null;
            List<KeyValuePair<int, MapleFMShop>>.Enumerator enumerator = shops.ToList<KeyValuePair<int, MapleFMShop>>().GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    KeyValuePair<int, MapleFMShop> current = enumerator.Current;
                    if (current.Value.owner.Equals(str) & current.Value.permit == permit)
                    {
                        value = current.Value;
                        return value;
                    }
                }
                return value;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
        }

		public List<MapleFMShop> getShopList()
		{
			List<MapleFMShop> list = shops.Values.ToList<MapleFMShop>();
			list.Sort();
			return list;
		}


        public List<MapleFMShop> getShopListOwl()
        {
            List<MapleFMShop> list = owledShops.Values.ToList<MapleFMShop>();
            list.Sort();
            return list;
        }

        public int owlListChannels(int channel)
        {
            int count = 0;
            List<MapleFMShop> list = owledShops.Values.ToList<MapleFMShop>();
            list.Sort();
            foreach (MapleFMShop shop in list)
            {
                if (shop.channel == channel)
                    count++;
            }
            return count;
        }

        public string getStoreOnTopCoords(string coord)
        {
            string coords = null;
            string[] XY = coord.Split(',');
            short x = (short)int.Parse(XY[0]);
            short y = (short)int.Parse(XY[1]);
            List<MapleFMShop> list = shops.Values.ToList<MapleFMShop>();
            foreach (MapleFMShop shop in list)
            {
                if (Math.Abs(shop.x - x) <= 64 && Math.Abs(shop.y - y) <= 70)
                {
                    coords = shop.x.ToString() + "," + shop.y.ToString();
                    return coords;
                }
            }
            return coords;
        }


        public int getStoreOnTopUID(string coord)
        {
            int UID = 0;
            string[] XY = coord.Split(',');
            short x = (short)int.Parse(XY[0]);
            short y = (short)int.Parse(XY[1]);
            List<MapleFMShop> list = shops.Values.ToList<MapleFMShop>();
            foreach (MapleFMShop shop in list)
            {
                if (Math.Abs(shop.x - x) <= 64 && Math.Abs(shop.y - y) <= 70)
                {
                    UID = shop.playerUID;
                }
            }
            return UID;
        }

    }
}
