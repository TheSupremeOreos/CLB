using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PixelCLB
{
    public class Harvest
    {
        public int harvestID;
        public Point point;
        public HarvestType Type;

        public Harvest(int id, HarvestType type, Point p)
        {
            harvestID = id;
            Type = type;
            point = p;
        }
    }
}
