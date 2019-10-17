using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PixelCLB
{
    internal class MapleEquip : MapleItem
    {
        public MapleEquip(int id, byte position)
            : base(id, position, 1)
        {
            this.type = this.isEquip;
        }

        public MapleEquip(int id)
            : base(id)
        {
            this.type = this.isEquip;
        }
    }
}
