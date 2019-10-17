using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PixelCLB;


namespace PixelCLB
{
    public class MapleMap : IDisposable
    {
        private int ID;

        private Dictionary<int, Player> FCharacters = new Dictionary<int, Player>();
        public FootholdTree footholds;
        private Dictionary<int, Monster> FMobs = new Dictionary<int, Monster>();
        private Dictionary<int, NPC> npcList = new Dictionary<int, NPC>();
        private Dictionary<int, Portal> portalList = new Dictionary<int, Portal>();
        private Dictionary<int, int> FObjectHistory = new Dictionary<int, int>();
        private Dictionary<int, Harvest> harvestList = new Dictionary<int, Harvest>();

        public object mobLock = new object();

        private int NO_MOB = 9999999;

        public MapleMap(int id)
        {
            this.ID = id;
        }

        public void addChar(int id, Player p)
        {
            if (!this.FCharacters.ContainsKey(id))
            {
                this.FCharacters.Add(id, p);
            }
        }

        public void removeChar(int id)
        {
            if (!this.FCharacters.ContainsKey(id))
            {
                this.FCharacters.Remove(id);
            }
        }

        //Harvest Related
        public void addHarvest(Harvest harvest)
        {
            harvestList.Add(harvest.harvestID, harvest);
        }
        public void removeHarvest(int harvestID)
        {
            if (harvestList.ContainsKey(harvestID))
            {
                harvestList.Remove(harvestID);
            }
        }
        public int countHarvest()
        {
            return harvestList.Count();
        }

        public bool containsHarvest(int id)
        {
            return harvestList.ContainsKey(id);
        }
        public Dictionary<int, Harvest> getHarvestList()
        {
            return harvestList;
        }

        public void addMob(Monster mob)
        {
            lock (this.mobLock)
            {
                FMobs.Add(mob.ObjectID, mob);
            }
        }

        public void addNPC(int id, int oid, short x, short y)
        {
            if (!npcList.ContainsKey(oid))
            {
                NPC nPC = new NPC(id);
                nPC.ObjectID = oid;
                nPC.x = x;
                nPC.y = y;
                npcList.Add(nPC.ObjectID, nPC);
            }
        }


        public void addPortal(Portal p)
        {
            portalList.Add(p.ID, p);
        }

        public void clearObjects()
        {
            FCharacters = new Dictionary<int, Player>();
            FMobs = new Dictionary<int, Monster>();
            npcList = new Dictionary<int, NPC>();
            harvestList = new Dictionary<int, Harvest>();
        }

        public MapleMap cloneMap()
        {
            MapleMap mapleMap = new MapleMap(this.ID);
            mapleMap.footholds = footholds;
            mapleMap.portalList = portalList;
            return mapleMap;
        }


        public List<Monster> getAttackableMobs()
        {
            List<Monster> monsters = new List<Monster>();
            lock (this.mobLock)
            {
                Dictionary<int, Monster>.Enumerator enumerator = this.FMobs.GetEnumerator();
                try
                {
                    do
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        KeyValuePair<int, Monster> current = enumerator.Current;
                        if (current.Value.ID == this.NO_MOB || current.Value.serverDead)
                        {
                            continue;
                        }
                        monsters.Add(current.Value);
                    }
                    while (monsters.Count < 15);
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
            return monsters;
        }

        public List<Player> getPlayerList()
        {
            List<Player> players = new List<Player>();
            List<KeyValuePair<int, Player>> list = this.FCharacters.ToList();
            foreach (KeyValuePair<int, Player> p in list)
            {
                players.Add(p.Value);
            }
            return players;
        }

        public List<Monster> getFAttackableMobs()
        {
            List<Monster> monsters = new List<Monster>();
            lock (this.mobLock)
            {
                Dictionary<int, Monster>.Enumerator enumerator = this.FMobs.GetEnumerator();
                try
                {
                    do
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        KeyValuePair<int, Monster> current = enumerator.Current;
                        if (current.Value.ID == this.NO_MOB || current.Value.serverDead)
                        {
                            continue;
                        }
                        monsters.Add(current.Value);
                    }
                    while (monsters.Count < 15);
                }
                finally
                {
                    ((IDisposable)enumerator).Dispose();
                }
            }
            return monsters;
        }

        public Monster getMobFromOid(int oid)
        {
            Monster item;
            lock (this.mobLock)
            {
                if (!this.FMobs.ContainsKey(oid))
                {
                    return null;
                }
                else
                {
                    item = this.FMobs[oid];
                }
            }
            return item;
        }

        public NPC getNPC(int oid)
        {
            if (!npcList.ContainsKey(oid))
            {
                return null;
            }
            else
            {
                return npcList[oid];
            }
        }

        public List<NPC> getNPCList()
        {
            return npcList.Values.ToList<NPC>();
        }

        public Portal getPortal(int p)
        {
            if (!portalList.ContainsKey(p))
            {
                return null;
            }
            else
            {
                return portalList[p];
            }
        }

        public List<Portal> getPortalList()
        {
            List<Portal> portals = new List<Portal>();
            foreach (KeyValuePair<int, Portal> fPortal in portalList)
            {
                portals.Add(fPortal.Value);
            }
            return portals;
        }

        public Portal getPortalByName(string name)
        {
            List<Portal> portals = getPortalList();
            foreach (Portal p in portals)
            {
                if (p.name == name)
                    return p;
            }
            return null;
        }


        public void removeMob(int oid)
        {
            lock (this.mobLock)
            {
                if (this.FMobs.ContainsKey(oid))
                {
                    this.FMobs[oid].serverDead = true;
                    this.FMobs.Remove(oid);
                }
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }

    }
}