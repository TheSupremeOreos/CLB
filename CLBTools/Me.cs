using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PixelCLB;
using System.Drawing;
using System.Windows.Forms;

namespace PixelCLB.CLBTools
{
    public class Me
    {
        public int FUID;
        private int FLevel;
        public string ign;
        private string FGUILD;

        private Point FPosition;

        private int FmapID;
        private MapleMap FMap;
        private byte FSpawnpoint;
        private int FUsedPortals;

        private short Fx;
        private short Fy;
        private short Ffoothold = -1;


        private byte FGender;
        private byte FSkinColor;
        private int FFace;
        private int FHair;

        private short FJob;
        private short FStr;
        private short FDex;
        private short FInt;
        private short FLuk;
        private int FHP;
        private int FMaxHP;
        private int FMP;
        private int FMaxMP;

        private short FAP;
        private short FSP;

        private long FExp;
        private int FFame;
        private long FMeso;

        public Dictionary<InventoryType, Inventory> FInventorys = new Dictionary<InventoryType, Inventory>();

        private short FFatigue;
        private int FDilligenceExp;
        private int FWillpowerExp;
        private int FEmpathyExp;

        private short FPotentialExp;
        private short FPotentialLevel;
        private int FBattlePoints;
        private int[] FExtendedSP;



        public Me()
        {
        }


        public int uid
        {
            get
            {
                return FUID;
            }
            set
            {
                FUID = value;
            }
        }

        public string guild
        {
            get
            {
                return FGUILD;
            }
            set
            {
                FGUILD = value;
            }
        }


        public short x
        {
            get
            {
                return Fx;
            }
            set
            {
                Fx = value;
            }
        }

        public int mapID
        {
            get
            {
                return FmapID;
            }
            set
            {
                FmapID = value;
            }
        }
        public short y
        {
            get
            {
                return Fy;
            }
            set
            {
                Fy = value;
            }
        }
        public short foothold
        {
            get
            {
                return Ffoothold;
            }
            set
            {
                Ffoothold = value;
            }
        }


        public int Level
        {
            get
            {
                return FLevel;
            }
            set
            {
                FLevel = value;
            }
        }
        public MapleMap Map
        {
            get
            {
                return this.FMap;
            }
            set
            {
                this.FMap = value;
            }
        }
        public byte Gender
        {
            get
            {
                return FGender; 
            }
            set
            {
                FGender = value;
            }
        }
        public byte SkinColor
        {
            get
            {
                return FSkinColor;
            }
            set
            {
                FSkinColor = value;
            }
        }
        
        public int Face
        {
            get
            {
                return FFace;
            }
            set
            {
               FFace = value;
            }
        }
        public int Hair
        {
            get
            {
              return FHair;
            }
            set
            {
                FHair = value;
            }
        }
        public int HP
        {
            get
            {
                return FHP;
            }
            set
            {
                FHP = value;
            }
        }
        public int MaxHP
        {
            get
            {
                return FMaxHP;
            }
            set
            {
                FMaxHP = value;
            }
        }
        public int MP
        {
            get
            {
                return FMP;
            }
            set
            {
                FMP = value;
            }
        }
        public int MaxMP
        {
            get
            {
                return FMaxMP;
            }
            set
            {
                FMaxMP = value;
            }
        }
        public short Job
        {
            get
            {
                return FJob;
            }
            set
            {
                FJob = value;
            }
        }
        public short Str
        {
            get
            {
                return FStr;
            }
            set
            {
                FStr = value;
            }
        }
        public short Dex
        {
            get
            {
                return FDex;
            }
            set
            {
                FDex = value;
            }
        }
        public short Int
        {
            get
            {
                return FInt;
            }
            set
            {
                FInt = value;
            }
        }
        public short Luk
        {
            get
            {
                return FLuk;
            }
            set
            {
                FLuk = value;
            }
        }
        public short AP
        {
            get
            {
                return FAP;
            }
            set
            {
                FAP = value;
            }
        }
        public short SP
        {
            get
            {
                return FSP;
            }
            set
            {
                FSP = value;
            }
        }
        
        public long Exp
        {
            get
            {
                return FExp;
            }
            set
            {
                FExp = value;
            }
        }
        public int Fame
        {
            get
            {
                return FFame;
            }
            set
            {
                FFame = value;
            }
        }
        public long Meso
        {
            get
            {
                return FMeso;
            }
            set
            {
                FMeso = value;
            }
        }
        public int[] ExtendedSP
        {
            get
            {
                return FExtendedSP;
            }
            set
            {
                FExtendedSP = value;
            }
        }
        public int DilligenceExp
        {
            get
            {
                return FDilligenceExp;
            }
            set
            {
                FDilligenceExp = value;
            }
        }
        public int WillpowerExp
        {
            get
            {
                return FWillpowerExp;
            }
            set
            {
                FWillpowerExp = value;
            }
        }

        public int EmpathyExp
        {
            get
            {
                return FEmpathyExp;
            }
            set
            {
                FEmpathyExp = value;
            }
        }
        public short Fatigue
        {
            get
            {
                return FFatigue;
            }
            set
            {
                FFatigue = value;
            }
        }

        public int BattlePoints
        {
            get
            {
                return FBattlePoints;
            }
            set
            {
                FBattlePoints = value;
            }
        }

        public byte Spawnpoint
        {
            get
            {
                return FSpawnpoint;
            }
            set
            {
                FSpawnpoint = value;
            }
        }
        public Point Position
        {
            get
            {
                return this.FPosition;
            }
            set
            {
                this.FPosition = value;
            }
        }


        public void loadMap(Client c)
        {
            FMap = Database.loadMap(FmapID, c);
        }

        public int UsedPortals
        {
            get
            {
                return FUsedPortals;
            }
            set
            {
                FUsedPortals = value;
            }
        }
        public void loadMap(int id)
        {
            Me fUsedPortals = this;
            fUsedPortals.FUsedPortals = fUsedPortals.FUsedPortals + 1;
            FmapID = id;
            //loadMap();
        }

        public Dictionary<InventoryType, Inventory> Inventorys
        {
            get
            {
                return this.FInventorys;
            }
            set
            {
                this.FInventorys = value;
            }
        }
        public void InitInventory(InventoryType t, byte limit)
        {
            this.FInventorys[t] = new Inventory(t, limit);
        }
    }
}
