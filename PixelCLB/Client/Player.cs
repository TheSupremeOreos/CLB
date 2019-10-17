using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
    public class Player
    {
        public int uid;

        public string ign;

        public short x;

        public short y;

        public short foothold = -1;

        public Player(string _ign, int _uid, short _x, short _y, short _foothold)
        {
            uid = _uid;
            ign = _ign;
            x = _x;
            y = _y;
            if (_foothold != 0)
                foothold = _foothold;
        }
    }
}
