using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PixelCLB
{
    public class MiniMapInfo
    {
        public Bitmap canvas;

        public short centerX;

        public short centerY;

        public short height;

        public short width;

        public int id;

        public MiniMapInfo(int id, Bitmap map, short cx, short cy, short h, short w)
        {
            this.id = id;
            this.canvas = map;
            this.centerX = cx;
            this.centerY = cy;
            this.height = h;
            this.width = w;
        }
    }
}
